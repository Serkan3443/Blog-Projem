using Blog_Sitem.Models;
using BlogSitem.BLL.Repositories;
using BlogSitem.DLL.BlogSiteDatabase;
using BlogSitem.DLL.BlogSiteDatabase.ORMManager;
using BlogSitem.DLL.MesajEnumları;
using BlogSitem.DLL.UnitOfWorkManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.Mvc;

namespace Blog_Sitem.Controllers
{
    public class MakaleController : BaseController
    {
        YorumlarRepository _yorumlarRepo;
        MakalelerRepository makalelerRepo;
        MakaleKategorileriRepository _kategoriRepo;
        BlogSitemDB _db = new BlogSitemDB();
        UnitOfWork _unitOfWork;
        UyariMesajlari uyariMesaj = new UyariMesajlari();

        public MakaleController()
        {
            _yorumlarRepo = new YorumlarRepository(_db);
            makalelerRepo = new MakalelerRepository(_db);
            _kategoriRepo = new MakaleKategorileriRepository(_db);
            _unitOfWork = new UnitOfWork(_db);
        }


        // GET: Makale
        public ActionResult MakalelerIndex()
        {
            //TempData["makalelist"] = makalelerRepo.MakaleListesi(true);1.yöntem Bu Makaleler tablosuna ait verileri getirir
            TempData["makalelist"] = makalelerRepo.Sp_MakaleListesi(true);//MakaleRepository`de Sp_MakaleListesini tanımladım ve burada uyguluyorum.Bu yöntem ise SP_MakaleListesi yani Procedure deki verileri getirir.Uygun yöntem bizim için bu zaten.Sp_MakaleListesi(true)=>içine true yazmamın sebebi aktif olan makalaleri getirecektir bana MakalelerRepository`de buna uygun class yapmıştım ordan bakabilirim
            // TempData["makalelist"]=>ben bunu _Makaleler Partial view e gönderip o şekilde makalelerimi oluşturduğum precedure üzerinden çağıracağım 
            TempData["makaleKategoriList"] = _kategoriRepo.GetAll();//Bunu _kategoriler partial view`i için oluşturdum

            return View();
        }

        public ActionResult MakaleDetay(int id)
        {
            var makaleGetir = makalelerRepo.Sp_MakaleListesi(true).Where(k => k.MakalelerID == id).FirstOrDefault();
            TempData["makaleGetir"] = makaleGetir;
            //TempData["makaleYorumlariGetir"] = yorumlarRepo.MakaleYorumListesi(id);Artık bu MakaleYorumListesi metoduyla yorumları listelemiycem onun yerine kullanıcılar ve yorumları birleştiren Sp_Yorumlar procedurüyle yorumlarımı listeliycem aşağıdaki gibi;
            TempData["makaleYorumlariGetir"] = _yorumlarRepo.SpliMakaleYorumlari(id);

            TempData["makaleAltYorumlariGetir"] = _yorumlarRepo.SpliMakaleAltYorumlari(id);//Bu Tempdata ise bana yorumlara verilen cevap yorumlarını listeleyecek
            TempData["makaleID"] = id;//Burda bu TempDatayı vermemin amacı bunu ben yorumlarPartialView sayfasında tanımlayıcam çünkü hangi makalaye tıklarsam o makalenin ıd`si gelmesi için bu şekilde bu komutu yazma mecburiyetindeyim 
            return View(makaleGetir);
        }


        [HttpGet]
        public ActionResult MakaleYorum()//yorum ekler
        {
            var yorumList = _yorumlarRepo.YorumListesi();
            return View(yorumList);
        }

        [HttpPost]
        public ActionResult MakaleYorum(string yorumIcerik, int makaleId)//yorum ekler
        {

            var ekle = _yorumlarRepo.YorumEkle(yorumIcerik, 0, 8, makaleId);
           int sonuc= _unitOfWork.SaveChanges();

            Sp_YorumlarDOM yorumData = new Sp_YorumlarDOM();
            yorumData.YorumTarihi = DateTime.Now;
            yorumData.Adi = ViewBag.userAdiSoyadi;
            yorumData.Soyadi = KullaniciSoyadi;
            yorumData.Yorum = yorumIcerik;
            yorumData.UstYorumID = 0;
            return Json(yorumData, JsonRequestBehavior.AllowGet);

        }

        //public string DBKayitSonuc()
        //{
        //    int sonuc = _unitOfWork.SaveChanges();
        //    if (sonuc > 0)
        //    {
        //        return "Kayıt işlemi başarılı";
        //    }
        //    return "Kayıt gerçekleşmedi";
        //}
    }
}