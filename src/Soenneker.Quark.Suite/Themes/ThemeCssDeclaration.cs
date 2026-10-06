namespace Soenneker.Quark;

/// <summary>A literal CSS property and value for a theme selector rule.</summary>
/// <param name="Property">The CSS property name, including custom properties such as <c>--accent</c>.</param>
/// <param name="Value">The CSS value, including any required units. No utility conversion is performed.</param>
public readonly record struct ThemeCssDeclaration(string Property, string Value);
