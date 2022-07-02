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
    public class WheelController : ApiController
    {
        private nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();

        // GET: api/Wheel
        public IQueryable<wheel> Getwheel()
        {
            return db.wheel;
        }

        // GET: api/Wheel/5
        [ResponseType(typeof(wheel))]
        public IHttpActionResult Getwheel(int id)
        {
            wheel wheel = db.wheel.Find(id);
            if (wheel == null)
            {
                return NotFound();
            }

            return Ok(wheel);
        }

        // PUT: api/Wheel/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putwheel(int id, wheel wheel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != wheel.id)
            {
                return BadRequest();
            }

            db.Entry(wheel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!wheelExists(id))
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

        // POST: api/Wheel
        [ResponseType(typeof(wheel))]
        public IHttpActionResult Postwheel(wheel wheel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.wheel.Add(wheel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = wheel.id }, wheel);
        }

        // DELETE: api/Wheel/5
        [ResponseType(typeof(wheel))]
        public IHttpActionResult Deletewheel(int id)
        {
            wheel wheel = db.wheel.Find(id);
            if (wheel == null)
            {
                return NotFound();
            }

            db.wheel.Remove(wheel);
            db.SaveChanges();

            return Ok(wheel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool wheelExists(int id)
        {
            return db.wheel.Count(e => e.id == id) > 0;
        }
    }
}