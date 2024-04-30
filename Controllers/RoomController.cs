    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using hotelManagementSystem20.models;

    namespace hotelManagementSystem20.Controllers
    {
        public class RoomController : Controller
        {
            private HotelManagementSystemEntities4 db = new HotelManagementSystemEntities4();

            // GET: Room
            public ActionResult Index()
            {
                var rooms = db.room.ToList();
                return View(rooms);
            }

            // GET: Room/Details/5
            public ActionResult Details(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                room room = db.room.Find(id);
                if (room == null)
                {
                    return HttpNotFound();
                }
                return View(room);
            }

            // GET: Room/Create
            public ActionResult Create()
            {
                return View();
            }

            // POST: Room/Create
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
        

        // GET: Room/Edit/5
        public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                room room = db.room.Find(id);
                if (room == null)
                {
                    return HttpNotFound();
                }
                return View(room);
            }

            // POST: Room/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit(room room)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(room).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(room);
            }

            // GET: Room/Delete/5
            public ActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                room room = db.room.Find(id);
                if (room == null)
                {
                    return HttpNotFound();
                }
                return View(room);
            }

            // POST: Room/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
                room room = db.room.Find(id);
                db.room.Remove(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
