namespace Soenneker.Quark;

/// <summary>Theme-only selector configuration that does not alter component builder rendering.</summary>
public static class ThemeBuilderExtensions
{
    /// <summary>Snapshots the builder and scopes its theme declarations to a relative or absolute selector.</summary>
    public static ThemeValue<TBuilder> WithSelector<TBuilder>(this TBuilder builder, string selector, bool absolute = false)
        where TBuilder : class, ICssBuilder => ((ThemeValue<TBuilder>)builder).WithSelector(selector, absolute);

    /// <summary>Scopes a typed CSS value to a relative or absolute theme selector.</summary>
    public static ThemeValue<TBuilder> WithSelector<TBuilder>(this CssValue<TBuilder> value, string selector, bool absolute = false)
        where TBuilder : class, ICssBuilder => ((ThemeValue<TBuilder>)value).WithSelector(selector, absolute);
}
