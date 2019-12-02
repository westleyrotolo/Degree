using System;
namespace Degree.Models.Twitter
{
    public class HashtagsGrouped
    {
        public string Hashtags { get; set; }
        public int Tweetcount { get; set; }
        public double PositiveScore { get; set; }
        public double NeutralScore { get; set; }
        public double NegativeScore { get; set; }
        public int PositiveLabel { get; set; }
        public int NeutralLabel { get; set; }
        public int MixedLabel { get; set; }
        public int NegativeLabel { get; set; }
    }
}
