using System;
using System.Web.Mvc;
using CircleOfFunk.Builders;
using CircleOfFunk.Models;
using Recaptcha.Web;
using Recaptcha.Web.Mvc;

namespace CircleOfFunk.Controllers
{
    public class ContactController : CofController
    {
        public ActionResult Index()
        {
            ViewBag.CaptchaMessage = string.Empty;

            //var res = RenderViewToString("Index", new Contact());

            return View(new Contact());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public String Index(Contact model)
        {
            var captchaValid = IsCaptchaValid();

            if (!captchaValid.Valid)
            {
                return captchaValid.Message;
            }

            if (ModelState.IsValid)
            {
                new EmailBuilder(model).Send();
            }

            return string.Empty;
        }

        [ChildActionOnly]
        CaptchaValidation IsCaptchaValid()
        {
            var captchaValidation = new CaptchaValidation()
                {
                    Valid = true,
                    Message = string.Empty
                };

            var recaptchaHelper = this.GetRecaptchaVerificationHelper();

            if (String.IsNullOrEmpty(recaptchaHelper.Response))
            {
                captchaValidation.Message = "The captcha answer cannot be empty";
                captchaValidation.Valid = false;        
                return captchaValidation;
            }

            var recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();

            if (recaptchaResult != RecaptchaVerificationResult.Success)
            {
                captchaValidation.Message = "The captcha answer is invalid";
                captchaValidation.Valid = false;
                return captchaValidation;
            }

            return captchaValidation;
        }
    }

    public class CaptchaValidation
    {
        public bool Valid { get; set; }
        public string Message { get; set; }
    }
}