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

        public static async Task<T> InsertOrUpdateAsync(T obj)
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
        public static async Task<TweetRaw> InsertAsync(TweetRaw tweet)
        {
            try
            {

                using (var context = new AppDbContext())
                {

                    if (tweet.User != null)
                        if (context.Users.Any(x => x.Id == tweet.User.Id))
                        {
                            context.Entry(tweet.User).State = EntityState.Modified;
                        }
                        else
                        {
                            context.Entry(tweet.User).State = EntityState.Added;
                        }

                    if (tweet.QuotedStatus != null)
                    {
                        await InsertAsync(tweet.QuotedStatus);
                    }
                    if (tweet.RetweetedStatus != null)
                    {
                        await InsertAsync(tweet.RetweetedStatus);
                    }
                    if (context.TweetsRaw.Any(x => x.Id == tweet.Id))
                    {
                        context.Entry(tweet).State = EntityState.Modified;
                    }
                    else
                    {
                        context.Entry(tweet).State = EntityState.Added;
                    }
                    if (tweet.ExtendedTweet != null)
                    {
                        if (context.ExtendedTweets.Any(x => x.TweetRawId == tweet.Id))
                        {
                            context.Remove(tweet.ExtendedTweet);
                            context.Entry(tweet.ExtendedTweet).State = EntityState.Deleted;
                            await context.SaveChangesAsync();
                        }
                        else
                        {
                            context.Entry(tweet.ExtendedTweet).State = EntityState.Added;
                        }

                    }
                    await context.TweetsRaw.AddOrUpdate<TweetRaw>(tweet);
                    await context.SaveChangesAsync();
                    context.Entry(tweet).State = EntityState.Detached;
                    context.Entry(tweet.User).State = EntityState.Detached;
                    if (tweet.ExtendedTweet != null)
                        context.Entry(tweet.ExtendedTweet).State = EntityState.Detached;
                    return tweet;

                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public static IList<TweetRaw> Fetch()
        {
            using (var context = new AppDbContext())
            {
                var items = context.TweetsRaw.Take(10).ToList();
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
                    await AppDbHelper<User>.InsertOrUpdateAsync(t.User);
                }
                if (t.QuotedStatus != null)
                {
                    await AddTweet(t.QuotedStatus);
                }
                if (t.RetweetedStatus != null)
                {
                    await AddTweet(t.RetweetedStatus);
                }

                await AppDbHelper<TweetRaw>.InsertOrUpdateAsync(t);

                if (t.ExtendedTweet != null)
                {
                    t.ExtendedTweet.TweetRawId = t.Id;
                    await AppDbHelper<ExtendedTweet>.InsertOrUpdateAsync(t.ExtendedTweet);
                }
                if (t.Place != null)
                {
                    t.Place.TweetRawId = t.Id;
                    await AppDbHelper<Place>.InsertOrUpdateAsync(t.Place);
                    if (t.Place.BoundingBox != null)
                    {
                        t.Place.BoundingBox.PlaceId = t.Place.Id;
                        await AppDbHelper<BoundingBox>.InsertOrUpdateAsync(t.Place.BoundingBox);
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }
    }

}
