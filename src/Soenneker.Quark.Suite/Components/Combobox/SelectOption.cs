namespace Soenneker.Quark;

/// <summary>A choice with an independent logical value and display label.</summary>
/// <param name="Value">Unique logical value, including an optional empty value.</param>
/// <param name="Label">Display text; labels need not be unique.</param>
/// <param name="Disabled">Whether selection is unavailable.</param>
public sealed record SelectOption(string Value, string Label, bool Disabled = false);
