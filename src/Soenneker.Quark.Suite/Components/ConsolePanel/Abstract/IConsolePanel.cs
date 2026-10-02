namespace Soenneker.Quark;

/// <summary>
/// Represents a console surface with copy and download actions.
/// </summary>
public interface IConsolePanel : IElement
{
    /// <summary>Enables expandable exception formatting for child ConsoleLogEntry components. Defaults to false. Copy and download text is unaffected.</summary>
    bool FormatExceptions { get; set; }

    /// <summary>Presents detected JSON objects and arrays as collapsed, expandable details, including JSON encoded as a quoted string, in child ConsoleLogEntry components. Defaults to false. Copy and download text is unaffected.</summary>
    bool FormatJson { get; set; }

    /// <summary>Wraps expanded exception and JSON lines by default. Defaults to true. When ShowWrapToggle is false, this setting fixes the wrapping behavior.</summary>
    bool WrapLines { get; set; }

    /// <summary>Shows a compact wrapping control at the top right of expanded exception and JSON details. Defaults to true.</summary>
    bool ShowWrapToggle { get; set; }
}
