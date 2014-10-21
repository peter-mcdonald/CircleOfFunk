using CircleOfFunk.Builders;

namespace CircleOfFunk.Controllers
{
    public class BiographyController : CofController
    {
        public string GetBiography()
        {
            return new BiographyBuilder().Build();
        }
    }
}
