using EventiApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventiApp.Controllers
{

    public class MenuSelectionsController : Controller
    {
        private DataContext db = new DataContext();


        [HttpPost]
        public ActionResult Create(MenuSelection menuSelection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (menuSelection.IdMenuSelection == 0)
                    {
                        db.MenuSelections.Add(menuSelection);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(menuSelection).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Details","Invitations", new { id = menuSelection.IdInvitation });
                
            }
            catch (Exception ex)
            {
                return RedirectToAction("Details", "Invitations", new { id = menuSelection.IdInvitation});
            }
        }
    
    }
}