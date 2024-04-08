using BlogSitem.BLL.IRepositories;
using BlogSitem.DLL.BlogSiteDatabase;
using BlogSitem.DLL.BlogSiteDatabase.ORMManager;
using BlogSitem.DLL.RepositoryManager;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSitem.BLL.Repositories
{
    public class ResimlerRepository : Repository<Resimler>, IResimlerRepository
    {
        BlogSitemDB _db;
        public ResimlerRepository(BlogSitemDB context) : base(context)
        {
            _db = context;
        }

        public string ResimEkle(string buyukResimkonumu, string kucukResimKonumu,string resimKonumu,string baslik,DateTime eklenmeTarihi, bool aktifMi)
        {
            try
            {
                Resimler resimEkle= new Resimler();
                resimEkle.BuyukResimKonumu=buyukResimkonumu;
                resimEkle.KucukResimKonumu = kucukResimKonumu;
                resimEkle.EklenmeTarihi = eklenmeTarihi;
                resimEkle.AktifMi = aktifMi;
                resimEkle.Makaleler = _db.Makaleler.Where(m => m.MakalelerID == 2).FirstOrDefault();
                //Add(resimEkle);
                return "Resim ekleme işlemi başarılı";
            }
            catch (Exception)
            {

                return "Ekleme işlemi esnasında bir hata oluştu";
            }
        }

        public IResimlerRepository ResimGetir(int resimID, string resimAdi)
        {
            throw new NotImplementedException();
        }

        public string ResimGuncelle(int resimlerID, string buyukResimkonumu, string kucukResimKonumu,string resimKonumu, string baslik, DateTime eklenmeTarihi, bool aktifMi)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Resimler> ResimleriGetir(int resimID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Resimler> ResimlerListesi()
        {
            return GetAll();
        }

        public string ResimSil(int resimlerID)
        {
            throw new NotImplementedException();
        }

        public string ResimSil(int resimlerID, bool aktifMi)
        {
            throw new NotImplementedException();
        }
    }
}
