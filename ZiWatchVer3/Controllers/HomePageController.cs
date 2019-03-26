using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZiWatchVer3.Models;

namespace ZiWatchVer3.Controllers
{
    public class HomePageController : Controller
    {
        dbQLDHDataContext data = new dbQLDHDataContext();

        // GET: HomePage
        public ActionResult Index()
        {
            var newProduct = data.SANPHAMs.OrderByDescending(a => a.NGAYCAPNHAT).Take(7).ToList();
            return View(newProduct);
        }

        public ActionResult Category()
        {
            var categoryList = from cate in data.DANHMUCs select cate;
            return PartialView(categoryList);
        }

        public ActionResult Slider()
        {
            var slider = from slide in data.SLIDERs select slide;
            return PartialView(slider);
        }

        public ActionResult Modal(int id)
        {
            var modal = from md in data.SANPHAMs
                        where md.MASANPHAM == id
                        select md;
            return PartialView(modal.Single());
        }
    }
}