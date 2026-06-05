using Soenneker.Bradix;

namespace Soenneker.Quark;

/// <summary>
/// Represents the content container of a sheet.
/// </summary>
public interface ISheetContent : IElement
{
    /// <summary>
    /// Gets or sets side.
    /// </summary>
    Side Side { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether show close button.
    /// </summary>
    bool ShowCloseButton { get; set; }
}
