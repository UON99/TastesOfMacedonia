using Microsoft.AspNet.Identity;
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
        private int reserveid = 0;
        private masterEntities db = new masterEntities();
        private ApplicationDbContext accdb = new ApplicationDbContext();

        // GET: mytables
        [Authorize]
        public ActionResult Index()
        {

            JointIndex ji = new JointIndex();
            ji.favorites = db.favorites.ToList();
            ji.mytables = db.mytables.ToList();
            ji.ratings = db.Ratings.ToList();
            ViewBag.userID= User.Identity.GetUserId();
            
            return View(ji);


        }
        
        public ActionResult AddToFavorites(long id)
        {
            
                mytable mytable = db.mytables.Find(id);
                favorite fav = new favorite();

                fav.restaurant_name = mytable.name;
                fav.user = User.Identity.GetUserId();
                db.favorites.Add(fav);
                db.SaveChanges();
                return RedirectToAction("Index");

            
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult MakeReservation([Bind(Include = "Id,user,,restaurant_name")] favorite favorite)
        //{
        //    var favorite1= new favorite();
        //    var userID = User.Identity.GetUserId();
        //    favorite1.restaurant_name = favorite.restaurant_name;
        //    favorite1.Id = favorite.Id;
        //    favorite1.user = userID;
        //    db.favorites.Add(favorite1);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        
        public ActionResult RemoveFromFavorites(long id)
        {
            
                favorite fav = db.favorites.Find(id);
                db.favorites.Remove(fav);
                db.SaveChanges();
                return RedirectToAction("Index");

            
            //return View();
        }
        public ActionResult AddRating(long? id)
        {
            
            if (id != null)
            {
            mytable mytable = db.mytables.Find(id);
            Rating rating = new Rating();
                ViewBag.restaurant_name = mytable.name;
                rating.RestaurantId = mytable.id;
                rating.user = User.Identity.GetUserId();

                return View(rating);

            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRating([Bind(Include = "id,RestaurantId,rating1,user")] Rating rating)
        {
            var rate = new Rating();
            
            var user = User.Identity.GetUserId();

                rate.rating1 = rating.rating1;
                rate.RestaurantId = rating.RestaurantId;
                rate.user = User.Identity.GetUserId();
                db.Ratings.Add(rate);
                db.SaveChanges();
                return RedirectToAction("Index");
        }

        public ActionResult MakeReservation(long id)
        {
            
            if (id != null)
            {
            mytable mytable = db.mytables.Find(id);
            reservation reservation = new reservation();
           
            reservation.restaurant_name = mytable.name;
            reservation.user= User.Identity.GetUserId();
            return View(reservation);

            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeReservation([Bind(Include = "Id,user,,restaurant_name,datetime")] reservation reservation)
        {
            var reserve = new reservation();
            
            var userID = User.Identity.GetUserId();
                reserve.restaurant_name = reservation.restaurant_name;
                reserve.datetime = reservation.datetime;
                reserve.Id = reservation.Id;
                reserve.user = userID;     
                db.reservations.Add(reserve);
                db.SaveChanges();
                return RedirectToAction("AddToReservations");
        }
        [Authorize]
        public ActionResult AddToReservations()
        {  
            var userID = User.Identity.GetUserId();

            ViewBag.userid = userID;

            return View(db.reservations.ToList());

               
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
