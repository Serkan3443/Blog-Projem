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
    public class MenuRepository : Repository<Menu>,IMenuRepository
    {
        BlogSitemDB _db;
        public MenuRepository(BlogSitemDB context) : base(context)
        {
            _db = context;
        }

        public string MenuEkle(string menuAdi, int ustMenuID, string aciklama,bool aktifMi)
        {
            try
            {
                Menu menuEkle = new Menu();
                menuEkle.MenuAdi = menuAdi;
                menuEkle.UstMenuID = ustMenuID;
                menuEkle.Aciklama = aciklama;
                menuEkle.AktifMi = aktifMi;
                Add(menuEkle);
                return DefinationMessages.Ekleme_Başarılı.ToString();
            }
            catch (Exception)
            {
               
                return DefinationMessages.Ekleme_İşlemi_Esnasında_Hata_Oluştu.ToString();
            }
        }

        public string MenuGuncelle(int menulerID, string menuAdi, int ustMenuID, string aciklama,bool aktifMi)
        {
            var menuGuncelle = Find(m => m.MenulerID == menulerID).FirstOrDefault();
            try
            {
                menuGuncelle.MenuAdi = menuAdi;
                menuGuncelle.UstMenuID = ustMenuID;
                menuGuncelle.Aciklama = aciklama;
                menuGuncelle.AktifMi = aktifMi;
                return DefinationMessages.Güncelleme_Başarılı.ToString();
            }
            catch (Exception)
            {

                return DefinationMessages.Güncelleme_işlemi_esnasında_hata_oluştu.ToString();
            }
        }

        public IEnumerable<Menu> MenuListesi()
        {
            return GetAll();
        }

        public string MenuSil(int menulerID)
        {
            try
            {
                var pasifEt = Get(menulerID);
                pasifEt.AktifMi = false;

                return DefinationMessages.Silme_Başarılı.ToString();
            }
            catch (Exception)
            {

                return DefinationMessages.Silme_esnasında_bir_Hata_oluştu.ToString();
            }
        }

        public IEnumerable<Sp_MenuListesiDOM> Sp_MenuListesi()
        {
            return _db.Sp_MenuListesi().ToList();
        }

        public IEnumerable<Sp_MenuListesiDOM> Sp_MenuListesi(bool aktifMi)
        {
            return _db.Sp_MenuListesi().Where(m => m.AktifMi == aktifMi).ToList();
        }
    }
}
