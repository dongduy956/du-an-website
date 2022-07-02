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
    public class PromotionController : ApiController
    {
        private nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();

        // GET: api/Promotion
        public IQueryable<promotion> Getpromotion()
        {
            return db.promotion;
        }

        // GET: api/Promotion/5
        [ResponseType(typeof(promotion))]
        public IHttpActionResult Getpromotion(int id)
        {
            promotion promotion = db.promotion.Find(id);
            if (promotion == null)
            {
                return NotFound();
            }

            return Ok(promotion);
        }

        // PUT: api/Promotion/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putpromotion(int id, promotion promotion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != promotion.id)
            {
                return BadRequest();
            }

            db.Entry(promotion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!promotionExists(id))
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

        // POST: api/Promotion
        [ResponseType(typeof(promotion))]
        public IHttpActionResult Postpromotion(promotion promotion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.promotion.Add(promotion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = promotion.id }, promotion);
        }

        // DELETE: api/Promotion/5
        [ResponseType(typeof(promotion))]
        public IHttpActionResult Deletepromotion(int id)
        {
            promotion promotion = db.promotion.Find(id);
            if (promotion == null)
            {
                return NotFound();
            }

            db.promotion.Remove(promotion);
            db.SaveChanges();

            return Ok(promotion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool promotionExists(int id)
        {
            return db.promotion.Count(e => e.id == id) > 0;
        }
    }
}