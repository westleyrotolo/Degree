using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Degree.Models
{
    public class TweetRequest
    {
        private const string OR = " OR ";
        private const string AND = " ";
        private const string OPENBRACKETS = "(";
        private const string CLOSEBRACKETS = ")";
        private const string DATEFORMAT = "yyyyMMddHHmm";


        [JsonProperty("query", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Query { get; set; }

        [JsonProperty("next", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Next { get; set; }

        [JsonProperty("fromDate", NullValueHandling = NullValueHandling.Ignore)]
        public string FromDate => fromDate == default(DateTime) ? null : fromDate.ToString(DATEFORMAT);

        private DateTime fromDate;

        [JsonProperty("toDate", NullValueHandling = NullValueHandling.Ignore)]
        public string ToDate => toDate == default(DateTime) ? null : toDate.ToString(DATEFORMAT);

        [JsonProperty("maxResults")]
        public int MaxResults { get; set; }


        private DateTime toDate;
        

        public void AddFromDate(DateTime date)
        {
            fromDate = date.ToUniversalTime();
        }
        public void AddToDate(DateTime date)
        {
            toDate = date.ToUniversalTime();
        }



        #region query builder
        public class QueryBuilder
        {
            private string Query = "";
            public static QueryBuilder InitQuery()
            {
                return new QueryBuilder();
            }
            public QueryBuilder And()
            {
                this.Query += AND;
                return this;
            }
            public QueryBuilder Or()
            {
                this.Query += OR;
                return this;
            }
            public QueryBuilder OpenBrackets()
            {
                this.Query += OPENBRACKETS;
                return this;
            }
            public QueryBuilder CloseBreckets()
            {
                this.Query += CLOSEBRACKETS;
                return this;
            }
            public QueryBuilder Hashtag(string hashtag)
            {
                this.Query += $"#{NormalizeHashtag(hashtag)}";
                return this;
            }
            public QueryBuilder From(string username)
            {
                this.Query += $"from:{NormalizeUsername(username)}";
                return this;
            }
            public QueryBuilder To(string username)
            {
                this.Query += $"to:{NormalizeUsername(username)}";
                return this;
            }
            public QueryBuilder Url(string url)
            {
                this.Query += $"url:{url}";
                return this;
            }
            public QueryBuilder Mention(string mention)
            {
                this.Query += $"@{mention}";
                return this;
            }
            public QueryBuilder Place(string place)
            {
                this.Query += $"place:\"{place}\"";
                return this;
            }
            public QueryBuilder PlaceCountry(string placeCountry)
            {
                this.Query += $"place_country:{placeCountry}";
                return this;
            }
            public QueryBuilder HasGeo()
            {
                this.Query += "has:geo";
                return this;
            }
            public QueryBuilder HasProfileGeo()
            {
                this.Query += "has:profile_geo";
                return this;
            }
            public QueryBuilder IsVerified()
            {
                this.Query += "is:verified";
                return this;
            }
            public string Build()
            {
                return this.Query;
            }
            #endregion
            private static string NormalizeHashtag(string hashtag)
            {
                return hashtag.Trim().Replace("#", "");
            }
            private static string NormalizeUsername(string username)
            {
                return username.Trim().Replace("@", "");
            }
        }
    }
}
