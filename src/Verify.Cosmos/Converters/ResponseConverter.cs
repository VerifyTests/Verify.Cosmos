using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;

class ResponseConverter :
    WriteOnlyJsonConverter
{
    public override void Write(VerifyJsonWriter writer, object response, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        var o = (dynamic)response;
        writer.WritePropertyName("RequestCharge");
        serializer.Serialize(writer, o.RequestCharge);
        writer.WritePropertyName("Headers");
        serializer.Serialize(writer, o.Headers);
        writer.WritePropertyName("StatusCode");
        serializer.Serialize(writer, o.StatusCode);
        writer.WritePropertyName("Resource");
        serializer.Serialize(writer, o.Resource);
        writer.WriteEndObject();
    }

    public override bool CanConvert(Type type)
    {
        do
        {
            if (!type.IsGenericType)
            {
                return false;
            }

            var definition = type.GetGenericTypeDefinition();
            if (definition == typeof(Response<>))
            {
                return true;
            }

            type = type.BaseType;
        } while (type != null);

        return false;
    }
}