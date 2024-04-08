using BlogSitem.BLL.Repositories;
using BlogSitem.DLL.BlogSiteDatabase;
using BlogSitem.DLL.BlogSiteDatabase.ORMManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSiteUnitTest
{
    public class MakalelerUnit
    {
        BlogSitemDB db;
        MakalelerRepository makalelerRepository;

        public MakalelerUnit()
        {
            db=new BlogSitemDB();
            makalelerRepository=new MakalelerRepository(db);
        }

        public IEnumerable<Makaleler> MakaleListesi()
        {
            var list=makalelerRepository.GetAll();
            return list;
        }
    }
}
