using System;

namespace Soenneker.Quark;

public readonly struct QuarkPresetToken : IEquatable<QuarkPresetToken>
{
    private readonly Action<QuarkPresetContext>? _apply;

    public QuarkPresetToken(string name, Action<QuarkPresetContext> apply)
    {
        Name = name ?? string.Empty;
        _apply = apply;
    }

    public string Name { get; }

    public void Apply(QuarkPresetContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _apply?.Invoke(context);
    }

    public bool Equals(QuarkPresetToken other) => string.Equals(Name, other.Name, StringComparison.Ordinal);

    public override bool Equals(object? obj) => obj is QuarkPresetToken other && Equals(other);

    public override int GetHashCode() => StringComparer.Ordinal.GetHashCode(Name);

    public override string ToString() => Name;

    public static bool operator ==(QuarkPresetToken left, QuarkPresetToken right) => left.Equals(right);

    public static bool operator !=(QuarkPresetToken left, QuarkPresetToken right) => !left.Equals(right);
}
