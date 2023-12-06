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
    public class RearCamerasController : Controller
    {
        private DBShopDunkEntities1 db = new DBShopDunkEntities1();

        // GET: RearCameras
        public ActionResult Index()
        {
            return View(db.RearCameras.ToList());
        }

        // GET: RearCameras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RearCamera rearCamera = db.RearCameras.Find(id);
            if (rearCamera == null)
            {
                return HttpNotFound();
            }
            return View(rearCamera);
        }

        // GET: RearCameras/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RearCameras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDRCam,Resolution")] RearCamera rearCamera)
        {
            if (ModelState.IsValid)
            {
                db.RearCameras.Add(rearCamera);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rearCamera);
        }

        // GET: RearCameras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RearCamera rearCamera = db.RearCameras.Find(id);
            if (rearCamera == null)
            {
                return HttpNotFound();
            }
            return View(rearCamera);
        }

        // POST: RearCameras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDRCam,Resolution")] RearCamera rearCamera)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rearCamera).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rearCamera);
        }

        // GET: RearCameras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RearCamera rearCamera = db.RearCameras.Find(id);
            if (rearCamera == null)
            {
                return HttpNotFound();
            }
            return View(rearCamera);
        }

        // POST: RearCameras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RearCamera rearCamera = db.RearCameras.Find(id);
            db.RearCameras.Remove(rearCamera);
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
