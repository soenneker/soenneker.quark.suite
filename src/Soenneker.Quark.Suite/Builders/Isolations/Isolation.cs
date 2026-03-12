namespace Soenneker.Quark;

/// <summary>
/// Static utility for isolation. Tailwind: isolation-auto, isolation-isolate.
/// </summary>
public static class Isolation
{
    public static IsolationBuilder Auto => new("auto");
    public static IsolationBuilder Isolate => new("isolate");
}
