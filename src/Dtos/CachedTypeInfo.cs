namespace Soenneker.Quark.Dtos;

/// <summary>
/// Represents cached type information for CSS variable generation.
/// </summary>
public sealed class CachedTypeInfo
{
	/// <summary>
	/// Gets or sets the CSS selector string.
	/// </summary>
    public required string Selector;
	
	/// <summary>
	/// Gets or sets the array of accessors for retrieving option and style values.
	/// </summary>
    public required Accessor[] Accessors;
}