namespace Soenneker.Quark;

/// <summary>A theme declaration authored as literal CSS or a snapshot of typed builder utilities.</summary>
/// <typeparam name="TBuilder">The builder accepted by this theme property.</typeparam>
public readonly struct ThemeValue<TBuilder> where TBuilder : class, ICssBuilder
{
    internal string Value { get; }
    internal bool IsUtility { get; }
    private readonly string? _selector;
    private readonly bool _absolute;

    private ThemeValue(string value, bool isUtility, string? selector = null, bool absolute = false)
    {
        Value = value;
        IsUtility = isUtility;
        _selector = selector;
        _absolute = absolute;
    }

    /// <summary>Snapshots a builder without resolving its CSS declarations.</summary>
    public static implicit operator ThemeValue<TBuilder>(TBuilder builder) => new(builder.ToClass(), true);

    /// <summary>Snapshots a typed component CSS value for use in theme generation.</summary>
    public static implicit operator ThemeValue<TBuilder>(CssValue<TBuilder> value) => new(value.ToString(), true);

    /// <summary>Accepts literal CSS without utility interpretation.</summary>
    public static implicit operator ThemeValue<TBuilder>(string value) => new(value, false);

    /// <summary>Scopes this declaration to a selector relative to the component, or an absolute selector.</summary>
    public ThemeValue<TBuilder> WithSelector(string selector, bool absolute = false) => new(Value, IsUtility, selector, absolute);

    internal string ResolveSelector(string selector) => string.IsNullOrWhiteSpace(_selector)
        ? selector
        : _absolute ? _selector.Trim() : ThemeSelector.Resolve(selector, _selector);
}
