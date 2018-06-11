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
using musicapp;

namespace musicapp.Controllers
{
    public class detailplaylistsApiController : ApiController
    {
        private musicappEntities db = new musicappEntities();

        // GET: api/detailplaylistsApi
        public IQueryable<detailplaylist> Getdetailplaylists()
        {
            return db.detailplaylists;
        }

        // GET: api/detailplaylistsApi/5
        [ResponseType(typeof(detailplaylist))]
        public IHttpActionResult Getdetailplaylist(int id)
        {
            detailplaylist detailplaylist = db.detailplaylists.Find(id);
            if (detailplaylist == null)
            {
                return NotFound();
            }

            return Ok(detailplaylist);
        }

        // PUT: api/detailplaylistsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putdetailplaylist(int id, detailplaylist detailplaylist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != detailplaylist.iddetailPL)
            {
                return BadRequest();
            }

            db.Entry(detailplaylist).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!detailplaylistExists(id))
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

        // POST: api/detailplaylistsApi
        [ResponseType(typeof(detailplaylist))]
        public IHttpActionResult Postdetailplaylist(detailplaylist detailplaylist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.detailplaylists.Add(detailplaylist);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = detailplaylist.iddetailPL }, detailplaylist);
        }

        // DELETE: api/detailplaylistsApi/5
        [ResponseType(typeof(detailplaylist))]
        public IHttpActionResult Deletedetailplaylist(int id)
        {
            detailplaylist detailplaylist = db.detailplaylists.Find(id);
            if (detailplaylist == null)
            {
                return NotFound();
            }

            db.detailplaylists.Remove(detailplaylist);
            db.SaveChanges();

            return Ok(detailplaylist);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool detailplaylistExists(int id)
        {
            return db.detailplaylists.Count(e => e.iddetailPL == id) > 0;
        }
    }
}