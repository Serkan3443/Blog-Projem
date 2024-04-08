using BlogSitem.DLL.BlogSiteDatabase;
using BlogSitem.DLL.RepositoryManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSitem.BLL.IRepositories
{
    public interface IMakalelerRepository:IRepository<Makaleler>
    {
        string MakaleEkle(int yazarlarID, int makalekategorileriID, string makaleBaslik, string makaleIcerik);//Bu projede Stored Procedure yapmamın nedeni işte tam da budur.Nedir dersek stored Procedure yaptım çünkü makaleler tablosuna bağlı kullanıcılar,yazarlar ve kategoriler tablosu da bu tabloya bağlı olduğu için her bir tabloyu ayrı ayrı tanımlayıp işlem yapacağıma stored procedure uygulayıp bu üç tabloyu stored procedure ile tek bir tablo haline getirip kodumu daha sade bir şekilde yazarım 
        string MakaleGuncelle(int makaleID, int yazarlarID, int makalekategorileriID, string makaleBaslik, string makaleIcerik, bool aktifMi, int onaylayanKullaniciID);
        string MakaleSil(int makaleID);
        IEnumerable<Makaleler> MakaleListesi();
        IEnumerable<Makaleler> MakaleListesi(bool aktifMi);
        IEnumerable<Sp_MakaleListesiDOM> Sp_MakaleListesi();
    }
}
