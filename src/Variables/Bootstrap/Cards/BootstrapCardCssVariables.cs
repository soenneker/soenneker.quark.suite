using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Base CSS variables for Bootstrap cards.
/// </summary>
public sealed class BootstrapCardCssVariables : IBootstrapCssVariableGroup
{
	public string? Color { get; set; }

	public string? Background { get; set; }

	public string? BorderColor { get; set; }

	public string? BorderWidth { get; set; }

	public string? BorderRadius { get; set; }

	public string? InnerBorderRadius { get; set; }

	public string? CapBackground { get; set; }

	public string? CapColor { get; set; }

	public string? CapPaddingX { get; set; }

	public string? CapPaddingY { get; set; }

	public string? SpacerX { get; set; }

	public string? SpacerY { get; set; }

	public string? TitleSpacerY { get; set; }

	public string? TitleColor { get; set; }

	public string? SubtitleColor { get; set; }

	public string? ImgOverlayPadding { get; set; }

	public string? GroupMargin { get; set; }

	public string? DeckGap { get; set; }

	public string? ColumnsCount { get; set; }

	public string? ColumnsGap { get; set; }

	public string? ColumnsMargin { get; set; }

    public string GetSelector()
    {
        return ".card";
    }

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


