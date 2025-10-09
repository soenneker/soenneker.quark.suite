
namespace Soenneker.Quark;

/// <summary>
/// Legacy state for older Quark Steps content; kept for compatibility within this repo until removed.
/// </summary>
public sealed record StepContentState
{
    public string SelectedPanel { get; init; } = string.Empty;
}
