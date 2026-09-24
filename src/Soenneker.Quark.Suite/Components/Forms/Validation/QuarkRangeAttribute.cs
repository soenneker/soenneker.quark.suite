using System;

namespace Soenneker.Quark;

/// <summary>Requests a generated typed range without runtime type-converter discovery.</summary>
/// <remarks>Consumed by Soenneker.Quark.Gen.Validation, not by the framework's reflection-based Validator. Custom operand types must specify TypeConverterAttribute with a concrete converter type.</remarks>
[AttributeUsage(AttributeTargets.Property)]
public sealed class QuarkRangeAttribute(Type operandType, string minimum, string maximum) : Attribute
{
    /// <summary>Gets the comparable operand type.</summary>
    public Type OperandType { get; } = operandType;
    /// <summary>Gets the minimum expressed as text.</summary>
    public string Minimum { get; } = minimum;
    /// <summary>Gets the maximum expressed as text.</summary>
    public string Maximum { get; } = maximum;
    /// <summary>Gets or sets whether the minimum is excluded.</summary>
    public bool MinimumIsExclusive { get; set; }
    /// <summary>Gets or sets whether the maximum is excluded.</summary>
    public bool MaximumIsExclusive { get; set; }
    /// <summary>Gets or sets whether limits are parsed using invariant culture.</summary>
    public bool ParseLimitsInInvariantCulture { get; set; }
    /// <summary>Gets or sets whether values are converted using invariant culture.</summary>
    public bool ConvertValueInInvariantCulture { get; set; }
    /// <summary>Gets or sets the error format; {0}, {1}, and {2} are the display name, minimum, and maximum.</summary>
    public string? ErrorMessage { get; set; }
    /// <summary>Gets or sets the type exposing the error resource as a static string property.</summary>
    public Type? ErrorMessageResourceType { get; set; }
    /// <summary>Gets or sets the error resource property name.</summary>
    public string? ErrorMessageResourceName { get; set; }
}
