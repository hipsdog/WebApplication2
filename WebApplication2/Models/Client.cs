using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Client
    {
        public int ID { get; set; }
        public string Cliente { get; set; }
        public string Tipo { get; set; }
    }
    public class ClientsDBContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
    }
}