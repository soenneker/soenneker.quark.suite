namespace Soenneker.Quark;

/// <summary>
/// Represents the result of consumer-defined connection validation.
/// </summary>
public sealed class NodeEditorConnectionValidationResult
{
    /// <summary>Gets or sets whether the proposed connection is allowed.</summary>
    public bool Allowed { get; set; } = true;

    /// <summary>Gets or sets an optional explanation when the proposed connection is rejected.</summary>
    public string? Message { get; set; }

    /// <summary>
    /// Creates a validation result that accepts the proposed connection.
    /// </summary>
    /// <returns>An accepting validation result.</returns>
    public static NodeEditorConnectionValidationResult Accept() => new();

    /// <summary>
    /// Creates a validation result that rejects the proposed connection.
    /// </summary>
    /// <param name="message">An optional explanation for the rejection.</param>
    /// <returns>A rejecting validation result.</returns>
    public static NodeEditorConnectionValidationResult Reject(string? message = null) =>
        new() { Allowed = false, Message = message };
}
