using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's spinner CSS variables
/// </summary>
public sealed class BootstrapSpinnerCssVariables : IBootstrapCssVariableGroup
{
    /// <summary>
    /// Spinner width. Default: 2rem
    /// </summary>
    public string? SpinnerWidth { get; set; }

    /// <summary>
    /// Spinner height. Default: 2rem
    /// </summary>
    public string? SpinnerHeight { get; set; }

    /// <summary>
    /// Spinner vertical align. Default: -0.125em
    /// </summary>
    public string? SpinnerVerticalAlign { get; set; }

    /// <summary>
    /// Spinner border width. Default: 0.25em
    /// </summary>
    public string? SpinnerBorderWidth { get; set; }

    /// <summary>
    /// Spinner animation speed. Default: 0.75s
    /// </summary>
    public string? SpinnerAnimationSpeed { get; set; }

    /// <summary>
    /// Spinner animation name. Default: spinner-border
    /// </summary>
    public string? SpinnerAnimationName { get; set; }

	/// <summary>
	/// Gets the CSS selector for the spinner component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ":root";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the spinner component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (SpinnerWidth.HasContent())
            yield return ("--bs-spinner-width", SpinnerWidth);
        if (SpinnerHeight.HasContent())
            yield return ("--bs-spinner-height", SpinnerHeight);
        if (SpinnerVerticalAlign.HasContent())
            yield return ("--bs-spinner-vertical-align", SpinnerVerticalAlign);
        if (SpinnerBorderWidth.HasContent())
            yield return ("--bs-spinner-border-width", SpinnerBorderWidth);
        if (SpinnerAnimationSpeed.HasContent())
            yield return ("--bs-spinner-animation-speed", SpinnerAnimationSpeed);
        if (SpinnerAnimationName.HasContent())
            yield return ("--bs-spinner-animation-name", SpinnerAnimationName);
    }
}
