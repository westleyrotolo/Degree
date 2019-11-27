using CsvHelper;
using Degree.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using Degree.Services.Social.Twitter.TweetAuth;
using Degree.Services.Social.Twitter;
using Degree.Services.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Degree.Seed
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string response = "";
            do
            {
                Console.WriteLine("(D)ownload Tweets to files");
                Console.WriteLine("(S)tore Tweets in DB from files");
                Console.WriteLine("(G)et sentiment from Tweets");
                Console.Write("Inserisci comando: ");
                if (response.Equals("d", StringComparison.InvariantCultureIgnoreCase))
                {

                    var tweets = Degree.AppDbContext.AppDbHelper<TweetRaw>.Fetch().ToList();
                }
                else if (response.Equals("s", StringComparison.InvariantCultureIgnoreCase))
                {

                    var paths = new string[] { "WCF/WCF20192903/wcf2019329-", "WCF/WCF20193003/wcf2019330-", "WCF/WCF20193103/wcf2019331-" };

                    var tasks = paths.Select((x, i) => StoreTweet(paths[i], i));
                    await Task.WhenAll(tasks);
                   
                    Console.WriteLine("Finish.");
                    
                }

            } while (!response.Equals("0"));

           
        }
        private static async Task StoreTweet(string path, int nThread)
        {
            int index = 1;
            int page = 1;
            var content = await FileHelper.ReadFile($"{path}{page++}.json");
            while (!string.IsNullOrEmpty(content))
            {

                var result = JsonConvert.DeserializeObject<TweetResult>
                (
                    content,
                    new IsoDateTimeConverter
                    {
                        DateTimeFormat = "ddd MMM dd HH:mm:ss K yyyy",
                        Culture = new System.Globalization.CultureInfo("en-US")
                    }
                );
                var tweets = result.Results;
                int i = 1;
                Console.WriteLine("\nInsert New File...");
                foreach (var t in tweets)
                {
                    Console.WriteLine($"[Thread-{nThread}]{index++}. file:{page}.{i++}. Insert tweet {t.Id}, of {t.User.ScreenName}");
                    await AppDbContext.AppDbHelper<TweetRaw>.AddTweet(t);
                }

                content = await FileHelper.ReadFile($"{path}{page++}.json");
            }
        }
    }
}
