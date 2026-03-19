using Soenneker.Quark.Attributes;

namespace Soenneker.Quark;

/// <summary>
/// Min-width builder with fluent API. Tailwind-first (min-w-*).
/// </summary>
[TailwindPrefix("min-w-", Responsive = true)]
public sealed class MinWidthBuilder : ICssBuilder
{
    private readonly string _token;

    internal MinWidthBuilder(string token)
    {
        _token = token;
    }

    public string ToClass()
    {
        var cls = GetMinWidthClass(_token);
        return cls.Length == 0 ? string.Empty : cls;
    }

    public string ToStyle() => string.Empty;

    private static string GetMinWidthClass(string token)
    {
        return token switch
        {
            "0" => "min-w-0",
            "px" => "min-w-px",
            "full" => "min-w-full",
            "min" => "min-w-min",
            "max" => "min-w-max",
            "fit" => "min-w-fit",
            _ when token.StartsWith("min-w-") => token,
            _ when token.Length > 0 => "min-w-" + token,
            _ => string.Empty
        };
    }
}
