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
                await context.Set<T>().AddOrUpdate(obj);
                await context.SaveChangesAsync();
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
                    if (tweet.ExtendedTweet != null)
                    {
                        if (context.ExtendedTweets.Any(x => x.TweetRawId == tweet.Id))
                        {
                            context.Entry(tweet.ExtendedTweet).State = EntityState.Modified;
                        }
                        else
                        {
                            context.Entry(tweet.ExtendedTweet).State = EntityState.Added;
                        }
                       
                    }
                    if (tweet.QuotedStatus != null)
                    {
                       await InsertAsync(tweet.QuotedStatus);
                    }
                    if (tweet.RetweetedStatus != null)
                    {
                        await InsertAsync(tweet.RetweetedStatus);  
                    }
                    if (context.TweetsRaw.Any(x=>x.Id == tweet.Id))
                    {
                        context.Entry(tweet).State = EntityState.Modified;
                    }
                    else
                    {
                        context.Entry(tweet).State = EntityState.Added;
                    }
                
                    await context.TweetsRaw.AddOrUpdate<TweetRaw>(tweet);
                    await context.SaveChangesAsync();
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
    }
}
