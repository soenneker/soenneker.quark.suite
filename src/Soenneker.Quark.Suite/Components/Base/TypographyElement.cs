using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Focused text/typography base for components that expose additional text-only utilities.
/// </summary>
public abstract class TypographyElement : Element
{
    protected void BuildTypographyDefaultAttributes(Dictionary<string, object> attributes, CssValue<TextSizeBuilder>? textSize = null,
        CssValue<FontWeightBuilder>? fontWeight = null, CssValue<TrackingBuilder>? tracking = null, CssValue<ScrollMarginBuilder>? scrollMargin = null,
        CssValue<TextWrapBuilder>? textWrap = null, CssValue<BorderBuilder>? border = null, CssValue<PaddingBuilder>? padding = null,
        CssValue<TransitionBuilder>? transition = null)
    {
        var preset = BuildPresetContext();

        BuildClassAndStyleAttributes(attributes, (ref cls, ref sty) =>
        {
            if (TextSize is null && preset?.TextSize is null)
                AddCss(ref sty, ref cls, textSize);

            if (FontWeight is null && preset?.FontWeight is null)
                AddCss(ref sty, ref cls, fontWeight);

            if (Tracking is null && preset?.Tracking is null)
                AddCss(ref sty, ref cls, tracking);

            if (ScrollMargin is null && preset?.ScrollMargin is null)
                AddCss(ref sty, ref cls, scrollMargin);

            if (TextWrap is null && preset?.TextWrap is null)
                AddCss(ref sty, ref cls, textWrap);

            if (Border is null && preset?.Border is null)
                AddCss(ref sty, ref cls, border);

            if (Padding is null && preset?.Padding is null)
                AddCss(ref sty, ref cls, padding);

            if (Transition is null && preset?.Transition is null)
                AddCss(ref sty, ref cls, transition);
        });
    }
}
