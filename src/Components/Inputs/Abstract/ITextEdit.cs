using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents a single-line text input component.
/// </summary>
public interface ITextEdit : IInput
{
    /// <summary>
    /// Gets or sets the text value.
    /// </summary>
    string? Value { get; set; }

    /// <summary>
    /// Gets or sets the expression that identifies the bound value for validation.
    /// </summary>
    Expression<Func<string>>? ValueExpression { get; set; }
    
    /// <summary>
    /// Gets or sets the maximum number of characters allowed.
    /// </summary>
    int MaxLength { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the value changes.
    /// </summary>
    EventCallback<string?> ValueChanged { get; set; }

    /// <summary>
    /// Gets or sets the input mode hint for mobile keyboards.
    /// </summary>
    TextInputMode? InputMode { get; set; }
}