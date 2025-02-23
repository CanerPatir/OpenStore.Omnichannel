using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
// ReSharper disable CheckNamespace

namespace OpenStore.Omnichannel;

public class CultureConverter : JsonConverter<CultureInfo>
{
    public override CultureInfo Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => new(reader.GetString());

    public override void Write(Utf8JsonWriter writer, CultureInfo value, JsonSerializerOptions options) => writer.WriteStringValue(value.Name);
}