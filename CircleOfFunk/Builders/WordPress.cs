using System.Net.Http;

namespace CircleOfFunk.Builders
{
    public class WordPress
    {
        readonly string url;

        public WordPress(string url)
        {
            this.url = url;
        }

        public string ParsePage(string beginMark, string endMark)
        {
            var begin = @"<!--" + beginMark + @"-->";
            var end = @"<!--" + endMark + @"-->";

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                var content = response.Content.ReadAsStringAsync().Result;

                var startIndex = content.IndexOf(begin) + begin.Length;
                var endIndex = content.IndexOf(end);

                return content.Substring(startIndex, endIndex - startIndex);
            }
        }
    }
}