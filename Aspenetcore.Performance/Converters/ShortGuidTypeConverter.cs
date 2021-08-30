using System;
using System.ComponentModel;

namespace Aspnetcore.Performance.Extensions
{
    public readonly partial struct ShortGuid
    {
        private sealed class ShortGuidTypeConverter : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
                => sourceType == typeof(string);

            public override object? ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
                => value is string str ? Parse(str) : null;

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
                => destinationType == typeof(string);

            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
                => ((ShortGuid)value).ToString();
        }
    }
}
