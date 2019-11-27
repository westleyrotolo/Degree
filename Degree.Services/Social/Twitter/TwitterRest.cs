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
        public static async Task DownloadWCFTweets()
        {
            var auth = await TwitterAuthorize.AccessToken();
            await GetTweets(auth, WCFTweetRequest);
        }

        private static async Task GetTweets(TokenAuth auth, TweetRequest tweetRequest)
        {
            try
            {
                using (var handler = new HttpClientHandler())
                {
                    handler.ServerCertificateCustomValidationCallback =
                        (message, cert, chain, errors) => { return true; };
                    using (var httpClient = new HttpClient(handler))
                    using (var client = new HttpClient(handler))
                    {
                        string baseAddress = "https://api.twitter.com/1.1/tweets/search/30day/analysis.json";

                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer  {auth.AccessToken}");
                      
                        int index = 1;
                        do
                        {
                            Console.WriteLine($"Page: {index++}");
                            var json = JsonConvert.SerializeObject(tweetRequest);
                            var stringContent = new StringContent(json);
                            var request = client.PostAsync(baseAddress, stringContent).Result;
                            var content = await request.Content.ReadAsStringAsync();
                            await FileHelper.WriteFile($"wcf-{index}.json", content);
                            var result = JsonConvert.DeserializeObject<TweetResult>
                                                (
                                                    content,
                                                    new IsoDateTimeConverter
                                                    {
                                                        DateTimeFormat = "ddd MMM dd HH:mm:ss K yyyy",
                                                        Culture = new System.Globalization.CultureInfo("en-US")
                                                    }
                                                );
                            Console.WriteLine($"Found: {result.Results.Count}");
                            tweetRequest.Next = result.Next;
                            result.Results.ForEach(x =>
                            {
                                // Normalize ID, for store.
                                if (x.QuotedStatusId == 0)
                                    x.QuotedStatusId = null;
                                if (x.RetweetedStatusId == 0)
                                    x.RetweetedStatusId = null;
                                if (x.ExtendedTweet != null)
                                    x.ExtendedTweet.TweetRawId = x.Id;
                            });
                        } while (!string.IsNullOrEmpty(tweetRequest.Next));
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }


        }

        private static TweetRequest WCFTweetRequest 
        {
            get
            {
                var tweetRequest = new TweetRequest();
                tweetRequest.MaxResults = 500;
                tweetRequest.AddFromDate(new DateTime(2019, 3, 29, 0, 0, 0));
                tweetRequest.AddToDate(new DateTime(2019, 3, 31, 23, 59, 59));
                tweetRequest.Query = TweetRequest
                .QueryBuilder
                .InitQuery()
                .Hashtag("#Adozionigay")
                .Or()
                .Hashtag("#Prolgbt")
                .Or()
                .Hashtag("#Famigliatradizionale")
                .Or()
                .Hashtag("#cirinnà")
                .Or()
                .Hashtag("famigliaarcobaleno")
                .Or()
                .Hashtag("#Congressodellefamiglie")
                .Or()
                .Hashtag("#Congressomondialedellefamiglie")
                .Or()
                .Hashtag("#WCFVerona")
                .Or()
                .Hashtag("#NoWCFVerona")
                .Or()
                .Hashtag("#No194")
                .Or()
                .Hashtag("#noeutonasia")
                .Or()
                .Hashtag("#uteroinaffitto")
                .Or()
                .Hashtag("#NoDDLPillon")
                .Or()
                .Hashtag("#Pillon")
                .Or()
                .Hashtag("#Pilloff")
                .Or()
                .Hashtag("#Spadafora")
                .Or()
                .Hashtag("#Affidocondiviso")
                .Or()
                .Hashtag("#Affidoparitario")
                .Build();
                return tweetRequest;
            }

        }
    }
}
