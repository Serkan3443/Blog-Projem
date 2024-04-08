using BlogSitem.DLL.BlogSiteDatabase;
using BlogSitem.DLL.BlogSiteDatabase.ORMManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSitem.BLL.FakeDataManager
{
    public class YazarlarFakeData
    {
        public void EkleYazarFakeData()
        {
            BlogSitemDB db = new BlogSitemDB();
            if (db.Yazarlar.ToList().Count > 5)
            {
                return;
            }
            for (int i = 0; i <= 10; i++)
            {
                Yazarlar yazarlar = new Yazarlar();
                yazarlar.YazarAdi = FakeData.NameData.GetFirstName();
                yazarlar.YazarSoyadi = FakeData.NameData.GetSurname();
                yazarlar.Email = FakeData.NetworkData.GetEmail();
                yazarlar.KisacaHakkinda = FakeData.TextData.GetSentences(200);
                yazarlar.CalistigiFirma = FakeData.NameData.GetCompanyName();
                yazarlar.YasadigiUlke = FakeData.PlaceData.GetCounty();
                yazarlar.YasadigiSehir = FakeData.PlaceData.GetCity();


                db.Yazarlar.Add(yazarlar);
            }
            db.SaveChanges();
        }


    }
}
