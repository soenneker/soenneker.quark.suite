using System;

namespace Soenneker.Quark;

/// <summary>
/// Represents the quark preset token structure.
/// </summary>
public readonly struct QuarkPresetToken : IEquatable<QuarkPresetToken>
{
    private readonly Action<QuarkPresetContext>? _apply;

    public QuarkPresetToken(string name, Action<QuarkPresetContext> apply)
    {
        Name = name ?? string.Empty;
        _apply = apply;
    }

    /// <summary>
    /// Gets name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Applies quark Preset Token for the Quark Preset Token.
    /// </summary>
    /// <param name="context">HTTP context containing the Authorization header.</param>
    public void Apply(QuarkPresetContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _apply?.Invoke(context);
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="other">Value to compare with this instance.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public bool Equals(QuarkPresetToken other) => string.Equals(Name, other.Name, StringComparison.Ordinal);

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The obj.</param>
    /// <returns>A value indicating whether the operation succeeded.</returns>
    public override bool Equals(object? obj) => obj is QuarkPresetToken other && Equals(other);

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>The result of the operation.</returns>
    public override int GetHashCode() => StringComparer.Ordinal.GetHashCode(Name);

    /// <summary>
    /// Returns a string representation of the current instance.
    /// </summary>
    /// <returns>The result of the operation.</returns>
    public override string ToString() => Name;

    /// <summary>
    /// Determines whether two Quark Preset Token values are equal.
    /// </summary>
    /// <param name="left">Left for the operator == operation.</param>
    /// <param name="right">Right for the operator == operation.</param>
    /// <returns>true if the two values are equal; otherwise, false.</returns>
    public static bool operator ==(QuarkPresetToken left, QuarkPresetToken right) => left.Equals(right);

    /// <summary>
    /// Determines whether two Quark Preset Token values are different.
    /// </summary>
    /// <param name="left">Left for the operator != operation.</param>
    /// <param name="right">Right for the operator != operation.</param>
    /// <returns>true if the two values differ; otherwise, false.</returns>
    public static bool operator !=(QuarkPresetToken left, QuarkPresetToken right) => !left.Equals(right);
}
