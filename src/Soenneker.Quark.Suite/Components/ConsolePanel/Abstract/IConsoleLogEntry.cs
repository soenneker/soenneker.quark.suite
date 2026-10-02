namespace Soenneker.Quark;

/// <summary>A text log entry that optionally presents .NET exception traces as expandable details.</summary>
public interface IConsoleLogEntry : IElement
{
    /// <summary>The complete, unescaped log entry text.</summary>
    string Text { get; set; }

    /// <summary>Overrides the containing ConsolePanel's exception formatting setting. Null inherits the panel setting.</summary>
    bool? FormatExceptions { get; set; }

    /// <summary>Overrides the containing ConsolePanel's JSON formatting setting. Null inherits the panel setting. Surrounding text and invalid JSON are preserved.</summary>
    bool? FormatJson { get; set; }

    /// <summary>Overrides the panel's default wrapping behavior for expanded content. Null inherits the panel setting, or true outside a panel.</summary>
    bool? WrapLines { get; set; }

    /// <summary>Overrides whether the wrapping control is shown. Null inherits the panel setting, or true outside a panel. False fixes wrapping to WrapLines.</summary>
    bool? ShowWrapToggle { get; set; }
}
