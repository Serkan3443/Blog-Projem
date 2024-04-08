using BlogSitem.DLL.BlogSiteDatabase;
using BlogSitem.DLL.BlogSiteDatabase.ORMManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSitem.BLL.FakeDataManager
{
    public class MakalelerFakeData
    {
        public void EkleMakaleFakeData()
        {
            BlogSitemDB db = new BlogSitemDB();
            
            if (db.Makaleler.ToList().Count > 25)
            {
                return;
            }

            for (int i = 0; i <= 26; i++)
            {
                Makaleler makaleler = new Makaleler();
                makaleler.MakaleBaslik = FakeData.NameData.GetCompanyName();
                makaleler.MakaleIcerik = FakeData.TextData.GetSentences(1000);
                makaleler.YayinlanmaTarihi = DateTime.Now;
                makaleler.AktifMi = true;
                //makaleler.OnaylayanKullaniciID = FakeData.NumberData.GetNumber();
                makaleler.OnaylayanKullaniciID = db.Kullanicilar.Where(k => k.Yetkiler.YetkiAdi == "Admin").FirstOrDefault().KullanicilarID;//Burda şu deniliyor eğer yetkili kişi admin ise makalenin onaylamasına izin verip vermemesine admine kalsın yani o seçsin
                makaleler.Yazarlar = db.Yazarlar.ToList().FirstOrDefault();
                makaleler.MakaleKategorileri = db.MakaleKategorileri.ToList().FirstOrDefault();

                db.Makaleler.Add(makaleler);
            }
            db.SaveChanges();
        }

    }
}
