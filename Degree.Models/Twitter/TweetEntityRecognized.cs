using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Degree.Models.Twitter
{
    [Table("tweetsentities")]
    public class TweetEntityRecognized
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint Id { get; set; }
        public long TweetRawId { get; set; }
        public string EntityName { get; set; }
        public string EntityType { get; set; }
        public int Offset { get; set; }
        public int Length { get; set; }
        public double Score { get; set; }

        [JsonIgnore]
        public virtual TweetRaw TweetRaw { get; set; }
    }
}
