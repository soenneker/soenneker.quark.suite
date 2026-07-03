using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines visual variants for <see cref="StepperIndicator"/>.
/// </summary>
[EnumValue]
public sealed partial class StepperIndicatorVariant
{
    /// <summary>
    /// Filled active/completed indicator.
    /// </summary>
    public static readonly StepperIndicatorVariant Default = new(0);

    /// <summary>
    /// Outlined indicator with foreground active state.
    /// </summary>
    public static readonly StepperIndicatorVariant Outline = new(1);
}
