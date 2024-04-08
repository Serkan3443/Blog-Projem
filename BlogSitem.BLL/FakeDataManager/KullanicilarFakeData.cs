using BlogSitem.DLL.BlogSiteDatabase;
using BlogSitem.DLL.BlogSiteDatabase.ORMManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSitem.BLL.FakeDataManager
{
    public class KullanicilarFakeData
    {
        public void EkleKullaniciFakeData()
        {
            BlogSitemDB db = new BlogSitemDB();//BlogSitemDB den Kullanicilar classını çağırdım
            if (db.Kullanicilar.ToList().Count > 5)//ve Kullanicilar classımı burdan şöyle çağırdım;diyorumki  kullanıcı sayısı 5 ten fazla olsun ve listele bunu
            {
                return;//eğer Kullanici sayısı 5 ten fazla ise döngüyü sonlandır
            }

            for (int i = 0; i <= 20; i++)
            {
                Kullanicilar kullanicilar = new Kullanicilar();
                string adi = FakeData.NameData.GetFirstName();

                kullanicilar.Adi = adi;
                kullanicilar.Soyadi = FakeData.NameData.GetSurname();
                kullanicilar.KullaniciAdi = adi;
                kullanicilar.KullaniciSifresi = FakeData.ArrayData.GetElement(i).ToString();
                kullanicilar.EklenmeTarihi = DateTime.Now;
                kullanicilar.PasiflikTarihi = DateTime.Now;
                kullanicilar.AktifMi = true;
                kullanicilar.YazarMi = false;
                kullanicilar.Yetkiler = db.Yetkiler.FirstOrDefault();

                db.Kullanicilar.Add(kullanicilar);
            }
            db.SaveChanges();
        }
    }
}
