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
    public class groupproductController : ApiController
    {
        private nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();

        // GET: api/groupproduct
        public IQueryable<groupproduct> Getgroupproduct()
        {
            return db.groupproduct;
        }

        // GET: api/groupproduct/5
        [ResponseType(typeof(groupproduct))]
        public IHttpActionResult Getgroupproduct(int id)
        {
            groupproduct groupproduct = db.groupproduct.Find(id);
            if (groupproduct == null)
            {
                return NotFound();
            }

            return Ok(groupproduct);
        }

        // PUT: api/groupproduct/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putgroupproduct(int id, groupproduct groupproduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != groupproduct.id)
            {
                return BadRequest();
            }

            db.Entry(groupproduct).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!groupproductExists(id))
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

        // POST: api/groupproduct
        [ResponseType(typeof(groupproduct))]
        public IHttpActionResult Postgroupproduct(groupproduct groupproduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.groupproduct.Add(groupproduct);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = groupproduct.id }, groupproduct);
        }

        // DELETE: api/groupproduct/5
        [ResponseType(typeof(groupproduct))]
        public IHttpActionResult Deletegroupproduct(int id)
        {
            groupproduct groupproduct = db.groupproduct.Find(id);
            if (groupproduct == null)
            {
                return NotFound();
            }

            db.groupproduct.Remove(groupproduct);
            db.SaveChanges();

            return Ok(groupproduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool groupproductExists(int id)
        {
            return db.groupproduct.Count(e => e.id == id) > 0;
        }
    }
}