using DigitalLibrary.Models;
using DigitalLibraryContracts;
using DigitalLibraryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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

        [Authorize(Roles = "administrator")]
        public ActionResult AdminHome()
        {
            ViewBag.Message = "Your app description page.";
            return View();
        }

        [Authorize(Roles = "user")]
        public ActionResult UserHome()
        {
            return View();
        }

        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginPageModel login)
        {
            Service s = new Service();
            try
            {
                var user = s.Login(login.UserName, login.Password);
                Session["UserName"] = user.Username;
                Session["Password"] = user.Password;
                FormsAuthentication.SetAuthCookie(login.UserName, login.RememberMe);
            }
            catch
            {
                return RedirectToAction("Login");
            }
            
            if(Roles.IsUserInRole(login.UserName,"administrator"))
            {
                return RedirectToAction("AdminHome","Home");
            }
            if(Roles.IsUserInRole(login.UserName,"user"))
            {
                return RedirectToAction("UserHome","Home");
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
