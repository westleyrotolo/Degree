using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Degree.Models
{
    public class TweetResult
    {
        [JsonProperty("results", NullValueHandling = NullValueHandling.Ignore)]
        public List<TweetRaw> Results { get; set; }

        [JsonProperty("next", NullValueHandling = NullValueHandling.Ignore)]
        public string Next { get; set; }

        [JsonProperty("requestParameters", NullValueHandling = NullValueHandling.Ignore)]
        public RequestParameters RequestParameters { get; set; }
    }

    public class RequestParameters
    {
        [JsonProperty("maxResults", NullValueHandling = NullValueHandling.Ignore)]
        public long MaxResults { get; set; }

        [JsonProperty("fromDate", NullValueHandling = NullValueHandling.Ignore)]
        public string FromDate { get; set; }

        [JsonProperty("toDate", NullValueHandling = NullValueHandling.Ignore)]
        public string ToDate { get; set; }
    }

    public class TweetRaw
    {
        [Key]
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long Id { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("truncated")]
        public bool Truncated { get; set; }

        //represented Tweet is a reply, this field will contain the integer representation of the original Tweet’s ID
        [JsonProperty("in_reply_to_status_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? InReplyToStatusId { get; set; }

        //represented Tweet is a reply, this field will contain the integer representation of the original Tweet’s author ID. This will not necessarily always be the user directly mentioned in the Tweet. 
        [JsonProperty("in_reply_to_user_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? InReplyToUserId { get; set; }

        //represented Tweet is a reply, this field will contain the screen name of the original Tweet’s author
        [JsonProperty("in_reply_to_screen_name", NullValueHandling = NullValueHandling.Ignore)]
        public string InReplyToScreenName { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [ForeignKey("User")]
        [JsonIgnore]
        public long UserId { get; set; }

        [JsonProperty("coordinates", NullValueHandling = NullValueHandling.Ignore)]
        public Coordinates Coordinates { get; set; } 

        [JsonProperty("place", NullValueHandling = NullValueHandling.Ignore)]
        public Place Place { get; set; }

        [JsonIgnore]
        public long? QuotedStatusId { get; set; }

        [JsonProperty("is_quote_status")]
        public bool IsQuoteStatus { get; set; }

        [JsonProperty("quoted_status", NullValueHandling = NullValueHandling.Ignore)]
        public TweetRaw QuotedStatus { get; set; }

        [JsonIgnore]
        public bool IsRetweetStatus { get; set; }





        [JsonIgnore]
        public long? RetweetedStatusId { get; set; }
        
        //Users can amplify the broadcast of Tweets authored by other users by retweeting . Retweets can be distinguished from typical Tweets by the existence of a retweeted_status attribute. This attribute contains a representation of the original Tweet that was retweeted
        [JsonProperty("retweeted_status", NullValueHandling = NullValueHandling.Ignore)]
        public TweetRaw RetweetedStatus { get; set; }

        [JsonProperty("quote_count")]
        public long QuoteCount { get; set; }

        [JsonProperty("reply_count")]
        public long ReplyCount { get; set; }

        [JsonProperty("retweet_count")]
        public long RetweetCount { get; set; }

        [JsonProperty("favorite_count")]
        public long FavoriteCount { get; set; }


        [JsonProperty("extended_tweet", NullValueHandling = NullValueHandling.Ignore)]
        public ExtendedTweet ExtendedTweet { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonIgnore]
        public TweetSentiment TweetSentiment { get; set; }
    }
    public class ExtendedTweet
    {
        [JsonIgnore]
        [Key]
        [ForeignKey("ExtendedTweet")]
        public long TweetRawId { get; set; }

        [JsonProperty("full_text", NullValueHandling = NullValueHandling.Ignore)]
        public string FullText { get; set; }

        [JsonProperty("display_text_range", NullValueHandling = NullValueHandling.Ignore)]
        public List<long> DisplayTextRange { get; set; }

        [JsonIgnore]
        public TweetRaw TweetRaw { get; set; }
    }
    
    public class Place
    {
        [Key]
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonIgnore]
        public string Attributes { get; set; }
        [JsonProperty("bounding_box", NullValueHandling = NullValueHandling.Ignore)]
        public BoundingBox BoundingBox { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
        [JsonProperty("full_name")]
        public string FullName { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("place_type")]
        public string PlaceType { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonIgnore]
        public TweetRaw TweetRaw { get; set; }
        [ForeignKey("TweetRaw")]
        [JsonIgnore]
        public long TweetRawId {get;set;}
    }

    public class BoundingBox
    {
        [Key]
        [ForeignKey("Place")]
        [JsonIgnore]
        public string PlaceId { get; set; }

        [JsonProperty("coordinates")]
        public List<List<List<double>>> Coordinates { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonIgnore]
        public Place Place { get; set; }
    }
    public class Coordinates
    {
        [Key]
        [ForeignKey("TweetRaw")]
        [JsonIgnore]
        public long TweetRawId { get; set; }

        [JsonProperty("coordinates", NullValueHandling = NullValueHandling.Ignore)]
        public List<double> GeoCoordinates { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonIgnore]
        public TweetRaw TweetRaw{get;set;}
    }
    public class User
    {
        [Key]
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("Url")]
        public string Url { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        [JsonProperty("followers_count")]
        public int Followers { get; set; }

        [JsonProperty("friends_count")]
        public int Following { get; set; }

        [JsonProperty("statuses_count")]
        public int Statuses { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("profile_image_url_https")]
        public string ProfileImage { get; set; }

        [JsonProperty("profile_banner_url")]
        public string ProfileBanner { get; set; }

        [JsonProperty("default_profile")]
        public bool DefaultProfile { get; set; }
        [Column("default_profile_image")]
        [JsonProperty("default_profile_image")]
        public bool DefaultProfileImage { get; set; }


        [JsonIgnore]
        public List<TweetRaw> TweetRaws { get; set; }

    }
}
