using BlogSitem.DLL.BlogSiteDatabase;
using BlogSitem.DLL.RepositoryManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSitem.BLL.IRepositories
{
    public interface IYetkilerRepository:IRepository<Yetkiler>
    {
        string YetkiEkle(string yetkiAdi);

        string YetkiGuncelle(int yetkilerID, string yetkiAdi, bool aktifMi);

        string YetkiSil(int yetkilerID);
        IEnumerable<Yetkiler> YetkiListesi();
    }
}
