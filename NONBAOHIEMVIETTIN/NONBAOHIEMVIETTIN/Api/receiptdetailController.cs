﻿using System;
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
    public class receiptdetailController : ApiController
    {
        private nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();

        // GET: api/receiptdetail
        public IQueryable<receiptdetail> Getreceiptdetail()
        {
            return db.receiptdetail;
        }

        // GET: api/receiptdetail/5
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

        // PUT: api/receiptdetail/5
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

        // POST: api/receiptdetail
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

        // DELETE: api/receiptdetail/5
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