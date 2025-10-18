namespace Soenneker.Quark;

/// <summary>
/// Represents a label for a form field with optional required indicator.
/// </summary>
public interface IFieldLabel : IElement
{
    /// <summary>
    /// Gets or sets the ID of the form element that this label is associated with.
    /// </summary>
    string? For { get; set; }

    /// <summary>
    /// Gets or sets whether a required indicator (*) should be displayed next to the label.
    /// </summary>
    bool RequiredIndicator { get; set; }
}

