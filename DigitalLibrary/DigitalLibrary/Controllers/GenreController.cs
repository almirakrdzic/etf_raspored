using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace DigitalLibrary.Controllers
{
    [Authorize(Roles = "administrator")]
    public class GenreController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        //
        // GET: /Genre/

        public ActionResult Index()
        {
            return View(db.genres.ToList());
        }

        //
        // GET: /Genre/Details/5

        public ActionResult Details(int id = 0)
        {
            genre genre = db.genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        //
        // GET: /Genre/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Genre/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(genre genre)
        {
            if (ModelState.IsValid)
            {
                db.genres.Add(genre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return Index();
        }

        //
        // GET: /Genre/Edit/5

        public ActionResult Edit(int id = 0)
        {
            genre genre = db.genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        //
        // POST: /Genre/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        
        //
        // POST: /Genre/Delete/5
        [HttpPost]        
        public ActionResult DeleteConfirmed(int id)
        {
            genre genre = db.genres.Find(id);
            db.genres.Remove(genre);
            db.SaveChanges();
            return Index();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}