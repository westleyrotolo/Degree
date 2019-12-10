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
using Degree.AppDbContext;
using Degree.Services.TextAnalysis.Azure;
using Degree.Services;
using Degree.Models.Twitter;

namespace Degree.Seed
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Keys.LoadKey();
                string response = "";
                do
                {
                    Console.WriteLine("(D)ownload Tweets to files");
                    Console.WriteLine("(S)tore Tweets in DB from files");
                    Console.WriteLine("(G)et sentiment from Tweets");
                    Console.WriteLine("(E)xtract Hashtag");
                    Console.WriteLine("(C)oordinate Users");
                    Console.Write("Inserisci comando: ");
                    response = Console.ReadKey().KeyChar.ToString();
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
                    else if (response.Equals("g", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var tweets = AppDbHelper<TweetRaw>.FetchNotRetweeted();
                        var sentiments = await AzureSentiment.AnalyzeTweetSentiment(tweets);
                        foreach (var s in sentiments.Select((s, i) => new { tweet = s, index = i }))
                        {
                            Console.WriteLine($"{s.index}. TwId:{s.tweet.TweetRawId}, Sentiment:{s.tweet.Sentiment.ToString()}");
                            await AppDbHelper<TweetSentiment>.InsertOrUpdateSentimentAsync(s.tweet);
                        }
                        Console.WriteLine("Finish");
                    }
                    else if (response.Equals("e", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var tweets = AppDbHelper<TweetRaw>.Fetch();
                        var hashtags = new List<TweetHashtags>();
                        foreach (var t in tweets)
                        {
                            Constants.Hashtags.ToList().ForEach(x =>
                            {
                                if (t.Text.Contains(x, StringComparison.InvariantCultureIgnoreCase))
                                {

                                    var tweetHashtag = new TweetHashtags
                                    {
                                        TweetRawId = t.Id,
                                        Hashtags = x
                                    };
                                    hashtags.Add(tweetHashtag);
                                }

                            });

                        }
                        var fuck = hashtags.GroupBy(x => x.Hashtags).Select(x => new { h = x.Key, v = x.Count() }).ToList();
                        foreach (var f in fuck)
                        {
                            Console.WriteLine($"{f.h}, {f.v}");
                        }
                        int take = hashtags.Count / 4;
                        int p = 0;
                        var tasksItems = new List<List<TweetHashtags>>();
                        for (int i = 0; i < 4; i++)
                        {
                            tasksItems.Add(hashtags.Skip(take * p++).Take(take).ToList());
                        }
                        var tasks = tasksItems.Select((x, i) => StoreHashtags(x, i));
                        await Task.WhenAll(tasks);

                    }
                    else if (response.Equals("c", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var users = AppDbHelper<User>.FetchUsers();
                        await FetchGeoUsers(users);

                    }

                    } while (!response.Equals("0"));
            }
            catch (Exception ex)
            {

            }



        }
        private static async Task FetchGeoUsers(List<User> users)
        {
            var GeoCoding = new Services.Location.GoogleMaps.GoogleMapsHelper();
            Dictionary<string, string> Places = new Dictionary<string, string>();
            foreach (var u in users.Where(x => !string.IsNullOrEmpty(x.Location)).Select((x, i) => new { item = x, index = i }))
            {
                List<GeoUser> GeoUsers = new List<GeoUser>();

                try
                {
                    Console.WriteLine($"Item: {u.index}, User: {u.item.ScreenName}");
                    if (Places.ContainsValue(u.item.Location.ToLower()))
                    {
                        var places = Places.Where(x => x.Value == u.item.Location.ToLower())
                                .Select(x => x.Key)
                                .ToList();
                        List<GeoUser> temp = new List<GeoUser>();
                        foreach (var g in GeoUsers)
                        {
                            if (places.Contains(g.Id))
                            {

                                Console.WriteLine($"{g.DisplayName} exist");
                                var geoUser = new GeoUser
                                {
                                    Id = g.Id,
                                    DisplayName = g.DisplayName,
                                    Lat = g.Lat,
                                    Lon = g.Lon,
                                    Type = g.Type,
                                    UserId = u.item.Id
                                };
                                temp.Add(geoUser);
                            }
                        }
                        GeoUsers.AddRange(temp);
                    }
                    else
                    {
                        var geoUsers = await GeoCoding.GeoReverse(u.item.Location);
                        geoUsers.ForEach(x =>
                        {
                            Places.TryAdd(x.Id, u.item.Location.ToLower());
                            x.UserId = u.item.Id;
                        }
                        );
                        Console.WriteLine($"Item: {u.index}, Founded: {geoUsers.Count}");
                        GeoUsers.AddRange(geoUsers);

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR. {u.item.ScreenName}. Message: {ex.Message}");
                }
                await StoreGeoUsers(GeoUsers, u.index);
            }

        }
        private static async Task StoreGeoUsers(List<GeoUser> geoUsers, int index)
        {
            foreach (var g in geoUsers.Select((x,i)=>new { geoUser = x, index = i }))
            {
                Console.WriteLine($"Thread:{index}, Item: {g.index}, Place: {g.geoUser.DisplayName}");
                await AppDbHelper<GeoUser>.InsertOrUpdateGeoUsersAsync(g.geoUser);
            }


        }
        private static async Task StoreHashtags(List<TweetHashtags> hashtags, int index)
        {
                foreach (var h in hashtags.Select((x, i) => new { item = x, index = i }))
                {
                    Console.WriteLine($"Thread:{index}. Item: {h.index}. Insert in TwId: {h.item.TweetRawId}, Hashtags: {h.item.Hashtags}");
                    await AppDbHelper<TweetRaw>.InsertOrUpdateHashtagsAsync(h.item);
                }
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
