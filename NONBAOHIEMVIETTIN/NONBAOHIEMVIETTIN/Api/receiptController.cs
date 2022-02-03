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
    public class receiptController : ApiController
    {
        private nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();

        // GET: api/receipt
        public IQueryable<receipt> Getreceipt()
        {
            return db.receipt;
        }

        // GET: api/receipt/5
        [ResponseType(typeof(receipt))]
        public IHttpActionResult Getreceipt(int id)
        {
            receipt receipt = db.receipt.Find(id);
            if (receipt == null)
            {
                return NotFound();
            }

            return Ok(receipt);
        }

        // PUT: api/receipt/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putreceipt(int id, receipt receipt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != receipt.id)
            {
                return BadRequest();
            }

            db.Entry(receipt).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!receiptExists(id))
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

        // POST: api/receipt
        [ResponseType(typeof(receipt))]
        public IHttpActionResult Postreceipt(receipt receipt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.receipt.Add(receipt);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = receipt.id }, receipt);
        }

        // DELETE: api/receipt/5
        [ResponseType(typeof(receipt))]
        public IHttpActionResult Deletereceipt(int id)
        {
            receipt receipt = db.receipt.Find(id);
            if (receipt == null)
            {
                return NotFound();
            }

            db.receipt.Remove(receipt);
            db.SaveChanges();

            return Ok(receipt);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool receiptExists(int id)
        {
            return db.receipt.Count(e => e.id == id) > 0;
        }
    }
}