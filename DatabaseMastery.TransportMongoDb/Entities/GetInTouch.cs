using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DatabaseMastery.TransportMongoDb.Entities
{
    public class GetInTouch
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string GetInTouchId { get; set; }

        public string BadgeTitle { get; set; }
        public string MainTitle { get; set; }
        public string Description { get; set; }
        public string Feature1Title { get; set; }
        public string Feature1Description { get; set; }
        public string Feature2Title { get; set; }
        public string Feature2Description { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
    }
}
