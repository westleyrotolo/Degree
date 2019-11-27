using System;
using System.Collections.Generic;

namespace Degree.Models.Twitter
{
    public class TweetsSentiment
    {
        public long TweetRawId { get; set; }
        public DocumentSentimentLabel Sentiment { get; set; }
        public double PositiveScore { get; set; }
        public double NeutralScore { get; set; }
        public double NegativeScore { get; set; }
        public List<SentenceSentiment> Sentences { get; set; }

    }


    public class SentenceSentiment
    {
        public SentenceSentimentLabel Sentiment { get; set; }
        public double PositiveScore { get; set; }
        public double NeutralScore { get; set; }
        public double NegativeScore { get; set; }
        public int Offset { get; set; }
        public int Length { get; set; }
        public string[] Warnings { get; set; }
        public TweetsSentiment TweetsSentiment { get; set; }
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
