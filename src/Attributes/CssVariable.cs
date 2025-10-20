using System;

namespace Soenneker.Quark;

/// <summary>
/// Attribute to mark properties that should be converted to CSS custom properties or direct CSS properties
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class CssVariableAttribute : Attribute
{
    /// <summary>
    /// The CSS property name (e.g., "bs-primary", "bs-btn-bg" for variables, or "text-decoration", "color" for direct properties)
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// If true, this is a CSS custom property (variable) and will be prefixed with --. If false, it's a direct CSS property.
    /// </summary>
    public bool IsVariable { get; }

    public CssVariableAttribute(string name, bool isVariable = true)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        IsVariable = isVariable;
    }

    /// <summary>
    /// Gets the full CSS property name (with -- prefix if it's a variable)
    /// </summary>
    public string GetName()
    {
        return IsVariable ? "--" + Name : Name;
    }
}