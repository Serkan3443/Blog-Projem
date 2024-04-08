using Blog_Sitem.Models;
using BlogSitem.BLL.IRepositories;
using BlogSitem.BLL.Repositories;
using BlogSitem.DLL.BlogSiteDatabase;
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
    public class AdminMenulerController : Controller
    {
        BlogSitemDB _db;
        MenuRepository _menuRepo;
        UyariMesajlari uyariMesaji = new UyariMesajlari();
        UnitOfWork _unitOfWork;

        public AdminMenulerController()
        {
            _db = new BlogSitemDB();
            _menuRepo = new MenuRepository(_db);
            _unitOfWork = new UnitOfWork(_db);
        }

        public ActionResult AdminMenulerIndex()
        {
            var menuList = _menuRepo.Sp_MenuListesi();
            return View(menuList);
        }

        [HttpGet]
        public ActionResult AdminMenuEkle()
        {
            
            return View();
        }

        [ValidateInput(false),HttpPost]
        public ActionResult AdminMenuEkle(string menuAdi, int ustMenuID, string aciklama, bool aktifMi)
        {
            var ekle = _menuRepo.MenuEkle(menuAdi, ustMenuID, aciklama, aktifMi);

            if (ekle == DefinationMessages.Ekleme_İşlemi_Esnasında_Hata_Oluştu.ToString())
            {
                ViewBag.mesaj = uyariMesaji.Hatali(ekle);
                return View();
            }
            //Resimleri kaydet
            //kategori

            int kaydet = _unitOfWork.SaveChanges();

            if (kaydet <= (int)DefinationMessages.Failed)
            {
                ViewBag.mesaj = uyariMesaji.Hatali(DefinationMessages.Eklenirken_Hata_Olustu.ToString());
                return View();
            }

            if (kaydet > 0)
            {
                ViewBag.mesaj = uyariMesaji.Basarili(DefinationMessages.Ekleme_Başarılı.ToString());
                return RedirectToAction("AdminMenulerIndex");
            }

            return View();

        }
        [HttpGet]
        public ActionResult AdminMenuGuncelle(int id)
        {
            var menuBul = _menuRepo.Get(id);
            return View(menuBul);
        }
        [HttpPost]
        public ActionResult AdminMenuGuncelle(int id,string menuAdi,int ustMenuID,string menuAciklama,bool aktifMi)
        {
            var guncelle = _menuRepo.MenuGuncelle(id, menuAdi, ustMenuID, menuAciklama, aktifMi);

            if (guncelle == DefinationMessages.Ekleme_İşlemi_Esnasında_Hata_Oluştu.ToString())
            {
                ViewBag.mesaj =uyariMesaji.Hatali(guncelle);
                return View();
            }

            var sonuc = _unitOfWork.SaveChanges();

            if (sonuc <= (int)DefinationMessages.Failed)
            {
                ViewBag.mesaj = uyariMesaji.Hatali(DefinationMessages.Eklenirken_Hata_Olustu.ToString());
                return View();
            }

            if (sonuc > 0)
            {
                return RedirectToAction("AdminMenulerIndex");
            }

            var menuBul = _menuRepo.Get(id);

            return View(menuBul);

        }


        public ActionResult AdminMenuSil(int id)
        {
            var menuBul = _menuRepo.Get(id);
            return View(menuBul);
        }

        [HttpPost,ActionName("AdminMenuSil")]
        public ActionResult AdminMenuDelete(int id)
        {
            _menuRepo.MenuSil(id);
            var sonuc=_unitOfWork.SaveChanges();
            if (sonuc>0)
            {
                return RedirectToAction("AdminMenulerIndex");
            }
            var menuBul=_menuRepo.Get(id);
            return View(menuBul);
        }

    }
}