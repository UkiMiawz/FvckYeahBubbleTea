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
    public class BaseTeaController : ApiController
    {
        private TeaContext db = new TeaContext();
        // GET api/<controller>
        [HttpGet]
        public IEnumerable<BaseTea> Get()         
        {
            return db.BaseTeas.AsEnumerable();
        }

        // GET api/<controller>/5
        public BaseTea Get(int id)
        {
            BaseTea baseTea = db.BaseTeas.Find(id);
            if (baseTea == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return baseTea;
        }

        // POST api/<controller>
        public HttpResponseMessage Post(BaseTea baseTea)
        {
            if (ModelState.IsValid)
            {
                db.BaseTeas.Add(baseTea);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, baseTea);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = baseTea.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, BaseTea baseTea)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != baseTea.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(baseTea).State = EntityState.Modified;

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
            BaseTea baseTea = db.BaseTeas.Find(id);
            if (baseTea == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.BaseTeas.Remove(baseTea);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, baseTea);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}