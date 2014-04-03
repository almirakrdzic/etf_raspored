using DigitalLibrary.Models;
using DigitalLibraryContracts;
using DigitalLibraryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XCaptcha;

namespace DigitalLibrary.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginModel login)
        {
            Service s = new Service();
            try
            {
                var user = s.Login(login.UserName, login.Password);
                Session["UserName"] = user.Username;
                Session["Password"] = user.Password;
            }
            catch
            {
                RedirectToAction("Login");
            }
            return RedirectToAction("Index");
        }
    }
}
