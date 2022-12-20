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
    public class BuildingMaterialsController : ApiController
    {
        private BaseForExamMobileEntities db = new BaseForExamMobileEntities();

        // GET: api/BuildingMaterials
        [ResponseType(typeof(List<BuildingMaterials>))]
        public IHttpActionResult GetUsers()
        {
            return Ok(db.BuildingMaterials.ToList().ConvertAll(x => new classBuildingMaterials(x)));
        }
        [Route("api/BuildingMaterials/sortByBuildingMaterials")]
        [HttpGet] // There are HttpGet, HttpPost, HttpPut, HttpDelete.
        public async Task<IHttpActionResult> SortByCostOrAvailabilityInTheStore(int typeOfSort, string nameProduct)
        {
            Regex checkName = new Regex($@"{nameProduct}.*");
            switch (typeOfSort)
            {
                case 0:
                    return Ok(db.BuildingMaterials.ToList().ConvertAll(x => new classBuildingMaterials(x)).Where(x => checkName.IsMatch(x.Title)));
                case 1:
                    return Ok(db.BuildingMaterials.ToList().ConvertAll(x => new classBuildingMaterials(x)).Where(x => checkName.IsMatch(x.Title)).OrderBy(x => x.Cost));
                case 2:
                    return Ok(db.BuildingMaterials.ToList().ConvertAll(x => new classBuildingMaterials(x)).Where(x => checkName.IsMatch(x.Title)).OrderByDescending(x => x.Cost));
                case 3:
                    return Ok(db.BuildingMaterials.ToList().ConvertAll(x => new classBuildingMaterials(x)).Where(x => checkName.IsMatch(x.Title)).OrderBy(x => x.AvailabilityInTheStore));
                case 4:
                    return Ok(db.BuildingMaterials.ToList().ConvertAll(x => new classBuildingMaterials(x)).Where(x => checkName.IsMatch(x.Title)).OrderByDescending(x => x.AvailabilityInTheStore));
                default: return BadRequest();
            }
        }

        // GET: api/BuildingMaterials/5
        [ResponseType(typeof(BuildingMaterials))]
        public IHttpActionResult GetBuildingMaterials(int id)
        {
            BuildingMaterials buildingMaterials = db.BuildingMaterials.Find(id);
            if (buildingMaterials == null)
            {
                return NotFound();
            }

            return Ok(buildingMaterials);
        }

        // PUT: api/BuildingMaterials/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBuildingMaterials(int id, BuildingMaterials buildingMaterials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != buildingMaterials.ID)
            {
                return BadRequest();
            }

            db.Entry(buildingMaterials).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingMaterialsExists(id))
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

        // POST: api/BuildingMaterials
        [ResponseType(typeof(BuildingMaterials))]
        public IHttpActionResult PostBuildingMaterials(BuildingMaterials buildingMaterials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BuildingMaterials.Add(buildingMaterials);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = buildingMaterials.ID }, buildingMaterials);
        }

        // DELETE: api/BuildingMaterials/5
        [ResponseType(typeof(BuildingMaterials))]
        public IHttpActionResult DeleteBuildingMaterials(int id)
        {
            BuildingMaterials buildingMaterials = db.BuildingMaterials.Find(id);
            if (buildingMaterials == null)
            {
                return NotFound();
            }

            db.BuildingMaterials.Remove(buildingMaterials);
            db.SaveChanges();

            return Ok(buildingMaterials);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BuildingMaterialsExists(int id)
        {
            return db.BuildingMaterials.Count(e => e.ID == id) > 0;
        }
    }
}