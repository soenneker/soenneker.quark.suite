using System;

namespace Soenneker.Quark.Dtos;

/// <summary>
/// Represents an accessor for retrieving option values and style values from objects.
/// </summary>
public sealed class Accessor
{
	/// <summary>
	/// Gets or sets the function that retrieves the option value from an options object (returns CssValue&lt;T&gt; boxed or null).
	/// </summary>
    public required Func<object, object?> GetOptionValue;

	/// <summary>
	/// Gets or sets the function that retrieves the style value from a CssValue&lt;T&gt; object (returns StyleValue boxed or null).
	/// </summary>
    public required Func<object, object?> GetStyleValue;

	/// <summary>
	/// Gets or sets the precomputed CSS prefix string (format: "  " + cssName + ": ").
	/// </summary>
    public required string CssPrefix;
}