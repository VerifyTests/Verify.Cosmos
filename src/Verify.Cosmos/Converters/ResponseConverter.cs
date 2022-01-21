using Microsoft.Azure.Cosmos;

class ResponseConverter :
    WriteOnlyJsonConverter
{
    public override void Write(VerifyJsonWriter writer, object response)
    {
        writer.WriteStartObject();
        var o = (dynamic) response;
        writer.WriteProperty(o, o.RequestCharge, "RequestCharge");
        writer.WriteProperty(o, o.Headers, "Headers");
        writer.WriteProperty(o, o.StatusCode, "StatusCode");
        writer.WriteProperty(o, o.Resource, "Resource");
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