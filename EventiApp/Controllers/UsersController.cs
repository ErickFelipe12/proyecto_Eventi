using EventiApp.Models;
using EventiApp.Models.Enums;
using EventiApp.Utils;
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
    public class UsersController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Usuarios
        public ActionResult Index()
        {
            var d = db.Users.ToList();
            return View(db.Users.ToList());
        }

        
        public ActionResult Details(int id)
        {
            return View();
        }

        
        public ActionResult Create()
        {
            LoadCombos();
            return View();
        }

        
        [HttpPost]
        public ActionResult Create(User user)
        {
            try{
                if (ModelState.IsValid)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    var password = UserHelper.RandomPassword();
                    UserHelper.CreateUserASP(user.Email, RolesEnum.Client, password);
                    var client = new Client() { IdUser=user.IdUser, NumberEmployees =0, Address = " "};
                    db.Clients.Add(client);
                    db.SaveChanges();
                    //TODO : ENVIAR EMAIL AL CLIENTE CON LA CLAVE
                    return RedirectToAction("Index");
                }
                LoadCombos();
                return View(user);
            }
            catch(Exception ex)
            {
                LoadCombos();
                return View(user);
            }
        }

        private void LoadCombos()
        {
            ViewBag.IdDocumentType = new SelectList(db.Documents.ToList(), "IdDocumentType", "DocumentType");

        }

        
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var users = db.Users.Find(id);

            if (users == null)
            {
                return HttpNotFound();
            }
            LoadCombos();
            return View(users);

        }

        
        [HttpPost]
        public ActionResult Edit(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                LoadCombos();
                return View(user);
            }
            catch
            {
                LoadCombos();
                return View(user);
            }
       }

        


        [HttpPost]
        public ActionResult Employees(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(employee.IdEmployee == 0)
                    {
                        db.Employees.Add(employee);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(employee).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                  
                    return RedirectToAction("Index", "Manage");
                }
                
                return RedirectToAction("Index", "Manage");
            }
            catch
            {
                
                return RedirectToAction("Index", "Manage");
            }
        }

        [HttpPost]
        public ActionResult Client(Client client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (client.IdClient == 0)
                    {
                        db.Clients.Add(client);
                        db.SaveChanges();

                    }
                    else
                    {
                        db.Entry(client).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index", "Manage");
                }
                
                return RedirectToAction("Index", "Manage");
            }
            catch
            {
                
                return RedirectToAction("Index", "Manage");
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
