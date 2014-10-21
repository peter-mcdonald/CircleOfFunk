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
            return new NewsItemsBuilder().Build();
        }
    }
}
