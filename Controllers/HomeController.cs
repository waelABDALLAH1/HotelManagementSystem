using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hotelManagementSystem20.models;

namespace hotelManagementSystem20.Controllers
{
    public class HomeController : Controller
    {
        HotelManagementSystemEntities4 db = new HotelManagementSystemEntities4();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(users log)
        {
            if (string.IsNullOrEmpty(log.Username) || string.IsNullOrEmpty(log.Password))
            {
                ModelState.AddModelError("", "Please fill in both username and password fields.");
                return View(log);
            }

            var user = db.users.FirstOrDefault(x => x.Username == log.Username && x.Password == log.Password);
            if (user != null)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(log);
            }
        }

        public ActionResult Dashboard()
        {
            return View();
        }

    }
}
