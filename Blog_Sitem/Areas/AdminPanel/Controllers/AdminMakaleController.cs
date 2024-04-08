using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog_Sitem.Models;
using BlogSitem.BLL;
using BlogSitem.BLL.Manager;
using BlogSitem.BLL.Repositories;
using BlogSitem.DLL;
using BlogSitem.DLL.BlogSiteDatabase.ORMManager;
using BlogSitem.DLL.MesajEnumları;
using BlogSitem.DLL.UnitOfWorkManager;
namespace Blog_Sitem.Areas.AdminPanel.Controllers
{
    public class AdminMakaleController : Controller
    {
        BlogSitemDB _db;
        MakalelerRepository _makaleRepo;
        MakaleKategorileriRepository _KategoriRepo;
        UnitOfWork _unitOfWork;
        UyariMesajlari uyariMesaji = new UyariMesajlari();//Bu UyariMesajlari adlı classı UI katmanında Model adlı klasörün altında yaptım ve burada uyarı mesajları için kullanıcam.Bu classı oluşturmamın bir sebebi de uyarı mesajları daha güzel biçimde gözüksün diye biçim den dolayı diyelim işte

        public AdminMakaleController()
        {

            _db = new BlogSitemDB();
            _makaleRepo = new MakalelerRepository(_db);
            _KategoriRepo = new MakaleKategorileriRepository(_db);
            _unitOfWork = new UnitOfWork(_db);
           
        }
        // AdminMakale`nin Anasayfası
        public ActionResult AdminMakaleIndex()
        {

            //MakaleManager makale=new MakaleManager();
            //var list = makale.MakalelerListesi();
            return View(_makaleRepo.Sp_MakaleListesi());//Bunu MakaleRepository den çağırıyorum yani bu metodu orada tanımladım ve burada uyguluyorum
        }

        //Bu class neden oluşturdum;Çünkü MakalelerIndex arayüzünde Makale Ekle diye yeşil kutucuklu bir button vardı işte ben o butona tıkladığım zaman makale eklemesi için gerekli kodları yazacağım için bu metodu oluşturdum ve bu metod için de şimdi bir view sayfası oluşturup gerekli işlemleri o şekilde halletmiş olacağım
        //AdminMakale Ekleme Sayfası
        [HttpGet]
        public ActionResult AdminMakaleEkle()
        {
            var kategoriList = _KategoriRepo.KategorListesi();
            //var ekle = _makaleRepo.MakaleEkle(5, Convert.ToInt32(makaleKategori), makaleBaslik, makaleIcerik);
            return View(kategoriList);
        }
        #region Parametre isimleri
        /*
         Parametre isimleri view sayfasında verilen tag`in name attribute`si ile tutulur
        yani daha açıklayıcı anlatiyim:AdminMakaleEkle sayfasında Makale Kategori div`inde name=makaleKategori vermiştim ya, heh işte o makaleKategori aynen yukarıdaki classa da ekliyorum=>AdminMakaleEkle(string makaleKategori,string makaleBaslik,string makaleIcerik)bu şekilde.Yani ordaki name`ye ne yazıldıysa bire bir aynısı burdaki bağlı classa da yazılıacaktır.
        ValidateInput(false)=>Html kodları DB ye direkt olarak kaydedilemez.Method başında bu yapı yazılarak HTML kodları DB ye kaydedilebilir.
         */
        #endregion


        //AdminMakale Ekleme Sayfası
        [ValidateInput(false), HttpPost]//Burdaki ValidateInput(false) yazmasam ekleme vs gibi işlemleri veritabanına göndermez yani proje çalışmaz o yüzden bu kodu kesinlikle ve kesinlikle yazmam lazım.Peki bu yapı neden var derseniz;Veritabanına kod göndermek tehlikelidir.Yani güvenlik amacıyla herhangi bir saldırı olmaması için bu yapıyı kullanma ihtiyacı duyarız
        public ActionResult AdminMakaleEkle(string makaleKategori, string makaleBaslik, string makaleIcerik)
        {
            var kategoriList = _KategoriRepo.KategorListesi();
            var ekle = _makaleRepo.MakaleEkle(1, Convert.ToInt32(makaleKategori), makaleBaslik, makaleIcerik);//Bu kod makale tablosuna ekleme işlmleri yapıyor
            if (ekle == DefinationMessages.Ekleme_İşlemi_Esnasında_Hata_Oluştu.ToString())
            {
                ViewBag.mesaj =uyariMesaji.Hatali(ekle);
                return View(kategoriList);//Her View yazdığım yerde bu kategorlist i tanımlamam lazım çünkü model olarak kabul ediliyor burada o yüzden yazmasam hata olarak model boş olduğunu bana söyler.
            }
            int kaydet = _unitOfWork.SaveChanges();
            if (kaydet <= (int)DefinationMessages.Failed)
            {
                ViewBag.mesaj = uyariMesaji.Hatali(DefinationMessages.Eklenirken_Hata_Olustu.ToString());//Bunu böyle yaptığımda Kırmızı kutu içinde Eklenirken_Hata_Oluştu diye bir mesaj gelecektir.Neden kırmızı çünkü UyariMesajlari classında hata olması durumunda kırmızı kutu içerisinde mesajın gelmesini istediğim için böyle bir class yapmak istedim
                return View(kategoriList);
            }
            ViewBag.mesaj = uyariMesaji.Basarili(DefinationMessages.Ekleme_Başarılı.ToString());//Bunu böyle yaptığımda yeşil kutu içinde Ekleme_Başarılı diye bir mesaj verecektir.Neden yeşil çünkü UyariMesajlari classında başarılı olması durumunda yeşil olsun dediğim için.
            return View(kategoriList);
        }

         //AdminMakale Anasayfasında Güncelle Butonuno tıkladığımda beni götürecek sayfaya güncelleme yapmak için oluşturduğum class 
        public ActionResult AdminMakaleGuncelle(int id)
        {
            #region DropDownList ile Kategori listeleme
            //List<SelectListItem> kategoriList = new List<SelectListItem>();
            //kategoriList = (from k in _db.MakaleKategorileri.ToList()
            //                select new SelectListItem
            //                {
            //                    Text = k.KategoriAdi,
            //                    Value = k.MakaleKategorileriID.ToString(),
            //                }).ToList();
            //TempData["makaleKategorileri"]=kategoriList;
            //Yukarıdaki yapı yerine view tarafında foreach ile MakaleKategorilerini daha basit aşağıdaki gibi yaparız
            #endregion
            
            ViewBag.makaleKategori = _KategoriRepo.GetAll();
            var makaleBul = _makaleRepo.Get(id);
            return View(makaleBul);
                
        }


        //AdminMakale Anasayfasında Güncelle Butonuno tıkladığımda beni götürecek sayfaya güncelleme yapmak için oluşturduğum class 
        [HttpPost,ValidateInput(false)]
        public ActionResult AdminMakaleGuncelle(int id, string makaleBaslik, string makaleIcerik, string aktifMi,int makaleKategoriId)
        {
            #region DropDownList ile Kategori listeleme
            //List<SelectListItem> kategoriList = new List<SelectListItem>();
            //kategoriList = (from k in _db.MakaleKategorileri.ToList()
            //                select new SelectListItem
            //                {
            //                    Text = k.KategoriAdi,
            //                    Value = k.MakaleKategorileriID.ToString(),
            //                }).ToList();
            //TempData["makaleKategorileri"]=kategoriList;
            //Yukarıdaki yapı yerine view tarafında foreach ile MakaleKategorilerini daha basit aşağıdaki gibi yaparız
            #endregion

            ViewBag.makaleKategori = _KategoriRepo.GetAll();
            var guncelle = _makaleRepo.MakaleGuncelle(id,2,makaleKategoriId, makaleBaslik,makaleIcerik, Convert.ToBoolean(aktifMi),9);
            var sonuc = _unitOfWork.SaveChanges();
            if(sonuc>0)
            {
                //Bu şu anlama gelir:Eğer güncelleme başarılıysa MakaleList sayfasına gidecek,değilse aynı sayfa yeniden yüklenecek
                return RedirectToAction("AdminMakaleIndex");
            }
            var makaleBul = _makaleRepo.Get(id);
            return View(makaleBul);
        }
        

        //AdminMakale Anasayfasında Silme butonuna tıkladığımda silme işlemi yapacağım sayfaya götürecek class yapısıdır
        public ActionResult AdminMakaleSil(int id)//Bu classın view sayfasını oluşturuken Edit seçiyorum yani boş bir view değil edit olarak bir view yapıyorum
        {
            var makaleBul = _makaleRepo.Get(id);
            return View(makaleBul);
        }


        [HttpPost,ActionName("AdminMakaleSil")]//ActionName=>Bir metod yönlendirme Attribute`dür.Bu Attribute aynı isimde olan 2 metod için 2.metod farklı isimde tanımlayıp ama attribute içinde tanımmlanan methodun post/get olduğunu belirtmek için kullanılır.
        public ActionResult AdminMakaleDelete(int id)
        {
            _makaleRepo.MakaleSil(id);
            var sonuc=_unitOfWork.SaveChanges();
            if (sonuc>0)
            {
                return RedirectToAction("AdminMakaleIndex");
            }
            var makaleBul=_makaleRepo.Get(id);
            return View(makaleBul);
        }

//****************************************************************************************************************************
        //Bu sadece bir örnektir yani.Projeyle fazla bir alakası yok
        //Ve bunu oluşturuken Template olarak Create seçiyorum,ModelClass olarak şuan işlem yatığım tablo Makale olduğu için Makale classını seçiyorum ve DataContextClass olarak da zaten otomatik Makale Olarak geeliyor ve onu seçiyorum aynı zamanda Layout olarak da admin paneli üzerinden işlem yaptığım için AdminLayout seçip add diyorum ve view sayfam oluşmuş oluyor.
        public ActionResult AdminMakaleEkle_Deneme()
        {
            return View();
        }


    }
}