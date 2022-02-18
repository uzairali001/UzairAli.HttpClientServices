#if NET6_0_OR_GREATER
using System;
using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UzairAli.HttpClient.Converters;

public sealed class JsonStringTimeOnlyConverter : JsonConverter<TimeOnly>
{
    private readonly string _serializationFormat;

    public JsonStringTimeOnlyConverter(string? serializationFormat = null)
    {
        this._serializationFormat = serializationFormat ?? "HH:mm:ss";
    }

    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return TimeOnly.Parse(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_serializationFormat));
    }
}
#endif
