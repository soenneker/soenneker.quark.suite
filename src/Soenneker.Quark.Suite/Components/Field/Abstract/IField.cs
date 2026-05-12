using Soenneker.Bradix;

namespace Soenneker.Quark;

/// <summary>
/// Represents a form field container that groups related form controls, labels, and help text.
/// </summary>
public interface IField : IElement
{
    /// <summary>
    /// Gets or sets the field layout orientation.
    /// </summary>
    Orientation? Orientation { get; set; }

    /// <summary>
    /// Gets or sets whether the field should expose disabled state for composed label and title styling.
    /// </summary>
    bool Disabled { get; set; }
}

