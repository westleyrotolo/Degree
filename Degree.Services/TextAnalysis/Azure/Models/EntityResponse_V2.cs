using System;
using System.Collections.Generic;

namespace Degree.Services.TextAnalysis.Azure.Models.V2
{
    public class Match
    {
        public double wikipediaScore { get; set; }
        public double entityTypeScore { get; set; }
        public string text { get; set; }
        public int offset { get; set; }
        public int length { get; set; }
    }

    public class Entity
    {
        public string name { get; set; }
        public List<Match> matches { get; set; }
        public string wikipediaLanguage { get; set; }
        public string wikipediaId { get; set; }
        public string wikipediaUrl { get; set; }
        public string bingId { get; set; }
        public string type { get; set; }
        public string subType { get; set; }
    }

    public class Document
    {
        public string id { get; set; }
        public List<Entity> entities { get; set; }
    }

    public class EntityResponse_V2
    {
        public List<Document> documents { get; set; }
        public List<object> errors { get; set; }
    }
}
