using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NONBAOHIEMVIETTIN.Models;
using System.Web.Http.Cors;

namespace NONBAOHIEMVIETTIN.Api
{
    public class SubscribeController : ApiController
    {
        private nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();

        // GET: api/Subscribe
        public IQueryable<subscribe> Getsubscribe()
        {
            return db.subscribe;
        }

        // GET: api/Subscribe/5
        [ResponseType(typeof(subscribe))]
        public IHttpActionResult Getsubscribe(int id)
        {
            subscribe subscribe = db.subscribe.Find(id);
            if (subscribe == null)
            {
                return NotFound();
            }

            return Ok(subscribe);
        }

        // PUT: api/Subscribe/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putsubscribe(int id, subscribe subscribe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subscribe.id)
            {
                return BadRequest();
            }

            db.Entry(subscribe).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!subscribeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Subscribe
        [ResponseType(typeof(subscribe))]
        public IHttpActionResult Postsubscribe(subscribe subscribe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.subscribe.Add(subscribe);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = subscribe.id }, subscribe);
        }

        // DELETE: api/Subscribe/5
        [ResponseType(typeof(subscribe))]
        public IHttpActionResult Deletesubscribe(int id)
        {
            subscribe subscribe = db.subscribe.Find(id);
            if (subscribe == null)
            {
                return NotFound();
            }

            db.subscribe.Remove(subscribe);
            db.SaveChanges();

            return Ok(subscribe);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool subscribeExists(int id)
        {
            return db.subscribe.Count(e => e.id == id) > 0;
        }
    }
}