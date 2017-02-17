using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class EmpCli
    {

        public int ID { get; set; }
        public int IDEmp { get; set; }
        public int IDCli { get; set; }
    }

    public class EmpCliDBContext : DbContext
    {
        public DbSet<EmpCli> EmpClis { get; set; }
    }
}