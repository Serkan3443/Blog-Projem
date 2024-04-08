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
    public class YorumlarRepository:Repository<Yorumlar>,IYorumlarRepository
    {
        BlogSitemDB _db;
        public YorumlarRepository(BlogSitemDB context):base(context)
        {
            _db = context;
        }

        public int AltYorumSayisi(int yorumId)
        {
           // return _db.Yorumlar.Where(k => k.YorumlarID == yorumId).Count();
           return Find(k=>k.YorumlarID == yorumId).Count();
        }

        public IEnumerable<Yorumlar> MakaleYorumListesi(int makaleID)//Bu proceduresiz olan Makalelistesi için oluşturulmuş listeleme methoddudur
        {
            var getir = Find(k => k.Makaleler.MakalelerID == makaleID);
            return getir;
        }

        public IEnumerable<Sp_YorumlarDOM>SpliMakaleYorumlari(int makaleID)//Bu ise procedure ile oluşturulmuş MakaleYorumlari için listeleme yapacak olan methoddur
        {
            var getir=_db.Sp_YorumListesi().Where(y=>y.Makaleler_MakalelerID== makaleID).ToList();//Sonuna Tolist yazamasam proje hata verecektir sonucta procedure ile oluşturduğum verileri listeleyeceğim
            return getir;
        }

        public IEnumerable<Sp_YorumlarDOM> SpliMakaleAltYorumlari(int makaleID)//Bu ise procedure ile oluşturulmuş MakaleYorumlari için listeleme yapacak olan methoddur
        {
            var getir = _db.Sp_YorumListesi().Where(y => y.UstYorumID!=0 && y.Makaleler_MakalelerID==makaleID).ToList();//Sonuna Tolist yazamasam proje hata verecektir sonucta procedure ile oluşturduğum verileri listeleyeceğim
            //y.UstYorumID!=0 dememin sebebi çünkü alt yorumların id si 1 dir ve id`si 0 olanlar üst yorumlardır, aslında ben alt yorumları listelemek istediğim için sıfıra eşit olmasın diyorum yani üst yorumları getirme bana diyorum aslında
            return getir;
        }

        public int MakaleYorumSayisi(int makaleID)
        {
            throw new NotImplementedException();
        }

        public string YorumEkle(string yorum,int ustYorumID, int kullanicilarID, int makalelerID)
        {
            try
            {
                Yorumlar yorumEkle = new Yorumlar();
                yorumEkle.Yorum = yorum;
                yorumEkle.YorumTarihi = DateTime.Now;
                yorumEkle.UstYorumID = ustYorumID;
                yorumEkle.Kullanicilar = _db.Kullanicilar.Where(k => k.KullanicilarID == kullanicilarID).FirstOrDefault();
                yorumEkle.Makaleler = _db.Makaleler.Where(m => m.MakalelerID == makalelerID).FirstOrDefault();
                //_db.yorumlar.Add(yorumEkle);
                //_db.SaveChanges();
                //Yukarıdaki bu iki yapı Repository`de olduğu için kalıtımdan alarak işlem yapıyorum yani yukarıdaki bu iki yapıyı kullanmama gerek yok zaten Repository`de bunlar kayıtlı.Aşağıdaki gib yapıyorum
                Add(yorumEkle);

                return "Yorum Ekleme başarılı";
            }
            catch (Exception)
            {

                return "Yorum ekleme işlemi yapılırken hata oluştu";
            }
        }

        public string YorumGuncelle(int yorumID, string yorum, int ustYorumID, int kullanicilarID, int makalelerID,bool aktifMi)
        {
            var yorumGuncelle = Find(y => y.YorumlarID == yorumID).FirstOrDefault();
            try
            {
                yorumGuncelle.Yorum = yorum;
                yorumGuncelle.YorumTarihi = DateTime.Now;
                yorumGuncelle.UstYorumID = ustYorumID;
                yorumGuncelle.Kullanicilar = _db.Kullanicilar.Where(k => k.KullanicilarID == kullanicilarID).FirstOrDefault();
                yorumGuncelle.Makaleler = _db.Makaleler.Where(m => m.MakalelerID == makalelerID).FirstOrDefault();
                yorumGuncelle.AktifMi = aktifMi;
                return DefinationMessages.Güncelleme_Başarılı.ToString();
            }
            catch (Exception)
            {

                return DefinationMessages.Güncelleme_işlemi_esnasında_hata_oluştu.ToString();
            }
        }

        public IEnumerable<Yorumlar> YorumListesi()
        {
            return GetAll();
        }

        public string YorumSil(int yorumID)
        {
            try
            {
                var pasifEt = Get(yorumID);
                pasifEt.AktifMi = false;

                return DefinationMessages.Pasif_Başarılı.ToString();
            }
            catch (Exception)
            {

                return DefinationMessages.Pasif_Edilirken_Hata_Oluştu.ToString();   
            }
        }

        public IEnumerable<Sp_YorumlarDOM> Sp_YorumListesi(bool aktifMi)
        {
           return _db.Sp_YorumListesi().Where(y=>y.AktifMi==aktifMi).ToList();
        }
    }
}
