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
    public class TableGamesController : ApiController
    {
        private BaseForExamMobileEntities db = new BaseForExamMobileEntities();

        // GET: api/TableGames
        [ResponseType(typeof(List<TableGames>))]
        public IHttpActionResult GetUsers()
        {
            return Ok(db.TableGames.ToList().ConvertAll(x => new classTableGames(x)));
        }

        // GET: api/TableGames/5
        [ResponseType(typeof(TableGames))]
        public IHttpActionResult GetTableGames(int id)
        {
            TableGames tableGames = db.TableGames.Find(id);
            if (tableGames == null)
            {
                return NotFound();
            }

            return Ok(tableGames);
        }

        // PUT: api/TableGames/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTableGames(int id, TableGames tableGames)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tableGames.ID)
            {
                return BadRequest();
            }

            db.Entry(tableGames).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TableGamesExists(id))
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

        // POST: api/TableGames
        [ResponseType(typeof(TableGames))]
        public IHttpActionResult PostTableGames(TableGames tableGames)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TableGames.Add(tableGames);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tableGames.ID }, tableGames);
        }

        // DELETE: api/TableGames/5
        [ResponseType(typeof(TableGames))]
        public IHttpActionResult DeleteTableGames(int id)
        {
            TableGames tableGames = db.TableGames.Find(id);
            if (tableGames == null)
            {
                return NotFound();
            }

            db.TableGames.Remove(tableGames);
            db.SaveChanges();

            return Ok(tableGames);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TableGamesExists(int id)
        {
            return db.TableGames.Count(e => e.ID == id) > 0;
        }
    }
}