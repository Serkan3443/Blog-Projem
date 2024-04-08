using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog_Sitem.Models
{
    public class UyariMesajlari
    {
        public string Basarili(string mesaj)
        {
            return @"<div class='alert alert-success alert-dismissible'>
                  <button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>
                  <h5><i class='icon fas fa-check'></i>Alert!</h5>" + mesaj + "</div>";



           
        }

        public string Hatali(string mesaj)
        {
            return @"<div class='alert alert-danger alert-dismissible'>
                  <button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>
                  <h5><i class='icon fas fa-ban'></i> Alert!</h5>" + mesaj + "</div>";
        }
    }
}