using System;
namespace Degree.Models.WebApi
{
    public class ApiRequest
    {
        public int page { get; set; }
        public int itemPerPage { get; set; }
        public string[] hashtags { get; set; }
    }
}
