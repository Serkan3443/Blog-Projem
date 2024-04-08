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
    public class MakaleKategorileriRepository : Repository<MakaleKategorileri>, IMakaleKategorileriRepository
    {
        BlogSitemDB _db;
        public MakaleKategorileriRepository(BlogSitemDB context):base(context)
        {
            _db = context;
        }
        public string KategoriEkle(string kategoriAdi, string aciklama)
        {
            try
            {
                MakaleKategorileri kategoriEkle = new MakaleKategorileri();
                kategoriEkle.KategoriAdi = kategoriAdi;
                kategoriEkle.Aciklama = aciklama;
                Add(kategoriEkle);
                return DefinationMessages.Ekleme_Başarılı.ToString();
            }
            catch (Exception)
            {
                return DefinationMessages.Ekleme_İşlemi_Esnasında_Hata_Oluştu.ToString();
                
            }
        }

        public string KategoriGuncelle(int kategoriID, string kategoriAdi, string aciklama, bool aktifMi)
        {
            var kategoriGuncelle = Find(k => k.MakaleKategorileriID == kategoriID).FirstOrDefault();
            try
            {
                kategoriGuncelle.KategoriAdi = kategoriAdi;
                kategoriGuncelle.Aciklama = aciklama;
                kategoriGuncelle.AktifMi = aktifMi;
                Update(kategoriGuncelle);
                return DefinationMessages.Güncelleme_Başarılı.ToString();
            }
            catch (Exception)
            {

                return DefinationMessages.Güncelleme_işlemi_esnasında_hata_oluştu.ToString();
            }
        }

        public int KategoriSayisi(int kategoriID)
        {
           return Find(k=>k.MakaleKategorileriID==kategoriID).Count(); 
        }

        public string KategoriSil(int kategoriID)
        {
            try
            {
                var pasifEt = Get(kategoriID);
                pasifEt.AktifMi = false;
                return DefinationMessages.Pasif_Başarılı.ToString();
            }
            catch (Exception)
            {

               return  DefinationMessages.Pasif_Edilirken_Hata_Oluştu.ToString();
            }
        }

        public IEnumerable<MakaleKategorileri> KategorListesi()
        {
            return GetAll();
        }

        public IEnumerable<Makaleler> MakaleListesi(bool aktifMi)
        {
            throw new NotImplementedException();
        }
    }
}
