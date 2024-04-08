using BlogSitem.BLL.IRepositories;
using BlogSitem.DLL.BlogSiteDatabase;
using BlogSitem.DLL.BlogSiteDatabase.ORMManager;
using BlogSitem.DLL.MesajEnumları;
using BlogSitem.DLL.RepositoryManager;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSitem.BLL.Repositories
{
    public class KullanicilarRepository : Repository<Kullanicilar>, IKullanicilarRepository
    {
        BlogSitemDB _db;

        public KullanicilarRepository(BlogSitemDB context):base(context)
        {
            _db =context;
        }


        /// <summary>
        /// Kullanıcının bütün bilgilerini getirir
        /// </summary>
        /// <param name="kullaniciAdi"></param>
        /// <param name="sifre"></param>
        /// <returns></returns>
        public Kullanicilar Giris(string kullaniciAdi,string sifre)
        {
            var getir = Find(k => k.KullaniciAdi==kullaniciAdi && k.KullaniciSifresi == sifre).FirstOrDefault();
            return getir;
        }

        //public string Uyelik(string Adi,string Soyadi,string kullaniciAdi,string sifre)
        //{
        //    try
        //    {
        //        Kullanicilar UyeOl = new Kullanicilar();
        //        UyeOl.Adi = Adi;
        //        UyeOl.Soyadi = Soyadi;
        //        UyeOl.KullaniciAdi = kullaniciAdi;
        //        UyeOl.KullaniciSifresi = sifre;
        //        Add(UyeOl);

        //        return DefinationMessages.Tebrikler_Üyeliğiniz_Başarıyla_Oluştu.ToString();
        //    }
        //    catch (Exception)
        //    {

        //        return DefinationMessages.Üyelik_Oluşturma_Esnasında_Bir_Hata_Meydana_Geldi.ToString();
        //    }
        //}

        /// <summary>
        /// Kullanıcı değeri ile kullanıcı bilgilerini getirir
        /// </summary>
        /// <param name="kullaniciId"></param>
        /// <returns></returns>
        public Kullanicilar KullaniciGetir(int kullaniciId)//Bu metodu daha sonra AdminPanel deki Anassayfa Controller sayfasında çağıracağım
        {
            var getir=Find(k=>k.KullanicilarID==kullaniciId).FirstOrDefault();
            return getir;
        }

        public string KullaniciEkle(string Adi, string Soyadi, string kullaniciAdi, string Sifre)
        {
            try
            {
                Kullanicilar kullaniciEkle = new Kullanicilar();
                kullaniciEkle.Adi = Adi;
                kullaniciEkle.Soyadi = Soyadi;
                kullaniciEkle.KullaniciAdi = kullaniciAdi;
                kullaniciEkle.KullaniciSifresi = Sifre;
                kullaniciEkle.EklenmeTarihi = DateTime.Now;
                kullaniciEkle.PasiflikTarihi = DateTime.Now;
                Add(kullaniciEkle);

                return DefinationMessages.Ekleme_Başarılı.ToString();
            }
            catch (Exception)
            {

                return DefinationMessages.Ekleme_İşlemi_Esnasında_Hata_Oluştu.ToString();
            }
        }

        public string KullaniciGuncelle(int kullanicilarID, string Adi, string Soyadi, string kullaniciAdi, string Sifre, DateTime eklenmeTarihi, DateTime pasiflikTarihi, bool aktifMi, bool yazarMi, int yetkilerID)
        {
            var kullaniciGuncelle = Find(k => k.KullanicilarID == kullanicilarID).FirstOrDefault();
            try
            {
                kullaniciGuncelle.Adi = Adi;
                kullaniciGuncelle.Soyadi = Soyadi;
                kullaniciGuncelle.KullaniciAdi = kullaniciAdi;
                kullaniciGuncelle.KullaniciSifresi = Sifre;
                kullaniciGuncelle.EklenmeTarihi = DateTime.Now;
                kullaniciGuncelle.PasiflikTarihi = DateTime.Now;
                kullaniciGuncelle.AktifMi = aktifMi;
                kullaniciGuncelle.YazarMi = yazarMi;
                kullaniciGuncelle.Yetkiler = _db.Yetkiler.Where(k => k.YetkilerID == yetkilerID).FirstOrDefault();

                return DefinationMessages.Güncelleme_Başarılı.ToString();
            }
            catch (Exception)
            {

                return DefinationMessages.Güncelleme_işlemi_esnasında_hata_oluştu.ToString();
            }
        }

        public string KullaniciSil(int kullanicilarID)
        {
            try
            {
                var pasifEt = Get(kullanicilarID);
                pasifEt.AktifMi = false;

                return DefinationMessages.Pasif_Başarılı.ToString();
            }
            catch (Exception)
            {

                return DefinationMessages.Pasif_Edilirken_Hata_Oluştu.ToString();
            }
        }

        public IEnumerable<Kullanicilar> KullaniciListesi()
        {
            return GetAll();
        }
    }
}
