#if NET6_0_OR_GREATER
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UzairAli.HttpClientServices.Converters;

public sealed class JsonStringTimeOnlyConverter : JsonConverter<TimeOnly>
{
    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return TimeOnly.Parse(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        var isoTime = value.ToString("O");
        writer.WriteStringValue(isoTime);
    }
}
#endif