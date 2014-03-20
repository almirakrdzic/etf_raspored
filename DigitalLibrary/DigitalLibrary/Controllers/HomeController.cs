using DigitalLibraryContracts;
using DigitalLibraryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalLibrary.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            Service s = new Service();
            s.AddNewUser(new User()
            {
                Id=3,
                FirstName="Kresimir",
                LastName="Galic",
                Password = "password",
                Type=new UserType()
                {
                    Id=1
                },
                Username="kgalic1",
                Email="kresimir.galic@live.com"

            });

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
