using EventiApp.Models;
using EventiApp.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EventiApp.Controllers
{
    [Authorize]
    public class InvitationsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Invitaciones
        public ActionResult Index(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var invitations = db.Invitations.Where(a => a.IdEvent == id).ToList();
            if (invitations == null)
            {
                return HttpNotFound();
            }
            return View(invitations);
        }

        
        public ActionResult Details(int id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var invitation = db.Invitations.Include("Event").Where(i => i.IdInvitation == id).FirstOrDefault();
            var menus = db.Menus.Where(m => m.IdEvent == invitation.IdEvent).ToList();
            var selected = invitation.MenuSelections.Count() > 0 ? invitation.MenuSelections.FirstOrDefault().IdMenu : 0;

            ViewBag.IdMenu = new SelectList(menus, "IdMenu", "Description", selected);

            return View(invitation);
        }

        //invitación por aceptar
        public ActionResult Accepted(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var invitation = db.Invitations.Where(i => i.IdInvitation == id).FirstOrDefault();
            if (invitation == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            invitation.Accepted = !invitation.Accepted;
            db.Entry(invitation).State = EntityState.Modified;
            db.SaveChanges();


            return RedirectToAction("Details", new { id = invitation.IdInvitation});
        }

        private List<Event> GetDetailInvitation(int id)
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

        
        public ActionResult Create(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var invitation = new Invitation()
            {
                IdEvent = id
            };
            return View(invitation);
        }

        
        [HttpPost]
        public async Task<ActionResult> Create(Invitation invitation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var invitations = db.Invitations.Where(i => i.EmailEmployee == invitation.EmailEmployee
                                           && i.IdEvent == invitation.IdEvent).FirstOrDefault();

                    if(invitations == null){
                        var user = db.Users.Where(u => u.Email == invitation.EmailEmployee).FirstOrDefault(); 
                        if(user != null)
                            invitation.IdEmployee = user.Employees.FirstOrDefault().IdEmployee;

                        db.Invitations.Add(invitation);
                        db.SaveChanges();
                    };
                   
                    var msjBody = createBodyInvitation(invitation);
                    await MailHelper.SendMail(invitation.EmailEmployee, "Eventi, software para eventos", msjBody);

                    return RedirectToAction("Index", new { id = invitation.IdEvent});
                }
                return View(invitation);
            }
            catch (Exception ex)
            {
                return View(invitation);
            }
        }

        //Cuerpo del mensaje de invitación
        private string createBodyInvitation(Invitation invitation)
        {
            var url = "https://localhost:44300/Account/Register?email=" + invitation.EmailEmployee;
            var html = "<h2>Hola, Haz sido invitado a un evento<h2> </br>" +
                        "<p>visita la siguiente url para confirmar tu asistencia y continuar el proceso, ¡contamos contigo!</p> </br> " +
                        "<a href="+url+ ">Si voy</a>";
                
                
            return html;
        }


        
        public ActionResult Edit(int id)
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult Delete(int id)
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
