using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using hotelManagementSystem20.models;

namespace hotelManagementSystem20.Controllers
{
    public class ClientController : Controller
    {
        private HotelManagementSystemEntities4 db = new HotelManagementSystemEntities4();

        // GET: Client
        public ActionResult Index()
        {
            var clients = db.client.ToList();
            return View(clients);
        }

        // GET: Client/Details/5
        public ActionResult Details(int id)
        {
            var client = db.client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(client client)
        {
            if (ModelState.IsValid)
            {


                var nextValQuery = "SELECT NEXT VALUE FOR client_seq;";
                var nextVal = db.Database.SqlQuery<long>(nextValQuery).FirstOrDefault();


                client.client_id = (int)nextVal;



                db.client.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int id)
        {
            var client = db.client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Client/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int id)
        {
            var client = db.client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var client = db.client.Find(id);
            db.client.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
