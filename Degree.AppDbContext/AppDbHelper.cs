using Degree.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
                    exist = context.Users.Count(x=>x.Id == user.Id) > 0;
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
                        if (tweet.ExtendedTweet!=null)
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
                    /*
                    var s = context.SentenceSentiments.Where(x => x.TweetSentimentId == tweetSentiment.TweetRawId).ToList();
                    if (s.Count() > 0)
                    {
                        context.RemoveRange(s);
                    }
                    await context.SaveChangesAsync();
                    context.Update(tweetSentiment);
                    await context.SaveChangesAsync();
                    foreach (var sentence in tweetSentiment.Sentences)
                    {
                        await context.AddAsync(sentence);
                    }
                    */
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
        public static List<TweetRaw> Fetch()
        {
            using (var context = new AppDbContext())
            {
                var items = context.TweetsRaw.ToList();
                return items;
            }
        }
        public static List<TweetRaw> FetchNotRetweeted()
        {
            using (var context = new AppDbContext())
            {
                var items = context.TweetsRaw.Where(t => !t.IsRetweetStatus).ToList();
                return items;
            }
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
