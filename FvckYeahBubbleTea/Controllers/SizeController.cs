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
    public class SizeController : ApiController
    {
        private TeaContext db = new TeaContext();
        // GET api/<controller>
        [HttpGet]
        public IEnumerable<Size> Get()         
        {
            return db.Sizes.AsEnumerable();
        }

        // GET api/<controller>/5
        public Size Get(int id)
        {
            Size size = db.Sizes.Find(id);
            if (size == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return size;
        }

        // POST api/<controller>
        public HttpResponseMessage Post(Size size)
        {
            if (ModelState.IsValid)
            {
                db.Sizes.Add(size);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, size);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = size.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, Size size)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != size.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(size).State = EntityState.Modified;

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
            Size size = db.Sizes.Find(id);
            if (size == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Sizes.Remove(size);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, size);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}