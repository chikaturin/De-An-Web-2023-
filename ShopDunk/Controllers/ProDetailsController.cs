using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopDunk.Models;

namespace ShopDunk.Controllers
{
    public class ProDetailsController : Controller
    {
        private DBShopDunkEntities1 db = new DBShopDunkEntities1();

        // GET: ProDetails
        public ActionResult Index()
        {
            var proDetails = db.ProDetails.Include(p => p.Color).Include(p => p.Memory).Include(p => p.Product).Include(p => p.Voucher);
            return View(proDetails.ToList());
        }
        // GET: ProDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProDetail proDetail = db.ProDetails.Find(id);
            if (proDetail == null)
            {
                return HttpNotFound();
            }
            return View(proDetail);
        }
        public ActionResult StorageByName()
        {
            var proDetails = db.ProDetails.ToList(); 
            ViewBag.SumStorage = proDetails.Sum(p => p.RemainQuantity); 
            return View(proDetails);
        }

        // GET: ProDetails/Create
        public ActionResult Create()
        {
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "ColorName");
            ViewBag.IDMemory = new SelectList(db.Memories, "IDMem", "NameMem");
            ViewBag.ProID = new SelectList(db.Products, "ProID", "ProName");
            ViewBag.IDVoucher = new SelectList(db.Vouchers, "IDVoucher", "NameVoucher");
            return View();
        }

        // POST: ProDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProDeID,ProID,ColorID,RemainQuantity,SoldQuantity,ViewQuantity,IDVoucher,ConnectNetwork,IDMemory")] ProDetail proDetail)
        {
            if (ModelState.IsValid)
            {
                db.ProDetails.Add(proDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "ColorName", proDetail.ColorID);
            ViewBag.IDMemory = new SelectList(db.Memories, "IDMem", "NameMem", proDetail.IDMemory);
            ViewBag.ProID = new SelectList(db.Products, "ProID", "ProName", proDetail.ProID);
            ViewBag.IDVoucher = new SelectList(db.Vouchers, "IDVoucher", "NameVoucher", proDetail.IDVoucher);
            return View(proDetail);
        }

        // GET: ProDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProDetail proDetail = db.ProDetails.Find(id);
            if (proDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "ColorName", proDetail.ColorID);
            ViewBag.IDMemory = new SelectList(db.Memories, "IDMem", "NameMem", proDetail.IDMemory);
            ViewBag.ProID = new SelectList(db.Products, "ProID", "ProName", proDetail.ProID);
            ViewBag.IDVoucher = new SelectList(db.Vouchers, "IDVoucher", "NameVoucher", proDetail.IDVoucher);
            return View(proDetail);
        }

        // POST: ProDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProDeID,ProID,ColorID,RemainQuantity,SoldQuantity,ViewQuantity,IDVoucher,ConnectNetwork,IDMemory")] ProDetail proDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "ColorName", proDetail.ColorID);
            ViewBag.IDMemory = new SelectList(db.Memories, "IDMem", "NameMem", proDetail.IDMemory);
            ViewBag.ProID = new SelectList(db.Products, "ProID", "ProName", proDetail.ProID);
            ViewBag.IDVoucher = new SelectList(db.Vouchers, "IDVoucher", "NameVoucher", proDetail.IDVoucher);
            return View(proDetail);
        }

        // GET: ProDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProDetail proDetail = db.ProDetails.Find(id);
            if (proDetail == null)
            {
                return HttpNotFound();
            }
            return View(proDetail);
        }

        // POST: ProDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProDetail proDetail = db.ProDetails.Find(id);
            db.ProDetails.Remove(proDetail);
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
