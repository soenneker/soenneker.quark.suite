namespace Soenneker.Quark;

/// <summary>Requests suggestions for a field and decoded partial value at the current caret.</summary>
public sealed record QueryInputRequest(string Field, string Search, string Query)
{
    /// <summary>Parsed query context, including its logical expression and incomplete-edit errors.</summary>
    public QueryInputResult Parsed { get; init; } = QueryInputParser.Parse(Query);

    /// <summary>Maximum number of requested suggestions, suitable for limiting a server lookup.</summary>
    public int MaxSuggestions { get; init; } = 10;
}
