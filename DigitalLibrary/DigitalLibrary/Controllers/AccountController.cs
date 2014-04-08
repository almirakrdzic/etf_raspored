using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using DigitalLibrary.Filters;
using DigitalLibrary.Models;

namespace DigitalLibrary.Controllers
{
    [Authorize]
   // [InitializeSimpleMembership]
    public class AccountController : BaseController
    {
        public ActionResult EditProfile()
        {
            UserProfileModel profile = new UserProfileModel();
            String username = HttpContext.User.Identity.Name;
            DigitalLibraryService.Service service = new DigitalLibraryService.Service();
            DigitalLibraryContracts.User user = service.GetUser(username);
            profile.FirstName = user.FirstName;
            profile.LastName = user.LastName;
            profile.Email = user.Email;                      
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(UserProfileModel profile)
        {            
            DigitalLibraryContracts.User user = new DigitalLibraryContracts.User();
            user.Username = HttpContext.User.Identity.Name;
            user.FirstName = profile.FirstName;
            user.LastName = profile.LastName;
            user.Email = profile.Email;
            Int32 length = profile.Image.ContentLength;
            user.Image = new byte[length];
            profile.Image.InputStream.Read(user.Image, 0, length);            
            DigitalLibraryService.Service service = new DigitalLibraryService.Service();
            service.UpdateUser(user);
            if (Roles.IsUserInRole(user.Username, "administrator"))
            {
                return RedirectToAction("AdminHome", "Home");
            }
            if (Roles.IsUserInRole(user.Username, "user"))
            {
                return RedirectToAction("UserHome", "Home");
            }
            return RedirectToAction("Login","Home");       

        }

        public FileContentResult GetProfilePic(string username)
        {
            var userImage = new byte[] { };            
            DigitalLibraryService.Service service = new DigitalLibraryService.Service();
            DigitalLibraryContracts.User user = service.GetUser(username);
            return File(user.Image, "image/png");
        }
    }
}
