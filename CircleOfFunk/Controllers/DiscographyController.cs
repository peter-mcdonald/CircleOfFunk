using System.Web.Mvc;
using CircleOfFunk.Builders;

namespace CircleOfFunk.Controllers
{
    public class DiscographyController : CofController
    {
        public ActionResult Index()
        {
            return View();
        }

        public string GetDiscography()
        {
            return new DiscographyBuilder().Build();
        }

        [ChildActionOnly]
        public override void SetViewBagMenu()
        {
            ViewBag.CurrentSlide = (int)Menu.Slides.Discography;
        }
    }
}
