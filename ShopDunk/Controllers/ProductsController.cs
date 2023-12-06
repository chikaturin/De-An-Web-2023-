using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PagedList;
using ShopDunk.Models;
using PagedList.Mvc;

namespace ShopDunk.Controllers
{
    public class ProductsController : Controller
    {
        private DBShopDunkEntities1 db = new DBShopDunkEntities1();

        // GET: Products
        public ActionResult Search(int? page,string search = "")
        {
            int PageSize = 8;
            int PageNum = (page ?? 1);
            List<Product> products = db.Products.Where(row => row.ProName.Contains(search)).ToList();
            ViewBag.Search = search;
            return View(products.ToPagedList(PageNum, PageSize));
        }
        public ActionResult SearchOption(double? min = null, double? max = null)
        {
            double minPrice = min ?? 0; 
            double maxPrice = max ?? double.MaxValue;
            var list = db.Products.Where(p => (double)p.Price >= minPrice && (double)p.Price <= maxPrice).ToList();
            return View(list);
        }
        public ActionResult Index(string category, int? Page)
        {
            int PageSize = 4;
            int PageNum = (Page ?? 1);
            if (category == null)
            {
                var productlist = db.Products.OrderByDescending(x => x.ProName);
                return View(productlist.ToPagedList(PageNum, PageSize));
            }
            else
            {
                var productList = db.Products.OrderByDescending(x => x.ProName)
                    .Where(p => p.Category.NameCate == category);
                return View(productList);
            }
        }
        public ActionResult Home()
        {
            var Pro = (from pro in db.Products
                          join category in db.Categories on pro.CatID equals category.CatID
                          group pro by category into groupedProducts
                          select new
                          {
                              Category = groupedProducts.Key,
                              Products = groupedProducts.Take(4)
                          }).Take(4).ToList();

            var listproducts = Pro.SelectMany(x => x.Products);

            return View(listproducts.ToList());
        }
    public ActionResult DetailPro(int? id, int? Page)
        {
            int PageSize = 8;
            int PageNum = (Page ?? 1);

            var ProOfCate = (from pro in db.Products
                             where pro.CatID == id
                             select pro).ToList();

            var pagedProByCate = ProOfCate.ToPagedList(PageNum, PageSize);

            return View(pagedProByCate);
        }
        // GET: Memories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "ColorName");
            ViewBag.IDBatery = new SelectList(db.Batteries, "BatID", "NameBat");
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "NameCate");
            ViewBag.IDFCam = new SelectList(db.FrontCameras, "IDFCam", "Resolution");
            ViewBag.IDRCam = new SelectList(db.RearCameras, "IDRCam", "Resolution");
            ViewBag.IDScreen = new SelectList(db.Screens, "IDScr", "NameScr");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProID,ProName,CatID,ProImage,Price,DiscountPercent,CreatedDate,IDRCam,IDFCam,IDScreen,IDBatery")] Product product, int[] Colors)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Colors = new SelectList(db.Colors, "ColorID", "ColorName",Colors);
            ViewBag.IDBatery = new SelectList(db.Batteries, "BatID", "NameBat", product.IDBatery);
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "NameCate", product.CatID);
            ViewBag.IDFCam = new SelectList(db.FrontCameras, "IDFCam", "Resolution", product.IDFCam);
            ViewBag.IDRCam = new SelectList(db.RearCameras, "IDRCam", "Resolution", product.IDRCam);
            ViewBag.IDScreen = new SelectList(db.Screens, "IDScr", "NameScr", product.IDScreen);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDBatery = new SelectList(db.Batteries, "BatID", "NameBat", product.IDBatery);
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "NameCate", product.CatID);
            ViewBag.IDFCam = new SelectList(db.FrontCameras, "IDFCam", "Resolution", product.IDFCam);
            ViewBag.IDRCam = new SelectList(db.RearCameras, "IDRCam", "Resolution", product.IDRCam);
            ViewBag.IDScreen = new SelectList(db.Screens, "IDScr", "NameScr", product.IDScreen);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProID,ProName,CatID,ProImage,Price,DiscountPercent,CreatedDate,IDRCam,IDFCam,IDScreen,IDBatery")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDBatery = new SelectList(db.Batteries, "BatID", "NameBat", product.IDBatery);
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "NameCate", product.CatID);
            ViewBag.IDFCam = new SelectList(db.FrontCameras, "IDFCam", "Resolution", product.IDFCam);
            ViewBag.IDRCam = new SelectList(db.RearCameras, "IDRCam", "Resolution", product.IDRCam);
            ViewBag.IDScreen = new SelectList(db.Screens, "IDScr", "NameScr", product.IDScreen);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
