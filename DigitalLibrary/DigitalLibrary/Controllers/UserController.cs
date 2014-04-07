using DigitalLibrary.Models;
using DigitalLibraryContracts;
using DigitalLibraryService;
using Postal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace DigitalLibrary.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return RedirectToAction("Create");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                Service s = new Service();
                user.Type = new UserType()
                {
                    Id = 1
                };
                s.AddNewUser(user);
                if (ModelState.IsValid)
                {
                    // Attempt to register the user
                    try
                    {
                       dynamic email = new Email("Email");
                        email.To = user.Email;
                        email.UserName = user.Username;
                        email.ConfirmationToken = Guid.NewGuid().ToString("N");
                        Session["ConfirmationToken"] = email.ConfirmationToken;
                        email.Send();

                        return RedirectToAction("WaitingForConfirmation", "User");
                    }
                    catch (MembershipCreateUserException e)
                    {
                        ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                    }
                }

                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }        

            
        }

      

        private string ErrorCodeToString(MembershipCreateStatus membershipCreateStatus)
        {
            throw new NotImplementedException();
        }

        [AllowAnonymous]
        public ActionResult RegisterStepTwo()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult RegisterConfirmation(Guid Id)
        {
            var confirmationToken = Session["ConfirmationToken"].ToString();
            if (new Guid(confirmationToken) == Id)
            {
                return RedirectToAction("ConfirmationSuccess");
            }
            return RedirectToAction("ConfirmationFailure");
        }

        [AllowAnonymous]
        public ActionResult ConfirmationSuccess()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ConfirmationFailure()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult WaitingForConfirmation()
        {
            return View();
        }

      

    }
}
