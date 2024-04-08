using BlogSitem.DLL.BlogSiteDatabase;
using BlogSitem.DLL.RepositoryManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSitem.BLL.IRepositories
{
    public interface IYazarlarRepository:IRepository<Yazarlar>
    {
        string YazarEkle(string yazarAdi, string yazarSoyadi, string email, string kisacaHakkinda, string calistigiFirma, string yasadigiUlke, string yasadigiSehir, int kullanicilarID);

        string YazarGuncelle(int yazarlarID, string yazarAdi, string yazarSoyadi, string email, string calistigiFirma, string yasadigiSehir, int kullanicilarID, bool aktifMi);

        string YazarSil(int yazarlarID);
        IEnumerable<Yazarlar> YazarListesi();
        
    }
}
