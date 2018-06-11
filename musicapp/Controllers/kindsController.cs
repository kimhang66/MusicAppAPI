using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using musicapp;

namespace musicapp.Controllers
{
    public class kindsController : Controller
    {
        private musicappEntities db = new musicappEntities();

        // GET: kinds
        public ActionResult Index()
        {
            return View(db.kinds.ToList());
        }

        // GET: kinds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kind kind = db.kinds.Find(id);
            if (kind == null)
            {
                return HttpNotFound();
            }
            return View(kind);
        }

        // GET: kinds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: kinds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idkind,namekind")] kind kind)
        {
            if (ModelState.IsValid)
            {
                db.kinds.Add(kind);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kind);
        }

        // GET: kinds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kind kind = db.kinds.Find(id);
            if (kind == null)
            {
                return HttpNotFound();
            }
            return View(kind);
        }

        // POST: kinds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idkind,namekind")] kind kind)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kind).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kind);
        }

        // GET: kinds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kind kind = db.kinds.Find(id);
            if (kind == null)
            {
                return HttpNotFound();
            }
            return View(kind);
        }

        // POST: kinds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            kind kind = db.kinds.Find(id);
            db.kinds.Remove(kind);
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
