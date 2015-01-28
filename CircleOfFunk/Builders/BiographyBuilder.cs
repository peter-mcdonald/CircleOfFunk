using CircleOfFunk.Helpers;

namespace CircleOfFunk.Builders
{
    public class BiographyBuilder
    {
        public string Build()
        {
            if (SessionHelper.Exists("Biography"))
            {
                return SessionHelper.Get<string>("Biography");
            }

            var wordpress = new WordPress("http://circleoffunk.wordpress.com/about/");
            var result = wordpress.ParsePage("begin", "end");

            SessionHelper.Add("Biography", result);

            return result;
        }
    }
}