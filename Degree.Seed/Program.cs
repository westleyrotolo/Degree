using CsvHelper;
using Degree.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;

namespace Degree.Seed
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var file = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName, "CSV", "adozione.csv");
            try
            {
                using (var client = new HttpClient())
                {
                    string baseAddress = "https://api.twitter.com/oauth2/token";

                    string grant_type = "client_credentials";
                    string client_id = "v9oZgCQ1eNUoLSv5u38SoH7W8";
                    string client_secret = "IUIUV11IJSuW2ZWPyMsyzCEofh6jeokCyADm9YtJYKKO6IoIlX";
                    string encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(client_id + ":" + client_secret));
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);
                    string auth = "Basic djlvWmdDUTFlTlVvTFN2NXUzOFNvSDdXODpJVUlVVjExSUpTdVcyWldQeU1zeXpDRW9maDZqZW9rQ3lBRG05WXRKWUtLTzZJb0lsWA==";
                    var form = new Dictionary<string, string>
                {
                    {"grant_type", grant_type},
                };
                    var r = await client.PostAsync(baseAddress, new FormUrlEncodedContent(form));
                    var content = await r.Content.ReadAsStringAsync();
                }
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
                            Console.WriteLine($"Tweet: {tweet.tweet_text}.\n User: {tweet.user_screen_name}`\n Link:https://twitter.com/{tweet.user_id}/status/{tweet.tweet_id}\n\n");
                        }

                    }
                }
                else if (response.Equals("2"))
                {

                    var tweets = Degree.AppDbContext.AppDbHelper<TweetRaw>.Fetch().ToList();
                    Console.WriteLine($"Tweet: {tweets[7].tweet_text}.\n User: {tweets[7].user_screen_name}.n Link:https://twitter.com/{tweets[7].user_id}/status/{tweets[7].tweet_id}");

                    var tweet = Degree.Services.Social.Twitter.TwitterApi.FindById(tweets[7].tweet_id);
                }

            } while (!response.Equals("0"));
           
           
        }
    }
}
