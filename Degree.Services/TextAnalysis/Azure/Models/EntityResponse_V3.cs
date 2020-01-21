using System;
using System.Collections.Generic;

namespace Degree.Services.TextAnalysis.Azure.Models.V3
{
    public class Entity
    {
        public string Text { get; set; }
        public string Type { get; set; }
        public int Offset { get; set; }
        public int Length { get; set; }
        public double Score { get; set; }
        public string Subtype { get; set; }
    }

    public class Document
    {
        public string Id { get; set; }
        public List<Entity> Entities { get; set; }
    }

    public class EntityResponse_V3
    {
        public List<Document> Documents { get; set; }
        public List<object> Errors { get; set; }
        public string ModelVersion { get; set; }
    }
}
