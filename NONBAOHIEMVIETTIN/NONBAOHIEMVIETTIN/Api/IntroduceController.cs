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
    public class IntroduceController : ApiController
    {
        private nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();

        // GET: api/Introduce
        public IQueryable<introduce> Getintroduce()
        {
            return db.introduce;
        }

        // GET: api/Introduce/5
        [ResponseType(typeof(introduce))]
        public IHttpActionResult Getintroduce(int id)
        {
            introduce introduce = db.introduce.Find(id);
            if (introduce == null)
            {
                return NotFound();
            }

            return Ok(introduce);
        }

        // PUT: api/Introduce/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putintroduce(int id, introduce introduce)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != introduce.id)
            {
                return BadRequest();
            }

            db.Entry(introduce).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!introduceExists(id))
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

        // POST: api/Introduce
        [ResponseType(typeof(introduce))]
        public IHttpActionResult Postintroduce(introduce introduce)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.introduce.Add(introduce);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = introduce.id }, introduce);
        }

        // DELETE: api/Introduce/5
        [ResponseType(typeof(introduce))]
        public IHttpActionResult Deleteintroduce(int id)
        {
            introduce introduce = db.introduce.Find(id);
            if (introduce == null)
            {
                return NotFound();
            }

            db.introduce.Remove(introduce);
            db.SaveChanges();

            return Ok(introduce);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool introduceExists(int id)
        {
            return db.introduce.Count(e => e.id == id) > 0;
        }
    }
}