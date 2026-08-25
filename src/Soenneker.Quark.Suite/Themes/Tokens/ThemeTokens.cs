using System.Collections.Generic;

namespace Soenneker.Quark.Tokens;

/// <summary>
/// Strongly typed semantic and Tailwind theme tokens that can be authored in C# and emitted to CSS at build time.
/// </summary>
public sealed class ThemeTokens
{
    /// <summary>
    /// Gets or sets the light theme semantic token values written to <c>:root</c>.
    /// </summary>
    public ThemeTokenScheme Light { get; set; } = ThemeTokenScheme.CreateDefaultLight();

    /// <summary>
    /// Gets or sets the dark theme semantic token values written to <c>.dark</c>.
    /// </summary>
    public ThemeTokenScheme Dark { get; set; } = ThemeTokenScheme.CreateDefaultDark();

    /// <summary>
    /// Gets or sets additional custom variables written inside the generated <c>@theme inline</c> block.
    /// Keys should omit the <c>--</c> prefix.
    /// </summary>
    public Dictionary<string, string> InlineVariables { get; set; } = new();
}
