namespace Soenneker.Quark;

public static class CssValueBuilderExtensions
{
    public static CssValue<TBuilder> WithSelector<TBuilder>(this TBuilder builder, string selector, bool absolute = false) where TBuilder : class, ICssBuilder
    {
        CssValue<TBuilder> value = builder;
        return value.WithSelector(selector, absolute);
    }
}


