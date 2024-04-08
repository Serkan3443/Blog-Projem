using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogSitem.DLL;
using BlogSitem.BLL;
using BlogSitem.BLL.Manager;

namespace Blog_Sitem.Areas.AdminPanel.Controllers
{
    public class MakaleGridMVCController : Controller
    {
        // GET: AdminPanel/MakaleGridMVC
        public ActionResult MakaleGridMVCIndex()
        {
            MakaleManager makale=new MakaleManager();
            var makalelistesi = makale.MakalelerListesi();
            return View(makalelistesi);
        }
    }
}