using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog_Sitem.Controllers
{
    public class KategoriController : BaseController
    {
        // GET: Kategori
        public ActionResult KategoriIndex()
        {
            return View();
        }
    }
}