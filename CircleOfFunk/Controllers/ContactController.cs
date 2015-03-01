using System;
using System.Web.Mvc;
using CircleOfFunk.Builders;
using CircleOfFunk.EmailBody;
using CircleOfFunk.Models;
using Recaptcha.Web;
using Recaptcha.Web.Mvc;

namespace CircleOfFunk.Controllers
{
    public class ContactController : CofController
    {
        public string GetContactView()
        {
            ViewBag.CaptchaMessage = string.Empty;

            return RenderPartialViewToString("_Contact", new Contact());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public String ContactUs(Contact model)
        {
            var captchaValid = IsCaptchaValid();

            if (!captchaValid.Valid)
            {
                return captchaValid.Message;
            }

            if (ModelState.IsValid)
            {
                new EmailBuilder(model, new ContactBody(model)).Send();
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