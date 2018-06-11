using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using musicapp;

namespace musicapp.Controllers
{
    public class songsApiController : ApiController
    {
        private musicappEntities db = new musicappEntities();

        // GET: api/songs1
        public IEnumerable<Object> Getsongs()
        {
            var songs = (from s in db.songs
                         join a in db.singers on s.idsinger equals a.idsinger
                         join k in db.kinds on s.idkind equals k.idkind
                         join h in db.authors on s.idauthor equals h.idauthor
                         orderby s.idsong descending
                         select new
                         {
                             idSong = s.idsong,
                             nameSong = s.namesong,
                             imgSong = s.imagesong,
                             nameAuthor = h.nameauthor,
                             nameSinger = a.namesinger,
                             nameKind = k.namekind,
                             linkGG = s.linkgoogle
                         }).ToList();
            return songs;
        }

        // GET: api/songs1/5
        [ResponseType(typeof(song))]
        public IHttpActionResult Getsong(int id)
        {
            song song = db.songs.Find(id);
            if (song == null)
            {
                return NotFound();
            }

            return Ok(song);
        }

        // PUT: api/songs1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putsong(int id, song song)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != song.idsong)
            {
                return BadRequest();
            }

            db.Entry(song).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!songExists(id))
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

        // POST: api/songs1
        [ResponseType(typeof(song))]
        public IHttpActionResult Postsong(song song)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.songs.Add(song);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = song.idsong }, song);
        }

        // DELETE: api/songs1/5
        [ResponseType(typeof(song))]
        public async Task<IHttpActionResult> Deletesong(int id)
        {
            song song = await db.songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            db.songs.Remove(song);
            await db.SaveChangesAsync();

            return Ok(song);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool songExists(int id)
        {
            return db.songs.Count(e => e.idsong == id) > 0;
        }
    }
}