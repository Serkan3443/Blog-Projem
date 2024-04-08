using BlogSitem.DLL.BlogSiteDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSitem.BLL.IRepositories
{
    public interface IYorumlarRepository
    {
        int MakaleYorumSayisi(int makaleID);
        int AltYorumSayisi(int yorumId);
        string YorumEkle(string yorum,int ustYorumID,int kullanicilarID,int makalelerID);
        string YorumGuncelle(int yorumID, string yorum,int ustYorumID, int kullanicilarID, int makalelerID, bool aktifMi);
        string YorumSil(int yorumID);
        IEnumerable<Yorumlar> YorumListesi();
        IEnumerable<Yorumlar> MakaleYorumListesi(int makaleID);
        IEnumerable<Sp_YorumlarDOM> Sp_YorumListesi(bool aktifMi);
       
    }
}
