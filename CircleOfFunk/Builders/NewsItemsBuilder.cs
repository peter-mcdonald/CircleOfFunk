using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Xml;
using CircleOfFunk.Helpers;

namespace CircleOfFunk.Builders
{
    public class NewsItemsBuilder
    {
        const string defaultNewsImage = @"<img src=""../../Images/News/NewsItem.png"" />""";
        SyndicationFeed feed;

        public string Build()
        {
            if (SessionHelper.Exists("NewsList"))
            {
                return SessionHelper.Get<string>("NewsList");
            }

            CreateNewsFeed();
            var newsItems = FetchNewsItems();
            return FormatNewsItems(newsItems);
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
            return newsItems;
        }

        string FormatNewsItems(IEnumerable<NewsItem> newsItems)
        {
            var newslist = new StringBuilder();

            foreach (var newsItem in newsItems)
            {
                newslist.Append("<li>" + CreateImageTag(newsItem.ImageUrl));
                newslist.Append(CreateExternalLink(newsItem.LinkUrl, newsItem.Title));
                newslist.Append("<p>" + newsItem.Body + "</p></li>");
            }

            SessionHelper.Add("NewsList", newslist.ToString());
            return newslist.ToString();
        }

        string CreateImageTag(string imageUrl)
        {
            var tagbuilder = new TagBuilder("img");
            tagbuilder.Attributes["src"] = imageUrl;
            return tagbuilder.ToString();
        }

        string CreateExternalLink(string linkUrl, string title)
        {
            var tagBuilder = new TagBuilder("a");
            tagBuilder.Attributes["href"] = linkUrl;
            tagBuilder.Attributes["target"] = "_blank";
            tagBuilder.InnerHtml = title;
            return tagBuilder.ToString();
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

    public class NewsItem
    {
        public string Title { get; set; }
        public string LinkUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Body { get; set; }
    }
}