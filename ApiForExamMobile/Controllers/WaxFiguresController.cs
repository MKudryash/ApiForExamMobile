using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
        [Route("api/WaxFigures/sortByWaxFigures")]
        [HttpGet] // There are HttpGet, HttpPost, HttpPut, HttpDelete.
        public async Task<IHttpActionResult> SortByCostOrAvailabilityInTheStore(int typeOfSort, string nameProduct)
        {
            Regex checkName = new Regex($@"{nameProduct}.*");
            switch (typeOfSort)
            {
                case 0:
                    return Ok(db.WaxFigures.ToList().ConvertAll(x => new classWaxFigure(x)).Where(x => checkName.IsMatch(x.Title)));
                case 1:
                    return Ok(db.WaxFigures.ToList().ConvertAll(x => new classWaxFigure(x)).Where(x => checkName.IsMatch(x.Title)).OrderBy(x => x.Cost));
                case 2:
                    return Ok(db.WaxFigures.ToList().ConvertAll(x => new classWaxFigure(x)).Where(x => checkName.IsMatch(x.Title)).OrderByDescending(x => x.Cost));
                case 3:
                    return Ok(db.WaxFigures.ToList().ConvertAll(x => new classWaxFigure(x)).Where(x => checkName.IsMatch(x.Title)).OrderBy(x => x.AvailabilityInTheStore));
                case 4:
                    return Ok(db.WaxFigures.ToList().ConvertAll(x => new classWaxFigure(x)).Where(x => checkName.IsMatch(x.Title)).OrderByDescending(x => x.AvailabilityInTheStore));
                default: return BadRequest();
            }
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