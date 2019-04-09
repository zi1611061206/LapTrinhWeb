using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZiWatchVer3.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace ZiWatchVer3.Controllers
{
    public class AdminController : Controller
    {
        dbQLDHDataContext data = new dbQLDHDataContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        
        //Product
        public ActionResult Product(int? page)
        {
            int pageSize = 5;
            int pageNum = (page ?? 1);
            return View(data.SANPHAMs.ToList().OrderBy(n => n.MASANPHAM).ToPagedList(pageNum, pageSize));
        }

        [HttpGet]
        public ActionResult CreateProduct()
        {
            ViewBag.MaDanhMuc = new SelectList(data.DANHMUCs.ToList().OrderBy(n => n.TENDANHMUC), "MaDanhMuc", "TenDanhMuc");
            ViewBag.MaNhaSanXuat = new SelectList(data.NHASANXUATs.ToList().OrderBy(n => n.TENNHASANXUAT), "MaNhaSanXuat", "TenNhaSanXuat");
            ViewBag.MaMau = new SelectList(data.MAUSACs.ToList().OrderBy(n => n.TENMAU), "MaMau", "TenMau");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateProduct(SANPHAM sp, HttpPostedFileBase fileUpload)
        {
            ViewBag.MaDanhMuc = new SelectList(data.DANHMUCs.ToList().OrderBy(n => n.TENDANHMUC), "MaDanhMuc", "TenDanhMuc");
            ViewBag.MaNhaSanXuat = new SelectList(data.NHASANXUATs.ToList().OrderBy(n => n.TENNHASANXUAT), "MaNhaSanXuat", "TenNhaSanXuat");
            ViewBag.MaMau = new SelectList(data.MAUSACs.ToList().OrderBy(n => n.TENMAU), "MaMau", "TenMau");
            if (fileUpload == null)
            {
                ViewBag.Message = "Vui lòng chọn hình ảnh sản phẩm";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/images"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Message = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileUpload.SaveAs(path);
                    }
                    sp.HINHANH = fileName;
                    data.SANPHAMs.InsertOnSubmit(sp);
                    data.SubmitChanges();
                }
                return RedirectToAction("Product");
            }

        }

        //Category
        //Supplier
        //Color
        //Adminstrator
        //Customer
        //Order
        //OrderDetail
        //Slider
        //Contact

        //Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            var username = form["username"];
            var password = form["password"];
            QUANTRIVIEN ad = data.QUANTRIVIENs.SingleOrDefault(n => n.TENDANGNHAP == username && n.MATKHAU == password);
            if (ad != null)
            {
                Session["TaiKhoanQuanTri"] = ad;
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.Message = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
    }
}