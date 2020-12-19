using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class mytablesController : Controller
    {
        private masterEntities db = new masterEntities();
        private ApplicationDbContext accdb = new ApplicationDbContext();
        
        // GET: mytables
        public ActionResult Index()
        {
            return View(db.mytables.ToList());
           
        }

        public ActionResult Reservations() {
            return View("AddToReservations");
        }
       
        public ActionResult MakeReservation(long id)
        {
            /*
            FrontPageViewData viewData = new FrontPageViewData();
            viewData.Mytable = db.mytables.Find(id);
            viewData.db = db;  /// celata baza so site restaurants reservations i favorites
            var currentuser = System.Web.HttpContext.Current.User.Identity.Name;  /// ova go dobiva logiraniot user mozes i toa da go stavis vo viewdata.
           
            return View(viewData);
            */

            if (id != null)
            {
            mytable mytable = db.mytables.Find(id);
            Item item = new Item();
           
            item.restaurant = mytable.name;
            return View(item);

            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeReservation([Bind(Include = "restaurant,time")] Item item)
        {
            if (Session["reservation"] != null)
            {
                var reservations = (List<Item>)Session["reservation"];
                //mytable mytable = db.mytables.Find(id);
                
                int flag = 0;
                for (int i = 0; i < reservations.Count; i++)
                {
                    if (item.restaurant == reservations[i].restaurant)
                    {
                        flag = 1;
                    }
                    else {
                        flag = 0;
                    }
                }
                if (flag != 1)
                {
                    reservations.Add(new Item()
                    {
                        restaurant = item.restaurant,
                        time = item.time
                        
                    });
                }

                Session["reservation"] = reservations;
                return View("AddToReservations");
            }
            else
            {
                var reservations = new List<Item>();
                //mytable mytable = db.mytables.Find(id);
             

                reservations.Add(new Item()
                {
                    restaurant = item.restaurant,
                    time = item.time

                });
                Session["reservation"] = reservations;
                return View("AddToReservations");
            }
        }
        public ActionResult AddToReservations(long id)
        {
            if (Session["reservation"] != null)
            {
                var reservations = (List<Item>)Session["reservation"];
                mytable mytable = db.mytables.Find(id);
                
                if (mytable == null)
                {
                    return HttpNotFound();
                }
                int flag = 0;
                for (int i = 0; i < reservations.Count; i++) {
                    if (mytable.name == reservations[i].restaurant) {
                        flag = 1;
                    }
                }
                if (flag != 1) {
                    reservations.Add(new Item()
                    {
                        restaurant = mytable.name,
                        time = DateTime.UtcNow

                    });
                }
                
                Session["reservation"] = reservations;
                return View();
            }
            else {
                var reservations = new List<Item>();
                mytable mytable = db.mytables.Find(id);
                

                if (mytable == null)
                {
                    return HttpNotFound();
                }

                reservations.Add(new Item()
                {
                    restaurant = mytable.name,
                    time = DateTime.UtcNow

                });
                Session["reservation"] = reservations;
                return View();
            }
            
            
            
        }
        // GET: mytables/Details/5
        public ActionResult Details(long? id)
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
        public ActionResult Edit(long? id)
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
        public ActionResult Delete(long? id)
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
        public ActionResult DeleteConfirmed(long id)
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
