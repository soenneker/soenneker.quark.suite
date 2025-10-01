namespace Soenneker.Quark;

/// <summary>
/// Represents a preformatted code block component with syntax highlighting support.
/// </summary>
public interface IPreCode : IElement
{
    /// <summary>
    /// Gets or sets the programming language for syntax highlighting.
    /// </summary>
    string? Language { get; set; }

    /// <summary>
    /// Gets or sets whether syntax highlighting should be enabled.
    /// </summary>
    bool Highlight { get; set; }

    /// <summary>
    /// Gets or sets the color theme for syntax highlighting.
    /// </summary>
    string? Theme { get; set; }

    /// <summary>
    /// Gets or sets whether the code block should be scrollable when content overflows.
    /// </summary>
    bool Scrollable { get; set; }
}

