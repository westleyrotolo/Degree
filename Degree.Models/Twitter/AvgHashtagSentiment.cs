using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Degree.Models.Twitter
{
    public class GroupedTweetsGeo
    {
        public DateTime FromDate { get; set; }
        public List<TweetsGeoCode> Tweets { get; set; } = new List<TweetsGeoCode>();
    }
    public class TweetsGeoCode
    {
        public DateTime CreatedAt { get; set; }
        public double Latitude {  get; set; }
        public double Longitude { get; set; }
        public string Text { get; set; }
        public string Sentiment { get; set; }
        public double PositiveScore { get; set; }
        public double NegativeScore { get; set; }
        public double NeutralScore { get; set; }
        public string UserScreenName { get; set; }
        public string UserProfileName { get; set; }
    }

    public class AvgHashtagSentiment
    {
        public string Hashtags { get; set; }
        public List<AvgSentiment> AvgSentiments { get; set; } = new List<AvgSentiment>();
    }
    public class AvgSentiment
    {
        public double AvgPositiveScore
        {
            get
            {
                if (Tweets != 0)
                    return PositivesScore.Sum() / Tweets;
                return 0;
            }
        }
        public double AvgNeutralScore
        {
            get
            {
                if (Tweets != 0)
                    return NeutralsScore.Sum() / Tweets;
                return 0;
            }
        }
        public double AvgNegativeScore
        {
            get
            {
                if (Tweets != 0)
                    return NegativesScore.Sum() / Tweets;
                return 0;
            }
        }
        public double CumAvgPositiveScore
        {
            get
            {
                if (CumulativeTweets != 0)
                    return CumulativePositivesScore.Sum() / CumulativeTweets;
                return 0;
            }
        }
        public double CumAvgNeutralScore
        {
            get
            {
                if (CumulativeTweets != 0)
                    return CumulativeNeutralsScore.Sum() / CumulativeTweets;
                return 0;
            }
        }
        public double CumAvgNegativeScore
        {
            get
            {
                if (CumulativeTweets != 0)
                    return CumulativeNegativesScore.Sum() / CumulativeTweets;
                return 0;
            }
        }
        public int PositiveLabel { get; set; }
        public int NeutralLabel { get; set; }
        public int NegativeLabel { get; set; }
        public int MixedLabel { get; set; }
        public int Tweets { get; set; }
        public int CumulativeTweets { get; set; }
        [JsonIgnore]
        public List<double> PositivesScore { get; set; } = new List<double>();
        [JsonIgnore]
        public List<double> NegativesScore { get; set; } = new List<double>();
        [JsonIgnore]
        public List<double> NeutralsScore { get; set; } = new List<double>();
        [JsonIgnore]
        public List<double> CumulativePositivesScore { get; set; } = new List<double>();
        [JsonIgnore]
        public List<double> CumulativeNegativesScore { get; set; } = new List<double>();
        [JsonIgnore]
        public List<double> CumulativeNeutralsScore { get; set; } = new List<double>();


        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public void AddLabel(string label)
        {
            if (label.ToLower() == "positive")
                PositiveLabel++;
            if (label.ToLower() == "negative")
                NegativeLabel++;
            if (label.ToLower() == "neutral")
                NeutralLabel++;
            if (label.ToLower() == "mixed")
                MixedLabel++;
        }
    }
}
