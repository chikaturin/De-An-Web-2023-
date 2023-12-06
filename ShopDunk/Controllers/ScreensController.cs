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
    public class ScreensController : Controller
    {
        private DBShopDunkEntities1 db = new DBShopDunkEntities1();

        // GET: Screens
        public ActionResult Index()
        {
            return View(db.Screens.ToList());
        }

        // GET: Screens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Screen screen = db.Screens.Find(id);
            if (screen == null)
            {
                return HttpNotFound();
            }
            return View(screen);
        }

        // GET: Screens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Screens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDScr,NameScr,Resolution")] Screen screen)
        {
            if (ModelState.IsValid)
            {
                db.Screens.Add(screen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(screen);
        }

        // GET: Screens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Screen screen = db.Screens.Find(id);
            if (screen == null)
            {
                return HttpNotFound();
            }
            return View(screen);
        }

        // POST: Screens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDScr,NameScr,Resolution")] Screen screen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(screen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(screen);
        }

        // GET: Screens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Screen screen = db.Screens.Find(id);
            if (screen == null)
            {
                return HttpNotFound();
            }
            return View(screen);
        }

        // POST: Screens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Screen screen = db.Screens.Find(id);
            db.Screens.Remove(screen);
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
