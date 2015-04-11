using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FvckYeahBubbleTea.Models;

namespace FvckYeahBubbleTea.Controllers
{
    public class OrderController : ApiController
    {
        private TeaContext db = new TeaContext();
        // GET api/<controller>
        [HttpGet]
        public IEnumerable<Order> Get()         
        {
            return db.Orders.Include(x => x.BaseTea)
                            .Include(x => x.Flavor)
                            .Include(x => x.Size)
                            .Include(x => x.Toppings)
                            .AsEnumerable();
        }

        // GET api/<controller>/5
        public Order Get(int id)
        {
            Order order = db.Orders.Include(x => x.BaseTea)
                                   .Include(x => x.Flavor)
                                   .Include(x => x.Size)
                                   .Include(x => x.Toppings)
                                   .SingleOrDefault(x => x.Id == id);

            if (order == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return order;
        }

        // POST api/<controller>
        public HttpResponseMessage Post(Order order)
        {
            if (ModelState.IsValid)
            {
                Order newOrder = new Order
                {
                    BaseTeaId = order.BaseTea.Id,
                    FlavorId = order.Flavor.Id,
                    SizeId = order.Size.Id,
                    Toppings = new List<Topping>(),
                };

                foreach (var topping in order.Toppings)
                {
                    newOrder.Toppings.Add(db.Toppings.FirstOrDefault(x => x.Id == topping.Id));
                }

                db.Entry(newOrder).State = EntityState.Added;
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, order);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = order.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != order.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var originalOrder = db.Orders.Find(id);

            originalOrder.BaseTeaId = order.BaseTea.Id;
            originalOrder.FlavorId = order.Flavor.Id;
            originalOrder.SizeId = order.Size.Id;
            originalOrder.Toppings = new List<Topping>();

            foreach (var topping in order.Toppings)
            {
                originalOrder.Toppings.Add(db.Toppings.FirstOrDefault(x => x.Id == topping.Id));
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Orders.Remove(order);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, order);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}