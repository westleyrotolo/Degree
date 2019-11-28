using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Degree.Models
{
    [Table("tweetssentiment")]
    public class TweetSentiment
    {
        [Key]
        public long TweetRawId { get; set; }
        public DocumentSentimentLabel Sentiment { get; set; }
        public double PositiveScore { get; set; }
        public double NeutralScore { get; set; }
        public double NegativeScore { get; set; }
        public List<TweetSentenceSentiment> Sentences { get; set; } = new List<TweetSentenceSentiment>();

        [JsonIgnore]
        public TweetRaw TweetRaw { get; set; }

    }


    [Table("tweetsentencesentiment")]
    public class TweetSentenceSentiment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public SentenceSentimentLabel Sentiment { get; set; }
        public double PositiveScore { get; set; }
        public double NeutralScore { get; set; }
        public double NegativeScore { get; set; }
        public int Offset { get; set; }
        public int Length { get; set; }
        public string[] Warnings { get; set; }
        [JsonIgnore]
        public TweetSentiment TweetsSentiment { get; set; }
        public long TweetSentimentId { get; set; }
    }



    public enum DocumentSentimentLabel
    {
        Positive,
        Neutral,
        Negative,
        Mixed
    }

    public enum SentenceSentimentLabel
    {
        Positive,
        Neutral,
        Negative
    }

}
