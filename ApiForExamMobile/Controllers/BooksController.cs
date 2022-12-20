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
    public class BooksController : ApiController
    {
        private BaseForExamMobileEntities db = new BaseForExamMobileEntities();

        // GET: api/Books
        [ResponseType(typeof(List<Books>))]
        public IHttpActionResult GetBooks()
        {
            return Ok(db.Books.ToList().ConvertAll(x => new classBooks(x)));
        }
        [Route("api/Books/sortByBooks")]
        [HttpGet] // There are HttpGet, HttpPost, HttpPut, HttpDelete.
        public async Task<IHttpActionResult> SortByCostOrAvailabilityInTheStore(int typeOfSort, string nameProduct)
        {
            Regex checkName = new Regex($@"{nameProduct}.*");
            switch (typeOfSort)
            {
                case 0:
                    return Ok(db.Books.ToList().ConvertAll(x => new classBooks(x)).Where(x => checkName.IsMatch(x.Title)));
                case 1:
                    return Ok(db.Books.ToList().ConvertAll(x => new classBooks(x)).Where(x => checkName.IsMatch(x.Title)).OrderBy(x => x.Cost));
                case 2:
                    return Ok(db.Books.ToList().ConvertAll(x => new classBooks(x)).Where(x => checkName.IsMatch(x.Title)).OrderByDescending(x => x.Cost));
                case 3:
                    return Ok(db.Books.ToList().ConvertAll(x => new classBooks(x)).Where(x => checkName.IsMatch(x.Title)).OrderBy(x => x.AvailabilityInTheStore));
                case 4:
                    return Ok(db.Books.ToList().ConvertAll(x => new classBooks(x)).Where(x => checkName.IsMatch(x.Title)).OrderByDescending(x => x.AvailabilityInTheStore));
                default: return BadRequest();
            }
        }


        // GET: api/Books/5
        [ResponseType(typeof(Books))]
        public IHttpActionResult GetBooks(int id)
        {
            Books books = db.Books.Find(id);
            if (books == null)
            {
                return NotFound();
            }

            return Ok(books);
        }

        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBooks(int id, Books books)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != books.ID)
            {
                return BadRequest();
            }

            db.Entry(books).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BooksExists(id))
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

        // POST: api/Books
        [ResponseType(typeof(Books))]
        public IHttpActionResult PostBooks(Books books)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Books.Add(books);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = books.ID }, books);
        }

        // DELETE: api/Books/5
        [ResponseType(typeof(Books))]
        public IHttpActionResult DeleteBooks(int id)
        {
            Books books = db.Books.Find(id);
            if (books == null)
            {
                return NotFound();
            }

            db.Books.Remove(books);
            db.SaveChanges();

            return Ok(books);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BooksExists(int id)
        {
            return db.Books.Count(e => e.ID == id) > 0;
        }

    }
}