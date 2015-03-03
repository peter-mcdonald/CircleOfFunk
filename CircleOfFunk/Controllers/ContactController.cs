using System;
using System.Web.Mvc;
using CircleOfFunk.Builders;
using CircleOfFunk.EmailBody;
using CircleOfFunk.Models;

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
                new EmailBuilder(model.Subject, new ContactBody(model)).Send();
            }

            return string.Empty;
        }
    }
}