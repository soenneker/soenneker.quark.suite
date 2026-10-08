namespace Soenneker.Quark;

/// <summary>
/// Represents a multiline text input area (shadcn/ui).
/// </summary>
public interface ITextArea : IElement
{
    /// <summary>
    /// Gets or sets the ID of the owning form, including a form outside the control ancestry.
    /// </summary>
    string? Form { get; set; }

    /// <summary>
    /// Gets or sets browser autofill tokens, such as off, email, or section-shipping shipping street-address.
    /// </summary>
    string? AutoComplete { get; set; }

    /// <summary>
    /// Gets or sets the HTML cols attribute.
    /// </summary>
    int? Cols { get; set; }

    /// <summary>
    /// Gets or sets textarea wrapping: soft or hard.
    /// </summary>
    string? Wrap { get; set; }

    /// <summary>
    /// Gets or sets the HTML dirname attribute.
    /// </summary>
    string? DirName { get; set; }

    /// <summary>
    /// Gets or sets the HTML inputmode attribute.
    /// </summary>
    string? InputMode { get; set; }


    /// <summary>
    /// Gets or sets the textarea value.
    /// </summary>
    string? Value { get; set; }
}
