namespace Soenneker.Quark;

/// <summary>
/// Represents the quark preset.
/// </summary>
public abstract class QuarkPreset
{
    /// <summary>
    /// Applies quark Preset for the Quark Preset.
    /// </summary>
    /// <param name="context">HTTP context containing the Authorization header.</param>
    public abstract void Apply(QuarkPresetContext context);
}
