using Soenneker.Bradix;

namespace Soenneker.Quark;

/// <summary>
/// Represents the content container of a sheet.
/// </summary>
public interface ISheetContent : IElement
{
    Side Side { get; set; }
    bool ShowCloseButton { get; set; }
}
