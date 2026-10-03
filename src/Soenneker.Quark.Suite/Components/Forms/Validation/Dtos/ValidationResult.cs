using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Represents the result of a validation operation.
/// </summary>
public sealed class ValidationResult
{
    private Task<ValidationResult>? _task;
    internal Task<ValidationResult> AsTask() => _task ??= Task.FromResult(this);
    private static readonly ValidationResult _success = new() { Status = ValidationStatus.Success };
    private static readonly ValidationResult _none = new() { Status = ValidationStatus.None };

    /// <summary>
    /// Gets the validation status.
    /// </summary>
    public ValidationStatus Status { get; init; } = ValidationStatus.None;

    /// <summary>
    /// Gets the error message if validation failed.
    /// </summary>
    public string? ErrorText { get; init; }

    /// <summary>
    /// Gets the collection of member names that indicate which fields have validation errors.
    /// </summary>
    public IEnumerable<string>? MemberNames { get; init; }

    /// <summary>
    /// Gets whether the validation was successful.
    /// </summary>
    public bool IsValid => Status != ValidationStatus.Error;

    /// <summary>
    /// Creates a successful validation result.
    /// </summary>
    /// <returns>The same builder instance, so additional classes or variants can be chained.</returns>
    public static ValidationResult Success() => _success;

    /// <summary>
    /// Creates a validation result with no validation performed yet.
    /// </summary>
    /// <returns>The same builder instance, so additional classes or variants can be chained.</returns>
    public static ValidationResult None() => _none;

    /// <summary>
    /// Creates a failed validation result with an error message.
    /// </summary>
    /// <param name="errorText">Error Text for the error operation.</param>
    /// <returns>The same builder instance, so additional classes or variants can be chained.</returns>
    public static ValidationResult Error(string errorText) => new() 
    { 
        Status = ValidationStatus.Error, 
        ErrorText = errorText 
    };

    /// <summary>
    /// Creates a failed validation result with an error message and member names.
    /// </summary>
    /// <param name="errorText">Error Text for the error operation.</param>
    /// <param name="memberNames">member Names to process.</param>
    /// <returns>The same builder instance, so additional classes or variants can be chained.</returns>
    public static ValidationResult Error(string errorText, IEnumerable<string> memberNames) => new() 
    { 
        Status = ValidationStatus.Error, 
        ErrorText = errorText,
        MemberNames = memberNames
    };

    /// <summary>
    /// Combines multiple validation results into a single result.
    /// Returns Error if any result is an error, otherwise returns Success if all are successful.
    /// </summary>
    /// <param name="results">results to process.</param>
    /// <returns>The same builder instance, so additional classes or variants can be chained.</returns>
    public static ValidationResult Combine(params ValidationResult[] results) => Combine(results.AsSpan());

    /// <summary>Combines validation results without allocating a parameter array.</summary>
    /// <param name="results">The results to combine in order.</param>
    /// <returns>The combined status, error text, and distinct member names.</returns>
    public static ValidationResult Combine(params ReadOnlySpan<ValidationResult> results)
    {
        if (results.Length == 0)
            return None();

        var hasError = false;
        List<string>? errorMessages = null;
        List<string>? memberNames = null;
        HashSet<string>? memberSet = null;

        for (var i = 0; i < results.Length; i++)
        {
            var result = results[i];
            if (result.Status != ValidationStatus.Error)
                continue;

            hasError = true;

            if (!string.IsNullOrEmpty(result.ErrorText))
            {
                errorMessages ??= [];
                errorMessages.Add(result.ErrorText);
            }

            if (result.MemberNames is null)
                continue;

            foreach (var name in result.MemberNames)
            {
                if (string.IsNullOrEmpty(name))
                    continue;

                memberNames ??= [];

                if (memberSet is null && memberNames.Count >= 8)
                    memberSet = new HashSet<string>(memberNames, StringComparer.Ordinal);
                if (memberSet is not null ? memberSet.Add(name) : !ContainsOrdinal(memberNames, name))
                    memberNames.Add(name);
            }
        }

        if (!hasError)
            return Success();

        var allErrors = BuildErrorText(errorMessages);

        return new ValidationResult
        {
            Status = ValidationStatus.Error,
            ErrorText = allErrors,
            MemberNames = memberNames is { Count: > 0 } ? memberNames : null
        };
    }

    private static bool ContainsOrdinal(List<string> values, string value)
    {
        for (var i = 0; i < values.Count; i++)
        {
            if (string.Equals(values[i], value, StringComparison.Ordinal))
                return true;
        }

        return false;
    }

    private static string BuildErrorText(List<string>? errorMessages)
    {
        if (errorMessages is null || errorMessages.Count == 0)
            return string.Empty;

        if (errorMessages.Count == 1)
            return errorMessages[0];

        return string.Join(" ", errorMessages);
    }
}
