using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZiWatchVer3.Models;
using PagedList;
using PagedList.Mvc;

namespace ZiWatchVer3.Controllers
{
    public class HomePageController : Controller
    {
        dbQLDHDataContext data = new dbQLDHDataContext();

        // GET: HomePage
        public ActionResult Index(int ? page)
        {
            int pageSize = 4;
            int pageNum = (page ?? 1);
            var newProduct = data.SANPHAMs.OrderByDescending(a => a.NGAYCAPNHAT).Take(8).ToList();
            return View(newProduct.ToPagedList(pageNum,pageSize));
        }

        public ActionResult Slider()
        {
            var slider = from slide in data.SLIDERs select slide;
            return PartialView(slider);
        }
    }
}