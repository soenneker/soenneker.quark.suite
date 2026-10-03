using System.Globalization;

namespace Soenneker.Quark;

internal struct CurrentCultureInt32TextCache
{
    private int _value;
    private string? _text;
    private CultureInfo? _culture;
    internal string Get(int value)
    {
        var culture = CultureInfo.CurrentCulture;
        if (_text is null || _value != value || !culture.IsReadOnly || !ReferenceEquals(culture, _culture))
        {
            _text = value.ToString(culture);
            _value = value;
            _culture = culture;
        }
        return _text;
    }
}
