using Microsoft.Azure.Cosmos;

class FeedResponseConverter :
    WriteOnlyJsonConverter
{
    public override void Write(VerifyJsonWriter writer, object response)
    {
        writer.WriteStartObject();
        var o = (dynamic) response;
        writer.WriteMember(response, VerifyCosmos.RoundRequestCharge(o), "RequestCharge");
        writer.WriteMember(response, o.Count, "Count");
        writer.WriteMember(response, o.Headers, "Headers");
        writer.WriteMember(response, o.StatusCode, "StatusCode");
        writer.WriteMember(response, o.Resource, "Resource");
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