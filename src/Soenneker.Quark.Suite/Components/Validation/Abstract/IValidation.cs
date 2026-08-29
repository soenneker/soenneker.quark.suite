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
    /// Compiled regular expression used When pattern-based validation is enabled.
    /// </summary>
    Regex? Pattern { get; }

    /// <summary>
    /// The bound <see cref="FieldIdentifier"/> When using data-annotation validation.
    /// </summary>
    FieldIdentifier FieldIdentifier { get; }

    /// <summary>
    /// The current validation error messages, if any.
    /// </summary>
    IEnumerable<string>? Messages { get; }

    /// <summary>
    /// Initialize this validation with an input component that provides values and disabled state.
    /// </summary>
    /// <param name="input">input to read or transform.</param>
    /// <returns>A task that completes when the Validation is ready for use.</returns>
    Task InitializeInput(IValidationInput input);

    /// <summary>
    /// Initialize or update the pattern and seed value used for pattern-based validation.
    /// </summary>
    /// <typeparam name="T">Type of value handled by the Validation.</typeparam>
    /// <param name="pattern">Pattern for the initialize input pattern operation.</param>
    /// <param name="value">CSS value used to construct the utility class.</param>
    /// <param name="enablePatternValidation">Whether enable pattern validation.</param>
    /// <returns>A task that completes when the Validation is ready for use.</returns>
    Task InitializeInputPattern<T>(string pattern, T value, bool enablePatternValidation = false);

    /// <summary>
    /// Clears the input-provided pattern used for pattern-based validation.
    /// </summary>
    void ClearInputPattern();

    /// <summary>
    /// Initialize or update the expression used to bind a model field for data-annotation validation.
    /// </summary>
    /// <typeparam name="T">Type of value handled by the Validation.</typeparam>
    /// <param name="expression">Expression for the initialize input expression operation.</param>
    /// <returns>A task that completes when the Validation is ready for use.</returns>
    Task InitializeInputExpression<T>(System.Linq.Expressions.Expression<Func<T>> expression);

    /// <summary>
    /// Notify that the input value has changed so validation can run in Auto mode.
    /// </summary>
    /// <typeparam name="T">Type of value handled by the Validation.</typeparam>
    /// <param name="newValue">New Value for the notify input changed operation.</param>
    /// <param name="overrideNewValue">Whether override new value.</param>
    /// <returns>A task that completes when the notify input changed operation is complete.</returns>
    Task NotifyInputChanged<T>(T newValue, bool overrideNewValue = false);

    /// <summary>
    /// Execute validation synchronously using the last known value.
    /// </summary>
    /// <returns>The resulting validation Status.</returns>
    ValidationStatus Validate();

    /// <summary>
    /// Validates the request Basic credentials against the configured username and password hash.
    /// </summary>
    /// <param name="newValidationValue">New Validation Value for the validate operation.</param>
    /// <returns>The resulting validation Status.</returns>
    ValidationStatus Validate(object newValidationValue);

    /// <summary>
    /// Execute validation asynchronously using the last known value.
    /// </summary>
    /// <returns>A task whose result is the requested validation Status.</returns>
    Task<ValidationStatus> ValidateCurrent();

    /// <summary>
    /// Execute validation asynchronously using the provided value.
    /// </summary>
    /// <param name="newValidationValue">New Validation Value for the validate value operation.</param>
    /// <returns>A task whose result is the requested validation Status.</returns>
    Task<ValidationStatus> ValidateValue(object newValidationValue);

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
    /// <param name="status">Status for the notify validation status changed operation.</param>
    /// <param name="messages">Messages to send or process.</param>
    void NotifyValidationStatusChanged(ValidationStatus status, IEnumerable<string>? messages = null);

    /// <summary>
    /// Two-way bindable callback for status changes.
    /// </summary>
    EventCallback<ValidationStatus> StatusChanged { get; }
}
