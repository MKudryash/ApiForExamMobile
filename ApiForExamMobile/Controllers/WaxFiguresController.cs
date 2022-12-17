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
using ApiForExamMobile.Models;

namespace ApiForExamMobile.Controllers
{
    public class WaxFiguresController : ApiController
    {
        private BaseForExamMobileEntities db = new BaseForExamMobileEntities();

        // GET: api/WaxFigures
        [ResponseType(typeof(List<WaxFigures>))]
        public IHttpActionResult GetUsers()
        {
            return Ok(db.WaxFigures.ToList().ConvertAll(x => new classWaxFigure(x)));
        }

        // GET: api/WaxFigures/5
        [ResponseType(typeof(WaxFigures))]
        public IHttpActionResult GetWaxFigures(int id)
        {
            WaxFigures waxFigures = db.WaxFigures.Find(id);
            if (waxFigures == null)
            {
                return NotFound();
            }

            return Ok(waxFigures);
        }

        // PUT: api/WaxFigures/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWaxFigures(int id, WaxFigures waxFigures)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != waxFigures.ID)
            {
                return BadRequest();
            }

            db.Entry(waxFigures).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WaxFiguresExists(id))
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

        // POST: api/WaxFigures
        [ResponseType(typeof(WaxFigures))]
        public IHttpActionResult PostWaxFigures(WaxFigures waxFigures)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WaxFigures.Add(waxFigures);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = waxFigures.ID }, waxFigures);
        }

        // DELETE: api/WaxFigures/5
        [ResponseType(typeof(WaxFigures))]
        public IHttpActionResult DeleteWaxFigures(int id)
        {
            WaxFigures waxFigures = db.WaxFigures.Find(id);
            if (waxFigures == null)
            {
                return NotFound();
            }

            db.WaxFigures.Remove(waxFigures);
            db.SaveChanges();

            return Ok(waxFigures);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WaxFiguresExists(int id)
        {
            return db.WaxFigures.Count(e => e.ID == id) > 0;
        }
    }
}