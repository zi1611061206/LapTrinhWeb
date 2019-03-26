using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZiWatchVer3.Models;

namespace ZiWatchVer3.Controllers
{
    public class ContactController : Controller
    {
        dbQLDHDataContext data = new dbQLDHDataContext();

        // GET: Contact
        public ActionResult ContactInfo()
        {
            var contact = from cont in data.CONTACTs select cont;
            return View(contact.Single());
        }
    }
}