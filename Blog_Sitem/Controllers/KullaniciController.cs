using BlogSitem.BLL.Repositories;
using BlogSitem.DLL.BlogSiteDatabase.ORMManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog_Sitem.Controllers
{
    public class KullaniciController : BaseController
    {
        BlogSitemDB _db;
        KullanicilarRepository _kullaniciRepo;

        public KullaniciController()
        {
                _db = new BlogSitemDB();
            _kullaniciRepo = new KullanicilarRepository(_db);
        }
        public ActionResult GirisIndex(int? id=null)
        {
            if (id!=null)
            {
                TempData["makaleID"] =id;
            }
            //https://bootsnipp.com/snippets/z1Bpy  bu linkteki login yapısı için sayfamıza dahil edildi
            return View();
        }

        [HttpPost]
        public ActionResult GirisIndex(string kullaniciAdi, string sifre)
        {
            var kullaniciGiris = _kullaniciRepo.Giris(kullaniciAdi, sifre);
            //var kullaniciGiris = KullaniciGiris(kullaniciAdi, sifre);

            if (kullaniciGiris!=null)
            {
                //Session Nedir=>Oturum demek yani şöyle;Kullanıcının belirli bir süre boyunca web uygulamasıyla etkileşimde bulunduğu zaman dilimini temsil eder.

                string AdSoyad = kullaniciGiris.Adi + " " + kullaniciGiris.Soyadi;
                string yetki = kullaniciGiris.Yetkiler.YetkilerID.ToString();
                Session.Add("userName", AdSoyad);//Bu komut session oluşturur
                Session.Add("userYetki", yetki);
                string kullaniciAdSoyad = kullaniciGiris.Adi + " " + kullaniciGiris.Soyadi;
                TempData["userAdiSoyadi"] = kullaniciAdSoyad;//Bunu yazmamın sebebi sayfada giriş sekmesine bastığımda kullanıcı adı ve şifreyi girdikten sonra sayfada sağ üstte hangi kullanıcı girdiyse onun ismini yazsın diye
                TempData["kullaniciID"] = kullaniciGiris.KullanicilarID;

                if (TempData["makaleID"]!=null)
                {
                    
                    return RedirectToAction("MakaleDetay", "Makale", new {id= TempData["makaleID"] });
                }  
                return RedirectToAction("AnasayfaIndex", "Anasayfa");
            }
            return View();
        }
        public ActionResult LoginLink()
        {
            return View();
        }
        public ActionResult KullaniciCikis()
        {
            Session.Clear();
            return RedirectToAction("GirisIndex");
        }
    }
}