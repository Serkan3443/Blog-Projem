using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog_Sitem.Areas.AdminPanel.Controllers
{
    public class SayfaBulunamadiController : Controller
    {
        
        public ActionResult Page404Index()
        {
            return View();
        }
    }
}