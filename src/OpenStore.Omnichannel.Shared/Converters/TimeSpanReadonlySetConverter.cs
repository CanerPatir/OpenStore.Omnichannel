using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

// ReSharper disable CheckNamespace

namespace OpenStore.Omnichannel;

public class TimeSpanReadonlySetConverter : JsonConverter<ISet<TimeSpan>>
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        Converters = { new TimeSpanConverter() }
    };

    public override ISet<TimeSpan> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Null:
                return null;
            case JsonTokenType.StartArray:
                var list = new HashSet<TimeSpan>();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndArray)
                        break;
                    list.Add(TimeSpan.Parse(reader.GetString(), CultureInfo.InvariantCulture));
                }

                return list;
            default:
                return JsonSerializer.Deserialize<HashSet<TimeSpan>>(ref reader, JsonSerializerOptions);
        }
    }

    public override void Write(Utf8JsonWriter writer, ISet<TimeSpan> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        if (value != null)
        {
            foreach (var timeSpan in value)
            {
                writer.WriteStringValue(timeSpan.ToString(format: null, CultureInfo.InvariantCulture));
            }
        }

        writer.WriteEndArray();
    }
}