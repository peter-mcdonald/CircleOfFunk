using System.Net.Http;
using CircleOfFunk.Helpers;

namespace CircleOfFunk.Builders
{
    public class SlappedUpSoulBuilder
    {
        const string SlappedUpSoul = "SlappedUpSoul";

        public string Build()
        {
            if (SessionHelper.Exists(SlappedUpSoul))
            {
                return SessionHelper.Get<string>(SlappedUpSoul);
            }

            var wordPress = new WordPress("http://slappedupsoul.wordpress.com/about/");
            var result = wordPress.ParsePage("begin", "end");

            SessionHelper.Add(SlappedUpSoul, result);

            return result;
        }
    }
}