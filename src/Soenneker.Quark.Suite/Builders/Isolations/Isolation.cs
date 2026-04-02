namespace Soenneker.Quark;

/// <summary>
/// Static utility for isolation. Tailwind: isolation-auto, isolation-isolate.
/// </summary>
public static class Isolation
{
    /// <summary>
    /// `auto` — browser-default sizing/behavior for the underlying utility.
    /// </summary>
    public static IsolationBuilder Auto => new("auto");
    /// <summary>
    /// Fluent step for `Isolate` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static IsolationBuilder Isolate => new("isolate");
}
