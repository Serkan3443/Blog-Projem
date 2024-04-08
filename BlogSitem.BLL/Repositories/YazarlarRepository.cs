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
    public class YazarlarRepository : Repository<Yazarlar>, IYazarlarRepository
    {
        BlogSitemDB _db;
        public YazarlarRepository(BlogSitemDB context) : base(context)
        {
            _db = context;
        }

        public string YazarEkle(string yazarAdi, string yazarSoyadi, string email, string kisacaHakkinda, string calistigiFirma, string yasadigiUlke, string yasadigiSehir, int kullanicilarID)
        {
            try
            {
                Yazarlar yazarEkle = new Yazarlar();
                yazarEkle.YazarAdi = yazarAdi;
                yazarEkle.YazarSoyadi = yazarSoyadi;
                yazarEkle.Email = email;
                yazarEkle.KisacaHakkinda= kisacaHakkinda;
                yazarEkle.CalistigiFirma = calistigiFirma;
                yazarEkle.YasadigiUlke = yasadigiUlke;
                yazarEkle.YasadigiSehir = yasadigiSehir;
                yazarEkle.Kullanicilar = _db.Kullanicilar.Where(k => k.KullanicilarID == kullanicilarID).FirstOrDefault();
                Add(yazarEkle);
                return "Yaar ekleme başarılı oldu";
            }
            catch (Exception)
            {

                return "Yazar ekleme esnasında bir hata oluştu";
            }
        }

        public string YazarGuncelle(int yazarlarID, string yazarAdi, string yazarSoyadi, string email,string calistigiFirma,string yasadigiSehir, int kullanicilarID,bool aktifMi)
        {
            var yazarGuncelle1 = Find(y => y.YazarlarID == yazarlarID).FirstOrDefault();

            var yazarlarGuncelle = Get(yazarlarID);
            try
            {
               yazarlarGuncelle.YazarAdi = yazarAdi;
               yazarlarGuncelle.YazarSoyadi = yazarSoyadi;
               yazarlarGuncelle.Email = email;
               yazarlarGuncelle.CalistigiFirma = calistigiFirma;
               yazarlarGuncelle.YasadigiSehir = yasadigiSehir;
               yazarlarGuncelle.Kullanicilar = _db.Kullanicilar.Where(k => k.KullanicilarID == kullanicilarID).FirstOrDefault();
                yazarlarGuncelle.AktifMi = aktifMi;
                //Update(yazarlarGuncelle);
                return "Güncelleme işlemi başarılı";
            }
            catch (Exception)
            {

                return "Güncelleme esnasında bir hata oluştu";
            }
        }

        public IEnumerable<Yazarlar> YazarListesi()
        {
            return GetAll();
        }

        public string YazarSil(int yazarlarID)
        {
            try
            {
                var pasifEt = Get(yazarlarID);
                pasifEt.AktifMi = false;
                return DefinationMessages.Pasif_Başarılı.ToString();
            }
            catch (Exception)
            {

                return DefinationMessages.Pasif_Edilirken_Hata_Oluştu.ToString();
            }
        }
    }
}
