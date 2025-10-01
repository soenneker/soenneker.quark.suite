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

    /// <summary>
    /// The CSS class selector where this variable should be applied (e.g., ".btn-primary", ".card")
    /// If null or empty, the variable will be applied to :root
    /// </summary>
    public string? Class { get; }

    public CssVariableAttribute(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Class = null;
    }

    public CssVariableAttribute(string name, string @class)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Class = @class ?? throw new ArgumentNullException(nameof(@class));
    }

    /// <summary>
    /// Gets the full Bootstrap CSS custom property name
    /// </summary>
    public string GetName()
    {
        return "--" + Name;
    }

    /// <summary>
    /// Gets the CSS selector for this variable
    /// </summary>
    public string GetSelector()
    {
        return string.IsNullOrEmpty(Class) ? ":root" : Class;
    }
}
