
namespace Soenneker.Quark;

/// <summary>
/// Legacy state for older Quark Steps; kept for compatibility within this repo until removed.
/// </summary>
public sealed record StepState
{
    public string SelectedStep { get; init; } = string.Empty;
    public StepRenderMode RenderMode { get; init; } = StepRenderMode.Default;
}
