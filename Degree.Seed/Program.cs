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

namespace Degree.Seed
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var file = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName, "CSV", "adozione.csv");
            try
            {

            }
            catch (Exception ex)
            {

            }
          
            string response = "";
            do
            {
                Console.Write("Inserisci comando");
                response = Console.ReadKey().KeyChar.ToString();
                if (response.Equals("1"))
                {
                    using (var reader = new StreamReader(file))
                    using (var csv = new CsvReader(reader))
                    {
                        var tweets = csv.GetRecords<TweetRaw>();
                        foreach (var tweet in tweets)
                        {
                            await AppDbContext.AppDbHelper<TweetRaw>.InsertOrUpdateAsync(tweet);
                     
                        }

                    }
                }
                else if (response.Equals("2"))
                {

                    var tweets = Degree.AppDbContext.AppDbHelper<TweetRaw>.Fetch().ToList();
                }
                else if (response.Equals("3"))
                {
                    var auth =await TwitterAuthorize.AccessToken();
                    var tweets = await TwitterRest.GetTweets(auth);
                    foreach (var t in tweets)
                    {
                        await AppDbContext.AppDbHelper<TweetRaw>.InsertAsync(t);
                    }
                }

            } while (!response.Equals("0"));
           
           
        }
    }
}
