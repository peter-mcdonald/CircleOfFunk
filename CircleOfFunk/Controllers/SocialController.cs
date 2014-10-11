using System.Web.Mvc;

namespace CircleOfFunk.Controllers
{
    public class SocialController : CofController
    {
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public override void SetViewBagMenu()
        {
            ViewBag.CurrentSlide = (int)Menu.Slides.Social;
        }
    }
}
