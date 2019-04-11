using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZiWatchVer3.Models;

namespace ZiWatchVer3.Controllers
{
    public class ManagerController : Controller
    {
        dbQLDHDataContext data = new dbQLDHDataContext();

        //Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            var username = form["Email"];
            var matKhau = form["Password"];

            if (String.IsNullOrEmpty(username))
            {
                ViewData["Loi1"] = "Cần nhập tên đăng nhập để đăng nhập";
            }
            else if (String.IsNullOrEmpty(matKhau))
            {
                ViewData["Loi2"] = "Mật khẩu không đúng";
            }
            else
            {
                QUANTRIVIEN qtv = data.QUANTRIVIENs.SingleOrDefault(n => n.TENDANGNHAP == username && n.MATKHAU == matKhau);
                if (qtv != null)
                {
                    Session["TaiKhoanAdmin"] = qtv;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }

        public ActionResult AccountAdmin()
        {
            return PartialView();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "HomePage");
        }

    }
}