namespace Soenneker.Quark;

/// <summary>
/// Represents a label element for form inputs.
/// </summary>
public interface ILabel : IElement
{
    /// <summary>
    /// Name of the input element to which the label is connected.
    /// </summary>
    string? For { get; set; }

    /// <summary>
    /// Gets or sets whether to apply the Bootstrap form-label class for form styling.
    /// </summary>
    bool FormLabel { get; set; }
}