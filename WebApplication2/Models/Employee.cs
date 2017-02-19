using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Employee
    {
        public int ID { get; set; }

        [Required]
        public string Empleado { get; set; }

        [Required]
        public List<string> GetClients { get; set; }
    }

    public class Aux
    {
        public int empIdAux { get; set; }
        public string empAux { get; set; }
        public string cliAux { get; set; }

    }

    public class EmployeesDBContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
    }
}