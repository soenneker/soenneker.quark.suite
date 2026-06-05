namespace Soenneker.Quark;

/// <summary>
/// Represents the body section of a form field that contains the input control.
/// </summary>
public interface IFieldBody : IComponent
{
    /// <summary>
    /// Gets or sets columns.
    /// </summary>
    CssValue<GridColsBuilder>? Columns { get; set; }
}
