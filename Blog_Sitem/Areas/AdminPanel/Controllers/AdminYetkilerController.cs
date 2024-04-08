using Blog_Sitem.Models;
using BlogSitem.BLL.Repositories;
using BlogSitem.DLL.BlogSiteDatabase.ORMManager;
using BlogSitem.DLL.MesajEnumları;
using BlogSitem.DLL.UnitOfWorkManager;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog_Sitem.Areas.AdminPanel.Controllers
{
    public class AdminYetkilerController : Controller
    {
        BlogSitemDB _db;
        YetkilerRepository _yetkiRepo;
        UyariMesajlari uyariMesaji=new UyariMesajlari();
        UnitOfWork _unitOfWork;

        public AdminYetkilerController()
        {
            _db = new BlogSitemDB();
            _yetkiRepo = new YetkilerRepository(_db);
            _unitOfWork = new UnitOfWork(_db);
        }

        public ActionResult AdminYetkilerIndex()
        {
            var yetkiList = _yetkiRepo.YetkiListesi();
            return View(yetkiList);
        }

        public ActionResult AdminYetkiGuncelle(int id)
        {
            var yetkiBul = _yetkiRepo.Get(id);
            return View(yetkiBul);
        }

        [HttpPost]
        public ActionResult AdminYetkiGuncelle(int id,string yetkiAdi,bool aktifMi)
        {
            var guncelle = _yetkiRepo.YetkiGuncelle(id, yetkiAdi,aktifMi);
            if (guncelle==DefinationMessages.Güncelleme_işlemi_esnasında_hata_oluştu.ToString())
            {
                ViewBag.mesaj = uyariMesaji.Hatali(guncelle);
                return View();
            }
            int sonuc = _unitOfWork.SaveChanges();
            if (sonuc<=(int)DefinationMessages.Failed)
            {
                ViewBag.mesaj = uyariMesaji.Hatali(DefinationMessages.Eklenirken_Hata_Olustu.ToString());
                return View();
            }
            if (sonuc>0)
            {
                return RedirectToAction("AdminYetkilerIndex");
            }
            var yetkiBul=_yetkiRepo.Get(id);
            return View(yetkiBul);
        }

        public ActionResult AdminYetkiSil(int id)
        {
            var yetkiBul = _yetkiRepo.Get(id);
            return View(yetkiBul);

        }

        [HttpPost,ActionName("AdminYetkiSil")]
        public ActionResult AdminYetkiDelete(int id)
        {
            _yetkiRepo.YetkiSil(id);

            var sonuc=_unitOfWork.SaveChanges();
            if (sonuc>0)
            {
                return RedirectToAction("AdminYetkilerIndex");
            }
            var yetkiBul= _yetkiRepo.Get(id);
            return View(yetkiBul);
        }

    }
}