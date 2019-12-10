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
using Degree.Models.Dto;
using Degree.Services.Location.GoogleMaps;
using System.Threading.Tasks;
using Degree.Services.TextAnalysis.Azure;

namespace Degree.Services.Social.Twitter
{
    public class TwitterApi
    {
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
        GoogleMapsHelper GetLocation;
        public void StreamTwitter(Action<TweetDto> Update)
        {
            Login();
            GetLocation = new GoogleMapsHelper();
            stream = Stream.CreateFilteredStream();
            stream.AddTrack("terremoto");
       //     stream.AddLocation(new Tweetinvi.Models.Coordinates(49.246292, -123.116226), new Tweetinvi.Models.Coordinates(-33.865143, 151.209900));

            stream.MatchingTweetReceived += (sender, args) =>
            {
                if (args.Tweet.Coordinates != null)
                {
                    Console.WriteLine(args.Json);
                }
                if (!string.IsNullOrEmpty(args.Tweet.Place?.Geometry?.Type))
                {

                }
                Task.Run(async () =>
                {
                    var tweetRaw = AdapterTweet(args.Tweet);
                    if (!string.IsNullOrEmpty(tweetRaw?.User?.Location))
                    {
                        var geoUsers = await GetLocation.GeoReverse(tweetRaw.User.Location);
                        tweetRaw.GeoUsers.AddRange(geoUsers);
                    }


                 //   var tweetsSentiment = await AzureSentiment.AnalyzeTweetSentiment(new List<TweetRaw>() { tweetRaw });
                //    if (tweetsSentiment != null && tweetsSentiment.Count > 0)
               //     {
                //        tweetRaw.TweetSentiment = tweetsSentiment[0];
                //    }
                    var tweet = CreateTweetDto(tweetRaw);
                    Update?.Invoke(tweet);
                });
            };
         //   stream.StartStreamMatchingAllConditions();
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
                QuoteCount = (long)tweet.QuoteCount,
                QuotedStatusId = tweet.QuotedStatusId,
                ReplyCount = (long)tweet.ReplyCount,
                RetweetedStatus = tweet.RetweetedTweet != null ? AdapterTweet(tweet.RetweetedTweet) : null,
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
        public TweetDto CreateTweetDto(TweetRaw x)
        {
            return new TweetDto
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                Text = x.Text,
                InReplyToStatusId = x.InReplyToStatusId,
                InReplyToScreenName = x.InReplyToScreenName,
                UserDefaultProfile = x.User.DefaultProfile,
                UserDefaultProfileImage = x.User.DefaultProfileImage,
                FavoriteCount = x.FavoriteCount,
                UserFollowers = x.User.Followers,
                UserFollowing = x.User.Following,
                IsQuoteStatus = x.IsQuoteStatus,
                IsRetweetStatus = x.IsRetweetStatus,
                UserName = x.User.Name,
                UserScreenName = x.User.ScreenName,
                NegativeScore = x.TweetSentiment != null ? x.TweetSentiment.NegativeScore : -1,
                PositiveScore = x.TweetSentiment != null ? x.TweetSentiment.PositiveScore : -1,
                NeutralScore = x.TweetSentiment != null ? x.TweetSentiment.NeutralScore : -1,
                QuoteCount = x.QuoteCount,
                ReplyCount = x.ReplyCount,
                RetweetCount = x.RetweetCount,
                Sentiment = x.TweetSentiment != null ? x.TweetSentiment.Sentiment.ToString() : "",
                UserId = x.User.Id,
                UserProfileBanner = x.User.ProfileBanner,
                UserProfileImage = x.User.ProfileImage,
                UserVerified = x.User.Verified,
                SentimentSentence = (x.TweetSentiment != null && x.TweetSentiment.Sentences != null) ?
                     x.TweetSentiment.Sentences.Select((s) =>
                     new SentimentSentenceDto
                     {
                         Sentiment = s.Sentiment.ToString(),
                         Length = s.Length,
                         NegativeScore = s.NegativeScore,
                         NeutralScore = s.NeutralScore,
                         PositiveScore = s.PositiveScore
                     }
                     ).ToList() : null,
                Hashtags = (x.TweetsHashtags != null) ?
                     x.TweetsHashtags.Select((h) => h.Hashtags).ToList() : null,
                GeoCoordinate = (x.GeoUsers != null && x.GeoUsers.Count > 0) ?
                                    new GeoCoordinateDto
                                    {
                                        Lat = x.GeoUsers[0].Lat,
                                        Lon = x.GeoUsers[0].Lon,
                                        GeoName = x.GeoUsers[0].DisplayName
                                    } : null
            };
        }

    }
}
