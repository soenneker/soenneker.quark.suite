using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Interface for Bootstrap CSS variable groups that can generate CSS without reflection.
/// </summary>
public interface IBootstrapCssVariableGroup
{
    /// <summary>Gets the CSS selector for this variable group (e.g., ":root", ".btn", ".accordion").</summary>
    string GetSelector();

    /// <summary>Gets all CSS variable name/value pairs for properties that have content.</summary>
    /// <returns>Tuples of (CSS property name, property value). Only includes non-null, non-empty values.</returns>
    IEnumerable<(string CssPropertyName, string Value)> GetCssVariables();
}