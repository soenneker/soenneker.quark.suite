namespace Soenneker.Quark;

/// <summary>
/// Represents a multiline text input area (shadcn/ui).
/// </summary>
public interface ITextArea : IElement
{
    /// <summary>
    /// Gets or sets the textarea value.
    /// </summary>
    string? Value { get; set; }
}
