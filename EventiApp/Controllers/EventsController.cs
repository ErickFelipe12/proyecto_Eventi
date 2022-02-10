using EventiApp.Models;
using EventiApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EventiApp.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Eventos
        public ActionResult Index()
        {
            var events = new List<Event>();
            if (this.User.IsInRole(RolesEnum.Client))
            {
                var idClient = GetIdClient();
                events = db.Events.Where(e => e.IdClient == idClient).ToList();
            }
            else if (this.User.IsInRole(RolesEnum.Guest))
            {
                events = GetEventsEmployee();
            }
            else
            {
                events = db.Events.ToList();
            }

            return View(events);
        }

        //Próximo evento
        public ActionResult NextEvent()
        {
            var limit = 3;
            var events = new List<Event>();
            if (this.User.IsInRole(RolesEnum.Client))
            {
                var idClient = GetIdClient();
                events = db.Events.Where(e => e.IdClient == idClient).ToList();
            }
            else if (this.User.IsInRole(RolesEnum.Guest))
            {
                events = GetEventsEmployee();
            }
            else
            {
                events = db.Events.ToList();
            }

            return View(events.OrderByDescending(a => a.IdEvent).Take(limit));
        }

        
        
        public ActionResult Details(int id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event eventDetail = db.Events.Find(id);
            if (eventDetail == null)
            {
                return HttpNotFound();
            }
            return View(eventDetail);
        }

        
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Create(Event eventData)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    eventData.IdClient = GetIdClient();
                    db.Events.Add(eventData);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(eventData);
            }
            catch (Exception ex)
            {
                return View(eventData);
            }
        }

        private int GetIdClient()
        {
            var userName = this.User.Identity.Name;
            var client =
                            (from users in db.Users
                             join clients in db.Clients on users.IdUser equals clients.IdUser
                             where users.Email == userName
                             select new { clients.IdClient }).FirstOrDefault();
            return client.IdClient;

        }

        private List<Event> GetEventsEmployee()
        {
            var userName = this.User.Identity.Name;
            var query =
                            (from users in db.Users
                             join employees in db.Employees on users.IdUser equals employees.IdUser
                             join invitations in db.Invitations on employees.IdEmployee equals invitations.IdEmployee
                             join eventDetail in db.Events on invitations.IdEvent equals eventDetail.IdEvent
                             where users.Email == userName
                             select new { eventDetail, invitations.IdInvitation }).ToList();
            var listEvent = new List<Event>();

            foreach (var item in query)
            {
                var addEvent = new Event();
                var lstInvitation = new List<Invitation>();
                lstInvitation.Add(new Invitation() { IdInvitation = item.IdInvitation });
                addEvent = item.eventDetail;
                addEvent.Invitations = lstInvitation;
                listEvent.Add(addEvent);
            }
            return listEvent;

        }

        
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var eventDetail = db.Events.Find(id);

            if (eventDetail == null)
            {
                return HttpNotFound();
            }
            return View(eventDetail);

        }

        
        [HttpPost]
        public ActionResult Edit(Event eventDetail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(eventDetail).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(eventDetail);
            }
            catch
            {
                return View(eventDetail);
            }
        }


        
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var eventDetail = db.Events.Find(id);

            if (eventDetail == null)
            {
                return HttpNotFound();
            }
            return View(eventDetail);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event eventDelete = db.Events.Find(id);
            db.Events.Remove(eventDelete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
