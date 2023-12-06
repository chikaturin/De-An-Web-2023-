using ShopDunk.Models;
using ShopDunk.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShopDunk.Controllers
{
    public class DetailProductVMController : Controller
    {
        private DBShopDunkEntities1 db = new DBShopDunkEntities1();
        // GET: DetailProductVM
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DetailProduct(int? id)
        {
            if (id != null)
            {
                ProDetail pro = db.ProDetails.FirstOrDefault(p => p.ProID == id);

                if (pro != null)
                {
                    var productDetails = (from ProDetail in db.ProDetails
                                          join Color in db.Colors on ProDetail.ColorID equals Color.ColorID
                                          join Memory in db.Memories on ProDetail.IDMemory equals Memory.IDMem
                                          group new { Color, Memory } by new { ProDetail.ColorID, ProDetail.IDMemory } into grouped
                                          select new DetailProductVMItem
                                          {
                                              _color = grouped.Select(g => g.Color).FirstOrDefault(),
                                              _mem = grouped.Select(g => g.Memory).FirstOrDefault(),
                                          }).Take(4).ToList();

                    DetailProductVM detailProductVM = new DetailProductVM
                    {
                        //ProDetail
                        ProDeID = pro.ProDeID,
                        RemainQuantity = pro.RemainQuantity,
                        SoldQuantity = pro.SoldQuantity,
                        ViewQuantity = pro.ViewQuantity,
                        ConnectNetwork = pro.ConnectNetwork,

                        //Product
                        ProID = pro.ProID,
                        ProName = pro.Product.ProName,
                        ProImage = pro.Product.ProImage,
                        Price = pro.Product.Price,
                        PriceReduce = pro.Product.PriceReduce,

                        //Category
                        CatID = pro.Product.CatID,
                        NameCate = pro.Product.Category.NameCate,

                        //Camera
                        IDFCam = pro.Product.FrontCamera.IDFCam,
                        ResolutionF = pro.Product.FrontCamera.Resolution,

                        IDRCam = pro.Product.RearCamera.IDRCam,
                        ResolutionR = pro.Product.RearCamera.Resolution,
                        //Memory
                        NameMem=pro.Memory.NameMem,
                        //Screen
                        IDScr = pro.Product.Screen.IDScr,
                        NameScr = pro.Product.Screen.NameScr,
                        Resolution = pro.Product.Screen.Resolution,

                        //Battery
                        BatID = pro.Product.Battery.BatID,
                        Storage = pro.Product.Battery.Storage,
                        NameBat = pro.Product.Battery.NameBat,
                        _vms = productDetails,
                        RelatePro = RelatePro()
                    };

                    return View(detailProductVM);
                }
                else
                {
                    return Content("Product not found");
                }
            }

            return Content("Missing ID");
        }
        public List<DetailProductVM> RelatePro(int? ProID = 5)
        {
            if (ProID != null)
            {
                List<Product> pro = db.Products.Where(c=>c.Category.CatID==ProID).ToList();
                List<DetailProductVM> prolistitem = pro.Select(
                    pros => new DetailProductVM
                    {
                        ProID = pros.ProID,
                        ProName = pros.ProName,
                        ProImage = pros.ProImage,
                        Price = pros.Price,
                        PriceReduce = pros.PriceReduce,

                    }).ToList();
                return prolistitem;
            }
            return null;
        }
        public ActionResult GetRelatedProductByProduct(int? proID = 5)
        {
            List<DetailProductVM> commentList = RelatePro(proID);
            return View(commentList);
        }
    }
}