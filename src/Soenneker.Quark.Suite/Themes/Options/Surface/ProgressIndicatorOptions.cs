namespace Soenneker.Quark;

/// <summary>
/// Represents the progress indicator options.
/// </summary>
public sealed class ProgressIndicatorOptions : ComponentOptions
{
    public ProgressIndicatorOptions()
    {
        Selector = "[data-slot='progress-indicator']";
    }
}
