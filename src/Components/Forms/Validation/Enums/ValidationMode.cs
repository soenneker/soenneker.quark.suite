using Intellenum;

namespace Soenneker.Quark;

/// <summary>
/// An enumeration for Quark, representing validation mode.
/// </summary>
[Intellenum<string>]
public sealed partial class ValidationMode
{
    /// <summary>
    /// Automatic validation mode - validates on value change.
    /// </summary>
    public static readonly ValidationMode Auto = new("auto");

    /// <summary>
    /// Manual validation mode - validates only When explicitly triggered.
    /// </summary>
    public static readonly ValidationMode Manual = new("manual");
}
