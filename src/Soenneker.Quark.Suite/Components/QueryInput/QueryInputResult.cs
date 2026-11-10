using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>Parsed query text. Applications must validate and translate filters before executing queries.</summary>
public sealed record QueryInputResult(string Text, IReadOnlyList<QueryInputFilter> Filters, IReadOnlyList<string> Errors)
{
    /// <summary>Logical query tree, with implicit AND between adjacent operands and AND before OR. Null for an empty query. Check Errors before executing.</summary>
    public QueryInputExpression? Expression { get; init; }
}
