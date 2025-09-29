
namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's carousel CSS variables
/// </summary>
public sealed class BootstrapCarouselCssVariables
{
    /// <summary>
    /// Carousel control color. Default: var(--bs-white)
    /// </summary>
    [CssVariable("bs-carousel-control-color")]
    public string? CarouselControlColor { get; set; }

    /// <summary>
    /// Carousel control opacity. Default: 0.5
    /// </summary>
    [CssVariable("bs-carousel-control-opacity")]
    public string? CarouselControlOpacity { get; set; }

    /// <summary>
    /// Carousel control hover opacity. Default: 0.9
    /// </summary>
    [CssVariable("bs-carousel-control-hover-opacity")]
    public string? CarouselControlHoverOpacity { get; set; }

    /// <summary>
    /// Carousel control transition. Default: opacity 0.15s ease
    /// </summary>
    [CssVariable("bs-carousel-control-transition")]
    public string? CarouselControlTransition { get; set; }

    /// <summary>
    /// Carousel indicator width. Default: 30px
    /// </summary>
    [CssVariable("bs-carousel-indicator-width")]
    public string? CarouselIndicatorWidth { get; set; }

    /// <summary>
    /// Carousel indicator height. Default: 3px
    /// </summary>
    [CssVariable("bs-carousel-indicator-height")]
    public string? CarouselIndicatorHeight { get; set; }

    /// <summary>
    /// Carousel indicator hit area height. Default: 10px
    /// </summary>
    [CssVariable("bs-carousel-indicator-hit-area-height")]
    public string? CarouselIndicatorHitAreaHeight { get; set; }

    /// <summary>
    /// Carousel indicator spacing. Default: 3px
    /// </summary>
    [CssVariable("bs-carousel-indicator-spacer")]
    public string? CarouselIndicatorSpacer { get; set; }

    /// <summary>
    /// Carousel indicator opacity. Default: 0.5
    /// </summary>
    [CssVariable("bs-carousel-indicator-opacity")]
    public string? CarouselIndicatorOpacity { get; set; }

    /// <summary>
    /// Carousel indicator active opacity. Default: 1
    /// </summary>
    [CssVariable("bs-carousel-indicator-active-opacity")]
    public string? CarouselIndicatorActiveOpacity { get; set; }

    /// <summary>
    /// Carousel indicator active background color. Default: var(--bs-primary)
    /// </summary>
    [CssVariable("bs-carousel-indicator-active-bg")]
    public string? CarouselIndicatorActiveBg { get; set; }

    /// <summary>
    /// Carousel indicator transition. Default: opacity 0.6s ease
    /// </summary>
    [CssVariable("bs-carousel-indicator-transition")]
    public string? CarouselIndicatorTransition { get; set; }

    /// <summary>
    /// Carousel caption color. Default: var(--bs-white)
    /// </summary>
    [CssVariable("bs-carousel-caption-color")]
    public string? CarouselCaptionColor { get; set; }

    /// <summary>
    /// Carousel caption padding Y. Default: 1.25rem
    /// </summary>
    [CssVariable("bs-carousel-caption-padding-y")]
    public string? CarouselCaptionPaddingY { get; set; }

    /// <summary>
    /// Carousel caption spacing. Default: 1.25rem
    /// </summary>
    [CssVariable("bs-carousel-caption-spacer")]
    public string? CarouselCaptionSpacer { get; set; }

    /// <summary>
    /// Carousel control icon width. Default: 2rem
    /// </summary>
    [CssVariable("bs-carousel-control-icon-width")]
    public string? CarouselControlIconWidth { get; set; }

    /// <summary>
    /// Carousel control icon height. Default: 2rem
    /// </summary>
    [CssVariable("bs-carousel-control-icon-height")]
    public string? CarouselControlIconHeight { get; set; }

    /// <summary>
    /// Carousel control prev icon background. Default: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16' fill='%23fff'%3e%3cpath d='M11.354 1.646a.5.5 0 0 1 0 .708L5.707 8l5.647 5.646a.5.5 0 0 1-.708.708l-6-6a.5.5 0 0 1 0-.708l6-6a.5.5 0 0 1 .708 0z'/%3e%3c/svg%3e")
    /// </summary>
    [CssVariable("bs-carousel-control-prev-icon-bg")]
    public string? CarouselControlPrevIconBg { get; set; }

    /// <summary>
    /// Carousel control next icon background. Default: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16' fill='%23fff'%3e%3cpath d='M4.646 1.646a.5.5 0 0 1 .708 0l6 6a.5.5 0 0 1 0 .708l-6 6a.5.5 0 0 1-.708-.708L10.293 8 4.646 2.354a.5.5 0 0 1 0-.708z'/%3e%3c/svg%3e")
    /// </summary>
    [CssVariable("bs-carousel-control-next-icon-bg")]
    public string? CarouselControlNextIconBg { get; set; }

    /// <summary>
    /// Carousel transition. Default: transform 0.6s ease-in-out
    /// </summary>
    [CssVariable("bs-carousel-transition")]
    public string? CarouselTransition { get; set; }

    /// <summary>
    /// Carousel transition transform. Default: translateX(0)
    /// </summary>
    [CssVariable("bs-carousel-transition-transform")]
    public string? CarouselTransitionTransform { get; set; }

    /// <summary>
    /// Carousel fade transform. Default: scale(0.8)
    /// </summary>
    [CssVariable("bs-carousel-fade-transform")]
    public string? CarouselFadeTransform { get; set; }

    /// <summary>
    /// Carousel show transform. Default: none
    /// </summary>
    [CssVariable("bs-carousel-show-transform")]
    public string? CarouselShowTransform { get; set; }

    /// <summary>
    /// Carousel show transform translate. Default: translateX(0)
    /// </summary>
    [CssVariable("bs-carousel-show-transform-translate")]
    public string? CarouselShowTransformTranslate { get; set; }
}
