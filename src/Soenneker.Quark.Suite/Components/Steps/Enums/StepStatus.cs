using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// An enumeration for Quark, representing step status values.
/// </summary>
[EnumValue]
public sealed partial class StepStatus
{
    /// <summary>
    /// Step is pending and not yet started.
    /// </summary>
    public static readonly StepStatus Pending = new(0);

    /// <summary>
    /// Step is currently active.
    /// </summary>
    public static readonly StepStatus Active = new(1);

    /// <summary>
    /// Step has been completed.
    /// </summary>
    public static readonly StepStatus Completed = new(2);

    /// <summary>
    /// Step is disabled and cannot be accessed.
    /// </summary>
    public static readonly StepStatus Disabled = new(3);
}
