using BlogSitem.DLL.BlogSiteDatabase;
using BlogSitem.DLL.RepositoryManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSitem.BLL.IRepositories
{
    public interface IMakaleKategorileriRepository:IRepository<MakaleKategorileri>
    {
        int KategoriSayisi(int kategoriID);
        string KategoriEkle(string kategoriAdi, string aciklama);
        string KategoriGuncelle(int kategoriID,string kategoriAdi,string aciklama,bool aktifMi);
        string KategoriSil(int kategoriID);
        IEnumerable<MakaleKategorileri> KategorListesi();
        IEnumerable<Makaleler> MakaleListesi(bool aktifMi);
    }
}
