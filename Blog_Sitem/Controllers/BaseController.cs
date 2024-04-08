using BlogSitem.BLL.Repositories;
using BlogSitem.DLL.BlogSiteDatabase;
using BlogSitem.DLL.BlogSiteDatabase.ORMManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog_Sitem.Controllers
{
     //BaseController=>Bütün Controller`a kalıtım veren Controller`dır.Yani tüm Controller sayfalarının Atası gibi 
    //Log Nedir=>Kayıt tutmaya yarar.Kayıt tutmak demektir(sayfaya ne zaman girilmiş ,hangi bağlantılara tıklanmış vs.).Aynı zamanda Logları hiçbir zaman session`a atamayız

    public class BaseController : Controller
    {
        BlogSitemDB _db;
        KullanicilarRepository _kullaniciRepo;
        public string KullaniciAdi { get; set; }
        public string KullaniciSoyadi { get; set; }
        public string _kullaniciBilgileri;

        public BaseController()
        {
            _db = new BlogSitemDB();
            _kullaniciRepo = new KullanicilarRepository(_db);
            if (TempData["userAdiSoyadi"]!=null)
            {
                _kullaniciBilgileri = TempData["userAdiSoyadi"].ToString();
                TempData["userAdiSoyadi"] = TempData["adSoyad"];
            }
        }

        public ActionResult Index()
        {
            TempData[""] = null;
            return View();
        }

        public Kullanicilar KullaniciGiris(string kullaniciAdi,string sifre)
        {
            var kullaniciVarMi = _kullaniciRepo.Giris(kullaniciAdi, sifre);
            if (kullaniciVarMi==null)
            {
                return null;
            }
            _kullaniciBilgileri = kullaniciVarMi.Adi + " " + kullaniciVarMi.Soyadi;
            TempData["adSoyad"] = _kullaniciBilgileri;

            kullaniciAdi = kullaniciVarMi.Adi;
            return kullaniciVarMi;
        }

        public void KullaniciBilgileri(string kullaniciAdiSoyadi)
        {
            if (TempData["adSoyad"]!=null)
            {
                kullaniciAdiSoyadi = TempData["adSoyad"].ToString();
            }
            kullaniciAdiSoyadi = string.Empty;
        }
    }
}