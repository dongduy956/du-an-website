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
    public class OrderDetailController : ApiController
    {
        private nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();

        // GET: api/OrderDetail
        public IQueryable<orderdetail> Getorderdetail()
        {
            return db.orderdetail;
        }

        // GET: api/OrderDetail/5
        [ResponseType(typeof(orderdetail))]
        public IHttpActionResult Getorderdetail(int id)
        {
            orderdetail orderdetail = db.orderdetail.Find(id);
            if (orderdetail == null)
            {
                return NotFound();
            }

            return Ok(orderdetail);
        }

        // PUT: api/OrderDetail/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putorderdetail(int id, orderdetail orderdetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderdetail.idproduct)
            {
                return BadRequest();
            }

            db.Entry(orderdetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!orderdetailExists(id))
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

        // POST: api/OrderDetail
        [ResponseType(typeof(orderdetail))]
        public IHttpActionResult Postorderdetail(orderdetail orderdetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.orderdetail.Add(orderdetail);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (orderdetailExists(orderdetail.idproduct))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = orderdetail.idproduct }, orderdetail);
        }

        // DELETE: api/OrderDetail/5
        [ResponseType(typeof(orderdetail))]
        public IHttpActionResult Deleteorderdetail(int id)
        {
            orderdetail orderdetail = db.orderdetail.Find(id);
            if (orderdetail == null)
            {
                return NotFound();
            }

            db.orderdetail.Remove(orderdetail);
            db.SaveChanges();

            return Ok(orderdetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool orderdetailExists(int id)
        {
            return db.orderdetail.Count(e => e.idproduct == id) > 0;
        }
    }
}