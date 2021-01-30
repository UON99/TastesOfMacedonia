
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
    public class HomeController : Controller
    {

        ResApi api = new ResApi();

        public void GetReservations(List<reservation> reservations, HttpResponseMessage res)
        {

            var results = res.Content.ReadAsStringAsync().Result;
            reservations = JsonConvert.DeserializeObject<List<reservation>>(results);
            reservations = reservations.ToList();

        }


        public async Task<ActionResult> Index()
        {
            List<reservation> reservations = new List<reservation>();
            HttpClient client = api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/reservations");
            if (res.IsSuccessStatusCode)
            {
                GetReservations(reservations, res);
            }

            return View(reservations);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Reservations()
        {
            return View();
        }
    }
}