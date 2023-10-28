using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace vueblogapi.Model
{
    public class Blog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string title { get; set; }
        public string date { get; set; }
        public string author { get; set; }

        [BsonRepresentation(BsonType.Array)]
        public List<string>? likes { get; set; }
    }
}
