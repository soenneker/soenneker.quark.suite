using Soenneker.Quark.Attributes;

namespace Soenneker.Quark;

/// <summary>
/// Max-width builder with fluent API. Tailwind-first (max-w-*).
/// </summary>
[TailwindPrefix("max-w-", Responsive = true)]
public sealed class MaxWidthBuilder : ICssBuilder
{
    private readonly string _token;

    internal MaxWidthBuilder(string token)
    {
        _token = token;
    }

    public string ToClass()
    {
        var cls = GetMaxWidthClass(_token);
        return cls.Length == 0 ? string.Empty : cls;
    }

    public string ToStyle() => string.Empty;

    private static string GetMaxWidthClass(string token)
    {
        return token switch
        {
            "none" => "max-w-none",
            "xs" => "max-w-xs",
            "sm" => "max-w-sm",
            "md" => "max-w-md",
            "lg" => "max-w-lg",
            "xl" => "max-w-xl",
            "2xl" => "max-w-2xl",
            "3xl" => "max-w-3xl",
            "4xl" => "max-w-4xl",
            "5xl" => "max-w-5xl",
            "6xl" => "max-w-6xl",
            "7xl" => "max-w-7xl",
            "full" => "max-w-full",
            "min" => "max-w-min",
            "max" => "max-w-max",
            "fit" => "max-w-fit",
            "screen" => "max-w-screen",
            "prose" => "max-w-prose",
            _ when token.StartsWith("max-w-") => token,
            _ when token.Length > 0 => "max-w-" + token,
            _ => string.Empty
        };
    }
}
