using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class mytablesController : Controller
    {
        private masterEntities db = new masterEntities();


        // GET: mytables
        public ActionResult Index()
        {
                return View(db.mytables.ToList());
            
        }
        
            
    
        // GET: mytables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mytable mytable = db.mytables.Find(id);
            if (mytable == null)
            {
                return HttpNotFound();
            }
            return View(mytable);
        }

        // GET: mytables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: mytables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,lon,lat,name,cuisine,opening_hours,website,phone")] mytable mytable)
        {
            if (ModelState.IsValid)
            {
                db.mytables.Add(mytable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mytable);
        }

        // GET: mytables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mytable mytable = db.mytables.Find(id);
            if (mytable == null)
            {
                return HttpNotFound();
            }
            return View(mytable);
        }

        // POST: mytables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,lon,lat,name,cuisine,opening_hours,website,phone")] mytable mytable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mytable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mytable);
        }

        // GET: mytables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mytable mytable = db.mytables.Find(id);
            if (mytable == null)
            {
                return HttpNotFound();
            }
            return View(mytable);
        }

        // POST: mytables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            mytable mytable = db.mytables.Find(id);
            db.mytables.Remove(mytable);
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
