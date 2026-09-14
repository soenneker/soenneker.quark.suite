using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Soenneker.Lucide.Enums.Icons;

namespace Soenneker.Quark;

/// <summary>
/// Defines the searchable select component.
/// </summary>
public interface ISearchableSelect
{
    /// <summary>
    /// Available choices with unique logical values. Display labels may repeat and values may be empty.
    /// </summary>
    IReadOnlyList<SelectOption> Choices { get; set; }

    /// <summary>
    /// The selected logical value.
    /// </summary>
    string? Value { get; set; }

    /// <summary>
    /// Raised when the selected value changes.
    /// </summary>
    EventCallback<string?> ValueChanged { get; set; }

    /// <summary>
    /// Gets or sets placeholder.
    /// </summary>
    string Placeholder { get; set; }

    /// <summary>
    /// Gets or sets empty text.
    /// </summary>
    string EmptyText { get; set; }

    /// <summary>
    /// Gets or sets disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Additional attributes applied to the input, including its accessible name.
    /// </summary>
    IReadOnlyDictionary<string, object>? InputAttributes { get; set; }
}
