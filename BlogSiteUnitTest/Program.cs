using BlogSitem.DLL.BlogSiteDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSiteUnitTest
{
    public class Program
    {
        static void Main(string[] args)
        {
             Console.WriteLine(CreateSP());//Metodu çağırıyorum anlamına gelir
            //Makale();
           
            Console.Read();
        }

        //Bu class Makaleler için test işlemi olarak uygulandı yani hata verip vermeme testi
        public static void Makale()
        {
            MakalelerUnit makaleTest = new MakalelerUnit();
            foreach (var item in makaleTest.MakaleListesi())
            {
                string metin = item.MakaleIcerik;
                if (item.MakaleIcerik.Count() > 250)
                {
                    metin = item.MakaleIcerik.Substring(0, 100);
                }
                Console.WriteLine("ID: " + item.MakalelerID + "\t\tBaşlık: " + item.MakaleBaslik + "\t\tİçerik: " + metin);
            }
        }
        //Bu metod sp procedure için oluşturuldu yani hata verip vermemesi şeklinde uyarı vermesi yani test etmesi için bu katmanda(BlogSiteUnitTest katmanı),bu class içinde(CreateSP()) oluşturuldu
        public static string CreateSP()
        {
            ExistsStoredProcedure createSP = new ExistsStoredProcedure();

            var createMenuSp = createSP.Sp_MenuListesi();
            var createMakaleSp = createSP.Sp_MakaleListesi();

            return createSP.Sp_MakaleListesi();
        }

    }
}
