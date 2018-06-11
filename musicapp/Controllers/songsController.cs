using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using musicapp;
using musicapp.Models;

namespace musicapp.Controllers
{
    public class songsController : Controller
    {
        private musicappEntities db = new musicappEntities();

        // GET: songs
        public ActionResult Index()
        {
            var songs = db.songs.Include(s => s.author).Include(s => s.kind).Include(s => s.singer);
            return View(songs.ToList());
        }

        // GET: songs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            song song = db.songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // GET: songs/Create
        public ActionResult Create()
        {
            ViewBag.Id = Request["id"];
            ViewBag.idauthor = new SelectList(db.authors, "idauthor", "nameauthor");
            ViewBag.idkind = new SelectList(db.kinds, "idkind", "namekind");
            ViewBag.idsinger = new SelectList(db.singers, "idsinger", "namesinger");
            return View();
        }

        // POST: songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idsong,namesong,imagesong,idsinger,idauthor,idkind,linkgoogle")] song song)
        {

            song.linkgoogle = "https://drive.google.com/uc?id=" + song.linkgoogle + "&export=download";
            if (ModelState.IsValid)
            {
                db.songs.Add(song);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idauthor = new SelectList(db.authors, "idauthor", "nameauthor", song.idauthor);
            ViewBag.idkind = new SelectList(db.kinds, "idkind", "namekind", song.idkind);
            ViewBag.idsinger = new SelectList(db.singers, "idsinger", "namesinger", song.idsinger);
            return View(song);
        }

        // GET: songs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            song song = db.songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            ViewBag.idauthor = new SelectList(db.authors, "idauthor", "nameauthor", song.idauthor);
            ViewBag.idkind = new SelectList(db.kinds, "idkind", "namekind", song.idkind);
            ViewBag.idsinger = new SelectList(db.singers, "idsinger", "namesinger", song.idsinger);
            return View(song);
        }

        // POST: songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idsong,namesong,imagesong,idsinger,idauthor,idkind,linkgoogle")] song song)
        {
            if (ModelState.IsValid)
            {
                db.Entry(song).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idauthor = new SelectList(db.authors, "idauthor", "nameauthor", song.idauthor);
            ViewBag.idkind = new SelectList(db.kinds, "idkind", "namekind", song.idkind);
            ViewBag.idsinger = new SelectList(db.singers, "idsinger", "namesinger", song.idsinger);
            return View(song);
        }

        // GET: songs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            song song = db.songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // POST: songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            song song = db.songs.Find(id);
            db.songs.Remove(song);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
