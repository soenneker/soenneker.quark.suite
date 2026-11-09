namespace Soenneker.Quark;

/// <summary>Requests suggestions for a field and decoded partial value at the current caret.</summary>
public sealed record QueryInputRequest(string Field, string Search, string Query);
