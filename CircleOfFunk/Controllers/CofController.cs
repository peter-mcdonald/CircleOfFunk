using System;
using System.IO;
using System.Web.Mvc;
using Recaptcha.Web;
using Recaptcha.Web.Mvc;

namespace CircleOfFunk.Controllers
{
    public class CofController : Controller
    {
        protected string RenderViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentNullException("viewName");
            }

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindView(ControllerContext, viewName, string.Empty);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentNullException("viewName");
            }

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        protected CaptchaValidation IsCaptchaValid()
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