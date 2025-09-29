using Soenneker.Quark;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Represents a single validation unit that tracks status, messages, and connects an input to a handler.
/// </summary>
public interface IValidation : IDisposable
{
    /// <summary>
    /// Current validation status for this unit.
    /// </summary>
    ValidationStatus Status { get; }

    /// <summary>
    /// Compiled regular expression used when pattern-based validation is enabled.
    /// </summary>
    Regex? Pattern { get; }

    /// <summary>
    /// The bound <see cref="FieldIdentifier"/> when using data-annotation validation.
    /// </summary>
    FieldIdentifier FieldIdentifier { get; }

    /// <summary>
    /// The current validation error messages, if any.
    /// </summary>
    IEnumerable<string>? Messages { get; }

    /// <summary>
    /// Initialize this validation with an input component that provides values and disabled state.
    /// </summary>
    Task InitializeInput(IValidationInput input);

    /// <summary>
    /// Initialize or update the pattern and seed value used for pattern-based validation.
    /// </summary>
    Task InitializeInputPattern<T>(string pattern, T value);

    /// <summary>
    /// Initialize or update the expression used to bind a model field for data-annotation validation.
    /// </summary>
    Task InitializeInputExpression<T>(System.Linq.Expressions.Expression<System.Func<T>> expression);

    /// <summary>
    /// Notify that the input value has changed so validation can run in Auto mode.
    /// </summary>
    Task NotifyInputChanged<T>(T newValue, bool overrideNewValue = false);

    /// <summary>
    /// Execute validation synchronously using the last known value.
    /// </summary>
    ValidationStatus Validate();

    /// <summary>
    /// Execute validation synchronously using the provided value.
    /// </summary>
    ValidationStatus Validate(object newValidationValue);

    /// <summary>
    /// Execute validation asynchronously using the last known value.
    /// </summary>
    Task<ValidationStatus> ValidateAsync();

    /// <summary>
    /// Execute validation asynchronously using the provided value.
    /// </summary>
    Task<ValidationStatus> ValidateAsync(object newValidationValue);

    /// <summary>
    /// Reset this validation to the None status and clear messages.
    /// </summary>
    void Clear();

    /// <summary>
    /// Notify subscribers that a validation run is starting.
    /// </summary>
    void NotifyValidationStarted();

    /// <summary>
    /// Update the validation status and messages, and notify subscribers.
    /// </summary>
    void NotifyValidationStatusChanged(ValidationStatus status, IEnumerable<string>? messages = null);

    /// <summary>
    /// Two-way bindable callback for status changes.
    /// </summary>
    EventCallback<ValidationStatus> StatusChanged { get; }
}
