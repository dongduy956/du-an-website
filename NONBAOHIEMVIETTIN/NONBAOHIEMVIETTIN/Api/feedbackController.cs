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
    public class FeedbackController : ApiController
    {
        private nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();

        // GET: api/Feedback
        public IQueryable<feedback> Getfeedback()
        {
            return db.feedback;
        }

        // GET: api/Feedback/5
        [ResponseType(typeof(feedback))]
        public IHttpActionResult Getfeedback(int id)
        {
            feedback feedback = db.feedback.Find(id);
            if (feedback == null)
            {
                return NotFound();
            }

            return Ok(feedback);
        }

        // PUT: api/Feedback/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putfeedback(int id, feedback feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != feedback.id)
            {
                return BadRequest();
            }

            db.Entry(feedback).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!feedbackExists(id))
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

        // POST: api/Feedback
        [ResponseType(typeof(feedback))]
        public IHttpActionResult Postfeedback(feedback feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.feedback.Add(feedback);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = feedback.id }, feedback);
        }

        // DELETE: api/Feedback/5
        [ResponseType(typeof(feedback))]
        public IHttpActionResult Deletefeedback(int id)
        {
            feedback feedback = db.feedback.Find(id);
            if (feedback == null)
            {
                return NotFound();
            }

            db.feedback.Remove(feedback);
            db.SaveChanges();

            return Ok(feedback);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool feedbackExists(int id)
        {
            return db.feedback.Count(e => e.id == id) > 0;
        }
    }
}