using EventiApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
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
        public ActionResult Create(Menu menu, HttpPostedFileBase Photo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    /* db.Menus.Add(menu);
                     db.SaveChanges();*/
                    if (Photo != null)
                    {
                        string name = RandoPhotoName();
                        var ubiFinal = Server.MapPath("~/Content/menus/" + name);
                        Photo.SaveAs(ubiFinal);
                        menu.Photo = name;
                    }

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

        public string RandoPhotoName()
        {
            var random = new Random();
            string chars = WebConfigurationManager.AppSettings["CharsPassWord"];
            return new string(Enumerable.Repeat(chars, 20)
              .Select(s => s[random.Next(s.Length)]).ToArray()) + ".jpg";
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
        public ActionResult Edit(Menu menu, HttpPostedFileBase NewPhoto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (NewPhoto != null)
                    {
                        if (menu.Photo != null)
                            System.IO.File.Delete(Server.MapPath("~/Content/IconosCategoria/" + menu.Photo));

                        string name = RandoPhotoName();
                        var ubiFinal = Server.MapPath("~/Content/menus/" + name);
                        NewPhoto.SaveAs(ubiFinal);
                        menu.Photo = name;
                    }

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


        public ActionResult Reporte(int id)
        {
            List<Menu> menus = new List<Menu>();
            menus = db.Menus.Where(m => m.IdEvent == id).ToList();
            return new Rotativa.ViewAsPdf("Reporte", menus);

        }

    }
}