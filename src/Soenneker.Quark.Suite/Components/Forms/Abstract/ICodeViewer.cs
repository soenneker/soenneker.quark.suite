using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Soenneker.Lucide.Enums.Icons;

namespace Soenneker.Quark;

/// <summary>
/// Defines the code viewer component.
/// </summary>
public interface ICodeViewer
{
    /// <summary>Whether valid JSON is indented for display. Invalid JSON is preserved unchanged. Defaults to true.</summary>
    bool FormatJson { get; set; }

    /// <summary>
    /// Gets or sets text.
    /// </summary>
    string? Text { get; set; }

    /// <summary>
    /// Gets or sets language.
    /// </summary>
    string Language { get; set; }

    /// <summary>
    /// Gets or sets aria label.
    /// </summary>
    string AriaLabel { get; set; }

    /// <summary>
    /// Gets or sets min lines.
    /// </summary>
    int MinLines { get; set; }

    /// <summary>
    /// Gets or sets max lines.
    /// </summary>
    int MaxLines { get; set; }
}
