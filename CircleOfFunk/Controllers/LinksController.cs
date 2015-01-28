using CircleOfFunk.Builders;

namespace CircleOfFunk.Controllers
{
    public class LinksController : CofController
    {
        public string GetLinks()
        {
            return new LinksBuilder().Build();
        }

    }
}
