using System;
using Tweetinvi;
using Degree.Services.Social.Twitter.TweetAuth;
using Tweetinvi.Models;

namespace Degree.Services.Social.Twitter
{
    public class TwitterApi
    {
        public TwitterApi()
        {
        }
        private static void Login()
        {

            Auth.SetCredentials(TwitterAuthorize.GenerateCredentials());
            var authenticatedUser = User.GetAuthenticatedUser();
        }
        public static ITweet FindById(long id)
        {
            Login();
            var tweet = Tweet.GetTweet(id);
            return tweet;
        }
    }
}
