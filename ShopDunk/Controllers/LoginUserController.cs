using ShopDunk.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShopDunk.Controllers
{
    public class LoginUserController : Controller
    {
        // GET: LoginUser
        DBShopDunkEntities1 db = new DBShopDunkEntities1();

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAccount(AdminUser _user)
        {
            var check = db.AdminUsers
                            .Where(s => s.Username == _user.Username && s.Password == _user.Password)
                            .FirstOrDefault();

            if (check == null)
            {
                ViewBag.ErrorInfo = "SaiInfo";
                return View("Index");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["Username"] = _user.Username;
                return RedirectToAction("Index", "Products");
            }
        }
        [HttpPost]
        public ActionResult LoginAccountCus(Customer _cus)
        {
            var check = db.Customers.Where(s => s.CusPhone == _cus.CusPhone && s.CusPassword == _cus.PasswordConfirm).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo = "SaiInfo";
                return View("LoginCus");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["CusPhone"] = _cus.CusPhone;
                return RedirectToAction("Home", "Products");
            }
        }

        public ActionResult UserProfile()
        {
            if (Session["CusPhone"] != null)
            {
                string cusPhone = Session["CusPhone"].ToString();
                var user = db.Customers.FirstOrDefault(c => c.CusPhone == cusPhone);

                if (user != null)
                {
                    return View(user);
                }
            }

            return RedirectToAction("LoginCus");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profile(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var Customer = db.Customers.FirstOrDefault(c => c.CusPhone == customer.CusPhone);
                if (Customer != null)
                {
                    Customer.CusName = customer.CusName;
                    Customer.CusEmail = customer.CusEmail;
                    Customer.Address = customer.Address;
                    Customer.Gender = customer.Gender;
                    Customer.DateOfBirth = customer.DateOfBirth;

                    db.Entry(Customer).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Profile", "LoginUser");
                }
            }

            return View(customer);
        }


        public ActionResult LoginCus()
        {
            return View();
        }
        public ActionResult RegisterCus()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterCus(Customer _user)
        {
            if (ModelState.IsValid)
            {

                var check_Username = db.Customers.Where(s => s.CusPassword == _user.CusPassword).FirstOrDefault();
                if (check_Username == null)
                {
                    DateTime currentDate = DateTime.Now;
                    if (_user.DateOfBirth.HasValue)
                    {
                        int age = currentDate.Year - _user.DateOfBirth.Value.Year;

                        if (_user.DateOfBirth > currentDate)
                        {
                            ViewBag.ErrorRegister = "Ngày sinh không phù hợp, Ngày sinh của bạn vượt quá năm hiện tại!";
                            return View();
                        }
                        if ((_user.DateOfBirth > currentDate.AddYears(-age)))
                        {
                            age--;
                        }

                        if (age < 16)
                        {
                            ViewBag.ErrorRegister = "Bạn chưa đủ 16 tuổi để đăng ký!";
                            return View();
                        }
                    }
                    else
                    {
                        // Xử lý trường hợp ngày sinh là null
                        ViewBag.ErrorRegister = "Ngày sinh không được trống";
                        return View();
                    }
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Customers.Add(_user);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ViewBag.ErrorRegister = "Người dùng đã tồn tại";
                    return View();
                }
            }
            ViewBag.ErrorRegister = "Đăng ký không thành công. Vui lòng kiểm tra lại thông tin đăng ký.";
            return View();
        }
        public ActionResult LogOutUser()
        {
            Session.Abandon();
            return RedirectToAction("Home", "Products");
        }
    }
}