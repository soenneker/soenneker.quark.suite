namespace Soenneker.Quark;

/// <summary>
/// Represents a close button for a sheet.
/// </summary>
public interface ISheetClose : IElement
{
    bool AutoClose { get; set; }
}
