using System;
using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Provides data for the validation events.
/// </summary>
public class ValidatorEventArgs : EventArgs
{
    /// <summary>
    /// A default <see cref="ValidatorEventArgs"/> constructor.
    /// </summary>
    public ValidatorEventArgs(object value)
    {
        Value = value;
        Status = ValidationStatus.None;
    }

    /// <summary>
    /// Gets the value to check for validation.
    /// </summary>
    public object Value { get; }

    /// <summary>
    /// Gets or sets the validation result.
    /// </summary>
    public ValidationStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the validation custom error message.
    /// </summary>
    public string? ErrorText { get; set; }

    /// <summary>
    /// Gets the collection of member names that indicate which fields have validation errors.
    /// </summary>
    public IEnumerable<string>? MemberNames { get; set; }
}
