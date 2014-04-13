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
    public class GenreController : BaseController
    {
        private DatabaseEntities db = new DatabaseEntities();

        //
        // GET: /Genre/

        public ActionResult Index()
        {
            List<DataLayer.genre> genresL = db.genres.Where(g => g.active == true).ToList();
            return View(genresL);
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
        public ActionResult Create(genre genre)
        {
            if (ModelState.IsValid)
            {
                genre.active = true;
                db.genres.Add(genre);
                db.SaveChanges();
               return View("Delete", db.genres.Where(g => g.active == true).ToList());                
            }
            return View(genre);
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
                return View("Delete", db.genres.Where(g => g.active == true).ToList()); 
            }
            return View(genre);
        }

        
        //
        // POST: /Genre/Delete/5
        [HttpPost]        
        public ActionResult Delete(int id)
        {
            try
            {
                DataLayer.genre genre = db.genres.Where(g => g.id == id).FirstOrDefault();
                genre.active = false;
                db.SaveChanges();
                return View("Delete",db.genres.Where(g => g.active == true).ToList());
            }
            catch
            {
                return View("Delete",db.genres.Where(g => g.active == true).ToList());
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}