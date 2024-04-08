using BlogSitem.DLL.BlogSiteDatabase;
using BlogSitem.DLL.RepositoryManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSitem.BLL.IRepositories
{
    public interface IResimlerRepository:IRepository<Resimler>
    {
        string ResimEkle(string buyukResimkonumu, string kucukResimKonumu, string resimKonumu,string baslik,DateTime eklenmeTarihi, bool aktifMi);
        string ResimGuncelle(int resimlerID, string buyukResimkonumu, string kucukResimKonumu,string resimKonumu,string baslik,DateTime eklenmeTarihi, bool aktifMi);
        string ResimSil(int resimlerID,bool aktifMi);
        IEnumerable<Resimler> ResimleriGetir(int resimID);
        IResimlerRepository ResimGetir(int resimID, string resimAdi);

    }
}
