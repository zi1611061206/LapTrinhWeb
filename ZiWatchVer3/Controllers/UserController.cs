using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZiWatchVer3.Models;

namespace ZiWatchVer3.Controllers
{
    public class UserController : Controller
    {
        dbQLDHDataContext data = new dbQLDHDataContext();
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection form, KHACHHANG khachhang)
        {
            var hoTen = form["FullName"];
            var email = form["Email"];
            var matKhau = form["Password"];
            var nhapLaiMatKhau = form["ReEnterPassword"];
            var gioiTinh = form["Sex"];
            var diaChi = form["Address"];
            var soDienThoai = form["PhoneNumber"];
            var mailList = from m in data.KHACHHANGs select m.EMAIL;


            if (String.IsNullOrEmpty(hoTen))
            {
                ViewData["Loi1"] = "Họ tên khách hàng không được để trống";
            }
            if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi2"] = "Cần nhập Email để đăng nhập";
            }
            foreach (var item in mailList)
            {
                if (email == item)
                {
                    ViewData["Loi2.1"] = "Email đã tồn tại";
                }
            }
            if (String.IsNullOrEmpty(matKhau))
            {
                ViewData["Loi3"] = "Mật khẩu không được để trống";
            }
            if (String.IsNullOrEmpty(nhapLaiMatKhau))
            {
                ViewData["Loi4"] = "Mật khẩu không được để trống";
            }
            if (nhapLaiMatKhau != matKhau)
            {
                ViewData["Loi4.1"] = "Mật khẩu không trùng khớp";
            }
            if (gioiTinh != "Nam" || gioiTinh != "Nu")
            {
                ViewData["Loi5"] = "Vui lòng nhập chính xác Nam hoặc Nu";
            }
            if (String.IsNullOrEmpty(diaChi))
            {
                ViewData["Loi6"] = "Địa chỉ khách hàng không được để trống";
            }
            if (String.IsNullOrEmpty(soDienThoai))
            {
                ViewData["Loi7"] = "Số điện thoại khách hàng không được để trống";
            }

            else
            {
                bool sex = false;
                if (gioiTinh == "Nu")
                    sex = true;
                khachhang.HOTEN = hoTen;
                khachhang.EMAIL = email;
                khachhang.MATKHAU = matKhau;
                khachhang.GIOITINH = sex;
                khachhang.DIACHI = diaChi;
                khachhang.SODIENTHOAI = soDienThoai;
                data.KHACHHANGs.InsertOnSubmit(khachhang);
                data.SubmitChanges();
                return RedirectToAction("DangNhap");
            }
            return this.Register();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            var email = form["Email"];
            var matKhau = form["Password"];

            if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi1"] = "Cần nhập Email để đăng nhập";
            }
            else if (String.IsNullOrEmpty(matKhau))
            {
                ViewData["Loi2"] = "Mật khẩu không đúng";
            }
            else
            {
                KHACHHANG khachhang = data.KHACHHANGs.SingleOrDefault(n => n.EMAIL == email && n.MATKHAU == matKhau);
                if (khachhang != null)
                {
                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["TaiKhoan"] = khachhang;
                }
                else
                {
                    ViewBag.Thongbao = "Email hoặc mật khẩu không đúng";
                }
            }
            return View();
        }
    }
}