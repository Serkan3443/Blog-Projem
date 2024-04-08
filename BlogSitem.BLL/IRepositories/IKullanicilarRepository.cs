using BlogSitem.DLL.BlogSiteDatabase;
using BlogSitem.DLL.RepositoryManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSitem.BLL.IRepositories
{
    public interface IKullanicilarRepository:IRepository<Kullanicilar>
    {
        string KullaniciEkle(string Adi, string Soyadi, string kullaniciAdi, string Sifre);

        string KullaniciGuncelle(int kullanicilarID, string Adi, string Soyadi, string kullaniciAdi, string Sifre, DateTime eklenmeTarihi, DateTime pasiflikTarihi, bool aktifMi, bool yazarMi, int yetkilerID);

        string KullaniciSil(int kullanicilarID);

        IEnumerable<Kullanicilar> KullaniciListesi();
    }
}
