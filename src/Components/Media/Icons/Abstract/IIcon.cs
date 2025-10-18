using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Interface for the Icon component
/// </summary>
public interface IIcon : IComponent
{
    string? Name { get; set; }
    IconStyle? IconStyle { get; set; }
    IconFamily? Family { get; set; }
    IconSize? IconSize { get; set; }
    CssValue<SizeBuilder>? Size { get; set; }
    bool SpinReverse { get; set; }
    IconRotate? Rotate { get; set; }
    int? RotateByDegrees { get; set; }
    IconFlip? Flip { get; set; }
    IconAnimation? IconAnimation { get; set; }
}

