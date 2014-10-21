using System.Web.Mvc;
using CircleOfFunk.Builders;

namespace CircleOfFunk.Controllers
{
    public class DiscographyController : CofController
    {
        public string GetDiscography()
        {
            return new DiscographyBuilder().Build();
        }
    }
}
