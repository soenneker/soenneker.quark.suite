using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Provides built-in color sequences for <see cref="Spinner"/>.
/// </summary>
public static class SpinnerColorPalettes
{
    /// <summary>
    /// Gets the blue, red, yellow, and green sequence used by the Google-style spinner.
    /// </summary>
    public static IReadOnlyList<string> Google { get; } = ["#4285f4", "#db4437", "#f4b400", "#0f9d58"];
}
