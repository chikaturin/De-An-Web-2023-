using ShopDunk.Models.ViewModel;
using ShopDunk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopDunk.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        DBShopDunkEntities1 database = new DBShopDunkEntities1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowCart()
        {
            if (Session["Cart"] == null)
                return View("EmptyCart");
            Cart _cart = Session["Cart"] as Cart;
            return View(_cart);
        }
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddToCart(int id)
        {
            var _pro = database.Products.SingleOrDefault(s => s.ProID == id);
            if (_pro != null)
                GetCart().Add_Product_Cart(_pro);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public ActionResult Update_Cart_Quantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(form["idPro"]);
            int _quantity = int.Parse(form["cartQuantity"]);
            cart.Update_quantity(id_pro, _quantity);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public PartialViewResult BagCart()
        {
            int total_quantity_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
                total_quantity_item = cart.Total_quantity();
            ViewBag.QuantityCart = total_quantity_item;
            return PartialView("BagCart");
        }
        public ActionResult InputVoucher(string search = "")
        {
            Cart cart = Session["Cart"] as Cart;

            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            decimal total = cart.Total_money();
            Voucher vouchers = database.Vouchers.FirstOrDefault(row => row.NameVoucher == search);
            DateTime date = DateTime.Now;
            Voucher voucher = Session["Voucher"] as Voucher;
            if (vouchers != null && vouchers.Expired_time > date)
            {
                decimal discountAmount = total * (decimal)vouchers.DiscountLevel / 100;
                Session["Voucher"] = discountAmount;
            }
            else
            {
                ViewBag.DiscountAmount = 0;
                Session["Voucher"] = 0;
            }
            ViewBag.Search = search;
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public Voucher GetVoucherInfo(int voucherId)
        {
            return database.Vouchers.FirstOrDefault(v => v.IDVoucher == voucherId);
        }
        public ActionResult CheckOut()
        {
            if (Session["CusPhone"] != null)
            {
                Cart cart = Session["Cart"] as Cart; 
                string cus = Session["CusPhone"].ToString();
                var cus1 = database.Customers.Where(c => c.CusPhone == cus).Select(c=>c.Address).FirstOrDefault();
                if (Session["Cart"] != null || cart != null)
                {

                    Order _order = new Order();
                    _order.OrderDate = DateTime.Now;
                    _order.CusPhone = Session["CusPhone"].ToString();
                    if (Session["Voucher"]!=null)
                    {
                        decimal voucher = Convert.ToDecimal(Session["Voucher"]);
                        var price = cart.Total_money() - voucher;
                        _order.TotalValue = (double)price;
                    }
                    
                    if (cus != null)
                    {
                        _order.AddressDeliverry = cus1;
                    }
                    database.Orders.Add(_order);
                    database.SaveChanges();

                    foreach (var item in cart.Items)
                    {
                        if (cart != null)
                        {
                            ProDetail prodetails = database.ProDetails.FirstOrDefault(s => s.ProID == item._product.ProID);

                            if (prodetails.RemainQuantity <= 0 || item._quantity > prodetails.RemainQuantity)
                            {
                                return Content("Sản phẩm " + item._product.ProName + " đã hết hàng hoặc không đủ số lượng");
                            }
                            OrderDetail _order_detail = new OrderDetail();
                            _order_detail.OrderID = _order.OrderID;
                            _order_detail.ProSupID = prodetails.ProDeID;
                            _order_detail.UnitPrice = item._product.PriceReduce;
                            database.OrderDetails.Add(_order_detail);
                            foreach (var p in database.ProDetails.Where(s => s.ProDeID == _order_detail.ProSupID))
                            {
                                _order_detail.Quantity = item._quantity;
                                var update_quan_pro = p.SoldQuantity + item._quantity;
                                p.SoldQuantity = update_quan_pro;
                                var update_quan = p.RemainQuantity - item._quantity;
                                p.RemainQuantity = update_quan;
                                var view_quan = p.ViewQuantity + 1;
                                p.ViewQuantity = view_quan;
                            }
                        }
                    }
                    database.SaveChanges();
                    cart.ClearCart();
                    return RedirectToAction("CheckOut_Success", new { id = _order.OrderID });
                }
                else
                {
                    return View("Empty", "ShoppingCart");
                }
            }
            else
            {
                return RedirectToAction("LoginCus", "LoginUser");
            }
        }
        public ActionResult CheckOut_Success(int? id)
        {
            if (id != null)
            {
                if (Session["Cart"] != null)
                {
                    Voucher voucher = Session["Voucher"] as Voucher;
                    Order orders = database.Orders.FirstOrDefault(p => p.OrderID == id);
                    if (orders != null)
                    {
                        Cart cart = new Cart();
                        var products = (from orderDetail in database.OrderDetails
                                        join proDetail in database.ProDetails on orderDetail.ProSupID equals proDetail.ProDeID
                                        join product in database.Products on proDetail.ProID equals product.ProID
                                        where orderDetail.OrderID == id
                                        select new CartItem
                                        {
                                            _product = product,
                                            _color = proDetail.Color,
                                            _quantity = (int)orderDetail.Quantity,
                                        }).ToList();
                        Cart cartvm = new Cart
                        {
                            OrderID = orders.OrderID,
                            OrderDate = orders.OrderDate,
                            CusName = orders.Customer.CusName,
                            CusEmail = orders.Customer.CusEmail,
                            CusPhone = orders.Customer.CusPhone,
                            items = products,
                        };

                        ViewBag.OrderID = orders.OrderID;

                        return View(cartvm);
                    }
                    else
                    {
                        return Content("Đơn hàng không tồn tại");
                    }
                }
            }
            return Content("Missing ID");
        }
        public ActionResult Purchases()
        {
            if (Session["CusPhone"] != null)
            {
                string phone = Session["CusPhone"].ToString();
                Order orders = database.Orders.FirstOrDefault(p => p.CusPhone == phone);
                if (orders != null)
                {
                    Cart cart = new Cart();
                    var products = (from orderDetail in database.OrderDetails
                                    join proDetail in database.ProDetails on orderDetail.ProSupID equals proDetail.ProDeID
                                    join Order in database.Orders on orderDetail.OrderID equals Order.OrderID
                                    join product in database.Products on proDetail.ProID equals product.ProID
                                    where Order.CusPhone == phone
                                    select new CartItem
                                    {
                                       
                                        _order=Order,
                                        _product = product,
                                        _quantity = (int)orderDetail.Quantity,
                                    }).ToList();
                    var price = cart.Total_money();
                    Cart cartvm = new Cart
                    {
                        CusName = orders.Customer.CusName,
                        CusEmail = orders.Customer.CusEmail,
                        CusPhone = orders.Customer.CusPhone,
                        items = products,
                    };

                    ViewBag.OrderID = orders.OrderID;

                    return View(cartvm);
                }
                else
                {
                    return Content("Đơn hàng không tồn tại");
                }
            }
            else
            {
                return Content("Error");
            }
        }
        public ActionResult purchasesoforder(string id = "")
        { 
            if (!string.IsNullOrEmpty(id))
            {
                Order order = database.Orders.FirstOrDefault(p => p.CusPhone == id);

                if (order != null)
                {
                    var products = (from orderDetail in database.OrderDetails
                                    join proDetail in database.ProDetails on orderDetail.ProSupID equals proDetail.ProDeID
                                    join ord in database.Orders on orderDetail.OrderID equals ord.OrderID
                                    join product in database.Products on proDetail.ProID equals product.ProID
                                    where ord.CusPhone == id
                                    select new CartItem
                                    {
                                        _order = ord,
                                        _product = product,
                                        _quantity = (int)orderDetail.Quantity,
                                    }).ToList();

                    Cart cartvm = new Cart
                    {
                        CusName = order.Customer.CusName,
                        CusEmail = order.Customer.CusEmail,
                        CusPhone = order.Customer.CusPhone,
                        items = products,
                    };

                    ViewBag.OrderID = order.OrderID;

                    return View(cartvm);
                }
                else
                {
                    return Content("Đơn hàng không tồn tại");
                }
            }

            return Content("Đơn hàng không tồn tại");
        }


    }
}