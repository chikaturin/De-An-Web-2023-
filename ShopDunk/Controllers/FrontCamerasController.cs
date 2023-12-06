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
    public class FrontCamerasController : Controller
    {
        private DBShopDunkEntities1 db = new DBShopDunkEntities1();

        // GET: FrontCameras
        public ActionResult Index()
        {
            return View(db.FrontCameras.ToList());
        }

        // GET: FrontCameras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FrontCamera frontCamera = db.FrontCameras.Find(id);
            if (frontCamera == null)
            {
                return HttpNotFound();
            }
            return View(frontCamera);
        }

        // GET: FrontCameras/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FrontCameras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDFCam,Resolution")] FrontCamera frontCamera)
        {
            if (ModelState.IsValid)
            {
                db.FrontCameras.Add(frontCamera);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(frontCamera);
        }

        // GET: FrontCameras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FrontCamera frontCamera = db.FrontCameras.Find(id);
            if (frontCamera == null)
            {
                return HttpNotFound();
            }
            return View(frontCamera);
        }

        // POST: FrontCameras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDFCam,Resolution")] FrontCamera frontCamera)
        {
            if (ModelState.IsValid)
            {
                db.Entry(frontCamera).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(frontCamera);
        }

        // GET: FrontCameras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FrontCamera frontCamera = db.FrontCameras.Find(id);
            if (frontCamera == null)
            {
                return HttpNotFound();
            }
            return View(frontCamera);
        }

        // POST: FrontCameras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FrontCamera frontCamera = db.FrontCameras.Find(id);
            db.FrontCameras.Remove(frontCamera);
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
