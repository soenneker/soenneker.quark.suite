using System;

namespace Soenneker.Quark;

/// <summary>Requests a generated property comparison without the reflection contract of CompareAttribute.</summary>
/// <remarks>Consumed by Soenneker.Quark.Gen.Validation, not by the framework's reflection-based Validator.</remarks>
[AttributeUsage(AttributeTargets.Property)]
public sealed class QuarkCompareAttribute(string otherProperty) : Attribute
{
    /// <summary>Gets the name of the public property to compare.</summary>
    public string OtherProperty { get; } = otherProperty;
    /// <summary>Gets or sets the error format; {0} and {1} are the two display names.</summary>
    public string? ErrorMessage { get; set; }
    /// <summary>Gets or sets the type exposing the error resource as a static string property.</summary>
    public Type? ErrorMessageResourceType { get; set; }
    /// <summary>Gets or sets the error resource property name.</summary>
    public string? ErrorMessageResourceName { get; set; }
}
