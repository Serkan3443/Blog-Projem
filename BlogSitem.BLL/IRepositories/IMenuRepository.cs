using BlogSitem.DLL.BlogSiteDatabase;
using BlogSitem.DLL.RepositoryManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSitem.BLL.IRepositories
{
    public interface IMenuRepository:IRepository<Menu>
    {
        string MenuEkle(string menuAdi, int ustMenuID, string aciklama,bool aktifMi);
        string MenuGuncelle(int menulerID, string menuAdi, int ustMenuID, string aciklama,bool aktifMi);
        string MenuSil(int menulerID);
        IEnumerable<Menu> MenuListesi();
        IEnumerable<Sp_MenuListesiDOM> Sp_MenuListesi();
        IEnumerable<Sp_MenuListesiDOM> Sp_MenuListesi(bool aktifMi);
    }
}
