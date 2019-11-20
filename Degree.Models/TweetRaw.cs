using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Degree.Models
{
    public class TweetRaw
    {
        [Key]
        public long id { get; set; }
        public DateTime tweet_created_at { get; set; }
        public long tweet_id { get; set; }
        public long tweet_id_str { get; set; }
        public string tweet_text { get; set; }
        public string tweet_source { get; set; }
        public long tweet_truncated { get; set; }
        public string tweet_in_reply_to_status_id { get; set; }
        public string tweet_in_reply_to_user_id { get; set; }
        public string tweet_in_reply_to_screen_name { get; set; }
        public string tweet_place_country { get; set; }
        public string tweet_place_full_name { get; set; }
        public int tweet_retweet_count { get; set; }
        public int tweet_favorite_count { get; set; }
        public int tweet_favorited { get; set; }
        public int tweet_retweeted { get; set; }
        public string tweet_lang { get; set; }
        public long user_id { get; set; }
        public long user_id_str { get; set; }
        public string user_name { get; set; }
        public string user_screen_name { get; set; }
        public string user_location { get; set; }
        public string user_description { get; set; }
        public int user_protected { get; set; }
        public int user_verified { get; set; }
        public int user_followers_count { get; set; }
        public int user_friends_count { get; set; }
        public int user_listed_count { get; set; }
        public int user_favourites_count { get; set; }
        public int user_statuses_count { get; set; }
        public string user_created_at { get; set; }
        public int user_geo_enabled { get; set; }
        public string user_lang { get; set; }
        public string tweet_sentiment { get; set; }
    }
}
