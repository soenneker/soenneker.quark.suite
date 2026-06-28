
using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Represents the card options.
/// </summary>
public sealed class CardOptions : ComponentOptions
{
    public CardOptions()
    {
        Selector = "[data-slot='card']";
    }

    /// <summary>
    /// Gets or sets anchor styling scoped to the card.
    /// </summary>
    public AnchorOptions? Anchors { get; set; }

    /// <summary>
    /// Gets or sets action styling scoped to the card.
    /// </summary>
    public CardActionOptions? Actions { get; set; }

    /// <summary>
    /// Gets or sets body/content styling scoped to the card.
    /// </summary>
    public CardBodyOptions? Bodies { get; set; }

    /// <summary>
    /// Gets or sets button styling scoped to the card.
    /// </summary>
    public ButtonOptions? Buttons { get; set; }

    /// <summary>
    /// Gets or sets description styling scoped to the card.
    /// </summary>
    public CardDescriptionOptions? Descriptions { get; set; }

    /// <summary>
    /// Gets or sets footer styling scoped to the card.
    /// </summary>
    public CardFooterOptions? Footers { get; set; }

    /// <summary>
    /// Gets or sets header styling scoped to the card.
    /// </summary>
    public CardHeaderOptions? Headers { get; set; }

    /// <summary>
    /// Gets or sets image styling scoped to the card.
    /// </summary>
    public CardImgOptions? Images { get; set; }

    /// <summary>
    /// Gets or sets subtitle styling scoped to the card.
    /// </summary>
    public CardSubtitleOptions? Subtitles { get; set; }

    /// <summary>
    /// Gets or sets text styling scoped to the card.
    /// </summary>
    public CardTextOptions? Texts { get; set; }

    /// <summary>
    /// Gets or sets title styling scoped to the card.
    /// </summary>
    public CardTitleOptions? Titles { get; set; }

    private protected override void CollectChildCssRules(List<ComponentCssRule> buffer, string baseSelector)
    {
        AddChildCssRules(buffer, Anchors, "[data-slot='anchor']", "[data-slot='anchor']", baseSelector);
        AddChildCssRules(buffer, Actions, "[data-slot='card-action']", "[data-slot='card-action']", baseSelector);
        AddChildCssRules(buffer, Bodies, "[data-slot='card-content']", "[data-slot='card-content']", baseSelector);
        AddChildCssRules(buffer, Buttons, "[data-slot='button']", "[data-slot='button']", baseSelector);
        AddChildCssRules(buffer, Descriptions, "[data-slot='card-description']", "[data-slot='card-description']", baseSelector);
        AddChildCssRules(buffer, Footers, "[data-slot='card-footer']", "[data-slot='card-footer']", baseSelector);
        AddChildCssRules(buffer, Headers, "[data-slot='card-header']", "[data-slot='card-header']", baseSelector);
        AddChildCssRules(buffer, Images, "[data-slot='card-img']", "[data-slot='card-img']", baseSelector);
        AddChildCssRules(buffer, Subtitles, "[data-slot='card-subtitle']", "[data-slot='card-subtitle']", baseSelector);
        AddChildCssRules(buffer, Texts, "[data-slot='card-text']", "[data-slot='card-text']", baseSelector);
        AddChildCssRules(buffer, Titles, "[data-slot='card-title']", "[data-slot='card-title']", baseSelector);
    }
}
