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
using DigitalLibraryContracts.Helpers;
using System.Drawing;
using System.IO;
using Recaptcha;
using DataLayer;
namespace DigitalLibrary.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            var db = new DataLayer.DatabaseEntities();
            return View(db.users.Where(u => u.active == true).ToList());
        }

        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RecaptchaControlMvc.CaptchaValidatorAttribute]
        public ActionResult Create(User user, bool captchaValid)
        {
            if (!captchaValid)
            {
                ModelState.AddModelError("", "Incorrect captcha answer.");
                return View();
            }

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

                return RedirectToAction("Index", "Home");
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
     

        public ActionResult Details(int id)
        {
            var db = new DataLayer.DatabaseEntities();
            DataLayer.user user = db.users.Find(id);
            return View(user);

        }

        public ActionResult PromoteUser(int id)
        {
            var db = new DataLayer.DatabaseEntities();
            DataLayer.user user = db.users.Find(id);
            user.type = 1;
            db.SaveChanges();
            return View("Details",user);
        }

        public FileContentResult GetProfilePic(string username)
        {
            var userImage = new byte[] { };
            DigitalLibraryService.Service service = new DigitalLibraryService.Service();
            DigitalLibraryContracts.User user = service.GetUser(username);
            if (user.Image == null)
            {
                MemoryStream stream = new MemoryStream();               
                Image im = Image.FromFile(Server.MapPath("~//Content//img//user.png"));
                im.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                byte[] bytes = stream.ToArray();
                return File(bytes, "image/png");
            }

            return File(user.Image, "image/jpg");
        }

    }
}
