using Microsoft.Azure.Cosmos;

class ResponseConverter :
    WriteOnlyJsonConverter
{
    public override void Write(VerifyJsonWriter writer, object response)
    {
        writer.WriteStartObject();
        var o = (dynamic) response;
        writer.WriteMember(o, VerifyCosmos.RoundRequestCharge(o), "RequestCharge");
        writer.WriteMember(o, o.Headers, "Headers");
        writer.WriteMember(o, o.StatusCode, "StatusCode");
        writer.WriteMember(o, o.Resource, "Resource");
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