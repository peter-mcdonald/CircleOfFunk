using System.Web.Mvc;
using CircleOfFunk.Builders;

namespace CircleOfFunk.Controllers
{
    public class BiographyController : CofController
    {
        public ActionResult Index()
        {
            return View();
        }

        public string GetBiography()
        {
            return new BiographyBuilder().Build();
        }

        [ChildActionOnly]
        public override void SetViewBagMenu()
        {
            ViewBag.CurrentSlide = (int)Menu.Slides.Biography;
        }
    }
}
