using System;
using System.Text.RegularExpressions;

namespace Soenneker.Quark;

/// <summary>
/// Simple validators that can be used directly in markup.
/// Much simpler than the complex ValidationRules system.
/// </summary>
public static class Validators
{
    /// <summary>
    /// Validates that the value is not empty.
    /// </summary>
    public static readonly SimpleValidator IsNotEmpty = new(
        "This field is required", 
        value => value is string s && !string.IsNullOrWhiteSpace(s));

    /// <summary>
    /// Validates that the value is empty.
    /// </summary>
    public static readonly SimpleValidator IsEmpty = new(
        "This field must be empty",
        value => value is string s && string.IsNullOrWhiteSpace(s));

    /// <summary>
    /// Validates that the value is a valid email address.
    /// </summary>
    public static readonly SimpleValidator IsEmail = new(
        "Please enter a valid email address",
        value => value is string email && IsValidEmail(email));

    /// <summary>
    /// Validates that the value is a valid URL.
    /// </summary>
    public static readonly SimpleValidator IsUrl = new(
        "Please enter a valid URL",
        value => value is string url && Uri.TryCreate(url, UriKind.Absolute, out _));

    /// <summary>
    /// Validates that the value is numeric.
    /// </summary>
    public static readonly SimpleValidator IsNumeric = new(
        "Please enter a valid number",
        value => value is string s && decimal.TryParse(s, out _));

    /// <summary>
    /// Validates that the value is an integer.
    /// </summary>
    public static readonly SimpleValidator IsInteger = new(
        "Please enter a valid integer",
        value => value is string s && int.TryParse(s, out _));

    /// <summary>
    /// Validates that the value represents a checked state.
    /// </summary>
    public static readonly SimpleValidator IsChecked = new(
        "This field must be checked",
        value => value is string s && s == "on");

    /// <summary>
    /// Validates that the value contains only alphanumeric characters.
    /// </summary>
    public static readonly SimpleValidator IsAlphanumeric = new(
        "Only letters and numbers are allowed",
        value => value is string s && Regex.IsMatch(s, @"^[a-zA-Z0-9]+$"));

    /// <summary>
    /// Validates that the value contains only digits.
    /// </summary>
    public static readonly SimpleValidator IsDigitsOnly = new(
        "Only digits are allowed",
        value => value is string s && Regex.IsMatch(s, @"^\d+$"));

    /// <summary>
    /// Creates a minimum length validator.
    /// </summary>
    /// <param name="minLength">The minimum required length.</param>
    /// <param name="message">Optional custom error message.</param>
    /// <returns>A validator that checks minimum length.</returns>
    public static SimpleValidator MinLength(int minLength, string? message = null) => new(
        message ?? $"Must be at least {minLength} characters",
        value => value is string s && s.Length >= minLength);

    /// <summary>
    /// Creates a maximum length validator.
    /// </summary>
    /// <param name="maxLength">The maximum allowed length.</param>
    /// <param name="message">Optional custom error message.</param>
    /// <returns>A validator that checks maximum length.</returns>
    public static SimpleValidator MaxLength(int maxLength, string? message = null) => new(
        message ?? $"Must be no more than {maxLength} characters",
        value => value is string s && s.Length <= maxLength);

    /// <summary>
    /// Creates a minimum value validator.
    /// </summary>
    /// <param name="minValue">The minimum allowed value.</param>
    /// <param name="message">Optional custom error message.</param>
    /// <returns>A validator that checks minimum value.</returns>
    public static SimpleValidator MinValue(decimal minValue, string? message = null) => new(
        message ?? $"Must be at least {minValue}",
        value => value is string s && decimal.TryParse(s, out var num) && num >= minValue);

    /// <summary>
    /// Creates a maximum value validator.
    /// </summary>
    /// <param name="maxValue">The maximum allowed value.</param>
    /// <param name="message">Optional custom error message.</param>
    /// <returns>A validator that checks maximum value.</returns>
    public static SimpleValidator MaxValue(decimal maxValue, string? message = null) => new(
        message ?? $"Must be no more than {maxValue}",
        value => value is string s && decimal.TryParse(s, out var num) && num <= maxValue);

    /// <summary>
    /// Simple email validation using regex.
    /// </summary>
    private static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }
        catch
        {
            return false;
        }
    }
}
