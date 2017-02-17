using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string Empleado { get; set; }
        public List<string> GetClients { get; set; }
    }

    public class EmployeesDBContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
    }
}