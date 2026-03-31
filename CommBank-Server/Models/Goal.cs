using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CommBank.Models
{
    public class Goal
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? Name { get; set; }

        public decimal TargetAmount { get; set; }

        public DateTime TargetDate { get; set; }

        public decimal Balance { get; set; }

        public DateTime Created { get; set; }

        public List<string>? TransactionIds { get; set; }

        public List<string>? TagIds { get; set; }

        public string? UserId { get; set; }

        public string? Icon { get; set; }
    }
}