using System;

namespace Soenneker.Quark;

/// <summary>
/// Attribute to mark properties that should be converted to CSS properties
/// </summary>
[AttributeUsage(AttributeTargets.Property, Inherited = true)]
public sealed class CssPropertyAttribute : Attribute
{
    /// <summary>
    /// The CSS property name (e.g., "text-decoration", "margin", "padding")
    /// </summary>
    public string PropertyName { get; }

    public CssPropertyAttribute(string propertyName)
    {
        PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
    }
}

