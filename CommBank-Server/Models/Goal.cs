using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace CommBank.Models
{
    public class Goal
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string? Name { get; set; }

        [BsonElement("targetAmount")]
        public double TargetAmount { get; set; }

        [BsonElement("targetDate")]
        public System.DateTime TargetDate { get; set; }

        [BsonElement("balance")]
        public double Balance { get; set; }

        [BsonElement("created")]
        public System.DateTime Created { get; set; }

        [BsonElement("transactionIds")]
        public List<string>? TransactionIds { get; set; }

        [BsonElement("tagIds")]
        public List<string>? TagIds { get; set; }

        // ✅ Optional Icon field added
        [BsonElement("icon")]
        public string? Icon { get; set; }

        [BsonElement("userId")]
        public string? UserId { get; set; }
    }
}
