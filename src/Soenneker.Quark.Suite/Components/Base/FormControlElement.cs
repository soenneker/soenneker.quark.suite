using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Focused base for reusable form-control semantics (with string Value).
/// For components that use their own Value type, inherit from <see cref="FormControlElementBase"/> instead.
/// </summary>
public abstract class FormControlElement : FormControlElementBase, IFormValueExpression<string?>
{
    [Parameter]
    public Expression<Func<string?>>? ValueExpression { get; set; }

    protected override FieldIdentifier? ResolveEditField() => ValueExpression is not null ? FieldIdentifier.Create(ValueExpression) : base.ResolveEditField();
    /// <summary>
    /// Gets or sets value.
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    protected override void BuildOwnedAttributes(Dictionary<string, object> attrs)
    {
        base.BuildOwnedAttributes(attrs);

        if (Value is not null)
            attrs["value"] = Value;
    }

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        base.ComputeRenderKeyCore(ref hc);
        hc.Add(Value);
    }
}
