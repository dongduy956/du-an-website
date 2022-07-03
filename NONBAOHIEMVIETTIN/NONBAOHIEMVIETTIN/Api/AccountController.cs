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
    
    public class AccountController : ApiController
    {
        private nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();

        // GET: api/Account
        public IQueryable<accounts> Getaccounts()
        {
            return db.accounts;
        }

        // GET: api/Account/5
        [ResponseType(typeof(accounts))]
        public IHttpActionResult Getaccounts(int id)
        {
            accounts accounts = db.accounts.Find(id);
            if (accounts == null)
            {
                return NotFound();
            }

            return Ok(accounts);
        }

        // PUT: api/Account/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putaccounts(int id, accounts accounts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accounts.id)
            {
                return BadRequest();
            }

            db.Entry(accounts).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!accountsExists(id))
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

        // POST: api/Account
        [ResponseType(typeof(accounts))]
        public IHttpActionResult Postaccounts(accounts accounts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.accounts.Add(accounts);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = accounts.id }, accounts);
        }

        // DELETE: api/Account/5
        [ResponseType(typeof(accounts))]
        public IHttpActionResult Deleteaccounts(int id)
        {
            accounts accounts = db.accounts.Find(id);
            if (accounts == null)
            {
                return NotFound();
            }

            db.accounts.Remove(accounts);
            db.SaveChanges();

            return Ok(accounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool accountsExists(int id)
        {
            return db.accounts.Count(e => e.id == id) > 0;
        }
    }
}