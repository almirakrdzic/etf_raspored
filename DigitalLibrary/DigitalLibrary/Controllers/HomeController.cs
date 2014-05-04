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
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Net;

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
                try
                {

                    if (ValidateUser("uid=" + login.UserName + ",ou=people,dc=etf,dc=unsa,dc=ba", login.Password))
                    {
                    Session["UserName"] = login.UserName;
                    Session["Password"] = login.Password;
                    FormsAuthentication.SetAuthCookie(login.UserName, login.RememberMe);

                    }
                    else RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Index");
                }


                if (Roles.IsUserInRole(login.UserName, "administrator"))
                {
                    return RedirectToAction("AdminHome", "Home");
                }
                
                return RedirectToAction("UserHome", "Home");                
              
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

        private bool ValidateUser(string userName, string password)
        {
            bool validation;
            try
            {               
                LdapConnection ldc = new LdapConnection(new LdapDirectoryIdentifier("webmail.etf.unsa.ba", 389));
                NetworkCredential nc = new NetworkCredential(userName,password);
                ldc.Credential = nc;
                ldc.SessionOptions.SecureSocketLayer = false;
                ldc.SessionOptions.ProtocolVersion = 3;
                ldc.AuthType = AuthType.Basic;
                ldc.Bind(nc); // user has authenticated at this point, as the credentials were used to login to the dc.                
                validation = true;
            }
            catch (Exception)
            {
                validation = false;
            }
            return validation;
        } 
       
    }
}
