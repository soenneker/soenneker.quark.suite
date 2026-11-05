using System;
using System.Linq.Expressions;

namespace Soenneker.Quark;

/// <summary>Identifies a control's bound value in a Blazor form.</summary>
public interface IFormValueExpression<TValue>
{
    /// <summary>Gets or sets the model field expression, supplied automatically by @bind-Value.</summary>
    Expression<Func<TValue>>? ValueExpression { get; set; }
}
