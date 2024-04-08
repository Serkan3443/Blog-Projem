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
    public class MakalelerRepository : Repository<Makaleler>, IMakalelerRepository
    {
        BlogSitemDB _db;
        public MakalelerRepository(BlogSitemDB context) : base(context)
        {
            _db = context;
        }

        public string MakaleEkle(int yazarlarID, int makalekategorileriID, string makaleBaslik, string makaleIcerik)
        {
            try
            {
                Makaleler makaleEkle = new Makaleler();
                makaleEkle.MakaleBaslik = makaleBaslik;
                makaleEkle.MakaleIcerik = makaleIcerik;
                makaleEkle.YayinlanmaTarihi =DateTime.Now;
                makaleEkle.Yazarlar = _db.Yazarlar.Where(y => y.YazarlarID == yazarlarID).FirstOrDefault();
                makaleEkle.MakaleKategorileri = _db.MakaleKategorileri.Where(k => k.MakaleKategorileriID == makalekategorileriID).FirstOrDefault();
                Add(makaleEkle);
                return DefinationMessages.Ekleme_Başarılı.ToString();//Artık mesaj yazmak yerine MesajEnumları(DLL katmanında tanımladım) adlı enum classında daha önce tanımladığım hazır mesajları bu şekilde tanımlıyorum
            }
            catch (Exception)
            {

                return DefinationMessages.Ekleme_İşlemi_Esnasında_Hata_Oluştu.ToString();//Artık mesaj yazmak yerine MesajEnumları(DLL katmanında yaptım) adlı enum classında daha önce tanımladığım hazır mesajları bu şekilde tanımlıyorum
            }
        }

        public string MakaleGuncelle(int makaleID, int yazarlarID, int makalekategorileriID, string makaleBaslik, string makaleIcerik,bool aktifMi, int onaylayanKullaniciID)
        {
            var makaleGuncelle = Find(m => m.MakalelerID == makaleID).FirstOrDefault();
            try
            {
                makaleGuncelle.Yazarlar = _db.Yazarlar.Where(y => y.YazarlarID == yazarlarID).FirstOrDefault();
                makaleGuncelle.MakaleKategorileri = _db.MakaleKategorileri.Where(m => m.MakaleKategorileriID == makalekategorileriID).FirstOrDefault();
                makaleGuncelle.YayinlanmaTarihi = DateTime.Now;
                makaleGuncelle.MakaleBaslik = makaleBaslik;
                makaleGuncelle.MakaleIcerik = makaleIcerik;
                makaleGuncelle.AktifMi = aktifMi;
                makaleGuncelle.OnaylayanKullaniciID = onaylayanKullaniciID;
              
                
                //Update(makaleGuncelle);
                return "Makale güncelleme işlemi başarılı";
            }
            catch (Exception)
            {

                return "Makale güncellenirken bir hata oluştu";
            }
        }

        public IEnumerable<Makaleler> MakaleListesi()
        {
            return GetAll();
        }

        public IEnumerable<Makaleler> MakaleListesi(bool aktifMi)
        {
            return Find(m => m.AktifMi == aktifMi);
        }

        public string MakaleSil(int makaleID)
        {
            try
            {
                var pasifEt = Get(makaleID);
                pasifEt.AktifMi = false;
                return DefinationMessages.Pasif_Başarılı.ToString();
            }
            catch (Exception)
            {

                return DefinationMessages.Pasif_Edilirken_Hata_Oluştu.ToString();
            }
        }

        /// <summary>
        /// Aktif olan makaleleri getirir.True aktif olanalrı ve false ise pasif olan makaleleri getirir bu true false değerini MakaleController daki tempdate tanımladığım yerde veriyorum o şeiklde aktif mi pasif olacağını seçiyorum.
        /// </summary>
        /// <param name="aktifMi"></param>
        /// <returns></returns>
        public IEnumerable<Sp_MakaleListesiDOM> Sp_MakaleListesi(bool aktifMi)
        {
            var getSP = _db.Sp_MakaleListesi().Where(k => k.AktifMi == aktifMi).ToList();//Aktif olan makaleleri getir diye şart koymuş
            //Gerek kalmadı yazarlar=>repo
            //Gerek kalmadı kullanicilar=>repo
            //Gerek kalmadı kategoriler=>repo
            //Makaleler=>repo=>4 defa repo çağıracağımıza bir sp ile sadece tek makaleler reposu işimizi görecektir.Yani her bir repository`de ayrı ayrı listeleme oluşturmak yerine codefirst ile bir tane procedure oluşturdum ve bu procedure yazarlar,kullanicilar,kategoriler ve makaleler tablsounu tek bir tablo şeklinde(procedure şeklinde) oluşturup bana getirdi yani procedure oluşturmamın ana amacı bu.Kod fazlalığından ve karmaşıklıktan kurtardı beni.Bunu Makalecontroller sayfasında tanımlayıp daha sonra _Makaleler adlı partial view`de uygulayıp makale sayfası daha da düzenlemiş oldum
            return getSP;
        }


        /// <summary>
        /// Aktif ve pasif olan tüm makaleleri getirir.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Sp_MakaleListesiDOM> Sp_MakaleListesi()
        {
            var getSP = _db.Sp_MakaleListesi();//Bütün makaleleri getir deniliyor.Nerden dersek Sp_MakaleListesi adlı procedureden getir
            return getSP;
        }
    }
}
