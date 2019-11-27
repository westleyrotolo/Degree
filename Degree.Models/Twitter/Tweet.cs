using System;
using System.Collections.Generic;

namespace Degree.Models
{
    public class Tweet
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public bool IsRetweeted { get; set;}
        public bool IsQuoted { get; set; }
        public string Quoted { get; set; }
        public long QuoteCount { get; set; }
        public long ReplyCount { get; set; }
        public long RetweetCount { get; set; }
        public long FavoriteCount { get; set; }
        public List<string> Hashtags { get; set; } = new List<string>();
    }

}
