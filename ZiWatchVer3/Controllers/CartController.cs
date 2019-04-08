using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ZiWatchVer3.Models;

namespace ZiWatchVer3.Controllers
{
    public class CartController : Controller
    {
        dbQLDHDataContext data = new dbQLDHDataContext();

        // GET: Cart
        public List<TheOrder> GetCart()
        {
            List<TheOrder> lstCart = Session["TheOrder"] as List<TheOrder>;
            if (lstCart == null)
            {
                lstCart = new List<TheOrder>();
                Session["TheOrder"] = lstCart;
            }
            return lstCart;
        }

        //ADD: Cart
        public ActionResult AddCart(int maSanPham, string strURL)
        {
            List<TheOrder> lstCart = GetCart();
            TheOrder product = lstCart.Find(n => n.MaSanPham == maSanPham);
            if (product == null)
            {
                product = new TheOrder(maSanPham);
                lstCart.Add(product);
                return Redirect(strURL);
            }
            else
            {
                product.SoLuong++;
                return Redirect(strURL);
            }
        }

        //NUM
        private int GetNum()
        {
            int numTotal = 0;
            List<TheOrder> lstCart = Session["TheOrder"] as List<TheOrder>;
            if (lstCart != null)
            {
                numTotal = lstCart.Sum(n => n.SoLuong);
            }
            return numTotal;
        }

        //TOTAL PRICE
        private double GetTotalPrice()
        {
            double totalPrice = 0;
            List<TheOrder> lstCart = Session["TheOrder"] as List<TheOrder>;
            if (lstCart != null)
            {
                totalPrice = lstCart.Sum(n => n.ThanhTien);
            }
            return totalPrice;
        }

        //VIEW
        public ActionResult ShoppingCart()
        {
            List<TheOrder> lstCart = GetCart();
            if (lstCart.Count == 0)
            {
                return RedirectToAction("Index", "HomePage");
            }
            ViewBag.Num = GetNum();
            ViewBag.Price = GetTotalPrice();
            return View(lstCart);
        }

        //Cart display icon
        public ActionResult CartPartial()
        {
            ViewBag.Num = GetNum();
            ViewBag.Price = GetTotalPrice();
            return PartialView();
        }

        //Delete cart
        public ActionResult DeleteCart(int maSanPham)
        {
            List<TheOrder> lstCart = GetCart();
            TheOrder product = lstCart.SingleOrDefault(n => n.MaSanPham == maSanPham);
            if (product != null)
            {
                lstCart.RemoveAll(n => n.MaSanPham == maSanPham);
                return RedirectToAction("ShoppingCart");
            }
            if (lstCart.Count == 0)
            {
                return RedirectToAction("Index", "HomePage");
            }
            return RedirectToAction("ShoppingCart");
        }

        //Update cart
        public ActionResult UpdateCart(int maSanPham, FormCollection f)
        {
            List<TheOrder> lstCart = GetCart();
            TheOrder product = lstCart.SingleOrDefault(n => n.MaSanPham == maSanPham);
            if (product != null)
            {
                product.SoLuong = int.Parse(f["txbAmount"].ToString());
            }
            return RedirectToAction("ShoppingCart");
        }

        //Clear Cart
        public ActionResult ClearCart()
        {
            List<TheOrder> lstCart = GetCart();
            lstCart.Clear();
            return RedirectToAction("Index", "HomePage");
        }

        //Order
        [HttpGet]
        public ActionResult Order()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "User");
            }
            if (Session["TheOrder"] == null)
            {
                return RedirectToAction("Index", "HomePage");
            }
            List<TheOrder> lstCart = GetCart();
            ViewBag.Num = GetNum();
            ViewBag.Price = GetTotalPrice();
            return View(lstCart);
        }

        [HttpPost]
        public ActionResult Order(FormCollection f)
        {
            DONHANG dh = new DONHANG();
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
            List<TheOrder> lstCart = GetCart();
            dh.MAKHACHHANG = kh.MAKHACHHANG;
            dh.DIACHI = kh.DIACHI;
            dh.SODIENTHOAI = kh.SODIENTHOAI;
            dh.THOIGIAN = DateTime.Now;
            dh.TONG = GetTotalPrice();
            dh.TRANGTHAI = false;
            data.DONHANGs.InsertOnSubmit(dh);
            data.SubmitChanges();
            foreach (var item in lstCart)
            {
                CHITIETDONHANG ctdh = new CHITIETDONHANG();
                ctdh.MADONHANG = dh.MADONHANG;
                ctdh.MASANPHAM = item.MaSanPham;
                ctdh.SOLUONG = item.SoLuong;
                ctdh.THANHTIEN = item.ThanhTien;
                data.CHITIETDONHANGs.InsertOnSubmit(ctdh);
            }
            data.SubmitChanges();
            Session["TheOrder"] = null;
            return RedirectToAction("Confirm", "Cart");
        }

        //Confirm
        public ActionResult Confirm()
        {
            return View();
        }
    }
}