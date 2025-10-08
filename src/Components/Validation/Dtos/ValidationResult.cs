using System.Collections.Generic;
using System.Linq;

namespace Soenneker.Quark;

/// <summary>
/// Represents the result of a validation operation.
/// </summary>
public sealed class ValidationResult
{
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
    public static ValidationResult Success() => new() { Status = ValidationStatus.Success };

    /// <summary>
    /// Creates a validation result with no validation performed yet.
    /// </summary>
    public static ValidationResult None() => new() { Status = ValidationStatus.None };

    /// <summary>
    /// Creates a failed validation result with an error message.
    /// </summary>
    /// <param name="errorText">The error message.</param>
    public static ValidationResult Error(string errorText) => new() 
    { 
        Status = ValidationStatus.Error, 
        ErrorText = errorText 
    };

    /// <summary>
    /// Creates a failed validation result with an error message and member names.
    /// </summary>
    /// <param name="errorText">The error message.</param>
    /// <param name="memberNames">The member names that have errors.</param>
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
    public static ValidationResult Combine(params ValidationResult[] results)
    {
        if (results == null || results.Length == 0)
            return None();

        var errors = results.Where(r => r.Status == ValidationStatus.Error).ToList();
        
        if (errors.Count > 0)
        {
            var allErrors = string.Join(" ", errors.Select(e => e.ErrorText).Where(e => !string.IsNullOrEmpty(e)));
            var allMemberNames = errors.SelectMany(e => e.MemberNames ?? Enumerable.Empty<string>()).Distinct().ToList();
            
            return new ValidationResult
            {
                Status = ValidationStatus.Error,
                ErrorText = allErrors,
                MemberNames = allMemberNames.Count > 0 ? allMemberNames : null
            };
        }

        return Success();
    }
}

