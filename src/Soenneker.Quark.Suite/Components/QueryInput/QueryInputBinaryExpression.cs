namespace Soenneker.Quark;

/// <summary>Combines two query expressions using a logical operator.</summary>
public sealed record QueryInputBinaryExpression(QueryInputExpression Left, QueryInputOperator Operator, QueryInputExpression Right) : QueryInputExpression;
