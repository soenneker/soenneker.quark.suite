namespace Soenneker.Quark;

/// <summary>
/// Represents an unordered (bulleted) list element.
/// </summary>
public interface IUnorderedList : IElement
{
    /// <summary>Gets or sets the ListStyleImage utilities, including responsive and state variants.</summary>
    CssValue<ListStyleImageBuilder>? ListStyleImage { get; set; }

    /// <summary>Gets or sets the ListStylePosition utilities, including responsive and state variants.</summary>
    CssValue<ListStylePositionBuilder>? ListStylePosition { get; set; }

    /// <summary>Gets or sets the ListStyleType utilities, including responsive and state variants.</summary>
    CssValue<ListStyleTypeBuilder>? ListStyleType { get; set; }

}

