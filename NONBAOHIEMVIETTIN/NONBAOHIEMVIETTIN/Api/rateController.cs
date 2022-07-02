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

namespace NONBAOHIEMVIETTIN.Api
{
    public class RateController : ApiController
    {
        private nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();

        // GET: api/Rate
        public IQueryable<rate> Getrate()
        {
            return db.rate;
        }

        // GET: api/Rate/5
        [ResponseType(typeof(rate))]
        public IHttpActionResult Getrate(int id)
        {
            rate rate = db.rate.Find(id);
            if (rate == null)
            {
                return NotFound();
            }

            return Ok(rate);
        }

        // PUT: api/Rate/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putrate(int id, rate rate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rate.id)
            {
                return BadRequest();
            }

            db.Entry(rate).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!rateExists(id))
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

        // POST: api/Rate
        [ResponseType(typeof(rate))]
        public IHttpActionResult Postrate(rate rate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.rate.Add(rate);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rate.id }, rate);
        }

        // DELETE: api/Rate/5
        [ResponseType(typeof(rate))]
        public IHttpActionResult Deleterate(int id)
        {
            rate rate = db.rate.Find(id);
            if (rate == null)
            {
                return NotFound();
            }

            db.rate.Remove(rate);
            db.SaveChanges();

            return Ok(rate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool rateExists(int id)
        {
            return db.rate.Count(e => e.id == id) > 0;
        }
    }
}