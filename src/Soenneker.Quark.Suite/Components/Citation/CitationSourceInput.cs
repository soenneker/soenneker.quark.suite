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
    /// Executes the create operation.
    /// </summary>
    /// <param name="url">The url.</param>
    /// <param name="title">The title.</param>
    /// <param name="description">The description.</param>
    /// <returns>The result of the operation.</returns>
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
