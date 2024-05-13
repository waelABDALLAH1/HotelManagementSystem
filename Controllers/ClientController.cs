using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using hotelManagementSystem20.models;

namespace hotelManagementSystem20.Controllers
{
    public class ClientController : Controller
    {
        private HotelManagementSystemEntities1 db = new HotelManagementSystemEntities1();

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
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            client client = db.client.Find(id);
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
                try
                {
                    // Récupérer l'entité client depuis la base de données
                    var existingClient = db.client.Find(client.client_id);

                    // Vérifier si l'entité existe
                    if (existingClient != null)
                    {
                        // Mettre à jour les propriétés de l'entité existante avec les nouvelles valeurs
                        db.Entry(existingClient).CurrentValues.SetValues(client);

                        // Enregistrer les modifications
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Gérer le cas où l'entité n'existe pas (peut-être a été supprimée)
                        ModelState.AddModelError("", "Le client que vous essayez de modifier n'existe pas.");
                        return View(client);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Gérer le conflit de concurrence ici
                    // Par exemple, recharger l'entité à partir de la base de données et afficher un message à l'utilisateur
                    db.Entry(client).Reload();
                    ModelState.AddModelError("", "La modification que vous essayez d'apporter a été modifiée par un autre utilisateur. Veuillez réessayer.");
                    return View(client);
                }
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
