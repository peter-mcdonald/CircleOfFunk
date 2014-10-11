using System.Web.Mvc;
using CircleOfFunk.Builders;

namespace CircleOfFunk.Controllers
{
    public class SlappedUpSoulController : CofController
    {
        public ActionResult Index()
        {
            return View();
        }

        public string GetSlappedUpSoul()
        {
            return new SlappedUpSoulBuilder().Build();
        }

        [ChildActionOnly]
        public override void SetViewBagMenu()
        {
            ViewBag.CurrentSlide = (int)Menu.Slides.SlappedUpSoul;
        }

    }
}
