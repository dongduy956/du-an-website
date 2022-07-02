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
    public class WithdrawController : ApiController
    {
        private nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();

        // GET: api/Withdraw
        public IQueryable<history_withdraw> Gethistory_withdraw()
        {
            return db.history_withdraw;
        }

        // GET: api/Withdraw/5
        [ResponseType(typeof(history_withdraw))]
        public IHttpActionResult Gethistory_withdraw(int id)
        {
            history_withdraw history_withdraw = db.history_withdraw.Find(id);
            if (history_withdraw == null)
            {
                return NotFound();
            }

            return Ok(history_withdraw);
        }

        // PUT: api/Withdraw/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puthistory_withdraw(int id, history_withdraw history_withdraw)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != history_withdraw.id)
            {
                return BadRequest();
            }

            db.Entry(history_withdraw).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!history_withdrawExists(id))
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

        // POST: api/Withdraw
        [ResponseType(typeof(history_withdraw))]
        public IHttpActionResult Posthistory_withdraw(history_withdraw history_withdraw)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.history_withdraw.Add(history_withdraw);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = history_withdraw.id }, history_withdraw);
        }

        // DELETE: api/Withdraw/5
        [ResponseType(typeof(history_withdraw))]
        public IHttpActionResult Deletehistory_withdraw(int id)
        {
            history_withdraw history_withdraw = db.history_withdraw.Find(id);
            if (history_withdraw == null)
            {
                return NotFound();
            }

            db.history_withdraw.Remove(history_withdraw);
            db.SaveChanges();

            return Ok(history_withdraw);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool history_withdrawExists(int id)
        {
            return db.history_withdraw.Count(e => e.id == id) > 0;
        }
    }
}