using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CommBank.Models;

[BsonIgnoreExtraElements] // 忽略 Atlas 中多餘或未定義欄位，避免反序列化出錯
public class Goal
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("name")]
    public string? Name { get; set; }

    [BsonElement("icon")]
    public string? Icon { get; set; }

    [BsonElement("targetAmount")]
    public double TargetAmount { get; set; } = 0;

    [BsonElement("targetDate")]
    public DateTime TargetDate { get; set; }

    [BsonElement("balance")]
    public double Balance { get; set; } = 0.00;

    [BsonElement("created")]
    public DateTime Created { get; set; } = DateTime.Now;

    [BsonElement("transactionIds")]
    [BsonRepresentation(BsonType.ObjectId)]
    public List<string>? TransactionIds { get; set; }

    [BsonElement("tagIds")]
    [BsonRepresentation(BsonType.ObjectId)]
    public List<string>? TagIds { get; set; }

    [BsonElement("userId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? UserId { get; set; }
}