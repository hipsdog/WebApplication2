using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class NewsController : Controller
    {
        private NewsDBContext db = new NewsDBContext();
        private EmployeesDBContext db2 = new EmployeesDBContext();


        // GET: News
        public ActionResult Index(string empleado, string cliente)
        {

            ViewBag.Empleado = (from r in db.News select r.Empleado).Distinct();
            ViewBag.Cliente = (from r in db.News select r.Cliente).Distinct();
            ViewBag.Fecha = (from r in db.News select r.Fecha).Distinct();

            var news = from m in db.News
                       orderby m.Empleado
                       where m.Cliente == cliente || cliente == null || cliente == ""
                       where m.Empleado == empleado || empleado == null || empleado == ""
                       select m;

            return View(news);
        }

        // GET: News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            New @new = db.News.Find(id);
            if (@new == null)
            {
                return HttpNotFound();
            }
            return View(@new);
        }

        // GET: News/Create
        public ActionResult Create()
        {
            ViewBag.Empleado = (from r in db2.Employees select r.Empleado).Distinct();
            ViewBag.Cliente = (from r in db2.Employees select r.Cliente).Distinct();
            return View();
        }

        // POST: News/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.

        //public ActionResult Create(New FormData)

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,Noticia,Fecha,Cliente,Empleado")] New @new)
        {



            //Console.WriteLine(@new);
            if (ModelState.IsValid)
            {
                db.News.Add(@new);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
        public ActionResult CreateEmployee([Bind(Include = "ID,Empleado")] New @new)
        {
            if (ModelState.IsValid)
            {
                db.News.Add(@new);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@new);
        }

        // GET: News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            New @new = db.News.Find(id);
            if (@new == null)
            {
                return HttpNotFound();
            }
            return View(@new);
        }

        // POST: News/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Noticia,Fecha,Cliente,Empleado")] New @new)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@new).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@new);
        }

        // GET: News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            New @new = db.News.Find(id);
            if (@new == null)
            {
                return HttpNotFound();
            }
            return View(@new);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            New @new = db.News.Find(id);
            db.News.Remove(@new);
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
