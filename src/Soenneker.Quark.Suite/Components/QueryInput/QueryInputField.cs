using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>A query qualifier and its optional suggested values. Keys must be unique and contain no whitespace, colon, quotes, or leading minus.</summary>
public sealed record QueryInputField(string Key, string Label, IReadOnlyList<SelectOption>? Values = null)
{
    /// <summary>Optional server-backed value lookup for this field. Overrides the component provider; receives cancellation when superseded.</summary>
    public Func<QueryInputRequest, CancellationToken, ValueTask<IReadOnlyList<SelectOption>>>? SuggestValues { get; init; }
}
