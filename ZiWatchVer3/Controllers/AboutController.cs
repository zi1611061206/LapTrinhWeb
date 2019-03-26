using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZiWatchVer3.Models;

namespace ZiWatchVer3.Controllers
{
    public class AboutController : Controller
    {
        dbQLDHDataContext data = new dbQLDHDataContext();

        // GET: About
        public ActionResult AboutUS()
        {
            var about = from aboutList in data.QUANTRIVIENs select aboutList;
            return View(about);
        }
    }
}