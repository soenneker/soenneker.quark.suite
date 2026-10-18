using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Defines the searchable select component.
/// </summary>
public interface ISearchableSelect
{
    /// <summary>Optional remote provider. When supplied, replaces local Choices filtering. Continuation tokens must advance.</summary>
    Func<SelectSearchRequest, CancellationToken, ValueTask<SelectSearchResult>>? LoadOptions { get; set; }
    /// <summary>Resolves selected values absent from loaded options.</summary>
    Func<IReadOnlyList<string>, CancellationToken, ValueTask<IReadOnlyList<SelectOption>>>? Resolve { get; set; }
    /// <summary>Allows multiple selection using Values instead of Value.</summary>
    bool Multiple { get; set; }
    /// <summary>Selected logical values in multiple mode.</summary>
    IReadOnlyList<string> Values { get; set; }
    /// <summary>Raised when multiple selection changes.</summary>
    EventCallback<IReadOnlyList<string>> ValuesChanged { get; set; }
    /// <summary>Remote search debounce delay in milliseconds, clamped to 0–10000.</summary>
    int DebounceMilliseconds { get; set; }
    /// <summary>Requested remote page size, clamped to 1–1000.</summary>
    int PageSize { get; set; }

    /// <summary>Renders only a measured window of matching options. Keyboard navigation still covers all choices.</summary>
    bool Virtualize { get; set; }

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
