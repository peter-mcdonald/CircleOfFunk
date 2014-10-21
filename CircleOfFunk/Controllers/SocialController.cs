namespace CircleOfFunk.Controllers
{
    public class SocialController : CofController
    {
        public string GetSocialView()
        {
            return RenderPartialViewToString("_Social", null);
        }
    }
}
