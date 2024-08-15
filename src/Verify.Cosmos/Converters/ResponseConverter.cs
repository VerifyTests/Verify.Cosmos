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
        var current= type;
        do
        {
            if (!current.IsGenericType)
            {
                return false;
            }

            var definition = current.GetGenericTypeDefinition();
            if (definition == typeof(Response<>))
            {
                return true;
            }

            current = current.BaseType;
        } while (current != null);

        return false;
    }
}