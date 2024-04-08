using BlogSitem.DLL.BlogSiteDatabase;
using BlogSitem.DLL.BlogSiteDatabase.ORMManager;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSitem.BLL.Manager
{
    public class MakaleManager
    {
        public List<Makaleler> MakalelerListesi()
        {
            BlogSitemDB blogSitemDB = new BlogSitemDB();
            var list = blogSitemDB.Makaleler.ToList();
            return list;
        }
 
    }
}
