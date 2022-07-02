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
    public class NewstypeController : ApiController
    {
        private nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();

        // GET: api/Newstype
        public IQueryable<newstype> Getnewstype()
        {
            return db.newstype;
        }

        // GET: api/Newstype/5
        [ResponseType(typeof(newstype))]
        public IHttpActionResult Getnewstype(int id)
        {
            newstype newstype = db.newstype.Find(id);
            if (newstype == null)
            {
                return NotFound();
            }

            return Ok(newstype);
        }

        // PUT: api/Newstype/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putnewstype(int id, newstype newstype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != newstype.id)
            {
                return BadRequest();
            }

            db.Entry(newstype).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!newstypeExists(id))
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

        // POST: api/Newstype
        [ResponseType(typeof(newstype))]
        public IHttpActionResult Postnewstype(newstype newstype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.newstype.Add(newstype);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = newstype.id }, newstype);
        }

        // DELETE: api/Newstype/5
        [ResponseType(typeof(newstype))]
        public IHttpActionResult Deletenewstype(int id)
        {
            newstype newstype = db.newstype.Find(id);
            if (newstype == null)
            {
                return NotFound();
            }

            db.newstype.Remove(newstype);
            db.SaveChanges();

            return Ok(newstype);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool newstypeExists(int id)
        {
            return db.newstype.Count(e => e.id == id) > 0;
        }
    }
}