using hotelManagementSystem20.models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace hotelManagementSystem20.Controllers
{
    public class ReservationController : Controller
    {
        private HotelManagementSystemEntities3 db = new HotelManagementSystemEntities3();
        // GET: Reservation
        public ActionResult Index()
        {
            var reservations = db.Reservation.ToList();
            return View(reservations);
         
        }

        public ActionResult check(Reservation reservation)
        {

            // Récupérer les détails des chambres disponibles pour la période spécifiée
            var availableRooms = db.room
                .Where(r => !db.ReservationDetail
                                .Any(rd => db.ReservationDetail
                                                .Any(res => res.Reservation_ID == rd.Reservation_ID &&
                                                            res.DateDepart <= reservation.dateArrivee &&
                                                            res.DateArrivee >= reservation.dateDepart) &&
                                            rd.Room_ID == r.room_id))
                .ToList();
            ViewBag.ReservationId = reservation.Reservation_Id;

            // Exécuter la requête de mise à jour pour le nombre de chambres réservées
            string updateChambreQuery = $"UPDATE Reservation SET nombreChambres = (SELECT COUNT(*) FROM ReservationDetail WHERE Reservation_ID = {reservation.Reservation_Id});";
            db.Database.ExecuteSqlCommand(updateChambreQuery);

            // Exécuter la requête de mise à jour pour le prix total
            string updatePrixQuery = $"UPDATE Reservation SET prixTotale = (SELECT SUM(r.price) FROM Room r, Reservation res, ReservationDetail rd WHERE rd.Reservation_ID = {reservation.Reservation_Id} AND r.Room_id = rd.Room_id);";
            db.Database.ExecuteSqlCommand(updatePrixQuery);
            return View(availableRooms);
        }

        
    public ActionResult Add(int roomId, int reservationId)
        {
            // Récupérer les informations nécessaires pour créer une instance de ReservationDetail
            var room = db.room.Find(roomId);
            var reservation = db.Reservation.Find(reservationId);


            var dateDepart = reservation.dateDepart;
            var dateArrivee = reservation.dateArrivee;

            // Créer une nouvelle instance de ReservationDetail
            ReservationDetail reservationDetail = new ReservationDetail
            {
                Room_ID = roomId,
                Reservation_ID = reservationId,
                DateDepart = dateDepart,
                DateArrivee = dateArrivee
            };

            // Ajouter la nouvelle instance de ReservationDetail à la base de données
            db.ReservationDetail.Add(reservationDetail);
            db.SaveChanges();

            // Rediriger vers une vue ou une action appropriée
            return RedirectToAction("check",reservation); 
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

        // GET: Reservation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reservation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reservation reservation)
        {
            if (ModelState.IsValid)
            {

                var nextValQuery = "SELECT NEXT VALUE FOR Reservation_seq;";
                var nextVal = db.Database.SqlQuery<long>(nextValQuery).FirstOrDefault();
                var dateQuery = "SELECT getDate();";
                var date = db.Database.SqlQuery<DateTime>(dateQuery).FirstOrDefault();

                reservation.Reservation_Id = (int)nextVal;
                reservation.DateReservation = date;


                db.Reservation.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("check",reservation);
            }

            return View(reservation);
        }


        // GET: Reservation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reservation);
        }

        // GET: Reservation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservation.Find(id);
            db.Reservation.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }





    }
}