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
    public class RechargeController : ApiController
    {
        private nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();

        // GET: api/Recharge
        public IQueryable<history_recharge> Gethistory_recharge()
        {
            return db.history_recharge;
        }

        // GET: api/Recharge/5
        [ResponseType(typeof(history_recharge))]
        public IHttpActionResult Gethistory_recharge(int id)
        {
            history_recharge history_recharge = db.history_recharge.Find(id);
            if (history_recharge == null)
            {
                return NotFound();
            }

            return Ok(history_recharge);
        }

        // PUT: api/Recharge/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puthistory_recharge(int id, history_recharge history_recharge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != history_recharge.id)
            {
                return BadRequest();
            }

            db.Entry(history_recharge).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!history_rechargeExists(id))
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

        // POST: api/Recharge
        [ResponseType(typeof(history_recharge))]
        public IHttpActionResult Posthistory_recharge(history_recharge history_recharge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.history_recharge.Add(history_recharge);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = history_recharge.id }, history_recharge);
        }

        // DELETE: api/Recharge/5
        [ResponseType(typeof(history_recharge))]
        public IHttpActionResult Deletehistory_recharge(int id)
        {
            history_recharge history_recharge = db.history_recharge.Find(id);
            if (history_recharge == null)
            {
                return NotFound();
            }

            db.history_recharge.Remove(history_recharge);
            db.SaveChanges();

            return Ok(history_recharge);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool history_rechargeExists(int id)
        {
            return db.history_recharge.Count(e => e.id == id) > 0;
        }
    }
}