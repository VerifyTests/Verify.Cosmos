﻿using System;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using VerifyTests;

class ResponseConverter :
    WriteOnlyJsonConverter
{
    public override void WriteJson(JsonWriter writer, object? response, JsonSerializer serializer, IReadOnlyDictionary<string, object> context)
    {
        if (response == null)
        {
            return;
        }

        writer.WriteStartObject();
        var o = (dynamic)response!;
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