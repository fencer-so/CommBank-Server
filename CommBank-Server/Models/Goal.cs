using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CommBank.Models;

public class Goal
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? Name { get; set; }

    public UInt64 TargetAmount { get; set; } = 0;

    //[BsonDateTimeOptions(Kind = DateTimeKind.Local,Representation = BsonType.DateTime)]

    //[BsonRepresentation(BsonType.Document)]
    //public BsonDocument TargetDate { get; set; }

    //[BsonDateTimeOptions(Kind = DateTimeKind.Unspecified, Representation = BsonType.String)]
    // [BsonSerializer(typeof(CustomDateTimeSerializer))]
    public DateTime TargetDate { get; set; }

    public double Balance { get; set; } = 0.00;

    public DateTime Created { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public List<string>? TransactionIds { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public List<string>? TagIds { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string? UserId { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    public string? Icon { get; set; }
}

public class TimeDto
{
    [BsonElement("$date")]
    public dateDto date { get; set; }
}

public class dateDto
{
    [BsonElement("$numberLong")]
    public long Number { get; set; }
}

//public class CustomDateTimeSerializer : IBsonSerializer
//{
//    public Type ValueType => typeof(DateTime);

//    public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
//    {
//        var str = context.Reader.ReadRawBsonDocument();
//        //return DateTime.ParseExact(str, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

//        return DateTime.Now;
//    }

//    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
//    {
//        throw new NotImplementedException();
//    }
//}
//public sealed class DateOnlySerializerAsTicks : StructSerializerBase<DateOnly>
//{
//    private readonly Int64Serializer _serializer = new Int64Serializer();

//    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateOnly value)
//    {
//        return innerserializer
//    }
//}