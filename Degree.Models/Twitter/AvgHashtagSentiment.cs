using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Degree.Models.Twitter
{
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
                return -1;
            }
        }
        public double AvgNeutralScore
        {
            get
            {
                if (Tweets != 0)
                    return NeutralsScore.Sum() / Tweets;
                return -1;
            }
        }
        public double AvgNegativeScore
        {
            get
            {
                if (Tweets != 0)
                    return NegativesScore.Sum() / Tweets;
                return -1;
            }
        }
        public int PositiveLabel { get; set; }
        public int NeutralLabel { get; set; }
        public int NegativeLabel { get; set; }
        public int MixedLabel { get; set; }
        public int Tweets { get; set; }
        [JsonIgnore]
        public List<double> PositivesScore { get; set; }
        [JsonIgnore]
        public List<double> NegativesScore { get; set; }
        [JsonIgnore]
        public List<double> NeutralsScore { get; set; }

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
