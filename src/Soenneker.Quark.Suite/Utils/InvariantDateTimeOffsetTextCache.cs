using System;
using System.Globalization;

namespace Soenneker.Quark;

internal struct InvariantDateTimeOffsetTextCache
{
    private DateTimeOffset _value;
    private string? _text;

    internal string Get(DateTimeOffset value)
    {
        if (_text is null || !_value.EqualsExact(value))
        {
            _text = value.ToString("O", CultureInfo.InvariantCulture);
            _value = value;
        }
        return _text;
    }
}
