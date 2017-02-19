using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class EmployeesController : Controller
    {
        private EmployeesDBContext db = new EmployeesDBContext();
        private ClientsDBContext dbClients = new ClientsDBContext();
        private EmpCliDBContext dbEmpCli = new EmpCliDBContext();


        // GET: Employees
        //[Authorize]
        public ActionResult Index()
        {
            List<Aux> model = new List<Aux>();

            var Emp = db.Employees.ToList();
            var ec = dbEmpCli.EmpClis.ToList();
            var Cli = dbClients.Clients.ToList();
            var q = from x in Emp.AsEnumerable()
                    join y in ec.AsEnumerable() on x.ID equals y.IDEmp
                    join z in Cli.AsEnumerable() on y.IDCli equals z.ID
                    select new { empA = x, cliA = z };

            foreach (var item in q)
            {
                model.Add(new Aux()
                {
                    empAux = item.empA.Empleado,
                    empIdAux = item.empA.ID,
                    cliAux = item.cliA.Cliente
                });
            }


            return View(model);


        }

        // GET: Employees/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Cliente = (from r in dbClients.Clients select r.Cliente).Distinct();
            ViewBag.Empleado = (from r in db.Employees select r.Empleado).Distinct();
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,Empleado,GetClients")] Employee employee)
        {
            ViewBag.Cliente = (from r in dbClients.Clients select r.Cliente).Distinct();

            EmpCli empCli = new EmpCli();

            var aux = employee;
            int count = 0;
            if (employee.GetClients != null)
            {
                count = employee.GetClients.Count;
            }
            else
            {
                return View(employee);
            }

            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
            }

            empCli.IDEmp = employee.ID;


            for (int i = 0; i < count; i++)
            {

                var der = aux.GetClients[i];
                var a = dbClients.Clients.First(s => s.Cliente == der).ID;
                empCli.IDCli = a;
                dbEmpCli.EmpClis.Add(empCli);
                dbEmpCli.SaveChanges();


                if (i == (count - 1))
                {
                    return RedirectToAction("Index");

                }

            }
            return View(employee);

        }

        // GET: Employees/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            ViewBag.Cliente = (from r in dbClients.Clients select r.Cliente).Distinct();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,Empleado,Cliente")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
