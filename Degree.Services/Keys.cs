using System;
using System.IO;
using System.Linq;
namespace Degree.Services
{
    public static class Keys
    {

        public static void LoadKey()
        {
            var root = Directory.GetParent(Environment.CurrentDirectory).FullName;
            var path = Path.Combine(root, "config.txt");
            string text = File.ReadAllText(path);
            var properties = text.Replace("\r", "").Split("\n")
            .ToDictionary(k => k.Split(":")[0], v => v.Substring(v.IndexOf(v.Split(":")[1])));

            Twitter.CONSUMER_KEY = properties[nameof(Twitter.CONSUMER_KEY)];
            Twitter.CONSUMER_SECRET = properties[nameof(Twitter.CONSUMER_SECRET)];
            Twitter.ACCESS_TOKEN = properties[nameof(Twitter.ACCESS_TOKEN)];
            Twitter.ACCESS_TOKEN_SECRET = properties[nameof(Twitter.ACCESS_TOKEN_SECRET)];
            Azure.TEXT_ANALYSIS_KEY = properties[nameof(Azure.TEXT_ANALYSIS_KEY)];
            Azure.TEXT_ANALYSIS_SENTIMENT_URL = properties[nameof(Azure.TEXT_ANALYSIS_SENTIMENT_URL)];
            Azure.TEXT_ANALYSIS_ENTITY_URL = properties[nameof(Azure.TEXT_ANALYSIS_ENTITY_URL)];
            Azure.TEXT_ANALYSIS_ENTITY_PII_URL = properties[nameof(Azure.TEXT_ANALYSIS_ENTITY_PII_URL)];
        }
        public static class Twitter
        {

            public static string CONSUMER_KEY;
            public static string CONSUMER_SECRET;
            public static string ACCESS_TOKEN;
            public static string ACCESS_TOKEN_SECRET;
        }
        public static class Azure
        {
            public static string TEXT_ANALYSIS_SENTIMENT_URL;
            public static string TEXT_ANALYSIS_ENTITY_URL;
            public static string TEXT_ANALYSIS_ENTITY_PII_URL;
            public static string TEXT_ANALYSIS_KEY;
        }
    }
}
