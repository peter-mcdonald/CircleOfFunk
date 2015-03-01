using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Xml;
using CircleOfFunk.Helpers;
using CircleOfFunk.Models;

namespace CircleOfFunk.Builders
{
    public class NewsItemsBuilder
    {
        const string defaultNewsImage = @"<img src=""../../Images/News/NewsItem.png"" />""";
        SyndicationFeed feed;

        public IEnumerable<NewsItem> Build()
        {
            if (SessionHelper.Exists("NewsList"))
            {
                return SessionHelper.Get<IEnumerable<NewsItem>>("NewsList");
            }

            CreateNewsFeed();
            return FetchNewsItems();
        }

        void CreateNewsFeed()
        {
            var reader = XmlReader.Create(@"http://circleoffunk.wordpress.com/category/news/feed/");
            feed = SyndicationFeed.Load<SyndicationFeed>(reader);
        }

        IEnumerable<NewsItem> FetchNewsItems()
        {
            var newsItems = feed.Items
                                .Select(x => new NewsItem()
                                    {
                                        Title = x.Title.Text,
                                        LinkUrl = x.Links[0].Uri.ToString(),
                                        ImageUrl = ExtractImage(x.ElementExtensions),
                                        Body = RemoveImages(x.Summary.Text)
                                    }).ToList();

            SessionHelper.Add("NewsList", newsItems);
            return newsItems;
        }

        string RemoveImages(string text)
        {
            return Regex.Replace(text, @"<img\s[^>]*>(?:\s*?</img>)?", string.Empty, RegexOptions.IgnoreCase);
        }

        string ExtractImage(IEnumerable<SyndicationElementExtension> extensions)
        {
            foreach (var extension in extensions.Where(e => e.OuterName == "content"))
            {
                var element = extension.GetObject<XmlElement>();

                if (element.HasAttributes)
                {
                    var url = element.GetAttribute("url").ToLower();

                    if (url.Contains(".jpg") || url.Contains(".png"))
                    {
                        return url;
                    }
                }
            }

            return defaultNewsImage;
        }
    }
}