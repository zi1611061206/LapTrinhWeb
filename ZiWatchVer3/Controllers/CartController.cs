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
            if(lstCart==null)
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
            if(product==null)
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
            if(lstCart!=null)
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
            if(lstCart.Count==0)
            {
                return RedirectToAction("Index", "HomePage");
            }
            ViewBag.Num = GetNum();
            ViewBag.Price = GetTotalPrice();
            return View();
        }
    }
}