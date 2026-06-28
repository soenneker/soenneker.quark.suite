using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines autosave lifecycle states for form controls.
/// </summary>
[EnumValue<string>]
public sealed partial class AutoSaveState
{
    /// <summary>
    /// No autosave operation is pending or visible.
    /// </summary>
    public static readonly AutoSaveState Idle = new("idle");

    /// <summary>
    /// A value changed and is waiting for the configured delay.
    /// </summary>
    public static readonly AutoSaveState Pending = new("pending");

    /// <summary>
    /// The autosave callback is running.
    /// </summary>
    public static readonly AutoSaveState Saving = new("saving");

    /// <summary>
    /// The latest autosave completed successfully.
    /// </summary>
    public static readonly AutoSaveState Saved = new("saved");

    /// <summary>
    /// The latest autosave callback failed.
    /// </summary>
    public static readonly AutoSaveState Failed = new("failed");
}
