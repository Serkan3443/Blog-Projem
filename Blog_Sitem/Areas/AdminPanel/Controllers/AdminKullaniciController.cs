using Blog_Sitem.Models;
using BlogSitem.BLL.Repositories;
using BlogSitem.DLL.BlogSiteDatabase.ORMManager;
using BlogSitem.DLL.MesajEnumları;
using BlogSitem.DLL.UnitOfWorkManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog_Sitem.Areas.AdminPanel.Controllers
{
    public class AdminKullaniciController : Controller
    {
        BlogSitemDB _db;
        KullanicilarRepository _kullaniciRepo;
        UnitOfWork _uniOfWork;
        UyariMesajlari _uyariMesaj = new UyariMesajlari();

        public AdminKullaniciController()
        {
                _db=new BlogSitemDB();
            _kullaniciRepo = new KullanicilarRepository(_db);
            _uniOfWork = new UnitOfWork(_db);
        }

        public ActionResult AdminKullaniciIndex()
        {
           
            return View(_kullaniciRepo.KullaniciListesi());
        }

        public ActionResult AdminKullaniciEkle()
        {
            var kullaniciList = _kullaniciRepo.KullaniciListesi();
            return View(kullaniciList);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AdminKullaniciEkle(string Adi, string Soyadi, string kullaniciAdi,string sifre)
        {
            var kullaniciList = _kullaniciRepo.KullaniciListesi();
            var ekle = _kullaniciRepo.KullaniciEkle(Adi, Soyadi, kullaniciAdi, sifre);
            if (ekle==DefinationMessages.Ekleme_İşlemi_Esnasında_Hata_Oluştu.ToString())
            {
                ViewBag.mesaj = _uyariMesaj.Hatali(ekle);
                return View(kullaniciList);
            }
            int kaydet = _uniOfWork.SaveChanges();
            if (kaydet<=(int)DefinationMessages.Failed)
            {
                ViewBag.mesaj = _uyariMesaj.Hatali(DefinationMessages.Eklenirken_Hata_Olustu.ToString());
                return View(kullaniciList);
            }
            ViewBag.mesaj = _uyariMesaj.Basarili(DefinationMessages.Ekleme_Başarılı.ToString());
            return View(kullaniciList);
        }

        public ActionResult AdminKullaniciGuncelle(int id)
        {
            
            var kullaniciBul = _kullaniciRepo.Get(id);
            return View(kullaniciBul);
        }

        [HttpPost,ValidateInput(false)]
        public ActionResult AdminKullaniciGuncelle(int id, string Adi, string Soyadi, string kullaniciAdi, string Sifre, DateTime eklenmeTarihi, DateTime pasiflikTarihi, bool aktifMi, bool yazarMi, int? yetkilerID)
        {
            var guncelle = _kullaniciRepo.KullaniciGuncelle(id, Adi, Soyadi, kullaniciAdi, Sifre, eklenmeTarihi, pasiflikTarihi, aktifMi, yazarMi, 2);
            if (guncelle==DefinationMessages.Güncelleme_işlemi_esnasında_hata_oluştu.ToString())
            {
                ViewBag.mesaj = _uyariMesaj.Hatali(guncelle);
                return View();
            }
            var sonuc=_uniOfWork.SaveChanges();
            if (sonuc<=(int)DefinationMessages.Failed)
            {
                ViewBag.mesaj = _uyariMesaj.Hatali(DefinationMessages.Ekleme_İşlemi_Esnasında_Hata_Oluştu.ToString());
                return View();
            }
            if (sonuc>0)
            {
                return RedirectToAction("AdminKullaniciIndex");
            }
            var kullaniciBul = _kullaniciRepo.Get(id);
            return View(kullaniciBul);
        }

        public ActionResult AdminKullaniciSil(int id)
        {
            var kullaniciBul = _kullaniciRepo.Get(id);
            return View(kullaniciBul);
        }

        [HttpPost,ActionName("AdminKullaniciSil")]
        public ActionResult AdminKullaniciDelete(int id)
        {
            var sil = _kullaniciRepo.KullaniciSil(id);
            if (sil==DefinationMessages.Silme_esnasında_bir_Hata_oluştu.ToString())
            {
                ViewBag.mesaj = _uyariMesaj.Hatali(DefinationMessages.Eklenirken_Hata_Olustu.ToString());
                return View();
            }
            var sonuc = _uniOfWork.SaveChanges();
            if (sonuc<=(int)DefinationMessages.Failed)
            {
                ViewBag.mesaj = _uyariMesaj.Hatali(DefinationMessages.Eklenirken_Hata_Olustu.ToString());
                return View();
            }
            if (sonuc>0)
            {
                return RedirectToAction("AdminKullaniciIndex");
            }
            var kullaniciBul = _kullaniciRepo.Get(id);
            return View(kullaniciBul);
        }

    }
}