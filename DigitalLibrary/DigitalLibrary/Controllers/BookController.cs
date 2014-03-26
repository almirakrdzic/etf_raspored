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
    public class BookController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        //
        // GET: /Book/

        public ActionResult Index()
        {
            var books = db.books.Include(b => b.user);
            return View(books.ToList());
        }

        //
        // GET: /Book/Details/5

        public ActionResult Details(int id = 0)
        {
            book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        //
        // GET: /Book/Create

        public ActionResult Create()
        {
            ViewBag.added_by = new SelectList(db.users, "id", "username");
            return View();
        }

        //
        // POST: /Book/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(book book)
        {
            if (ModelState.IsValid)
            {
                db.books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.added_by = new SelectList(db.users, "id", "username", book.added_by);
            return View(book);
        }

        //
        // GET: /Book/Edit/5

        public ActionResult Edit(int id = 0)
        {
            book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.added_by = new SelectList(db.users, "id", "username", book.added_by);
            return View(book);
        }

        //
        // POST: /Book/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.added_by = new SelectList(db.users, "id", "username", book.added_by);
            return View(book);
        }

        //
        // GET: /Book/Delete/5

        public ActionResult Delete(int id = 0)
        {
            book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        //
        // POST: /Book/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            book book = db.books.Find(id);
            db.books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}