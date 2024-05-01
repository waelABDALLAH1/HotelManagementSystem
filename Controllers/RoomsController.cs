using hotelManagementSystem20.models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hotelManagementSystem20.Controllers
{
    public class RoomsController : Controller
    {
        private HotelManagementSystemEntities4 db = new HotelManagementSystemEntities4();

        // GET: Rooms
        public ActionResult Index()
        {
            var rooms = db.room.ToList();
            return View(rooms);
        }

        // GET: Rooms/Details/5
        public ActionResult Details(int id)
        {
            var room = db.room.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // GET: Rooms/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(room room)
        {
            if (ModelState.IsValid)
            {

                var nextValQuery = "SELECT NEXT VALUE FOR room_seq;";
                var nextVal = db.Database.SqlQuery<long>(nextValQuery).FirstOrDefault();


                room.room_id = (int)nextVal;


                db.room.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(room);
        }

        // GET: Rooms/Edit/5
        public ActionResult Edit(int id)
        {
            var room = db.room.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Rooms/Edit/5
        [HttpPost]
        public ActionResult Edit(room room)
        {
            try
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.ErrorMessage = "An error occurred while editing the room. Please try again later.";
                return View(room);
            }
        }

        // GET: Rooms/Delete/5
        public ActionResult Delete(int id)
        {
            var room = db.room.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var room = db.room.Find(id);
            db.room.Remove(room);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
