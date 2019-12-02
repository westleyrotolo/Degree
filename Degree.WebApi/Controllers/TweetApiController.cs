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
}
