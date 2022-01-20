using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;

class HeadersConverter :
    WriteOnlyJsonConverter<Headers>
{
    public override void Write(VerifyJsonWriter writer, Headers headers, JsonSerializer serializer)
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

            writer.WritePropertyName(key);
            serializer.Serialize(writer, headers.GetValueOrDefault(key));
        }

        writer.WriteEndObject();
    }
}