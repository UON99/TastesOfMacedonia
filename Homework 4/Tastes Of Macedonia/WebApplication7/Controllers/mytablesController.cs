using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication7.Helper;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{

    public class MytablesController : Controller
    {

        private readonly masterEntities db = new masterEntities();

        // GET: mytables


        [Authorize]
        public ActionResult Index()
        {

            //Showing main Restaurant view, using joint index to simultanously show favorites, restaurants and ratings.
            var joint_index = new JointIndex
            {
                favorites = db.favorites.ToList(),
                mytables = db.mytables.ToList(),
                ratings = db.Ratings.ToList()
            };
            ViewBag.userID = User.Identity.GetUserId();

            return View(joint_index);

        }

        //This method is used to add entries to the favorites db.
        public ActionResult AddToFavorites(long id)
        {

            var restaurant = db.mytables.Find(id);
            var favorite = new favorite
            {
                restaurant_name = restaurant.name,
                user = User.Identity.GetUserId()
            };

            db.favorites.Add(favorite);
            db.SaveChanges();

            return RedirectToAction("Index");


        }
        //This is a method used to Remove entries from the favorites db.
        public ActionResult RemoveFromFavorites(long? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            var favorite = db.favorites.Find(id);
            db.favorites.Remove(favorite);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        //This GET method is used to add Ratings.
        public ActionResult AddRating(long? id)
        {

            if (id == null) 
                return View();


            var restaurant = db.mytables.Find(id);

            ViewBag.restaurant_name = restaurant.name;
            Rating rating = new Rating
            {
                RestaurantId = restaurant.id,
                user = User.Identity.GetUserId()
            };

            return View(rating);
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
        public ActionResult MakeReservation(long? id)
        {

            if (id == null)
                return View();


            var restaurant = db.mytables.Find(id);
            var reservation = new reservation
            {
                restaurant_name = restaurant.name,
                user = User.Identity.GetUserId()
            };

            return View(reservation);

        }

        //POST method for making a reservation
        [HttpPost]
        public ActionResult MakeReservation(reservation reservation)
        {
            using (var client = ResApi.Initial())
            {
                //HTTP POST
                var postTask = client.PostAsJsonAsync<reservation>("api/reservations", new reservation()
                {
                    Id = reservation.Id,
                    restaurant_name = reservation.restaurant_name,
                    datetime = reservation.datetime,
                    user = User.Identity.GetUserId(),
                });
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
            HttpClient client = ResApi.Initial();
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
