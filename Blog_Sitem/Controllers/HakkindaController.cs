using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog_Sitem.Controllers
{
    public class HakkindaController : BaseController
    {
        // GET: Hakkinda
        public ActionResult HakkindaIndex()
        {
            return View();
        }
    }
}