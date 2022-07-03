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
    public class ContactController : ApiController
    {
        private nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();

        // GET: api/Contact
        public IQueryable<contact> Getcontact()
        {
            return db.contact;
        }

        // GET: api/Contact/5
        [ResponseType(typeof(contact))]
        public IHttpActionResult Getcontact(int id)
        {
            contact contact = db.contact.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // PUT: api/Contact/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcontact(int id, contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contact.id)
            {
                return BadRequest();
            }

            db.Entry(contact).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!contactExists(id))
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

        // POST: api/Contact
        [ResponseType(typeof(contact))]
        public IHttpActionResult Postcontact(contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.contact.Add(contact);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contact.id }, contact);
        }

        // DELETE: api/Contact/5
        [ResponseType(typeof(contact))]
        public IHttpActionResult Deletecontact(int id)
        {
            contact contact = db.contact.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            db.contact.Remove(contact);
            db.SaveChanges();

            return Ok(contact);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool contactExists(int id)
        {
            return db.contact.Count(e => e.id == id) > 0;
        }
    }
}