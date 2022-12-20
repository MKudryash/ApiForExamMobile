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
    public class HatShopsController : ApiController
    {
        private BaseForExamMobileEntities db = new BaseForExamMobileEntities();

        // GET: api/HatShops
        [ResponseType(typeof(List<HatShop>))]
        public IHttpActionResult GetUsers()
        {
            return Ok(db.HatShop.ToList().ConvertAll(x => new classHatShop(x)));
        }
        [Route("api/HatShops/sortByHatShops")]
        [HttpGet] // There are HttpGet, HttpPost, HttpPut, HttpDelete.
        public async Task<IHttpActionResult> SortByCostOrAvailabilityInTheStore(int typeOfSort, string nameProduct)
        {
            Regex checkName = new Regex($@"{nameProduct}.*");
            switch (typeOfSort)
            {
                case 0:
                    return Ok(db.HatShop.ToList().ConvertAll(x => new classHatShop(x)).Where(x => checkName.IsMatch(x.Title)));
                case 1:
                    return Ok(db.HatShop.ToList().ConvertAll(x => new classHatShop(x)).Where(x => checkName.IsMatch(x.Title)).OrderBy(x => x.Cost));
                case 2:
                    return Ok(db.HatShop.ToList().ConvertAll(x => new classHatShop(x)).Where(x => checkName.IsMatch(x.Title)).OrderByDescending(x => x.Cost));
                case 3:
                    return Ok(db.HatShop.ToList().ConvertAll(x => new classHatShop(x)).Where(x => checkName.IsMatch(x.Title)).OrderBy(x => x.AvailabilityInTheStore));
                case 4:
                    return Ok(db.HatShop.ToList().ConvertAll(x => new classHatShop(x)).Where(x => checkName.IsMatch(x.Title)).OrderByDescending(x => x.AvailabilityInTheStore));
                default: return BadRequest();
            }
        }
        // GET: api/HatShops/5
        [ResponseType(typeof(HatShop))]
        public IHttpActionResult GetHatShop(int id)
        {
            HatShop hatShop = db.HatShop.Find(id);
            if (hatShop == null)
            {
                return NotFound();
            }

            return Ok(hatShop);
        }

        // PUT: api/HatShops/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHatShop(int id, HatShop hatShop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hatShop.ID)
            {
                return BadRequest();
            }

            db.Entry(hatShop).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HatShopExists(id))
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

        // POST: api/HatShops
        [ResponseType(typeof(HatShop))]
        public IHttpActionResult PostHatShop(HatShop hatShop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HatShop.Add(hatShop);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hatShop.ID }, hatShop);
        }

        // DELETE: api/HatShops/5
        [ResponseType(typeof(HatShop))]
        public IHttpActionResult DeleteHatShop(int id)
        {
            HatShop hatShop = db.HatShop.Find(id);
            if (hatShop == null)
            {
                return NotFound();
            }

            db.HatShop.Remove(hatShop);
            db.SaveChanges();

            return Ok(hatShop);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HatShopExists(int id)
        {
            return db.HatShop.Count(e => e.ID == id) > 0;
        }
    }
}