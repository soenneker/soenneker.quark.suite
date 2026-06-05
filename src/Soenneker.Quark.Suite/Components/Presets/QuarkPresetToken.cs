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
    /// Executes the apply operation.
    /// </summary>
    /// <param name="context">The context.</param>
    public void Apply(QuarkPresetContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _apply?.Invoke(context);
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="other">The other.</param>
    /// <returns>A value indicating whether the operation succeeded.</returns>
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
    /// Executes the operator == operation.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>A value indicating whether the operation succeeded.</returns>
    public static bool operator ==(QuarkPresetToken left, QuarkPresetToken right) => left.Equals(right);

    /// <summary>
    /// Executes the operator != operation.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>A value indicating whether the operation succeeded.</returns>
    public static bool operator !=(QuarkPresetToken left, QuarkPresetToken right) => !left.Equals(right);
}
