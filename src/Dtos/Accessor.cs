using System;

namespace Soenneker.Quark.Dtos;

internal sealed class Accessor
{
    // options -> CssValue<T> (boxed or null)
    public required Func<object, object?> GetOptionValue;

    // CssValue<T> -> StyleValue (boxed or null)
    public required Func<object, object?> GetStyleValue;

    // Precomputed: "  " + cssName + ": "
    public required string CssPrefix;
}