using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's spacing CSS variables
/// </summary>
public sealed class BootstrapSpacingCssVariables : IBootstrapCssVariableGroup
{
    // Grid Gutters
    /// <summary>
    /// Gutter X. Default: 1.5rem
    /// </summary>
    public string? GutterX { get; set; }

    /// <summary>
    /// Gutter Y. Default: 0
    /// </summary>
    public string? GutterY { get; set; }

    // Spacer Base
    /// <summary>
    /// Spacer base. Default: 1rem
    /// </summary>
    public string? Spacer { get; set; }

    // Spacer Variations
    /// <summary>
    /// Spacer 0. Default: 0
    /// </summary>
    public string? Spacer0 { get; set; }

    /// <summary>
    /// Spacer 1. Default: 0.25rem
    /// </summary>
    public string? Spacer1 { get; set; }

    /// <summary>
    /// Spacer 2. Default: 0.5rem
    /// </summary>
    public string? Spacer2 { get; set; }

    /// <summary>
    /// Spacer 3. Default: 1rem
    /// </summary>
    public string? Spacer3 { get; set; }

    /// <summary>
    /// Spacer 4. Default: 1.5rem
    /// </summary>
    public string? Spacer4 { get; set; }

    /// <summary>
    /// Spacer 5. Default: 3rem
    /// </summary>
    public string? Spacer5 { get; set; }

    // Grid Gutter Variations
    /// <summary>
    /// Gutter X 0. Default: 0
    /// </summary>
    public string? GutterX0 { get; set; }

    /// <summary>
    /// Gutter Y 0. Default: 0
    /// </summary>
    public string? GutterY0 { get; set; }

    /// <summary>
    /// Gutter X 1. Default: 0.25rem
    /// </summary>
    public string? GutterX1 { get; set; }

    /// <summary>
    /// Gutter Y 1. Default: 0.25rem
    /// </summary>
    public string? GutterY1 { get; set; }

    /// <summary>
    /// Gutter X 2. Default: 0.5rem
    /// </summary>
    public string? GutterX2 { get; set; }

    /// <summary>
    /// Gutter Y 2. Default: 0.5rem
    /// </summary>
    public string? GutterY2 { get; set; }

    /// <summary>
    /// Gutter X 3. Default: 1rem
    /// </summary>
    public string? GutterX3 { get; set; }

    /// <summary>
    /// Gutter Y 3. Default: 1rem
    /// </summary>
    public string? GutterY3 { get; set; }

    /// <summary>
    /// Gutter X 4. Default: 1.5rem
    /// </summary>
    public string? GutterX4 { get; set; }

    /// <summary>
    /// Gutter Y 4. Default: 1.5rem
    /// </summary>
    public string? GutterY4 { get; set; }

    /// <summary>
    /// Gutter X 5. Default: 3rem
    /// </summary>
    public string? GutterX5 { get; set; }

    /// <summary>
    /// Gutter Y 5. Default: 3rem
    /// </summary>
    public string? GutterY5 { get; set; }

    // Container Max Widths
    /// <summary>
    /// Container max width small. Default: 540px
    /// </summary>
    public string? ContainerMaxWidthSm { get; set; }

    /// <summary>
    /// Container max width medium. Default: 720px
    /// </summary>
    public string? ContainerMaxWidthMd { get; set; }

    /// <summary>
    /// Container max width large. Default: 960px
    /// </summary>
    public string? ContainerMaxWidthLg { get; set; }

    /// <summary>
    /// Container max width extra large. Default: 1140px
    /// </summary>
    public string? ContainerMaxWidthXl { get; set; }

    /// <summary>
    /// Container max width 2XL. Default: 1320px
    /// </summary>
    public string? ContainerMaxWidth2xl { get; set; }

    // Container Padding
    /// <summary>
    /// Container padding X. Default: 1rem
    /// </summary>
    public string? ContainerPaddingX { get; set; }

    /// <summary>
    /// Container padding Y. Default: 0
    /// </summary>
    public string? ContainerPaddingY { get; set; }

    public string GetSelector()
    {
        return ":root";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (GutterX.HasContent())
            yield return ("--bs-gutter-x", GutterX);
        if (GutterY.HasContent())
            yield return ("--bs-gutter-y", GutterY);
        if (Spacer.HasContent())
            yield return ("--bs-spacer", Spacer);
        if (Spacer0.HasContent())
            yield return ("--bs-spacer-0", Spacer0);
        if (Spacer1.HasContent())
            yield return ("--bs-spacer-1", Spacer1);
        if (Spacer2.HasContent())
            yield return ("--bs-spacer-2", Spacer2);
        if (Spacer3.HasContent())
            yield return ("--bs-spacer-3", Spacer3);
        if (Spacer4.HasContent())
            yield return ("--bs-spacer-4", Spacer4);
        if (Spacer5.HasContent())
            yield return ("--bs-spacer-5", Spacer5);
        if (GutterX0.HasContent())
            yield return ("--bs-gutter-x-0", GutterX0);
        if (GutterY0.HasContent())
            yield return ("--bs-gutter-y-0", GutterY0);
        if (GutterX1.HasContent())
            yield return ("--bs-gutter-x-1", GutterX1);
        if (GutterY1.HasContent())
            yield return ("--bs-gutter-y-1", GutterY1);
        if (GutterX2.HasContent())
            yield return ("--bs-gutter-x-2", GutterX2);
        if (GutterY2.HasContent())
            yield return ("--bs-gutter-y-2", GutterY2);
        if (GutterX3.HasContent())
            yield return ("--bs-gutter-x-3", GutterX3);
        if (GutterY3.HasContent())
            yield return ("--bs-gutter-y-3", GutterY3);
        if (GutterX4.HasContent())
            yield return ("--bs-gutter-x-4", GutterX4);
        if (GutterY4.HasContent())
            yield return ("--bs-gutter-y-4", GutterY4);
        if (GutterX5.HasContent())
            yield return ("--bs-gutter-x-5", GutterX5);
        if (GutterY5.HasContent())
            yield return ("--bs-gutter-y-5", GutterY5);
        if (ContainerMaxWidthSm.HasContent())
            yield return ("--bs-container-max-width-sm", ContainerMaxWidthSm);
        if (ContainerMaxWidthMd.HasContent())
            yield return ("--bs-container-max-width-md", ContainerMaxWidthMd);
        if (ContainerMaxWidthLg.HasContent())
            yield return ("--bs-container-max-width-lg", ContainerMaxWidthLg);
        if (ContainerMaxWidthXl.HasContent())
            yield return ("--bs-container-max-width-xl", ContainerMaxWidthXl);
        if (ContainerMaxWidth2xl.HasContent())
            yield return ("--bs-container-max-width-2xl", ContainerMaxWidth2xl);
        if (ContainerPaddingX.HasContent())
            yield return ("--bs-container-padding-x", ContainerPaddingX);
        if (ContainerPaddingY.HasContent())
            yield return ("--bs-container-padding-y", ContainerPaddingY);
    }
}
