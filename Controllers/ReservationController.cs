using hotelManagementSystem20.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hotelManagementSystem20.Controllers
{
    public class ReservationController : Controller
    {
        private HotelManagementSystemEntities4 db = new HotelManagementSystemEntities4();
        // GET: Reservation
        public ActionResult Index()
        {
            var reservations = db.Reservation.ToList();
            return View(reservations);
         
        }
        public ActionResult Details(int id)
        {
            var reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }



    }
}