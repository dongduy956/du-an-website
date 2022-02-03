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
    public class productionController : ApiController
    {
        private nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();

        // GET: api/productions
        public IQueryable<production> Getproduction()
        {
            return db.production;
        }

        // GET: api/productions/5
        [ResponseType(typeof(production))]
        public IHttpActionResult Getproduction(int id)
        {
            production production = db.production.Find(id);
            if (production == null)
            {
                return NotFound();
            }

            return Ok(production);
        }

        // PUT: api/productions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putproduction(int id, production production)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != production.id)
            {
                return BadRequest();
            }

            db.Entry(production).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!productionExists(id))
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

        // POST: api/productions
        [ResponseType(typeof(production))]
        public IHttpActionResult Postproduction(production production)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.production.Add(production);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = production.id }, production);
        }

        // DELETE: api/productions/5
        [ResponseType(typeof(production))]
        public IHttpActionResult Deleteproduction(int id)
        {
            production production = db.production.Find(id);
            if (production == null)
            {
                return NotFound();
            }

            db.production.Remove(production);
            db.SaveChanges();

            return Ok(production);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool productionExists(int id)
        {
            return db.production.Count(e => e.id == id) > 0;
        }
    }
}