using System;
using System.IO;
using Tweetinvi.Models;
using System.Linq;
namespace Degree.Services.Social.Twitter.TweetAuth
{
    public class TwitterAuthorize
    {
        private static string CONSUMER_KEY;
        private static string CONSUMER_SECRET;
        private static string ACCESS_TOKEN;
        private static string ACCESS_TOKEN_SECRET;
        public static ITwitterCredentials GenerateCredentials()
        {
            LoadKey();
            return new TwitterCredentials(CONSUMER_KEY, CONSUMER_SECRET, ACCESS_TOKEN, ACCESS_TOKEN_SECRET);
        }
        private static void LoadKey()
        {
            var root = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            var path = Path.Combine(root, "config.txt");
            string text = File.ReadAllText(path);
            var properties = text.Replace("\r", "").Split("\n")
            .ToDictionary(k => k.Split(":")[0], v => v.Split(":")[1]);

            CONSUMER_KEY        = properties[nameof(CONSUMER_KEY)];
            CONSUMER_SECRET     = properties[nameof(CONSUMER_SECRET)];
            ACCESS_TOKEN        = properties[nameof(ACCESS_TOKEN)];
            ACCESS_TOKEN_SECRET = properties[nameof(ACCESS_TOKEN_SECRET)];
        }
    }
}   
