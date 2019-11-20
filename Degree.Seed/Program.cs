using CsvHelper;
using Degree.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Degree.Seed
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var file = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName, "CSV", "adozione.csv");
            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader))
            {
                var tweets = csv.GetRecords<TweetRaw>();
                int i = 0;
                foreach (var tweet in tweets)
                {
                    i++;
                    if (i == 100)
                        break;
                    await AppDbContext.AppDbHelper<TweetRaw>.InsertOrUpdateAsync(tweet);
                    Console.WriteLine($"Tweet: {tweet.tweet_text}.\n User: {tweet.user_screen_name}.n Link:https://twitter.com/{tweet.user_id}/status/{tweet.tweet_id}");
                }
               
            }
        }
    }
}
