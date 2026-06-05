namespace Soenneker.Quark;

/// <summary>
/// Defines the input group addon contract.
/// </summary>
public interface IInputGroupAddon : IElement
{
    /// <summary>
    /// Gets or sets addon align.
    /// </summary>
    InputGroupAddonAlign AddonAlign { get; set; }
}
