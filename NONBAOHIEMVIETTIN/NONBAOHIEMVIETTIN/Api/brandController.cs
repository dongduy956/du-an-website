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
    public class BrandController : ApiController
    {
        private nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();

        // GET: api/Brand
        public IQueryable<brand> Getbrand()
        {
            return db.brand;
        }

        // GET: api/Brand/5
        [ResponseType(typeof(brand))]
        public IHttpActionResult Getbrand(int id)
        {
            brand brand = db.brand.Find(id);
            if (brand == null)
            {
                return NotFound();
            }

            return Ok(brand);
        }

        // PUT: api/Brand/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putbrand(int id, brand brand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != brand.id)
            {
                return BadRequest();
            }

            db.Entry(brand).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!brandExists(id))
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

        // POST: api/Brand
        [ResponseType(typeof(brand))]
        public IHttpActionResult Postbrand(brand brand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.brand.Add(brand);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = brand.id }, brand);
        }

        // DELETE: api/Brand/5
        [ResponseType(typeof(brand))]
        public IHttpActionResult Deletebrand(int id)
        {
            brand brand = db.brand.Find(id);
            if (brand == null)
            {
                return NotFound();
            }

            db.brand.Remove(brand);
            db.SaveChanges();

            return Ok(brand);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool brandExists(int id)
        {
            return db.brand.Count(e => e.id == id) > 0;
        }
    }
}