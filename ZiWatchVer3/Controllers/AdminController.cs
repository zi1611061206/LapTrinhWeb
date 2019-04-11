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
            if(Session["TaiKhoanAdmin"]!=null)
            {
                return View();
            }
            return RedirectToAction("Login", "Manager");
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
                    sp.HINHANH1 = fileName;
                    sp.HINHANH2 = fileName;
                    data.SANPHAMs.InsertOnSubmit(sp);
                    data.SubmitChanges();
                }
                return RedirectToAction("Product");
            }

        }

        public ActionResult DetailsProduct(int id)
        {
            SANPHAM sp = data.SANPHAMs.SingleOrDefault(n => n.MASANPHAM == id);
            ViewBag.MaSanPham = sp.MASANPHAM;
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }

        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            SANPHAM sp = data.SANPHAMs.SingleOrDefault(n => n.MASANPHAM == id);
            ViewBag.MaSanPham = sp.MASANPHAM;
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }

        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            SANPHAM sp = data.SANPHAMs.SingleOrDefault(n => n.MASANPHAM == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaDanhMuc = new SelectList(data.DANHMUCs.ToList().OrderBy(n => n.TENDANHMUC), "MaDanhMuc", "TenDanhMuc", sp.MADANHMUC);
            ViewBag.MaNhaSanXuat = new SelectList(data.NHASANXUATs.ToList().OrderBy(n => n.TENNHASANXUAT), "MaNhaSanXuat", "TenNhaSanXuat", sp.MANHASANXUAT);
            ViewBag.MaMau = new SelectList(data.MAUSACs.ToList().OrderBy(n => n.TENMAU), "MaMau", "TenMau", sp.MAMAU);
            return View(sp);
        }

        [HttpPost, ActionName("DeleteProduct")]
        public ActionResult DeleteConfirm(int id)
        {
            SANPHAM sp = data.SANPHAMs.SingleOrDefault(n => n.MASANPHAM == id);
            ViewBag.MaSanPham = sp.MASANPHAM;
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.SANPHAMs.DeleteOnSubmit(sp);
            data.SubmitChanges();
            return RedirectToAction("Product");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditProduct(SANPHAM sp, HttpPostedFileBase fileUpload)
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
                    UpdateModel(sp);
                    data.SubmitChanges();
                }
                return RedirectToAction("Product");
            }

        }

        //Category
        public ActionResult Category(int? page)
        {
            int pageSize = 5;
            int pageNum = (page ?? 1);
            return View(data.DANHMUCs.ToList().OrderBy(n => n.MADANHMUC).ToPagedList(pageNum, pageSize));
        }

        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateCategory(DANHMUC dm)
        {
            if (ModelState.IsValid)
            {
                data.DANHMUCs.InsertOnSubmit(dm);
                data.SubmitChanges();
            }
            return RedirectToAction("Category");
        }

        //Supplier
        public ActionResult Supplier(int? page)
        {
            int pageSize = 5;
            int pageNum = (page ?? 1);
            return View(data.NHASANXUATs.ToList().OrderBy(n => n.MANHASANXUAT).ToPagedList(pageNum, pageSize));
        }

        [HttpGet]
        public ActionResult CreateSupplier()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateSupplier(NHASANXUAT nsx)
        {
            if (ModelState.IsValid)
            {
                data.NHASANXUATs.InsertOnSubmit(nsx);
                data.SubmitChanges();
            }
            return RedirectToAction("Supplier");
        }

        //Color
        public ActionResult Color(int? page)
        {
            int pageSize = 5;
            int pageNum = (page ?? 1);
            return View(data.MAUSACs.ToList().OrderBy(n => n.MAMAU).ToPagedList(pageNum, pageSize));
        }

        [HttpGet]
        public ActionResult CreateColor()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateColor(MAUSAC ms)
        {
            if (ModelState.IsValid)
            {
                data.MAUSACs.InsertOnSubmit(ms);
                data.SubmitChanges();
            }
            return RedirectToAction("Color");
        }

        //Adminstrator
        public ActionResult Adminstrator(int? page)
        {
            int pageSize = 5;
            int pageNum = (page ?? 1);
            return View(data.QUANTRIVIENs.ToList().OrderBy(n => n.MAQUANTRI).ToPagedList(pageNum, pageSize));
        }

        [HttpGet]
        public ActionResult CreateAdminstrator()
        {
            return View();
        }

        //Customer
        public ActionResult Customer(int? page)
        {
            int pageSize = 5;
            int pageNum = (page ?? 1);
            return View(data.KHACHHANGs.ToList().OrderBy(n => n.MAKHACHHANG).ToPagedList(pageNum, pageSize));
        }

        [HttpGet]
        public ActionResult CreateCustomer()
        {
            ViewBag.GioiTinh = new SelectList(new[] { new { ID = "0", Name = "Nam" }, new { ID = "1", Name = "Nữ" } }, "ID", "Name", 1);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateCustomer(KHACHHANG kh)
        {
            if (ModelState.IsValid)
            {
                data.KHACHHANGs.InsertOnSubmit(kh);
                data.SubmitChanges();
            }
            return RedirectToAction("Customer");
        }

        //Order
        //OrderDetail
        //Slider
        public ActionResult Slider(int? page)
        {
            int pageSize = 5;
            int pageNum = (page ?? 1);
            return View(data.SLIDERs.ToList().OrderBy(n => n.MA).ToPagedList(pageNum, pageSize));
        }

        [HttpGet]
        public ActionResult CreateSlider()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateSlider(SLIDER sl, HttpPostedFileBase fileUpload)
        {
            if (fileUpload == null)
            {
                ViewBag.Message = "Vui lòng chọn hình nền";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/images"), fileName);
                    fileUpload.SaveAs(path);
                    sl.HINHANH = fileName;
                    data.SLIDERs.InsertOnSubmit(sl);
                    data.SubmitChanges();
                }
                return RedirectToAction("Slider");
            }

        }

        //Contact
        public ActionResult Contact(int? page)
        {
            int pageSize = 5;
            int pageNum = (page ?? 1);
            return View(data.CONTACTs.ToList().OrderBy(n => n.MA).ToPagedList(pageNum, pageSize));
        }

        [HttpGet]
        public ActionResult CreateContact()
        {
            return View();
        }

    }
}