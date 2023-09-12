using MongoDB.Bson;
using Newtonsoft.Json;

class ObjectIdConverter : JsonConverter
{

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value is not null)
        {
            serializer.Serialize(writer, value.ToString());
        }
    }

    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override bool CanConvert(Type objectType)
    {
        return typeof(ObjectId).IsAssignableFrom(objectType);
    }


}