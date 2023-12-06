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
    public class MemoriesController : Controller
    {
        private DBShopDunkEntities1 db = new DBShopDunkEntities1();

        // GET: Memories
        public ActionResult Index()
        {
            return View(db.Memories.ToList());
        }

        // GET: Memories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Memory memory = db.Memories.Find(id);
            if (memory == null)
            {
                return HttpNotFound();
            }
            return View(memory);
        }

        // GET: Memories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Memories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDMem,NameMem,StorageMem")] Memory memory)
        {
            if (ModelState.IsValid)
            {
                db.Memories.Add(memory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(memory);
        }

        // GET: Memories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Memory memory = db.Memories.Find(id);
            if (memory == null)
            {
                return HttpNotFound();
            }
            return View(memory);
        }

        // POST: Memories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDMem,NameMem,StorageMem")] Memory memory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(memory);
        }

        // GET: Memories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Memory memory = db.Memories.Find(id);
            if (memory == null)
            {
                return HttpNotFound();
            }
            return View(memory);
        }

        // POST: Memories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Memory memory = db.Memories.Find(id);
            db.Memories.Remove(memory);
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
