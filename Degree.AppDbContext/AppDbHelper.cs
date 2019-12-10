﻿using Degree.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Degree.Models.Dto;
using Degree.Models.WebApi;
using Degree.Models.Twitter;
using System.Text.Json.Serialization;

namespace Degree.AppDbContext
{
    public static class AppDbHelper<T> where T : class
    {

        public static async Task<T> InsertOrUpdateTweetAsync(T obj)
        {
            using (var context = new AppDbContext())
            {
                bool exist = false;
                if (typeof(T) == typeof(User))
                {
                    var user = (User)(object)obj;
                    exist = context.Users.Count(x => x.Id == user.Id) > 0;
                }
                else if (typeof(T) == typeof(TweetRaw))
                {
                    var tweet = (TweetRaw)(object)obj;
                    exist = context.TweetsRaw.Count(x => x.Id == tweet.Id) > 0;
                }
                else if (typeof(T) == typeof(ExtendedTweet))
                {
                    var extTweet = (ExtendedTweet)(object)obj;
                    exist = context.ExtendedTweets.Count(x => x.TweetRawId == extTweet.TweetRawId) > 0;
                }
                else if (typeof(T) == typeof(Place))
                {
                    var place = (Place)(object)obj;
                    exist = context.Places.Count(x => x.Id == place.Id) > 0;
                }
                else if (typeof(T) == typeof(BoundingBox))
                {
                    var boundingBox = (BoundingBox)(object)obj;
                    exist = context.BoundingBoxes.Count(x => x.PlaceId == boundingBox.PlaceId) > 0;
                }
                if (!exist)
                {
                    if (typeof(T) == typeof(TweetRaw))
                    {
                        var tweet = ((TweetRaw)(object)obj);
                        if (tweet.ExtendedTweet != null)
                            context.Entry<ExtendedTweet>(((TweetRaw)(object)obj).ExtendedTweet).State = EntityState.Detached;
                        if (tweet.User != null)
                            context.Entry<User>(((TweetRaw)(object)obj).User).State = EntityState.Detached;
                        if (tweet.QuotedStatus != null)
                            context.Entry<TweetRaw>(((TweetRaw)(object)obj).QuotedStatus).State = EntityState.Detached;
                        if (tweet.RetweetedStatus != null)
                            context.Entry<TweetRaw>(((TweetRaw)(object)obj).RetweetedStatus).State = EntityState.Detached;
                        if (tweet.Place != null)
                        {
                            context.Entry<Place>(((TweetRaw)(object)obj).Place).State = EntityState.Detached;
                            if (tweet.Place.BoundingBox != null)
                            {
                                context.Entry<BoundingBox>(((TweetRaw)(object)obj).Place.BoundingBox).State = EntityState.Detached;
                            }
                        }
                        context.Entry<TweetRaw>(((TweetRaw)(object)obj)).State = EntityState.Added;
                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        await context.Set<T>().AddAsync(obj);
                        await context.SaveChangesAsync();

                    }
                }
                return obj;
            }
        }

        public static async Task<TweetSentiment> InsertOrUpdateSentimentAsync(TweetSentiment tweetSentiment)
        {
            using (var context = new AppDbContext())
            {
                if (context.TweetsSentiment.Count(x => x.TweetRawId == tweetSentiment.TweetRawId) > 0)
                {
                    context.Remove(tweetSentiment);
                    await context.SaveChangesAsync();
                    await context.AddAsync(tweetSentiment);
                    await context.SaveChangesAsync();
                }
                else
                {

                    await context.AddAsync(tweetSentiment);
                    await context.SaveChangesAsync();
                }

                return tweetSentiment;
            }

        }

        public static async Task<bool> InsertOrUpdateGeoUsersAsync(GeoUser geoUser)
        {
            try
            {

                using (var context = new AppDbContext())
                {
                    if (context.GeoUsers.Count(x => x.UserId == geoUser.UserId && x.Id == geoUser.Id) == 0)
                    {

                        await context.AddAsync(geoUser);
                        await context.SaveChangesAsync();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static async Task<TweetHashtags> InsertOrUpdateHashtagsAsync(TweetHashtags hashtags)
        {
            try
            {

                using (var context = new AppDbContext())
                {
                    if (context.TweetsHashtags.Count(x => x.TweetRawId == hashtags.TweetRawId && x.Hashtags == hashtags.Hashtags) == 0)
                    {

                        await context.AddAsync(hashtags);
                        await context.SaveChangesAsync();
                    }
                    return hashtags;
                }
            }
            catch (Exception ex)
            {
                return hashtags;
            }

        }

        public static List<HashtagsCount> GroupbyHashtags()
        {
            using (var context = new AppDbContext())
            {
                var hashtags = context.TweetsHashtags
                   .GroupBy(h => h.Hashtags)
                   .Select(h => new HashtagsCount { Hashtags = h.Key, Count = h.Count() })
                   .ToList();
                return hashtags;
            }
        }

        public static List<TweetRaw> Fetch()
        {
            using (var context = new AppDbContext())
            {
                var items = context.TweetsRaw
                .Include(x => x.User)
                .Include(x => x.GeoUsers)
                .Include(x => x.TweetSentiment)
                .ThenInclude(x => x.Sentences)
                .ToList();
                return items;
            }
        }

        public static List<TweetRaw> FetchNotRetweeted()
        {
            using (var context = new AppDbContext())
            {
                var items = context.TweetsRaw.Where(t => !t.IsRetweetStatus)
                .Include(x => x.User)
                .Include(x => x.GeoUsers)
                .Include(x => x.TweetSentiment)
                .ThenInclude(x => x.Sentences)
                .ToList();
                return items;
            }
        }

        public static List<UserDto> MoreActives(int skip=0, int take=0)
        {
            var query = "SELECT u.ScreenName, u.Name, u.ProfileImage, count(*) as Statuses, 0 as Favorites, 0 as Retweets from users as u inner join tweetsraw t on t.UserId = u.Id  where t.Lang = \"it\" group by u.Id order by Statuses desc;";
            using (var context = new AppDbContext())
            {
                var items = context.UserDto.FromSqlRaw(query).ToList();
                if (take == 0 &&
                    skip == 0)
                    return items.ToList();
                else
                {
                    return items.Skip(skip).Take(take).ToList();
                }
            }
        }

        public static List<UserDto> MoreRetweeted(int skip = 0, int take = 0)
        {
            var query = "SELECT u.ScreenName, u.Name, u.ProfileImage, sum(t.RetweetCount) as Retweets, 0 as Favorites, 0 as Statuses from users as u inner join tweetsraw t on t.UserId = u.Id where t.Lang = \"it\" group by u.Id order by Retweets desc;";
            using (var context = new AppDbContext())
            {

                var items = context.UserDto.FromSqlRaw(query).ToList();
                if (take == 0 && skip == 0)
                    return items.ToList();
                else
                {
                    return items.Skip(skip).Take(take).ToList();
                }
            }
        }

        public static List<UserDto> MoreFavorites(int skip = 0, int take = 0)
        {
            var query = "SELECT u.ScreenName, u.Name, u.ProfileImage, sum(t.FavoriteCount) as Favorites, 0 as Retweets, 0 as Statuses from users as u inner join tweetsraw as t on t.UserId = u.Id where t.Lang = \"it\" group by u.Id order by Favorites desc;";
            using (var context = new AppDbContext())
            {

                var items = context.UserDto.FromSqlRaw(query).ToList();
                if (take == 0 && skip == 0)
                    return items.ToList();
                else
                {
                    return items.Skip(skip).Take(take).ToList();
                }
            }
        }
        /*
         *
         *SELECT 
    COUNT(*), 
    AVG(s.PositiveScore), 
    AVG(s.NeutralScore), 
    AVG(s.NegativeScore), 
    SUM(s.Sentiment="Positive") AS PositiveLabel,
    SUM(s.Sentiment="Neutral") AS NeutralLabel,
    SUM(s.Sentiment="Mixed") AS MixedLabel,
    SUM(s.Sentiment="Negative") AS NegativeLabel
FROM
    degree.tweetsraw as t
INNER JOIN 
    degree.tweetssentiment as s ON COALESCE(t.RetweetedStatusId, t.Id) = s.TweetRawId
WHERE LOWER(t.text) LIKE '%%'
         */
        public static WordSentiment WordSentiment(string word)
        {
            using (var context = new AppDbContext())
            {
                var item = (from t in context.TweetsRaw
                            join s in context.TweetsSentiment on (t.RetweetedStatusId ?? t.Id) equals s.TweetRawId
                            where t.Text.ToLower().Contains(word)
                            group t by 1 into g
                            select new WordSentiment
                            {
                                Tweets = g.Count(),
                                MixedLabel = g.Count(x => x.TweetSentiment.Sentiment == DocumentSentimentLabel.Mixed),
                                PositiveLabel = g.Count(x => x.TweetSentiment.Sentiment == DocumentSentimentLabel.Positive),
                                NegativeLabel = g.Count(x => x.TweetSentiment.Sentiment == DocumentSentimentLabel.Negative),
                                NeutralLabel = g.Count(x => x.TweetSentiment.Sentiment == DocumentSentimentLabel.Neutral),
                                AvgPositiveScore = g.Average(x=>x.TweetSentiment.PositiveScore),
                                AvgNegativeScore = g.Average(x=>x.TweetSentiment.NegativeScore),
                                AvgNeutralScore = g.Average(x=>x.TweetSentiment.NeutralScore)
                            }).FirstOrDefault() ;
                return item;
            }
        }

        public static List<User> FetchUsers()
        {
            using (var context = new AppDbContext())
            {
                var items = context.Users.ToList();
                return items;
            }
        }
        public static List<AvgHashtagSentiment> AvgHashtags()
        {
            using (var context = new AppDbContext())
            {
                var avgHashtags = new List<AvgHashtagSentiment>();
                var items = context.TweetsRaw
                .Include(x => x.User)
                .Include(x => x.GeoUsers)
                .Include(x => x.TweetSentiment)
                .ThenInclude(x => x.Sentences)
                .ToList();
                var hashtags = new List<string>() { "ALL" };
                hashtags.AddRange(Models.Constants.Hashtags);
                foreach (var h in hashtags)
                {
                    var avg = GroupedHashtagSentiment(ref items, h);
                    avgHashtags.Add(avg);
                }
                return avgHashtags;

            }
        }
        private static AvgHashtagSentiment GroupedHashtagSentiment(ref List<TweetRaw> tweets, string hashtags)
        {
            var avg =new AvgHashtagSentiment();
            avg.Hashtags = hashtags;
            DateTime startDate = new DateTime(2019, 3, 29, 7, 0, 0);
            DateTime endDate = new DateTime(2019, 3, 31, 23, 59, 59);
            while (startDate < endDate)
            {
                var toDate = startDate.AddHours(1);
                avg.AvgSentiments.Add(new AvgSentiment
                {
                    FromDate = startDate,
                    ToDate = toDate
                });
                startDate = toDate;
            }
            foreach (var tweet in tweets)
            {
                if (tweet.TweetsHashtags.Count(x => x.Hashtags == hashtags) > 0 || hashtags == "ALL")
                {
                    var avgSentiment = avg.AvgSentiments
                        .FirstOrDefault(x => x.FromDate >= tweet.CreatedAt && x.ToDate <= tweet.CreatedAt);
                    if (avgSentiment != null)
                    {
                        var tweetSentiment = tweet.TweetSentiment != null ? tweet.TweetSentiment : tweets.FirstOrDefault(x => x.Id == tweet.RetweetedStatusId)?.TweetSentiment;
                        if (tweetSentiment != null)
                        {
                            avgSentiment.Tweets += 1;
                            avgSentiment.AddLabel(tweetSentiment.Sentiment.ToString());
                            avgSentiment.PositivesScore.Add(tweetSentiment.PositiveScore);
                            avgSentiment.NegativesScore.Add(tweetSentiment.NegativeScore);
                            avgSentiment.NeutralsScore.Add(tweetSentiment.NeutralScore);
                        }
                    }
                }
            }
            return avg;
        }
        public static List<TweetDto> FetchContains(string[] Hashtags, int page = 0, int itemPerPage = 0, bool skipRetweet = false, OrderSentiment orderSentiment = OrderSentiment.NoOrder)
        {
            using (var context = new AppDbContext())
            {
                // workaround - Linq non supporta Any
                var or = string.Join(" OR ", Hashtags.Select(x => $"LOWER(t.text) LIKE '%{x}%'"));
                var query = $"SELECT * FROM tweetsraw as t WHERE ({or}) AND t.IsRetweetStatus = false";
                var items = context.TweetsRaw
                .FromSqlRaw(query)
                .Include(x => x.User)
                .Include(x => x.TweetsHashtags)
                .Include(x => x.TweetSentiment)
                .ThenInclude(x => x.Sentences)
                .OrderByDescending(x => orderSentiment == OrderSentiment.Positive
                                    ? x.TweetSentiment.PositiveScore : (orderSentiment == OrderSentiment.Negative
                                    ? x.TweetSentiment.NegativeScore : 1))
                .ThenByDescending(x => orderSentiment == OrderSentiment.Reply
                                    ? x.ReplyCount : (orderSentiment == OrderSentiment.Retweet
                                    ? x.RetweetCount : (orderSentiment == OrderSentiment.Favorite
                                    ? x.FavoriteCount : 1)))
                .ThenByDescending(x => x.CreatedAt)
                .Skip(page * itemPerPage)
                .Take(itemPerPage)
                .ToList();
                var dtos = items.Select((x) =>
                new TweetDto
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
                }).ToList();
                return dtos;

            }
        }

        public static List<HashtagsGrouped> GroupSentimentByHashtags(bool withRetweet = true)
        {
            var withRetweetStr = withRetweet ? "COALESCE(t.RetweetedStatusId, t.Id)" : "t.Id";
            var query = $"SELECT h.Hashtags, COUNT(*) AS TweetCount, AVG(s.PositiveScore) AS PositiveScore, AVG(s.NeutralScore) AS NegativeScore, AVG(s.NegativeScore) AS NegativeScore, SUM(s.Sentiment=\"Positive\") AS PositiveLabel, SUM(s.Sentiment=\"Neutral\") AS NeutralLabel, SUM(s.Sentiment=\"Mixed\") AS MixedLabel, SUM(s.Sentiment=\"Negative\") AS NegativeLabel FROM degree.tweetshashtags AS h INNER JOIN degree.tweetsraw AS t ON h.TweetRawId = ${withRetweetStr} INNER JOIN degree.tweetssentiment as s ON {withRetweetStr} = s.TweetRawId GROUP BY h.Hashtags";
            using (var context = new AppDbContext())
            {
                var hashtagsGrounped = context
                 .Set<HashtagsGrouped>()
                 .FromSqlRaw(query)
                 .ToList();
                return hashtagsGrounped;
            }

        }

        private static bool ContainsText(string[] Hashtags, string Tweet)
        {

            foreach (var h in Hashtags)
            {
                if (Tweet.ToLower().Contains(h.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }



        public static async Task AddTweet(TweetRaw t)
        {
            try
            {

                if (t.User != null)
                {
                    t.UserId = t.User.Id;
                    await AppDbHelper<User>.InsertOrUpdateTweetAsync(t.User);
                }
                if (t.QuotedStatus != null)
                {
                    await AddTweet(t.QuotedStatus);
                }
                if (t.RetweetedStatus != null)
                {
                    await AddTweet(t.RetweetedStatus);
                }

                await AppDbHelper<TweetRaw>.InsertOrUpdateTweetAsync(t);

                if (t.ExtendedTweet != null)
                {
                    t.ExtendedTweet.TweetRawId = t.Id;
                    await AppDbHelper<ExtendedTweet>.InsertOrUpdateTweetAsync(t.ExtendedTweet);
                }
                if (t.Place != null)
                {
                    t.Place.TweetRawId = t.Id;
                    await AppDbHelper<Place>.InsertOrUpdateTweetAsync(t.Place);
                    if (t.Place.BoundingBox != null)
                    {
                        t.Place.BoundingBox.PlaceId = t.Place.Id;
                        await AppDbHelper<BoundingBox>.InsertOrUpdateTweetAsync(t.Place.BoundingBox);
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }
    }
    

}