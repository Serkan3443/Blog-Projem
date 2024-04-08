using BlogSitem.BLL.IRepositories;
using BlogSitem.DLL.BlogSiteDatabase;
using BlogSitem.DLL.BlogSiteDatabase.ORMManager;
using BlogSitem.DLL.MesajEnumları;
using BlogSitem.DLL.RepositoryManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSitem.BLL.Repositories
{
    public class YetkilerRepository:Repository<Yetkiler>,IYetkilerRepository
    {
        BlogSitemDB _db;

        public YetkilerRepository(BlogSitemDB context):base(context) 
        {
            _db =context;
        }

        public string YetkiEkle(string yetkiAdi)
        {
            try
            {
                Yetkiler yetkiEkle = new Yetkiler();
                yetkiEkle.YetkiAdi = yetkiAdi;
                Add(yetkiEkle);
                return DefinationMessages.Ekleme_Başarılı.ToString();
            }
            catch (Exception)
            {

                return DefinationMessages.Eklenirken_Hata_Olustu.ToString();
            }
        }

        public string YetkiGuncelle(int yetkilerID, string yetkiAdi, bool aktifMi)
        {
            var yetkiGuncelle = Find(y => y.YetkilerID == yetkilerID).FirstOrDefault();
            try
            {
                yetkiGuncelle.YetkiAdi = yetkiAdi;
                yetkiGuncelle.AktifMi = aktifMi;
                return DefinationMessages.Güncelleme_Başarılı.ToString();
            }
            catch (Exception )
            {

                return DefinationMessages.Güncelleme_işlemi_esnasında_hata_oluştu.ToString();
            }
        }

        public IEnumerable<Yetkiler> YetkiListesi()
        {
            return GetAll();
        }

        public string YetkiSil(int yetkilerID)
        {
            try
            {
                var pasifEt = Get(yetkilerID);
                pasifEt.AktifMi = false;

                return DefinationMessages.Pasif_Başarılı.ToString();
            }
            catch (Exception)
            {

                return DefinationMessages.Pasif_Edilirken_Hata_Oluştu.ToString();
            }
        }
    }
}
