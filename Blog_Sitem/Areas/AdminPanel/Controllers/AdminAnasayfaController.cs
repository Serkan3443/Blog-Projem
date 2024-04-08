using BlogSitem.BLL.Repositories;
using BlogSitem.DLL.BlogSiteDatabase.ORMManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog_Sitem.Areas.AdminPanel.Controllers
{
    public class AdminAnasayfaController : Controller
    {
        BlogSitemDB _db;
        KullanicilarRepository _kullanicilarRepo;

        public AdminAnasayfaController()
        {
            _db = new BlogSitemDB();
            _kullanicilarRepo = new KullanicilarRepository(_db);
        }
        public ActionResult AdminAnasayfaIndex(int? id=null)
        {
            if (id!=null)
            {
                var kullanici = _kullanicilarRepo.KullaniciGetir((int)id);//Burdaki KullaniciGetir metodunu KullanicilarRrpository sayfasında oluşturdum ve burada çağırıyorum
                Session.Add("kullaniciYetki", kullanici.Yetkiler.YetkilerID);
            }
            return View();
        }
    }
}