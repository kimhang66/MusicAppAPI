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
    public class playlistsApiController : ApiController
    {
        private musicappEntities db = new musicappEntities();

        // GET: api/playlistsApi
        public IEnumerable<Object> Getplaylists(Int32 idUser)
        {
            var playlists = (from p in db.playlists
                             where p.iduser == idUser
                             select new
                             {
                                 idPlaylist = p.idplaylist,
                                 namePlaylist = p.nameplaylist
                             });
            return playlists.ToList();
        }

        // GET: api/playlistsApi/5
        [ResponseType(typeof(playlist))]
        public IEnumerable<Object> Getplaylist(int id)
        {
            var playlists = (from p in db.detailplaylists
                             join s in db.songs on p.idsong equals s.idsong
                             join a in db.authors on s.idauthor equals a.idauthor
                             join k in db.kinds on s.idkind equals k.idkind
                             join si in db.singers on s.idsinger equals si.idsinger
                             orderby p.iddetailPL descending
                             where p.idPlaylist == id
                             select new
                             {
                                 idSong = s.idsong,
                                 nameSong = s.namesong,
                                 imgSong = s.imagesong,
                                 nameAuthor = a.nameauthor,
                                 nameSinger = si.namesinger,
                                 nameKind = k.namekind,
                                 linkGG = s.linkgoogle
                             });
            return playlists.ToList();
        }

        // PUT: api/playlistsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putplaylist(int id, playlist playlist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != playlist.idplaylist)
            {
                return BadRequest();
            }

            db.Entry(playlist).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!playlistExists(id))
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

        // POST: api/playlistsApi
        [ResponseType(typeof(playlist))]
        public IHttpActionResult Postplaylist(playlist playlist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.playlists.Add(playlist);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = playlist.idplaylist }, playlist);
        }

        // DELETE: api/playlistsApi/5
        [ResponseType(typeof(playlist))]
        public IHttpActionResult Deleteplaylist(Int32 id)
        {
            //playlist playlist = db.playlists.Find(id);
            //if (playlist == null)
            //{
            //    return NotFound();
            //}

            //db.playlists.Remove(playlist);
            //db.SaveChanges();

            //return Ok(playlist);

            playlist list = db.playlists.Find(id);
            detailplaylist detail = db.detailplaylists.Where(b => b.idPlaylist == id)
                .FirstOrDefault();
            if (list == null)
            {
                return Ok(400);
            }
            if (detail != null)
            {
                db.detailplaylists.RemoveRange(db.detailplaylists.Where(c => c.idPlaylist == id));
                db.playlists.Remove(list);
                db.SaveChanges();
                return Ok(200);
            }
            else
            {
                db.playlists.Remove(list);
                db.SaveChanges();
                return Ok(200);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool playlistExists(int id)
        {
            return db.playlists.Count(e => e.idplaylist == id) > 0;
        }
    }
}