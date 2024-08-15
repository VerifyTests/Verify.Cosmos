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
        var current= type;
        do
        {
            if (!current.IsGenericType)
            {
                return false;
            }

            var definition = current.GetGenericTypeDefinition();
            if (definition == typeof(FeedResponse<>))
            {
                return true;
            }

            current = current.BaseType;
        } while (current != null);

        return false;
    }
}