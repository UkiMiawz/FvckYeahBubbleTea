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
    public class FlavorController : ApiController
    {
        private TeaContext db = new TeaContext();
        // GET api/<controller>
        [HttpGet]
        public IEnumerable<Flavor> Get()         
        {
            return db.Flavors.AsEnumerable();
        }

        // GET api/<controller>/5
        public Flavor Get(int id)
        {
            Flavor flavor = db.Flavors.Find(id);
            if (flavor == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return flavor;
        }

        // POST api/<controller>
        public HttpResponseMessage Post(Flavor flavor)
        {
            if (ModelState.IsValid)
            {
                db.Flavors.Add(flavor);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, flavor);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = flavor.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, Flavor flavor)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != flavor.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(flavor).State = EntityState.Modified;

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
            Flavor flavor = db.Flavors.Find(id);
            if (flavor == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Flavors.Remove(flavor);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, flavor);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}