﻿using DigitalLibraryContracts;
using DigitalLibraryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalLibrary.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            Service s = new Service();
            user.Type = new UserType()
            {
                Id = 1
            };
            s.AddNewUser(user);

            return View();
        }

    }
}