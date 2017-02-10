using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Models
{
    public class New 
    {
        public int ID { get; set; }

        [AllowHtml]
        public string Noticia { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string Empleado { get; set; }
    }

    public class NewsDBContext: DbContext
    {
        public DbSet<New> News { get; set; }
    }
}