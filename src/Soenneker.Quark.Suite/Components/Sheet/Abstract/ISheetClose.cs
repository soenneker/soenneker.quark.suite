namespace Soenneker.Quark;

/// <summary>
/// Represents a close button for a sheet.
/// </summary>
public interface ISheetClose : IElement
{
    /// <summary>
    /// Gets or sets a value indicating whether auto close.
    /// </summary>
    bool AutoClose { get; set; }
}
