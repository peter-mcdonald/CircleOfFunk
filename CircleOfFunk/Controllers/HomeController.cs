using System;
using System.Web.Mvc;
using CircleOfFunk.Builders;

namespace CircleOfFunk.Controllers
{
    public class HomeController : CofController
    {
        public ActionResult Index()
        {
            return View();
        }

        public String GetNewsItems()
        {
            var model = new NewsItemsBuilder().Build();
            return RenderPartialViewToString("_NewsList", model);
        }
    }
}
