
using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Represents the progress options.
/// </summary>
public sealed class ProgressOptions : ComponentOptions
{
    public ProgressOptions()
    {
        Selector = "[data-slot='progress']";
    }

    /// <summary>
    /// Gets or sets progress indicator styling scoped to progress.
    /// </summary>
    public ProgressIndicatorOptions? Indicators { get; set; }

    private protected override void CollectChildCssRules(List<ComponentCssRule> buffer, string baseSelector)
    {
        AddChildCssRules(buffer, Indicators, "[data-slot='progress-indicator']", "[data-slot='progress-indicator']", baseSelector);
    }
}
