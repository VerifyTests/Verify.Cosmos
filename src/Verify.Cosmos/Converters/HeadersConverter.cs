using System.Collections.Generic;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using VerifyTests;

class HeadersConverter :
    WriteOnlyJsonConverter<Headers>
{
    public override void WriteJson(JsonWriter writer, Headers? headers, JsonSerializer serializer, IReadOnlyDictionary<string, object> context)
    {
        if (headers == null)
        {
            return;
        }

        writer.WriteStartObject();
        foreach (var key in headers.AllKeys())
        {
            if (key.StartsWith("x-ms-") || key == "etag" || key == "lsn")
            {
                continue;
            }

            writer.WritePropertyName(key);
            serializer.Serialize(writer, headers.GetValueOrDefault(key));
        }

        writer.WriteEndObject();
    }
}