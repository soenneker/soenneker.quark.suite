namespace Soenneker.Quark;

/// <summary>
/// Represents a dynamic heading component that can render as h1 through h6 based on scale.
/// </summary>
public interface IHeading : IElement
{

    /// <summary>
    /// Gets or sets the semantic HTML heading level independently of visual scale.
    /// </summary>
    HeadingLevel? Level { get; set; }

}
