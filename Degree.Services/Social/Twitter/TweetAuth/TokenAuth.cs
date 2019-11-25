using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Degree.Services.Social.Twitter.TweetAuth
{
    public class TokenAuth
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}
