using System;
using Tweetinvi.Models;

namespace Degree.Services.Social.Twitter.TweetAuth
{
    public class TwitterAuthorize
    {
        private static string CONSUMER_KEY = "v9oZgCQ1eNUoLSv5u38SoH7W8";
        private static string CONSUMER_SECRET = "IUIUV11IJSuW2ZWPyMsyzCEofh6jeokCyADm9YtJYKKO6IoIlX";
        private static string ACCESS_TOKEN = "172625287-UI5RCUc4f9j3lpqX4g5VRsGH7ddoO18MWhQeVG92";
        private static string ACCESS_TOKEN_SECRET = "TId1mNuFVQvU2or1PlAUiiCvjw1vQus3MKVUjMS9BjGEZ";
        public static ITwitterCredentials GenerateCredentials()
        {
            return new TwitterCredentials(CONSUMER_KEY, CONSUMER_SECRET, ACCESS_TOKEN, ACCESS_TOKEN_SECRET);
        }
    }
}   
