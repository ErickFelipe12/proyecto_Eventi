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
    [Authorize(Roles = RolesEnum.Administrator)]
    public class DocumentsController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Documentos
        public ActionResult Index()
        {
            return View(db.Documents.ToList());
        }

        
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Create(Document document)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Documents.Add(document);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(document);
            }
            catch
            {
                return View(document);
            }

        }

       
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = db.Documents.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);

        }

        
        [HttpPost]
        public ActionResult Edit(Document document)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(document).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(document);
            }
            catch
            {
                return View(document);
            }
            return View();
        }
        

     
    }
}
