using System;
using System.Globalization;

namespace Soenneker.Quark;

internal struct DecimalTextCache
{
    private decimal _value;
    private int _flags;
    private string? _text;

    internal string Get(decimal value)
    {
        Span<int> bits = stackalloc int[4];
        decimal.GetBits(value, bits);
        // Decimal equality ignores scale, but its default text format preserves it.
        if (_text is null || _value != value || _flags != bits[3])
        {
            _value = value;
            _flags = bits[3];
            _text = value.ToString(CultureInfo.InvariantCulture);
        }
        return _text;
    }
}
