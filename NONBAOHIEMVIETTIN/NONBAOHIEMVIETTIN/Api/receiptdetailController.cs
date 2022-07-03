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
    public class ReceiptDetailController : ApiController
    {
        private nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();

        // GET: api/ReceiptDetail
        public IQueryable<receiptdetail> Getreceiptdetail()
        {
            return db.receiptdetail;
        }

        // GET: api/ReceiptDetail/5
        [ResponseType(typeof(receiptdetail))]
        public IHttpActionResult Getreceiptdetail(int id)
        {
            receiptdetail receiptdetail = db.receiptdetail.Find(id);
            if (receiptdetail == null)
            {
                return NotFound();
            }

            return Ok(receiptdetail);
        }

        // PUT: api/ReceiptDetail/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putreceiptdetail(int id, receiptdetail receiptdetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != receiptdetail.idproduct)
            {
                return BadRequest();
            }

            db.Entry(receiptdetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!receiptdetailExists(id))
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

        // POST: api/ReceiptDetail
        [ResponseType(typeof(receiptdetail))]
        public IHttpActionResult Postreceiptdetail(receiptdetail receiptdetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.receiptdetail.Add(receiptdetail);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (receiptdetailExists(receiptdetail.idproduct))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = receiptdetail.idproduct }, receiptdetail);
        }

        // DELETE: api/ReceiptDetail/5
        [ResponseType(typeof(receiptdetail))]
        public IHttpActionResult Deletereceiptdetail(int id)
        {
            receiptdetail receiptdetail = db.receiptdetail.Find(id);
            if (receiptdetail == null)
            {
                return NotFound();
            }

            db.receiptdetail.Remove(receiptdetail);
            db.SaveChanges();

            return Ok(receiptdetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool receiptdetailExists(int id)
        {
            return db.receiptdetail.Count(e => e.idproduct == id) > 0;
        }
    }
}