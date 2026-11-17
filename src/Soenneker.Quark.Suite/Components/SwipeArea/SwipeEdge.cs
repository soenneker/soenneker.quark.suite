using Soenneker.Gen.EnumValues;
namespace Soenneker.Quark;

/// <summary>The component edge where a swipe must begin.</summary>
[EnumValue<string>]
public sealed partial class SwipeEdge
{
    /// <summary>Allow starts anywhere in the component.</summary>
    public static readonly SwipeEdge None = new("none");
    /// <summary>Require a start near the left edge.</summary>
    public static readonly SwipeEdge Left = new("left");
    /// <summary>Require a start near the right edge.</summary>
    public static readonly SwipeEdge Right = new("right");
    /// <summary>Require a start near the top edge.</summary>
    public static readonly SwipeEdge Top = new("top");
    /// <summary>Require a start near the bottom edge.</summary>
    public static readonly SwipeEdge Bottom = new("bottom");
}
