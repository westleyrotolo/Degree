using System;
using System.IO;
using Tweetinvi.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;

namespace Degree.Services.Social.Twitter.TweetAuth
{
    public class TwitterAuthorize
    {
        private const string GRANT_TYPE = "client_credentials";
        public static ITwitterCredentials TweetinviCredentials()
        {
            return new TwitterCredentials(Keys.Twitter.CONSUMER_KEY, Keys.Twitter.CONSUMER_SECRET, Keys.Twitter.ACCESS_TOKEN, Keys.Twitter.ACCESS_TOKEN_SECRET);
        }
        public static async Task<TokenAuth> AccessToken()
        {
            TokenAuth auth;
            using (var handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback =
                    (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(handler))
                using (var client = new HttpClient(handler))
                {
                    string baseAddress = "https://api.twitter.com/oauth2/token";


                    string grant_type = GRANT_TYPE;
                    string encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(Keys.Twitter.CONSUMER_KEY + ":" + Keys.Twitter.CONSUMER_SECRET));
                    client.DefaultRequestHeaders.Add("Authorization", $"Basic {encoded}");
                    var form = new Dictionary<string, string>
                {
                    {"grant_type", grant_type},
                };
                    var r = client.PostAsync(baseAddress, new FormUrlEncodedContent(form)).Result;
                    var content = await r.Content.ReadAsStringAsync();
                    auth = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenAuth>(content);
                    return auth;
                }
            }

        }

    }
}   
