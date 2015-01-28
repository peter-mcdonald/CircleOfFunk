using CircleOfFunk.Helpers;

namespace CircleOfFunk.Builders
{
    public class LinksBuilder
    {
        public string Build()
        {
            if (SessionHelper.Exists("Links"))
            {
                return SessionHelper.Get<string>("Links");
            }

            var wordpress = new WordPress("http://circleoffunk.wordpress.com/links/");
            var result = wordpress.ParsePage("begin", "end");

            SessionHelper.Add("Links", result);

            return result;
        }
    }
}