namespace Soenneker.Quark;

/// <summary>
/// Represents a console surface with copy and download actions.
/// </summary>
public interface IConsolePanel : IElement
{
    /// <summary>Enables expandable exception formatting for child ConsoleLogEntry components. Defaults to false. Copy and download text is unaffected.</summary>
    bool FormatExceptions { get; set; }
}
