using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DigitalLibrary;
using DigitalLibrary.Services;

namespace DigitalLibrary.Controllers
{
    public class CaptchaController : Controller
    {
        //
        // GET: /Captcha/

        public ActionResult Index()
        {
            string value = CaptchaService.GenerateCaptchaValue();

            var builder = new XCaptcha.ImageBuilder();
            var result = builder.Create(value);

            Session.Add(DigitalLibrary.Internal.Constants.SecretKey, result.Solution);

            return new FileContentResult(result.Image, result.ContentType);
        }

        public string Check()
        {
            return (string)Session[DigitalLibrary.Internal.Constants.SecretKey];
        }

//        public string Check(string value)
//        {
//            string val = (string)Session[DigitalLibrary.Internal.Constants.SecretKey];
//            return Convert.ToString(val == value);
//        }

    }
}
