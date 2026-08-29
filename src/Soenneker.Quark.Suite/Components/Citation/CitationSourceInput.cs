using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents the citation source input.
/// </summary>
public sealed class CitationSourceInput
{
    /// <summary>
    /// Gets or sets url.
    /// </summary>
    public string Url { get; set; } = "";

    /// <summary>
    /// Gets or sets title.
    /// </summary>
    public RenderFragment? Title { get; set; }

    /// <summary>
    /// Gets or sets description.
    /// </summary>
    public RenderFragment? Description { get; set; }

    /// <summary>
    /// Creates a Citation Source Input instance from the supplied inputs.
    /// </summary>
    /// <param name="url">URL of the resource to target.</param>
    /// <param name="title">Page title, when available.</param>
    /// <param name="description">Description for the create operation.</param>
    /// <returns>The same builder instance, so additional classes or variants can be chained.</returns>
    public static CitationSourceInput Create(string url, string? title = null, string? description = null)
    {
        return new CitationSourceInput
        {
            Url = url,
            Title = title is null ? null : builder => builder.AddContent(0, title),
            Description = description is null ? null : builder => builder.AddContent(0, description)
        };
    }
}
