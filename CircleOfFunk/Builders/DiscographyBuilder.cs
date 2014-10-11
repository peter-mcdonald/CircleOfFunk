using System.Net.Http;
using CircleOfFunk.Helpers;

namespace CircleOfFunk.Builders
{
    public class DiscographyBuilder
    {
        public string Build()
        {
            if (SessionHelper.Exists("Discography"))
            {
                return SessionHelper.Get<string>("Discography");
            }

            var content = string.Empty;

            using (var client = new HttpClient())
            {
                var response = client.GetAsync("http://circleoffunk.wordpress.com/discography/").Result;
                content = response.Content.ReadAsStringAsync().Result;
            }

            var start = content.IndexOf(@"<table id=");
            var end = content.IndexOf(@"</table>") + 8;

            var result = content.Substring(start, end - start);

            SessionHelper.Add("Discography", result);

            return result;
        }
    }
}