namespace Soenneker.Quark;

/// <summary>A field/value search operand, including any exclusion.</summary>
public sealed record QueryInputFilterExpression(QueryInputFilter Filter) : QueryInputExpression;
