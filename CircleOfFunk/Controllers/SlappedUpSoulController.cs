using System.Web.Mvc;
using CircleOfFunk.Builders;

namespace CircleOfFunk.Controllers
{
    public class SlappedUpSoulController : CofController
    {
        public string GetSlappedUpSoul()
        {
            return new SlappedUpSoulBuilder().Build();
        }
    }
}
