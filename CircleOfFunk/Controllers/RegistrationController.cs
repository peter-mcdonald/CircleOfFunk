using System.Web.Mvc;
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
        public void Rwgister()
        {

        }

    }
}
