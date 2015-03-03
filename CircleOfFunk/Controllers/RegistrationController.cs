using System;
using System.Web.Mvc;
using CircleOfFunk.Builders;
using CircleOfFunk.EmailBody;
using CircleOfFunk.Models;

namespace CircleOfFunk.Controllers
{
    public class RegistrationController : CofController
    {
        public string Register()
        {
            return RenderPartialViewToString("_register", new Register());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public String Register(Register model)
        {
            var captchaValid = IsCaptchaValid();

            if (!captchaValid.Valid)
            {
                return captchaValid.Message;
            }

            if (ModelState.IsValid)
            {
                new EmailBuilder("Registration", new RegisterBody(model)).Send();
            }

            return string.Empty;
        }
    }
}
