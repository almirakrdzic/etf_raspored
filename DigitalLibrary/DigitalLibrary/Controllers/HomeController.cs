using DigitalLibrary.Models;
using DigitalLibraryContracts;
using DigitalLibraryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Recaptcha;
using System.Threading.Tasks;
using DigitalLibraryContracts.Helpers;

namespace DigitalLibrary.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "administrator")]
        public ActionResult AdminHome()
        {
            return View();
        }

        [Authorize(Roles = "administrator")]
        public ActionResult AdminDashboard()
        {
            return View();
        }

       // [Authorize(Roles = "user")]
        public ActionResult UserHome()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]       
        public async Task<ActionResult> Login(UserLoginPageModel login)       
        {          
          

            if (ModelState.IsValid)
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
                    return RedirectToAction("Index");
                }


                if (Roles.IsUserInRole(login.UserName, "administrator"))
                {
                    return RedirectToAction("AdminHome", "Home");
                }
                if (Roles.IsUserInRole(login.UserName, "user"))
                {
                    return RedirectToAction("UserHome", "Home");                
               }
        }
            return RedirectToAction("Index");
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
