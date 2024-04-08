using Blog_Sitem.Models;
using BlogSitem.BLL.Repositories;
using BlogSitem.DLL.BlogSiteDatabase.ORMManager;
using BlogSitem.DLL.MesajEnumları;
using BlogSitem.DLL.UnitOfWorkManager;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog_Sitem.Areas.AdminPanel.Controllers
{
    public class AdminYorumlarController : Controller
    {
        BlogSitemDB _db;
        YorumlarRepository _yorumRepo;
        UnitOfWork _unitOfWork;
        UyariMesajlari uyariMesaji =new UyariMesajlari();

        public AdminYorumlarController()
        {
            _db = new BlogSitemDB();
            _yorumRepo = new YorumlarRepository(_db);
            _unitOfWork = new UnitOfWork(_db);
        }
        public ActionResult AdminYorumlarIndex()
        {
            
            
            var yorumlist = _yorumRepo.YorumListesi();

            return View(yorumlist);
        }

        public ActionResult AdminYorumGuncelle(int id)
        {
            ViewBag.yorumlar = _yorumRepo.GetAll();
            var yorumBul = _yorumRepo.Get(id);
            return View(yorumBul);
        }

        [HttpPost]
        public ActionResult AdminYorumGuncelle(int id, string yorum, int ustYorumID, bool aktifMi)
        {
            ViewBag.yorumlar=_yorumRepo.GetAll();
            var guncelle=_yorumRepo.YorumGuncelle(id,yorum, ustYorumID, 4, 2, aktifMi);

            var kaydet = _unitOfWork.SaveChanges();
            if (kaydet>0)
            {
                return RedirectToAction("AdminYorumlarIndex");
            }
            var yorumBul= _yorumRepo.Get(id);
            return View(yorumBul);

        }

        public ActionResult AdminYorumSil(int id)
        {
            var yorumBul = _yorumRepo.Get(id);
            return View(yorumBul);
        }

        [HttpPost,ActionName("AdminYorumSil")]
        public ActionResult AdminYorumDelete(int id)
        {
            _yorumRepo.YorumSil(id);

            int sonuc = _unitOfWork.SaveChanges();
            if (sonuc>0)
            {
                return RedirectToAction("AdminYorumlarIndex");
            }
            var yorumBul = _yorumRepo.Get(id);
            return View(yorumBul);
        }
    }
}