using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Degree.Models;
using Degree.Services.Social.Twitter.TweetAuth;
using Degree.Services.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq;
namespace Degree.Services.Social.Twitter
{
    public class TwitterRest
    {
        public static async Task<List<TweetRaw>> GetTweets(TokenAuth auth)
        {
            try
            {
                var tweets = new List<TweetRaw>();
                using (var handler = new HttpClientHandler())
                {
                    handler.ServerCertificateCustomValidationCallback =
                        (message, cert, chain, errors) => { return true; };
                    using (var httpClient = new HttpClient(handler))
                    using (var client = new HttpClient(handler))
                    {
                        string baseAddress = "https://api.twitter.com/1.1/tweets/search/30day/analysis.json";

                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer  {auth.AccessToken}");
                        var tweetRequest = new TweetRequest();
                        tweetRequest.Query = TweetRequest
                        .QueryBuilder
                        .InitQuery()
                        .Hashtag("#Sardine")
                        .Build();
                        int index = 0;
                        do
                        {
                            index++;
                            if (index == 2)
                                break;
                            var json = JsonConvert.SerializeObject(tweetRequest);
                            var stringContent = new StringContent(json);
                           var request = client.PostAsync(baseAddress, stringContent).Result;
                            var content = await request.Content.ReadAsStringAsync();
                            //var content = await FileHelper.ReadFile("file1.json");
                            await FileHelper.WriteFile($"file{index}.json", content);
                            var result = JsonConvert.DeserializeObject<TweetResult>
                                                (
                                                    content,
                                                    new IsoDateTimeConverter
                                                    {
                                                        DateTimeFormat = "ddd MMM dd HH:mm:ss K yyyy",
                                                        Culture = new System.Globalization.CultureInfo("en-US")
                                                    }
                                                );
                            tweetRequest.Next = result.Next;
                            result.Results.ForEach(x =>
                            {
                                if (x.QuotedStatusId == 0)
                                    x.QuotedStatusId = null;
                                if (x.RetweetedStatusId == 0)
                                    x.RetweetedStatusId = null;
                                if (x.ExtendedTweet != null)
                                    x.ExtendedTweet.TweetRawId = x.Id;
                            });
                            tweets.AddRange(result.Results);
                        } while (!string.IsNullOrEmpty(tweetRequest.Next));
                        return tweets;
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return null;
            }


        }
    }
}
