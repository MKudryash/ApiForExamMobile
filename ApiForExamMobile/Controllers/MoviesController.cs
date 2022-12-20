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
    public class MoviesController : ApiController
    {
        private BaseForExamMobileEntities db = new BaseForExamMobileEntities();

        // GET: api/Movies
        [ResponseType(typeof(List<Movies>))]
        public IHttpActionResult GetMovies()
        {
            return Ok(db.Movies.ToList().ConvertAll(x => new classMovies(x)));
        }
        [Route("api/Movies/sortByMovies")]
        [HttpGet] // There are HttpGet, HttpPost, HttpPut, HttpDelete.
        public async Task<IHttpActionResult> SortByCostOrAvailabilityInTheStore(int typeOfSort, string nameProduct)
        {
            Regex checkName = new Regex($@"{nameProduct}.*");
            switch (typeOfSort)
            {
                case 0:
                    return Ok(db.Movies.ToList().ConvertAll(x => new classMovies(x)).Where(x => checkName.IsMatch(x.Title)));
                case 1:
                    return Ok(db.Movies.ToList().ConvertAll(x => new classMovies(x)).Where(x => checkName.IsMatch(x.Title)).OrderBy(x => x.Cost));
                case 2:
                    return Ok(db.Movies.ToList().ConvertAll(x => new classMovies(x)).Where(x => checkName.IsMatch(x.Title)).OrderByDescending(x => x.Cost));
                case 3:
                    return Ok(db.Movies.ToList().ConvertAll(x => new classMovies(x)).Where(x => checkName.IsMatch(x.Title)).OrderBy(x => x.AvailabilityInTheStore));
                case 4:
                    return Ok(db.Movies.ToList().ConvertAll(x => new classMovies(x)).Where(x => checkName.IsMatch(x.Title)).OrderByDescending(x => x.AvailabilityInTheStore));
                default: return BadRequest();
            }
        }
        // GET: api/Movies/5
        [ResponseType(typeof(Movies))]
        public IHttpActionResult GetMovies(int id)
        {
            Movies movies = db.Movies.Find(id);
            if (movies == null)
            {
                return NotFound();
            }

            return Ok(movies);
        }

        // PUT: api/Movies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMovies(int id, Movies movies)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movies.ID)
            {
                return BadRequest();
            }

            db.Entry(movies).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoviesExists(id))
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

        // POST: api/Movies
        [ResponseType(typeof(Movies))]
        public IHttpActionResult PostMovies(Movies movies)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Movies.Add(movies);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = movies.ID }, movies);
        }

        // DELETE: api/Movies/5
        [ResponseType(typeof(Movies))]
        public IHttpActionResult DeleteMovies(int id)
        {
            Movies movies = db.Movies.Find(id);
            if (movies == null)
            {
                return NotFound();
            }

            db.Movies.Remove(movies);
            db.SaveChanges();

            return Ok(movies);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MoviesExists(int id)
        {
            return db.Movies.Count(e => e.ID == id) > 0;
        }
    }
}