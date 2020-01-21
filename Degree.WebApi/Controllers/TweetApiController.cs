using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Degree.AppDbContext;
using Degree.Models;
using Degree.Models.Dto;
using Degree.Models.WebApi;
using Degree.Models.Twitter;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace Degree.WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TweetApiController : ControllerBase
    {
        [HttpGet("")]
        public string Test()
        {
            return "Test";
        }
        [HttpPost("Search")]
        public IEnumerable<TweetDto> TweetsByHashtags([FromBody]ApiRequest apiRequest)
        {
            try
            {
                var tweets = AppDbHelper<TweetRaw>.FetchContains(apiRequest.hashtags, apiRequest.page, apiRequest.itemPerPage, true, apiRequest.OrderSentiment);
                return tweets;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("AvgWord")]
        public WordSentiment AvgWord(string word)
        {
            try
            {
                var avgWord = AppDbHelper<TweetRaw>.WordSentiment(word);
                return avgWord;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("GetHashtags")]
        public IEnumerable<HashtagsCount> GetHashtags()
        {
            try
            {
                var hashtags = AppDbHelper<TweetRaw>.GroupbyHashtags();
                return hashtags;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("GetTweetByTime")]
        public IEnumerable<TweetDto> GetTweetsByTime()
        {
            try
            {
                var tweets = AppDbHelper<TweetRaw>.FetchTweetInTime(new DateTime(2019, 3, 29, 0, 0, 0), new DateTime(2019, 3, 31, 23, 59, 59), true);
                return tweets;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("MoreActives")]
        public IEnumerable<UserDto> MoreActives(int skip, int take)
        {
            try
            {
                var users = AppDbHelper<TweetRaw>.MoreActives(skip, take);
                return users;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("MoreRetweets")]
        public IEnumerable<UserDto> MoreRetweets(int skip, int take)
        {
            try
            {
                var users = AppDbHelper<TweetRaw>.MoreRetweeted(skip, take);
                return users;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("MoreFavorites")]
        public IEnumerable<UserDto> MoreFavorites(int skip, int take)
        {
            try
            {
                var users = AppDbHelper<TweetRaw>.MoreFavorites(skip, take);
                return users;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        [HttpGet("GeoTweets")]
        public List<GroupedTweetsGeo> GeoTweets()
        {
            try
            {
                var geoTweets = AppDbHelper<TweetRaw>.TweetsGeoCode();
                return geoTweets;
            
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        [HttpGet("AvgSentiments")]
        public string AvgSentiments()
        {
            try
            {
                var avgs = AppDbHelper<TweetRaw>.AvgHashtags();
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(avgs, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
                return json;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("TagCloud")]
        public IEnumerable<TagCloud> TagClouds()
        {
            try
            {
                var tagClouds = AppDbHelper<TweetRaw>.TagWord();
                return tagClouds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("GetGroupedHashtags")]
        public IEnumerable<HashtagsGrouped> GetGroupedHashtagsBySentiment()
        {
            try
            {
                var groupedHashtags = AppDbHelper<HashtagsGrouped>.GroupSentimentByHashtags();
                return groupedHashtags;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
     
    }
    public class NotifyHub : Hub
    {
        
    }
}
