using System;

namespace Soenneker.Quark;

/// <summary>
/// Attribute to mark classes that represent CSS variables for a specific CSS selector
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class CssSelectorAttribute : Attribute
{
    /// <summary>
    /// The CSS class selector where this variable should be applied (e.g., ".btn-primary", ".card")
    /// If null or empty, the variable will be applied to :root
    /// </summary>
    public string? Class { get; }

    public CssSelectorAttribute(string? @class = null)
    {
        Class = @class;
    }

    /// <summary>
    /// Gets the CSS selector for this class
    /// </summary>
    public string GetSelector()
    {
        return string.IsNullOrEmpty(Class) ? ":root" : Class;
    }
}
