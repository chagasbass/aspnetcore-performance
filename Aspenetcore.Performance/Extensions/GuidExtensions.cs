using Microsoft.AspNetCore.WebUtilities;
using System;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Aspnetcore.Performance.Extensions
{
    /// <summary>
    /// Extensão criada para diminuir o tamanho do tráfego de um Guid ao ser recebido em um endpoint.
    /// </summary>
    [TypeConverter(typeof(ShortGuidTypeConverter))]
    [JsonConverter(typeof(ShortGuidJsonConverter))]
    public readonly partial struct ShortGuid
    {
        private readonly Guid _value;

        public ShortGuid(Guid value) => _value = value;

        public static implicit operator Guid(ShortGuid shortGuid) => shortGuid._value;
        public static implicit operator ShortGuid(Guid guid) => new(guid);

        public static ShortGuid Parse(string input) => new Guid(WebEncoders.Base64UrlDecode(input));

        public override string ToString()
        {
            Span<byte> bytes = stackalloc byte[16];
            _value.TryWriteBytes(bytes);
            return WebEncoders.Base64UrlEncode(bytes);
        }
    }
}
