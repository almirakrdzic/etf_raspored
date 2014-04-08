using DataLayer;
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

                        user.Type = new UserType()
                        {
                            Id = 1
                        };
                        user.ConfirmationToken = email.ConfirmationToken;
                        user.IsActive = false;
                        user.IsConfirmed = false;
                        using (var db = new DatabaseEntities())
                        {
                            
                            db.users.Add(user.ToModel());
                            db.SaveChanges();
                        }

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
            using (var db = new DatabaseEntities())
            {
                var confirmationToken = Session["ConfirmationToken"].ToString();
                var user = db.users.Where(u => u.confirmationToken == confirmationToken).FirstOrDefault();
                if (user != null)
                {
                    user.isConfirmed = true;
                    user.active = true;
                    db.SaveChanges();
                }
            }
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
