using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Supported semantic tone values.
/// </summary>
[EnumValue]
public sealed partial class SemanticTone
{
    /// <summary>Neutral presentation.</summary>
    public static readonly SemanticTone Neutral = new(0);
    /// <summary>Muted presentation.</summary>
    public static readonly SemanticTone Muted = new(1);
    /// <summary>Success presentation.</summary>
    public static readonly SemanticTone Success = new(2);
    /// <summary>Info presentation.</summary>
    public static readonly SemanticTone Info = new(3);
    /// <summary>Warning presentation.</summary>
    public static readonly SemanticTone Warning = new(4);
    /// <summary>Danger presentation.</summary>
    public static readonly SemanticTone Danger = new(5);
}
