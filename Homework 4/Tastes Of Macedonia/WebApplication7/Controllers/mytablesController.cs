using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication7.Helper;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{

    public class mytablesController : Controller
    {
        
        private masterEntities db = new masterEntities();

        // GET: mytables

        ResApi api = new ResApi();

     
        [Authorize]
        public ActionResult Index()
        {


            //Showing main Restaurant view, using joint index to simultanously show favorites, restaurants and ratings.
            JointIndex joint_index = new JointIndex();
            joint_index.favorites = db.favorites.ToList();
            joint_index.mytables = db.mytables.ToList();
            joint_index.ratings = db.Ratings.ToList();
            ViewBag.userID= User.Identity.GetUserId();
            
            return View(joint_index);


        }

        //This method is used to add entries to the favorites db.
        public ActionResult AddToFavorites(long id)
        {
            
                mytable restaurant = db.mytables.Find(id);
                favorite favorite = new favorite();

                favorite.restaurant_name = restaurant.name;
                favorite.user = User.Identity.GetUserId();
                db.favorites.Add(favorite);
                db.SaveChanges();
                return RedirectToAction("Index");

            
        }
        //This is a method used to Remove entries from the favorites db.
        public ActionResult RemoveFromFavorites(long id)
        {
            
                favorite favorite = db.favorites.Find(id);
                db.favorites.Remove(favorite);
                db.SaveChanges();
                return RedirectToAction("Index");

            
            //return View();
        }

        //This GET method is used to add Ratings.
        public ActionResult AddRating(long? id)
        {
            
            if (id != null)
            {
            mytable restaurant = db.mytables.Find(id);
            Rating rating = new Rating();
                ViewBag.restaurant_name = restaurant.name;
                rating.RestaurantId = restaurant.id;
                rating.user = User.Identity.GetUserId();

                return View(rating);

            }
            return View();
        }
        //This is the POST method to add Rating
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
        
        //This is a GET method for making a reservation
        public ActionResult MakeReservation(long id)
        {
            
            if (id != null)
            {
            mytable restaurant = db.mytables.Find(id);
            reservation reservation = new reservation();
            
            reservation.restaurant_name = restaurant.name;
            reservation.user= User.Identity.GetUserId();

            return View(reservation);

            }
            return View();
        }

        //POST method for making a reservation
        [HttpPost]
        public ActionResult MakeReservation(reservation reservation)
        {
            using (var client = api.Initial())
            {               
                //HTTP POST
                var postTask = client.PostAsJsonAsync<reservation>("api/reservations", new reservation()
                {
                    Id = reservation.Id,
                    restaurant_name = reservation.restaurant_name,
                    datetime = reservation.datetime,
                    user = User.Identity.GetUserId(),
            }) ;
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(reservation);
        }

        //Method for Adding a reservation
        [Authorize]
        public async Task<ActionResult> AddToReservations()
        {
            List<reservation> reservations = new List<reservation>();
            HttpClient client = api.Initial();
            HttpResponseMessage reservationMessage = await client.GetAsync("api/reservations");
            if (reservationMessage.IsSuccessStatusCode)
            {
                var results = reservationMessage.Content.ReadAsStringAsync().Result;
                reservations = JsonConvert.DeserializeObject<List<reservation>>(results);
                reservations = reservations.ToList();
            }

            var user_id = User.Identity.GetUserId();

            ViewBag.user_id = user_id;

            return View(reservations);
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
