using EventiApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Rotativa;

namespace EventiApp.Controllers
{
    public class MenusController : Controller
    {
        private DataContext db = new DataContext();

        public ActionResult Index(int id)
        {
            List<Menu> menus = new List<Menu>();
            menus = db.Menus.Where(m => m.IdEvent == id).ToList();
            return View(menus);

        }


        public ActionResult Details(int id)
        {
            Menu menus = new Menu();
            menus = db.Menus.Find(id);
            return View(menus);

        }

        
        public ActionResult Create(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var menu = new Menu()
            {
                IdEvent = id
            };
            return View(menu);
        }

        
        [HttpPost]
        public ActionResult Create(Menu menu)
        {
            try
            {
                if (ModelState.IsValid)
                {
                     db.Menus.Add(menu);
                     db.SaveChanges();
                    return RedirectToAction("Index", new { id = menu.IdEvent });
                }
                return View(menu);
            }
            catch (Exception ex)
            {
                return View(menu);
            }
        }
        
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var menu = db.Menus.Find(id);

            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);

        }

        
        [HttpPost]
        public ActionResult Edit(Menu menu)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(menu).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index",new { id = menu.IdEvent });
                }
                return View(menu);
            }
            catch
            {
                return View(menu);
            }
        }

        
         
          public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var menuDetail = db.Menus.Find(id);

            if (menuDetail == null)
            {
                return HttpNotFound();
            }
            return View(menuDetail);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menuDelete = db.Menus.Find(id);
            db.Menus.Remove(menuDelete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Imprimir()
        {
            return new ActionAsPdf("Index")
            { FileName = "Reporte Menus"};
        }

    }
}