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
    public class AdminKategoriController : Controller
    {
        MakaleKategorileriRepository _kategoriRepo;
        BlogSitemDB _db;
        UyariMesajlari uyariMesaji = new UyariMesajlari();
        UnitOfWork _unitOfWork;

        public AdminKategoriController()
        {
            _db = new BlogSitemDB();
            _kategoriRepo = new MakaleKategorileriRepository(_db);
            _unitOfWork = new UnitOfWork(_db);
        }

        // GET: AdminPanel/AdminKategori
        public ActionResult AdminKategoriIndex()
        {
          
            return View(_kategoriRepo.KategorListesi().ToList());
        }

        public ActionResult AdminKategoriEkle()
        {
            var kategoriList = _kategoriRepo.KategorListesi();
            return View(kategoriList);  
        }

        [HttpPost]
        public ActionResult AdminKategoriEkle(string kategoriAdi,string aciklama)
        {
            var ekle = _kategoriRepo.KategoriEkle(kategoriAdi, aciklama);
            if (ekle==DefinationMessages.Ekleme_İşlemi_Esnasında_Hata_Oluştu.ToString())
            {
                ViewBag.mesaj = uyariMesaji.Hatali(ekle);
                return View();  
            }
            int kaydet = _unitOfWork.SaveChanges();
            if (kaydet<=(int)DefinationMessages.Failed)
            {
                ViewBag.mesaj = uyariMesaji.Hatali(DefinationMessages.Eklenirken_Hata_Olustu.ToString());
                return View();
            }
            ViewBag.mesaj = uyariMesaji.Basarili(DefinationMessages.Ekleme_Başarılı.ToString());

            return View();
        }
        public ActionResult AdminKategoriGuncelle(int id)
        {
            ViewBag.kategori = _kategoriRepo.GetAll();
            var kategoriBul = _kategoriRepo.Get(id);
            return View(kategoriBul);
        }

        [HttpPost,ValidateInput(false)]
        public ActionResult AdminKategoriGuncelle(int id,string kategoriAdi,string aciklama,bool aktifMi)
        {
            ViewBag.kategori = _kategoriRepo.GetAll();

            var guncelle = _kategoriRepo.KategoriGuncelle(id, kategoriAdi, aciklama,aktifMi);
            var sonuc = _unitOfWork.SaveChanges();
            if (sonuc>0)
            {
                return RedirectToAction("AdminKategoriIndex");
            }
            var kategoriBul = _kategoriRepo.Get(id);
            return View(kategoriBul);
        }

        public ActionResult AdminKategoriSil(int id)
        {
            var kategoriBul=_kategoriRepo.Get(id);
            return View(kategoriBul);
        }

        [HttpPost,ActionName("AdminKategoriSil")]
        public ActionResult AdminKategoriDelete(int id)
        {
            _kategoriRepo.KategoriSil(id);

            var sonuc=_unitOfWork.SaveChanges();
            if (sonuc>0)
            {
                return RedirectToAction("AdminKategoriIndex");
            }
            var kategoriBul=_kategoriRepo.Get(id);
            return View(kategoriBul);
        }
    }
}