namespace Soenneker.Quark;

/// <summary>A logical query operator. AND binds more tightly than OR.</summary>
public enum QueryInputOperator
{
    /// <summary>Both operands must match.</summary>
    And,
    /// <summary>Either operand must match.</summary>
    Or
}
