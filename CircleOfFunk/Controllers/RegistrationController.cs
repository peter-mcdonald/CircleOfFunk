using CircleOfFunk.Models;

namespace CircleOfFunk.Controllers
{
    public class RegistrationController : CofController
    {
        public string Register()
        {
            return RenderPartialViewToString("_register", new Register());
        }

    }
}
