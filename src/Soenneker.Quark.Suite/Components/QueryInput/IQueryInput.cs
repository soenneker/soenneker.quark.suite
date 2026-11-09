using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>A keyboard-accessible query text editor with field and value completion.</summary>
public interface IQueryInput : IElement
{
    /// <summary>Raw query text, including incomplete edits.</summary>
    string Value { get; set; }
    /// <summary>Receives each text change.</summary>
    EventCallback<string> ValueChanged { get; set; }
    /// <summary>Receives parsed text, filters, and validation errors after each edit.</summary>
    EventCallback<QueryInputResult> ParsedChanged { get; set; }
    /// <summary>Available qualifiers. Suggested values do not restrict typed values.</summary>
    IReadOnlyList<QueryInputField> Fields { get; set; }
    /// <summary>Optional asynchronous value provider; superseded requests are cancelled and their results ignored.</summary>
    Func<QueryInputRequest, CancellationToken, ValueTask<IReadOnlyList<SelectOption>>>? SuggestValues { get; set; }
    /// <summary>Placeholder displayed when empty.</summary>
    string? Placeholder { get; set; }
    /// <summary>Accessible name of the input.</summary>
    string Label { get; set; }
    /// <summary>Prevents editing and closes suggestions.</summary>
    bool Disabled { get; set; }
    /// <summary>Maximum visible suggestions; must be positive.</summary>
    int MaxSuggestions { get; set; }
}
