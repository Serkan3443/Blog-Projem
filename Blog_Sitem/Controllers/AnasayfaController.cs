using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogSitem.BLL.FakeDataManager;
using BlogSitem.BLL.Manager;
using BlogSitem.DLL.BlogSiteDatabase;
using BlogSitem.DLL.BlogSiteDatabase.ORMManager;

namespace Blog_Sitem.Controllers
{
    public class AnasayfaController : BaseController
    {
        // GET: Anasayfa
        public ActionResult AnasayfaIndex()
        {
            //BlogSitemDB db = new BlogSitemDB();
            //MakaleManager makaleMng= new MakaleManager();
            //makaleMng.MakalelerListesi();

            //KullanicilarFakeData kullanicilarFakeData = new KullanicilarFakeData();
            //kullanicilarFakeData.EkleKullaniciFakeData();

            YazarlarFakeData yazarlarFakeData = new YazarlarFakeData();
            yazarlarFakeData.EkleYazarFakeData();

            MakalelerFakeData makalelerFakeData = new MakalelerFakeData();
            makalelerFakeData.EkleMakaleFakeData();
            return View();
        }
    }
}