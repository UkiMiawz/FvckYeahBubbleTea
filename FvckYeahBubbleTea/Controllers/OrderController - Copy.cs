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
    public class FinalOrderController : ApiController
    {
        private TeaContext db = new TeaContext();
        // GET api/<controller>
        [HttpGet]
        public IEnumerable<FinalOrder> Get()         
        {
            return db.FinalOrders.AsEnumerable();
        }

        // GET api/<controller>/5
        public FinalOrder Get(int id)
        {
            FinalOrder finalFinalOrder = db.FinalOrders.Find(id);
            if (finalFinalOrder == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return finalFinalOrder;
        }

        // POST api/<controller>
        public HttpResponseMessage Post(FinalOrder finalFinalOrder)
        {
            if (ModelState.IsValid)
            {
                db.FinalOrders.Add(finalFinalOrder);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, finalFinalOrder);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = finalFinalOrder.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, FinalOrder finalFinalOrder)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != finalFinalOrder.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(finalFinalOrder).State = EntityState.Modified;

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
            FinalOrder finalFinalOrder = db.FinalOrders.Find(id);
            if (finalFinalOrder == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.FinalOrders.Remove(finalFinalOrder);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, finalFinalOrder);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}