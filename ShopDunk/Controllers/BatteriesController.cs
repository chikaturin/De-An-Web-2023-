using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopDunk.Models;

namespace ShopDunk.Controllers
{
    public class BatteriesController : Controller
    {
        private DBShopDunkEntities1 db = new DBShopDunkEntities1();

        // GET: Batteries
        public ActionResult Index()
        {
            return View(db.Batteries.ToList());
        }

        // GET: Batteries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Battery battery = db.Batteries.Find(id);
            if (battery == null)
            {
                return HttpNotFound();
            }
            return View(battery);
        }

        // GET: Batteries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Batteries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BatID,Storage,NameBat")] Battery battery)
        {
            if (ModelState.IsValid)
            {
                db.Batteries.Add(battery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(battery);
        }

        // GET: Batteries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Battery battery = db.Batteries.Find(id);
            if (battery == null)
            {
                return HttpNotFound();
            }
            return View(battery);
        }

        // POST: Batteries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BatID,Storage,NameBat")] Battery battery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(battery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(battery);
        }

        // GET: Batteries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Battery battery = db.Batteries.Find(id);
            if (battery == null)
            {
                return HttpNotFound();
            }
            return View(battery);
        }

        // POST: Batteries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Battery battery = db.Batteries.Find(id);
            db.Batteries.Remove(battery);
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
