using Blog_Sitem.Models;
using BlogSitem.BLL.Repositories;
using BlogSitem.DLL.BlogSiteDatabase;
using BlogSitem.DLL.BlogSiteDatabase.ORMManager;
using BlogSitem.DLL.MesajEnumları;
using BlogSitem.DLL.UnitOfWorkManager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog_Sitem.Areas.AdminPanel.Controllers
{
    public class AdminYazarController : Controller
    {
        BlogSitemDB _db;
        YazarlarRepository _yazarRepo;
        UnitOfWork _unitOfWork;
        UyariMesajlari _uyariMesaj = new UyariMesajlari();

        public AdminYazarController()
        {
            _db = new BlogSitemDB();
            _yazarRepo = new YazarlarRepository(_db);
            _unitOfWork = new UnitOfWork(_db);
            
        }
        
        public ActionResult AdminYazarIndex()
        {
            return View(_yazarRepo.YazarListesi());
        }

        [HttpGet]
        public ActionResult AdminYazarEkle()
        {
            var yazarList = _yazarRepo.YazarListesi();
            return View(yazarList);
        }

        [HttpPost,ValidateInput(false)]
        public ActionResult AdminYazarEKle(string yazarAdi,string yazarSoyadi,string email,string kisacaHakkinda,string calistigiFirma,string yasadigiUlke,string yasadigiSehir)
        {
            var yazarList = _yazarRepo.YazarListesi();
            var ekle = _yazarRepo.YazarEkle(yazarAdi, yazarSoyadi, email, kisacaHakkinda, calistigiFirma, yasadigiUlke, yasadigiSehir,8);
            if (ekle==DefinationMessages.Ekleme_İşlemi_Esnasında_Hata_Oluştu.ToString())
            {
                ViewBag.mesaj = _uyariMesaj.Hatali(ekle);
                return View(yazarList);
            }
            int kaydet = _unitOfWork.SaveChanges();
            if (kaydet<=(int)DefinationMessages.Failed)
            {
                ViewBag.mesaj = _uyariMesaj.Hatali(DefinationMessages.Eklenirken_Hata_Olustu.ToString());
            }
            ViewBag.mesaj = _uyariMesaj.Basarili(DefinationMessages.Ekleme_Başarılı.ToString());
            return View(yazarList);
        }

        [HttpGet]
        public ActionResult AdminYazarGuncelle(int id)
        {
            ViewBag.yazarlar = _yazarRepo.GetAll();
            var yazarBul = _yazarRepo.Get(id);
            return View(yazarBul);
        }

        [HttpPost,ValidateInput(false)]
        public ActionResult AdminYazarGuncelle(int id,string yazarAdi, string yazarSoyadi, string email,string calistigiFirma, string yasadigiSehir, int? kullanicilarID,bool aktifMi)
        {
            ViewBag.yazarlar=_yazarRepo.GetAll();

            var guncelle = _yazarRepo.YazarGuncelle(id,yazarAdi, yazarSoyadi, email, calistigiFirma,yasadigiSehir,9,aktifMi);

            var kaydet = _unitOfWork.SaveChanges();
            if (kaydet>0)
            {
                return RedirectToAction("AdminYazarIndex");
            }
            var yazarBul = _yazarRepo.Get(id);
            return View(yazarBul);
        }

        public ActionResult AdminYazarSil(int id)
        {
          var yazarBul= _yazarRepo.Get(id);
            return View(yazarBul);
           
        }
        [HttpPost,ActionName("AdminYazarSil")]
        public ActionResult AdminYazarDelete(int id)
        {
            _yazarRepo.YazarSil(id);
           
            var sonuc = _unitOfWork.SaveChanges();
            if (sonuc > 0)
            {
                return RedirectToAction("AdminYazarIndex");
            }
            var yazarBul=_yazarRepo.Get(id);
            return View(yazarBul);

        }

        
    }
}