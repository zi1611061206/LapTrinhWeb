using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZiWatchVer3.Models;

namespace ZiWatchVer3.Controllers
{
    public class ProductController : Controller
    {
        dbQLDHDataContext data = new dbQLDHDataContext();

        // GET: Product
        public ActionResult AllView()
        {
            var product = data.SANPHAMs.OrderByDescending(a => a.NGAYCAPNHAT).ToList();
            return View(product);
        }

        public ActionResult Category()
        {
            var categoryList = from cate in data.DANHMUCs select cate;
            return PartialView(categoryList);
        }

        public ActionResult ForCategory(int id)
        {
            var product = from p in data.SANPHAMs
                          where p.MADANHMUC == id
                          select p;
            return View(product);
        }

        public ActionResult Supplier()
        {
            var supplierList = from supp in data.NHASANXUATs select supp;
            return PartialView(supplierList);
        }

        public ActionResult ForSupplier(int id)
        {
            var supplier = from s in data.SANPHAMs
                          where s.MANHASANXUAT == id
                          select s;
            return View(supplier);
        }

        public ActionResult Color()
        {
            var colorList = from color in data.MAUSACs select color;
            return PartialView(colorList);
        }

        public ActionResult ForColor(int id)
        {
            var color = from c in data.SANPHAMs
                          where c.MAMAU == id
                          select c;
            return View(color);
        }

        public ActionResult Details(int id)
        {
            var detail = from d in data.SANPHAMs
                        where d.MASANPHAM == id
                        select d;
            return View(detail.Single());
        }
    }
}