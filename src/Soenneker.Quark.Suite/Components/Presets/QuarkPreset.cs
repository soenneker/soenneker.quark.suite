namespace Soenneker.Quark;

/// <summary>
/// Represents the quark preset.
/// </summary>
public abstract class QuarkPreset
{
    /// <summary>
    /// Executes the apply operation.
    /// </summary>
    /// <param name="context">The context.</param>
    public abstract void Apply(QuarkPresetContext context);
}
