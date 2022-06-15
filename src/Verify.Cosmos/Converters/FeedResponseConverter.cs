using Microsoft.Azure.Cosmos;

class FeedResponseConverter :
    WriteOnlyJsonConverter
{
    public override void Write(VerifyJsonWriter writer, object response)
    {
        writer.WriteStartObject();
        var o = (dynamic) response;
        writer.WriteProperty(response, VerifyCosmos.RoundRequestCharge(o), "RequestCharge");
        writer.WriteProperty(response, o.Count, "Count");
        writer.WriteProperty(response, o.Headers, "Headers");
        writer.WriteProperty(response, o.StatusCode, "StatusCode");
        writer.WriteProperty(response, o.Resource, "Resource");
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
            if (definition == typeof(FeedResponse<>))
            {
                return true;
            }

            type = type.BaseType;
        } while (type != null);

        return false;
    }
}