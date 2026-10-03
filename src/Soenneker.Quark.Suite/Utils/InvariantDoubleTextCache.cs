using System;
using System.Globalization;

namespace Soenneker.Quark;

internal struct InvariantDoubleTextCache
{
    private long _bits;
    private string? _format;
    private string? _text;

    internal string Get(double value, string format)
    {
        var bits = BitConverter.DoubleToInt64Bits(value);
        if (_text is null || _bits != bits || _format != format)
        {
            var text = value.ToString(format, CultureInfo.InvariantCulture);
            _bits = bits;
            _format = format;
            _text = text;
        }
        return _text;
    }
}
