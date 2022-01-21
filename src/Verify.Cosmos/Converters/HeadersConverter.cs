using Microsoft.Azure.Cosmos;

class HeadersConverter :
    WriteOnlyJsonConverter<Headers>
{
    public override void Write(VerifyJsonWriter writer, Headers headers)
    {
        writer.WriteStartObject();
        foreach (var key in headers.AllKeys())
        {
            if (key.StartsWith("x-ms-") ||
                string.Equals(key, "etag", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(key, "date", StringComparison.OrdinalIgnoreCase) ||
                key == "lsn")
            {
                continue;
            }

            writer.WriteProperty(headers, headers.GetValueOrDefault(key), key);
        }

        writer.WriteEndObject();
    }
}