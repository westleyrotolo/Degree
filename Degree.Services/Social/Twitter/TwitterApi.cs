﻿using System;
using Tweetinvi;
using Degree.Services.Social.Twitter.TweetAuth;
using Tweetinvi.Models;
using Tweetinvi.Models.DTO;
using System.Net.Http;
using System.Collections.Generic;

namespace Degree.Services.Social.Twitter
{
    public class TwitterApi
    {
        public TwitterApi()
        {
        }
        private static void Login()
        {
            Auth.SetCredentials(TwitterAuthorize.TweetinviCredentials());
            var authenticatedUser = User.GetAuthenticatedUser();
        }
        public static ITweet FindById(long id)
        {
            Login();
         
            TweetinviConfig.CurrentThreadSettings.TweetMode = TweetMode.Extended;
            ITweet t;
            
              var tweet = Tweet.GetTweet(id);
            var extended = tweet.ExtendedTweet;
            return tweet;
         }

    }
}
