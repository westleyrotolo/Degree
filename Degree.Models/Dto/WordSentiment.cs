using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
namespace Degree.Models.Dto
{
    public class WordSentiment
    {
        public double AvgPositiveScore { get; set; }
        public double AvgNeutralScore { get; set; }
        public double AvgNegativeScore { get; set; }
        public int PositiveLabel { get; set; }
        public int NeutralLabel { get; set; }
        public int NegativeLabel { get; set; }
        public int MixedLabel { get; set; }
        public int Tweets { get; set; }


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
