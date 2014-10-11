using System.Net.Http;
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

            var content = string.Empty;

            using (var client = new HttpClient())
            {
                var response = client.GetAsync("http://circleoffunk.wordpress.com/about/").Result;
                content = response.Content.ReadAsStringAsync().Result;
            }

            var start = content.IndexOf(@"<!--begin-->") + 12;
            var end = content.IndexOf(@"<!--end-->");

            var result = content.Substring(start, end - start);

            SessionHelper.Add("Biography", result);

            return result;
        }
    }
}