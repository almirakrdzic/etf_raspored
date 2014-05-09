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
                    if (ValidateUser(login.UserName, login.Password))
                    {
                    Session["UserName"] = login.UserName;
                    Session["Password"] = login.Password;
                    FormsAuthentication.SetAuthCookie(login.UserName, login.RememberMe);
                    }
                    else return RedirectToAction("Index", "Home");
                }
                catch
                {
                    return RedirectToAction("Index", "Home");
                }
              
                if (Roles.IsUserInRole(login.UserName, "administrator"))
                {
                    return RedirectToAction("AdminHome", "Home");
                }                
                return RedirectToAction("UserHome", "Home");            
              
        }
            return RedirectToAction("Index", "Home");
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
                NetworkCredential nc = new NetworkCredential("uid=" + userName + ",ou=people,dc=etf,dc=unsa,dc=ba",password);
                ldc.Credential = nc;
                ldc.SessionOptions.SecureSocketLayer = false;
                ldc.SessionOptions.ProtocolVersion = 3;
                ldc.AuthType = AuthType.Basic;
                ldc.Bind(nc); // user has authenticated at this point, as the credentials were used to login to the dc.                
                string[] atrributes = new[] { "givenName", "sn", "mail" };
                SearchRequest request = new SearchRequest("ou=people,dc=etf,dc=unsa,dc=ba", "uid="+userName, System.DirectoryServices.Protocols.SearchScope.Subtree, atrributes);
                SearchResponse response = (SearchResponse) ldc.SendRequest(request);
                String Firstname = "";
                String Lastname = "";
                String Email = "";
                foreach (SearchResultEntry entry in response.Entries)
                {
                    Firstname = entry.Attributes["givenName"][0].ToString();
                    Lastname = entry.Attributes["sn"][0].ToString();
                    Email = entry.Attributes["mail"][0].ToString();
                    break;
                }

                Service s = new Service();
                if (s.GetUser(userName) == null)
                {
                    DigitalLibraryContracts.User u = new DigitalLibraryContracts.User();
                    u.Username = userName;
                    u.Password = password;
                    DigitalLibraryContracts.UserType type = new DigitalLibraryContracts.UserType();
                    type.Id = 2;
                    type.Name = "user";
                    u.Type = type;
                    u.FirstName = Firstname;
                    u.LastName = Lastname;
                    u.Email = Email;
                    s.AddNewUser(u);
                }

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
