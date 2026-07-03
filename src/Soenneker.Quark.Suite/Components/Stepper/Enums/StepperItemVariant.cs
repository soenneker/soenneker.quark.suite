using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines layout variants for <see cref="StepperItem"/>.
/// </summary>
[EnumValue]
public sealed partial class StepperItemVariant
{
    /// <summary>
    /// Inline indicator and content with flex separators between items.
    /// </summary>
    public static readonly StepperItemVariant Default = new(0);

    /// <summary>
    /// Stacks the trigger content and positions horizontal separators between indicator centers.
    /// </summary>
    public static readonly StepperItemVariant Stacked = new(1);
}
