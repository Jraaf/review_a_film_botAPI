using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Attempt2.Models
{
    [BsonIgnoreExtraElements]
    public class Review
    {
        public string TgUsername { get; set; }
        public string MovieId { get; set; }
        public int Marking { get; set; }
        public string? Text { get; set; }
    }
}
