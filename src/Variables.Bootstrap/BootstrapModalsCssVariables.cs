namespace Soenneker.Quark.Variables.Bootstrap;

/// <summary>
/// Bootstrap's modal CSS variables
/// </summary>
public sealed class BootstrapModalsCssVariables
{
    /// <summary>
    /// Modal z-index. Default: 1055
    /// </summary>
    [CssVariable("bs-modal-zindex")]
    public string? ModalZindex { get; set; }

    /// <summary>
    /// Modal width. Default: 500px
    /// </summary>
    [CssVariable("bs-modal-width")]
    public string? ModalWidth { get; set; }

    /// <summary>
    /// Modal padding. Default: 1rem
    /// </summary>
    [CssVariable("bs-modal-padding")]
    public string? ModalPadding { get; set; }

    /// <summary>
    /// Modal margin. Default: 0.5rem
    /// </summary>
    [CssVariable("bs-modal-margin")]
    public string? ModalMargin { get; set; }

    /// <summary>
    /// Modal color. Default: var(--bs-body-color)
    /// </summary>
    [CssVariable("bs-modal-color")]
    public string? ModalColor { get; set; }

    /// <summary>
    /// Modal background color. Default: var(--bs-body-bg)
    /// </summary>
    [CssVariable("bs-modal-bg")]
    public string? ModalBg { get; set; }

    /// <summary>
    /// Modal border. Default: var(--bs-border-width) solid var(--bs-border-color-translucent)
    /// </summary>
    [CssVariable("bs-modal-border")]
    public string? ModalBorder { get; set; }

    /// <summary>
    /// Modal border radius. Default: var(--bs-border-radius-lg)
    /// </summary>
    [CssVariable("bs-modal-border-radius")]
    public string? ModalBorderRadius { get; set; }

    /// <summary>
    /// Modal box shadow. Default: var(--bs-box-shadow)
    /// </summary>
    [CssVariable("bs-modal-box-shadow")]
    public string? ModalBoxShadow { get; set; }

    /// <summary>
    /// Modal backdrop background color. Default: rgba(0, 0, 0, 0.5)
    /// </summary>
    [CssVariable("bs-modal-backdrop-bg")]
    public string? ModalBackdropBg { get; set; }

    /// <summary>
    /// Modal backdrop opacity. Default: 0.5
    /// </summary>
    [CssVariable("bs-modal-backdrop-opacity")]
    public string? ModalBackdropOpacity { get; set; }

    /// <summary>
    /// Modal header border color. Default: var(--bs-border-color)
    /// </summary>
    [CssVariable("bs-modal-header-border-color")]
    public string? ModalHeaderBorderColor { get; set; }

    /// <summary>
    /// Modal header border width. Default: var(--bs-border-width)
    /// </summary>
    [CssVariable("bs-modal-header-border-width")]
    public string? ModalHeaderBorderWidth { get; set; }

    /// <summary>
    /// Modal footer border color. Default: var(--bs-border-color)
    /// </summary>
    [CssVariable("bs-modal-footer-border-color")]
    public string? ModalFooterBorderColor { get; set; }

    /// <summary>
    /// Modal footer border width. Default: var(--bs-border-width)
    /// </summary>
    [CssVariable("bs-modal-footer-border-width")]
    public string? ModalFooterBorderWidth { get; set; }

    /// <summary>
    /// Modal title line height. Default: var(--bs-modal-title-line-height)
    /// </summary>
    [CssVariable("bs-modal-title-line-height")]
    public string? ModalTitleLineHeight { get; set; }

    /// <summary>
    /// Modal content border radius. Default: var(--bs-border-radius-lg)
    /// </summary>
    [CssVariable("bs-modal-content-border-radius")]
    public string? ModalContentBorderRadius { get; set; }

    /// <summary>
    /// Modal content border. Default: var(--bs-modal-border)
    /// </summary>
    [CssVariable("bs-modal-content-border")]
    public string? ModalContentBorder { get; set; }

    /// <summary>
    /// Modal content border width. Default: var(--bs-modal-border-width)
    /// </summary>
    [CssVariable("bs-modal-content-border-width")]
    public string? ModalContentBorderWidth { get; set; }

    /// <summary>
    /// Modal content box shadow. Default: var(--bs-modal-box-shadow)
    /// </summary>
    [CssVariable("bs-modal-content-box-shadow")]
    public string? ModalContentBoxShadow { get; set; }

    /// <summary>
    /// Modal dialog margin. Default: 0.5rem
    /// </summary>
    [CssVariable("bs-modal-dialog-margin")]
    public string? ModalDialogMargin { get; set; }

    /// <summary>
    /// Modal dialog margin Y. Default: 1.75rem
    /// </summary>
    [CssVariable("bs-modal-dialog-margin-y-sm-up")]
    public string? ModalDialogMarginYSmUp { get; set; }

    /// <summary>
    /// Modal dialog transform. Default: translate(0, -50px)
    /// </summary>
    [CssVariable("bs-modal-dialog-transform")]
    public string? ModalDialogTransform { get; set; }

    /// <summary>
    /// Modal fade transform. Default: scale(0.8)
    /// </summary>
    [CssVariable("bs-modal-fade-transform")]
    public string? ModalFadeTransform { get; set; }

    /// <summary>
    /// Modal show transform. Default: none
    /// </summary>
    [CssVariable("bs-modal-show-transform")]
    public string? ModalShowTransform { get; set; }

    /// <summary>
    /// Modal show transform translate. Default: translate(0, -50px)
    /// </summary>
    [CssVariable("bs-modal-show-transform-translate")]
    public string? ModalShowTransformTranslate { get; set; }
}
