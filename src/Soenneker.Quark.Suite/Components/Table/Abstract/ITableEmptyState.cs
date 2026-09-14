using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Soenneker.Lucide.Enums.Icons;

namespace Soenneker.Quark;

/// <summary>
/// Defines the table empty state component.
/// </summary>
public interface ITableEmptyState
{
    /// <summary>
    /// Gets or sets col span.
    /// </summary>
    int ColSpan { get; set; }

    /// <summary>
    /// Gets or sets title.
    /// </summary>
    string Title { get; set; }

    /// <summary>
    /// Gets or sets description.
    /// </summary>
    string? Description { get; set; }

    /// <summary>
    /// Gets or sets kind.
    /// </summary>
    EmptyStateKind Kind { get; set; }

    /// <summary>
    /// Custom action content.
    /// </summary>
    RenderFragment? Actions { get; set; }

    /// <summary>
    /// Gets or sets media.
    /// </summary>
    RenderFragment? Media { get; set; }

    /// <summary>
    /// Content rendered inside the component.
    /// </summary>
    RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes applied to the root element.
    /// </summary>
    IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }
}
