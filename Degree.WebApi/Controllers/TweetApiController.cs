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
        [HttpGet("AvgSentiments")]
        public IEnumerable<AvgHashtagSentiment> AvgSentiments()
        {
            try
            {
                var avgs = AppDbHelper<TweetRaw>.AvgHashtags();
                return avgs;
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
        public async Task Send(TweetRaw tweetRaw)
        {
            
            await Clients.All.SendAsync("broadcastMessage", tweetRaw);
        }
    }
}
