using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's modal CSS variables
/// </summary>
public sealed class BootstrapModalCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Modal z-index. Default: 1055
	/// </summary>
	public string? Zindex { get; set; }

	/// <summary>
	/// Modal width. Default: 500px
	/// </summary>
	public string? Width { get; set; }

	/// <summary>
	/// Modal padding. Default: 1rem
	/// </summary>
	public string? Padding { get; set; }

	/// <summary>
	/// Modal margin. Default: 0.5rem
	/// </summary>
	public string? Margin { get; set; }

	/// <summary>
	/// Modal color. Default: var(--bs-body-color)
	/// </summary>
	public string? Color { get; set; }

	/// <summary>
	/// Modal background. Default: var(--bs-body-bg)
	/// </summary>
	public string? Background { get; set; }

	/// <summary>
	/// Modal border color. Default: var(--bs-border-color-translucent)
	/// </summary>
	public string? BorderColor { get; set; }

	/// <summary>
	/// Modal border width. Default: var(--bs-border-width)
	/// </summary>
	public string? BorderWidth { get; set; }

	/// <summary>
	/// Modal border radius. Default: var(--bs-border-radius-lg)
	/// </summary>
	public string? BorderRadius { get; set; }

	/// <summary>
	/// Modal box shadow. Default: var(--bs-box-shadow-sm)
	/// </summary>
	public string? BoxShadow { get; set; }

	/// <summary>
	/// Modal inner border radius. Default: calc(var(--bs-border-radius-lg) - (var(--bs-border-width)))
	/// </summary>
	public string? InnerBorderRadius { get; set; }

	/// <summary>
	/// Modal header padding X. Default: 1rem
	/// </summary>
	public string? HeaderPaddingX { get; set; }

	/// <summary>
	/// Modal header padding Y. Default: 1rem
	/// </summary>
	public string? HeaderPaddingY { get; set; }

	/// <summary>
	/// Modal header padding. Default: 1rem 1rem
	/// </summary>
	public string? HeaderPadding { get; set; }

	/// <summary>
	/// Modal header border color. Default: var(--bs-border-color)
	/// </summary>
	public string? HeaderBorderColor { get; set; }

	/// <summary>
	/// Modal header border width. Default: var(--bs-border-width)
	/// </summary>
	public string? HeaderBorderWidth { get; set; }

	/// <summary>
	/// Modal title line height. Default: 1.5
	/// </summary>
	public string? TitleLineHeight { get; set; }

	/// <summary>
	/// Modal footer gap. Default: 0.5rem
	/// </summary>
	public string? FooterGap { get; set; }

	/// <summary>
	/// Modal footer background. Default: transparent
	/// </summary>
	public string? FooterBg { get; set; }

	/// <summary>
	/// Modal footer border color. Default: var(--bs-border-color)
	/// </summary>
	public string? FooterBorderColor { get; set; }

	/// <summary>
	/// Modal footer border width. Default: var(--bs-border-width)
	/// </summary>
	public string? FooterBorderWidth { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the modal border.
	/// </summary>
	public string? Border { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the modal backdrop background.
	/// </summary>
	public string? BackdropBg { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the modal backdrop opacity.
	/// </summary>
	public string? BackdropOpacity { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the modal content border radius.
	/// </summary>
	public string? ContentBorderRadius { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the modal content border.
	/// </summary>
	public string? ContentBorder { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the modal content border width.
	/// </summary>
	public string? ContentBorderWidth { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the modal content box shadow.
	/// </summary>
	public string? ContentBoxShadow { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the modal dialog margin.
	/// </summary>
	public string? DialogMargin { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the modal dialog margin Y on small screens and up.
	/// </summary>
	public string? DialogMarginYSmUp { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the modal dialog transform.
	/// </summary>
	public string? DialogTransform { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the modal fade transform.
	/// </summary>
	public string? FadeTransform { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the modal show transform.
	/// </summary>
	public string? ShowTransform { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the modal show transform translate.
	/// </summary>
	public string? ShowTransformTranslate { get; set; }

	/// <summary>
	/// Gets the CSS selector for the modal component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ".modal";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the modal component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Zindex.HasContent())
            yield return ("--bs-modal-zindex", Zindex);
        if (Width.HasContent())
            yield return ("--bs-modal-width", Width);
        if (Padding.HasContent())
            yield return ("--bs-modal-padding", Padding);
        if (Margin.HasContent())
            yield return ("--bs-modal-margin", Margin);
        if (Color.HasContent())
            yield return ("--bs-modal-color", Color);
        if (Background.HasContent())
            yield return ("--bs-modal-bg", Background);
        if (BorderColor.HasContent())
            yield return ("--bs-modal-border-color", BorderColor);
        if (BorderWidth.HasContent())
            yield return ("--bs-modal-border-width", BorderWidth);
        if (Border.HasContent())
            yield return ("--bs-modal-border", Border);
        if (BorderRadius.HasContent())
            yield return ("--bs-modal-border-radius", BorderRadius);
        if (BoxShadow.HasContent())
            yield return ("--bs-modal-box-shadow", BoxShadow);
        if (InnerBorderRadius.HasContent())
            yield return ("--bs-modal-inner-border-radius", InnerBorderRadius);
        if (BackdropBg.HasContent())
            yield return ("--bs-modal-backdrop-bg", BackdropBg);
        if (BackdropOpacity.HasContent())
            yield return ("--bs-modal-backdrop-opacity", BackdropOpacity);
        if (HeaderPaddingX.HasContent())
            yield return ("--bs-modal-header-padding-x", HeaderPaddingX);
        if (HeaderPaddingY.HasContent())
            yield return ("--bs-modal-header-padding-y", HeaderPaddingY);
        if (HeaderPadding.HasContent())
            yield return ("--bs-modal-header-padding", HeaderPadding);
        if (HeaderBorderColor.HasContent())
            yield return ("--bs-modal-header-border-color", HeaderBorderColor);
        if (HeaderBorderWidth.HasContent())
            yield return ("--bs-modal-header-border-width", HeaderBorderWidth);
        if (TitleLineHeight.HasContent())
            yield return ("--bs-modal-title-line-height", TitleLineHeight);
        if (FooterGap.HasContent())
            yield return ("--bs-modal-footer-gap", FooterGap);
        if (FooterBg.HasContent())
            yield return ("--bs-modal-footer-bg", FooterBg);
        if (FooterBorderColor.HasContent())
            yield return ("--bs-modal-footer-border-color", FooterBorderColor);
        if (FooterBorderWidth.HasContent())
            yield return ("--bs-modal-footer-border-width", FooterBorderWidth);
        if (ContentBorderRadius.HasContent())
            yield return ("--bs-modal-content-border-radius", ContentBorderRadius);
        if (ContentBorder.HasContent())
            yield return ("--bs-modal-content-border", ContentBorder);
        if (ContentBorderWidth.HasContent())
            yield return ("--bs-modal-content-border-width", ContentBorderWidth);
        if (ContentBoxShadow.HasContent())
            yield return ("--bs-modal-content-box-shadow", ContentBoxShadow);
        if (DialogMargin.HasContent())
            yield return ("--bs-modal-dialog-margin", DialogMargin);
        if (DialogMarginYSmUp.HasContent())
            yield return ("--bs-modal-dialog-margin-y-sm-up", DialogMarginYSmUp);
        if (DialogTransform.HasContent())
            yield return ("--bs-modal-dialog-transform", DialogTransform);
        if (FadeTransform.HasContent())
            yield return ("--bs-modal-fade-transform", FadeTransform);
        if (ShowTransform.HasContent())
            yield return ("--bs-modal-show-transform", ShowTransform);
        if (ShowTransformTranslate.HasContent())
            yield return ("--bs-modal-show-transform-translate", ShowTransformTranslate);
    }
}

