namespace Soenneker.Quark;

/// <summary>
/// Represents a separator/divider (shadcn Separator). Supports horizontal and vertical orientation.
/// </summary>
public interface IHr : IComponent
{
    /// <summary>Orientation: horizontal or vertical.</summary>
    SeparatorOrientation Orientation { get; set; }
}