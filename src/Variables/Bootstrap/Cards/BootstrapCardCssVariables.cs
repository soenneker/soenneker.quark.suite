using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Base CSS variables for Bootstrap cards.
/// </summary>
public sealed class BootstrapCardCssVariables : IBootstrapCssVariableGroup
{
    /// <summary>
    /// Gets or sets the CSS variable value for the card text color.
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// Gets or sets the CSS variable value for the card background color.
    /// </summary>
    public string? Background { get; set; }

    /// <summary>
    /// Gets or sets the CSS variable value for the card border color.
    /// </summary>
    public string? BorderColor { get; set; }

    /// <summary>
    /// Gets or sets the CSS variable value for the card border width.
    /// </summary>
    public string? BorderWidth { get; set; }

    /// <summary>
    /// Gets or sets the CSS variable value for the card border radius.
    /// </summary>
    public string? BorderRadius { get; set; }

    /// <summary>
    /// Gets or sets the CSS variable value for the card inner border radius.
    /// </summary>
    public string? InnerBorderRadius { get; set; }

    /// <summary>
    /// Gets or sets the CSS variable value for the card cap background color.
    /// </summary>
    public string? CapBackground { get; set; }

    /// <summary>
    /// Gets or sets the CSS variable value for the card cap text color.
    /// </summary>
    public string? CapColor { get; set; }

    /// <summary>
    /// Gets or sets the CSS variable value for the card cap horizontal padding.
    /// </summary>
    public string? CapPaddingX { get; set; }

    /// <summary>
    /// Gets or sets the CSS variable value for the card cap vertical padding.
    /// </summary>
    public string? CapPaddingY { get; set; }

    /// <summary>
    /// Gets or sets the CSS variable value for the card horizontal spacer.
    /// </summary>
    public string? SpacerX { get; set; }

    /// <summary>
    /// Gets or sets the CSS variable value for the card vertical spacer.
    /// </summary>
    public string? SpacerY { get; set; }

    /// <summary>
    /// Gets or sets the CSS variable value for the card title vertical spacer.
    /// </summary>
    public string? TitleSpacerY { get; set; }

    /// <summary>
    /// Gets or sets the CSS variable value for the card title color.
    /// </summary>
    public string? TitleColor { get; set; }

    /// <summary>
    /// Gets or sets the CSS variable value for the card subtitle color.
    /// </summary>
    public string? SubtitleColor { get; set; }

    /// <summary>
    /// Gets or sets the CSS variable value for the card image overlay padding.
    /// </summary>
    public string? ImgOverlayPadding { get; set; }

    /// <summary>
    /// Gets or sets the CSS variable value for the card group margin.
    /// </summary>
    public string? GroupMargin { get; set; }

    /// <summary>
    /// Gets or sets the CSS variable value for the card deck gap.
    /// </summary>
    public string? DeckGap { get; set; }

    /// <summary>
    /// Gets or sets the CSS variable value for the card columns count.
    /// </summary>
    public string? ColumnsCount { get; set; }

    /// <summary>
    /// Gets or sets the CSS variable value for the card columns gap.
    /// </summary>
    public string? ColumnsGap { get; set; }

    /// <summary>
    /// Gets or sets the CSS variable value for the card columns margin.
    /// </summary>
    public string? ColumnsMargin { get; set; }

    /// <summary>
    /// Gets the CSS selector for the card component.
    /// </summary>
    /// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ".card";
    }

    /// <summary>
    /// Gets the collection of CSS variables for the card component.
    /// </summary>
    /// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Color.HasContent())
            yield return ("--bs-card-color", Color);
        if (Background.HasContent())
            yield return ("--bs-card-bg", Background);
        if (BorderColor.HasContent())
            yield return ("--bs-card-border-color", BorderColor);
        if (BorderWidth.HasContent())
            yield return ("--bs-card-border-width", BorderWidth);
        if (BorderRadius.HasContent())
            yield return ("--bs-card-border-radius", BorderRadius);
        if (InnerBorderRadius.HasContent())
            yield return ("--bs-card-inner-border-radius", InnerBorderRadius);
        if (CapBackground.HasContent())
            yield return ("--bs-card-cap-bg", CapBackground);
        if (CapColor.HasContent())
            yield return ("--bs-card-cap-color", CapColor);
        if (CapPaddingX.HasContent())
            yield return ("--bs-card-cap-padding-x", CapPaddingX);
        if (CapPaddingY.HasContent())
            yield return ("--bs-card-cap-padding-y", CapPaddingY);
        if (SpacerX.HasContent())
            yield return ("--bs-card-spacer-x", SpacerX);
        if (SpacerY.HasContent())
            yield return ("--bs-card-spacer-y", SpacerY);
        if (TitleSpacerY.HasContent())
            yield return ("--bs-card-title-spacer-y", TitleSpacerY);
        if (TitleColor.HasContent())
            yield return ("--bs-card-title-color", TitleColor);
        if (SubtitleColor.HasContent())
            yield return ("--bs-card-subtitle-color", SubtitleColor);
        if (ImgOverlayPadding.HasContent())
            yield return ("--bs-card-img-overlay-padding", ImgOverlayPadding);
        if (GroupMargin.HasContent())
            yield return ("--bs-card-group-margin", GroupMargin);
        if (DeckGap.HasContent())
            yield return ("--bs-card-deck-gap", DeckGap);
        if (ColumnsCount.HasContent())
            yield return ("--bs-card-columns-count", ColumnsCount);
        if (ColumnsGap.HasContent())
            yield return ("--bs-card-columns-gap", ColumnsGap);
        if (ColumnsMargin.HasContent())
            yield return ("--bs-card-columns-margin", ColumnsMargin);
    }
}