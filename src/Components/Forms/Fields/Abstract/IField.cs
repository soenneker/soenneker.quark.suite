namespace Soenneker.Quark;

/// <summary>
/// Represents a form field container that groups related form controls, labels, and help text.
/// </summary>
public interface IField : IElement
{
    /// <summary>
    /// Gets or sets whether the form controls should be aligned horizontally.
    /// </summary>
    bool Horizontal { get; set; }
}

