namespace Soenneker.Quark;

/// <summary>A text log entry that optionally presents .NET exception traces as expandable details.</summary>
public interface IConsoleLogEntry : IElement
{
    /// <summary>The complete, unescaped log entry text.</summary>
    string Text { get; set; }

    /// <summary>Overrides the containing ConsolePanel's exception formatting setting. Null inherits the panel setting.</summary>
    bool? FormatExceptions { get; set; }
}
