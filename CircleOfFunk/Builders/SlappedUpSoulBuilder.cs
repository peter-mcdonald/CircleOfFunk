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

            var content = string.Empty;

            using (var client = new HttpClient())
            {
                var response = client.GetAsync("http://slappedupsoul.wordpress.com/about/").Result;
                content = response.Content.ReadAsStringAsync().Result;
            }

            var start = content.IndexOf(@"<!--begin-->") + 12;
            var end = content.IndexOf(@"<!--end-->");

            var result = content.Substring(start, end - start);

            SessionHelper.Add(SlappedUpSoul, result);

            return result;
        }
    }
}