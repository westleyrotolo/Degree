using System;
using Tweetinvi;
using Degree.Services.Social.Twitter.TweetAuth;
using Tweetinvi.Models;
using Tweetinvi.Models.DTO;
using System.Net.Http;
using System.Collections.Generic;
using Degree.Models;
using System.Linq;
using Tweetinvi.Streaming;
using Degree.Models.Twitter;
using Degree.Services.Utils;

namespace Degree.Services.Social.Twitter
{
    public class TwitterApi
    {
        Action<TweetRaw> OnNewTweet;
        public TwitterApi() 
        {
        }
        private static void Login()
        {
            Keys.LoadKey();
            Auth.SetCredentials(TwitterAuthorize.TweetinviCredentials());
            var authenticatedUser = Tweetinvi.User.GetAuthenticatedUser();
        }
        public void NewTweet(TweetRaw tweetRaw)
        {
                
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
        IFilteredStream stream;
        public void StreamTwitter(Action<TweetRaw> Update)
        {
            Login();

            stream = Stream.CreateFilteredStream();
            stream.AddTrack("#nutella");
            stream.MatchingTweetReceived += (sender, args) =>
            {                
                var tweet = AdapterTweet(args.Tweet);
                Update?.Invoke(tweet);

            };
            stream.StartStreamMatchingAllConditions();
        }


        public void StopStream()
        {
            stream?.StopStream();
        }
        private TweetRaw AdapterTweet(ITweet tweet)
        {
            return new TweetRaw
            {
                Id = tweet.Id,
                CreatedAt = tweet.CreatedAt,
                User = AdapterUser(tweet.CreatedBy),
                Coordinates = tweet.Coordinates != null ? new Models.Coordinates { GeoCoordinates = new List<double>() { tweet.Coordinates.Latitude, tweet.Coordinates.Longitude } } : null,
                Text = tweet.ExtendedTweet != null ? tweet.ExtendedTweet.FullText : tweet.FullText,
                ExtendedTweet = tweet.ExtendedTweet != null ? new ExtendedTweet() { FullText = tweet.ExtendedTweet.FullText, DisplayTextRange = ToListLong(tweet.ExtendedTweet.DisplayTextRange) } : null,
                FavoriteCount = tweet.FavoriteCount,
                RetweetCount = tweet.RetweetCount,
                QuotedStatus = tweet.QuotedTweet != null ? AdapterTweet(tweet.QuotedTweet) : null,
                InReplyToScreenName = tweet.InReplyToScreenName,
                InReplyToStatusId = tweet.InReplyToStatusId,
                InReplyToUserId = tweet.InReplyToUserId,
                IsQuoteStatus = tweet.QuotedTweet != null,
                IsRetweetStatus = tweet.RetweetedTweet != null,
                Lang = tweet.Language?.ToString(),
                PlaceObj = tweet.Place,
                QuoteCount = (long) tweet.QuoteCount,
                QuotedStatusId = tweet.QuotedStatusId,
                ReplyCount = (long)tweet.ReplyCount,
                RetweetedStatus = tweet.RetweetedTweet != null  ? AdapterTweet(tweet.RetweetedTweet) : null,
                RetweetedStatusId = tweet.RetweetedTweet != null ? tweet.RetweetedTweet.Id : 0,
                Source = tweet.Source, 
                Truncated = tweet.Truncated, 
                UserId = tweet.CreatedBy.Id
        };
    }
        private List<long> ToListLong(int[] values)
        {
            return new List<long>();
        }
    private Models.User AdapterUser(IUser user)
    {
        return new Models.User
        {
            CreatedAt = user.CreatedAt,
            DefaultProfile = user.DefaultProfile,
            DefaultProfileImage = user.DefaultProfileImage,
            Description = user.Description,
            Followers = user.FollowersCount,
            Following = user.FriendsCount,
            Id = user.Id,
            Location = user.Location,
            Name = user.Name,
            ProfileBanner = user.ProfileBannerURL,
            ProfileImage = user.ProfileImageUrl,
            ScreenName = user.ScreenName,
            Statuses = user.StatusesCount,
            Url = user.Url,
            Verified = user.Verified

        };
    }

}
}
