using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

public sealed class BootstrapAccordionButtonCssVariables : IBootstrapCssVariableGroup
{
    public string? ButtonColor { get; set; }

    public string? ButtonBackground { get; set; }

    public string? ButtonFocusBorderColor { get; set; }

    public string? ButtonFocusBoxShadow { get; set; }

    public string GetSelector()
    {
        return ".accordion-button";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (ButtonColor.HasContent())
            yield return ("--bs-accordion-btn-color", ButtonColor);

        if (ButtonBackground.HasContent())
            yield return ("--bs-accordion-btn-bg", ButtonBackground);

        if (ButtonFocusBorderColor.HasContent())
            yield return ("--bs-accordion-btn-focus-border-color", ButtonFocusBorderColor);

        if (ButtonFocusBoxShadow.HasContent())
            yield return ("--bs-accordion-btn-focus-box-shadow", ButtonFocusBoxShadow);
    }
}