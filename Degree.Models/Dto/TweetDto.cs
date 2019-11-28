using System;
using System.Collections.Generic;

namespace Degree.Models.Dto
{
    public class TweetDto
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }
        public long? InReplyToStatusId { get; set; }
        public string InReplyToScreenName { get; set; }
        public bool IsRetweetStatus { get; set; }
        public bool IsQuoteStatus { get; set; }
        public long QuoteCount { get; set; }
        public long ReplyCount { get; set; }
        public long RetweetCount { get; set; }
        public long FavoriteCount { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string UserScreenName { get; set; }
        public bool UserVerified { get; set; }
        public int UserFollowers { get; set; }
        public int UserFollowing { get; set; }
        public string UserProfileImage { get; set; }
        public string UserProfileBanner { get; set; }
        public bool UserDefaultProfile { get; set; }
        public bool UserDefaultProfileImage { get; set; }
        public string Sentiment { get; set; }
        public double PositiveScore { get; set; }
        public double NeutralScore { get; set; }
        public double NegativeScore { get; set; }

        public List<SentimentSentenceDto> SentimentSentence { get; set; }
    }
    public class SentimentSentenceDto
    {
            public string Sentiment { get; set; }
            public double PositiveScore { get; set; }
            public double NeutralScore { get; set; }
            public double NegativeScore { get; set; }
            public int Offset { get; set; }
            public int Length { get; set; }
    }
}
