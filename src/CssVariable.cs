using System;

namespace Soenneker.Quark;

/// <summary>
/// Attribute to mark properties that should be converted to CSS custom properties
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class CssVariableAttribute : Attribute
{
    /// <summary>
    /// The Bootstrap CSS custom property name (e.g., "bs-primary", "bs-btn-bg")
    /// </summary>
    public string Name { get; }

    public CssVariableAttribute(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    /// <summary>
    /// Gets the full Bootstrap CSS custom property name
    /// </summary>
    public string GetName()
    {
        return "--" + Name;
    }
}
