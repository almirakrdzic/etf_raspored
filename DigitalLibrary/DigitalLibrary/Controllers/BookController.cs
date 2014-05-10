﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace DigitalLibrary.Controllers
{
    [Authorize(Roles = "administrator,user")]
    public class BookController : BaseController
    {
        private DatabaseEntities db = new DatabaseEntities();

        //
        // GET: /Book/

        public ActionResult Index()
        {
            var books = db.books.Include(b => b.user);
            return View(books.Where(book => book.active == true).ToList());

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


        public ActionResult DetailBook(int id = 0)
        {
            book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }


        public ActionResult GenreCategory(int id = 0)
        {
            DigitalLibraryService.Service s = new DigitalLibraryService.Service();
            List<DigitalLibraryContracts.Book> books = s.GetBooksForGenre(id.ToString());
            return View(books);
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
                return View("Delete",db.books.Where(b => b.active == true).ToList());
            }            
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
            book.active = false;            
            db.SaveChanges();
            return View("Delete", db.books.Where(b => b.active == true).ToList());
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