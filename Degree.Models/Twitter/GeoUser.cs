using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Degree.Models.Twitter
{
    public class GeoUser
    {
        [JsonProperty("place_id")]
        [Key]
        [Column(Order = 1)]
        public string Id { get; set; }
        [Column(Order = 2)]
        public long UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public string DisplayName { get; set; }
        public string Type { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public double Importance { get; set; }
    }
}
