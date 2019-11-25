using Degree.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
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
            using (var context = new AppDbContext())
            {
                // context.Entry(tweet).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                await context.AddAsync(tweet);
                await context.SaveChangesAsync();
                return tweet;
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
