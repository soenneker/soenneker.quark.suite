namespace Soenneker.Quark;

/// <summary>
/// Represents a code snippet component with syntax highlighting support.
/// </summary>
public interface ICode : IElement
{
    /// <summary>
    /// Gets or sets the programming language for syntax highlighting.
    /// </summary>
    string? Language { get; set; }

    /// <summary>
    /// Gets or sets whether the code should be displayed inline.
    /// </summary>
    bool Inline { get; set; }

    /// <summary>
    /// Gets or sets whether syntax highlighting should be enabled.
    /// </summary>
    bool Highlight { get; set; }

    /// <summary>
    /// Gets or sets the color theme for syntax highlighting.
    /// </summary>
    string? Theme { get; set; }
}

