namespace Soenneker.Quark;

/// <summary>A decoded free-text search operand.</summary>
public sealed record QueryInputTextExpression(string Text) : QueryInputExpression;
