using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aspnetcore.Performance.Extensions
{
    public readonly partial struct ShortGuid
    {
        private sealed class ShortGuidJsonConverter : JsonConverter<ShortGuid>
        {
            public override ShortGuid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var str = reader.GetString();
                if (str != null)
                    return Parse(str);

                return default;
            }

            public override void Write(Utf8JsonWriter writer, ShortGuid value, JsonSerializerOptions options)
                => writer.WriteStringValue(value.ToString());
        }
    }
}
