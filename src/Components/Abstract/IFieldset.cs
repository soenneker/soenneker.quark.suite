namespace Soenneker.Quark;

/// <summary>
/// Represents a fieldset element for grouping form controls.
/// </summary>
public interface IFieldset : IElement
{
    /// <summary>
    /// Gets or sets whether the fieldset is disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the form ID that this fieldset belongs to.
    /// </summary>
    string? Form { get; set; }
}

