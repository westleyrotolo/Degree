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

namespace Degree.WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TweetApiController : ControllerBase
    {

        [HttpPost("Search")]
        public IEnumerable<TweetDto> TweetsByHashtags([FromBody]ApiRequest apiRequest)
        {
            try
            { 
                var tweets = AppDbHelper<TweetRaw>.FetchContains(apiRequest.hashtags, apiRequest.page,apiRequest.itemPerPage, true);
                return tweets;
            }
            catch (Exception ex) 
		            {
                return null;
            }
        }
    }
}
