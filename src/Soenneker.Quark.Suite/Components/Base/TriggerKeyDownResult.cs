namespace Soenneker.Quark;

/// <summary>
/// Returned from <see cref="TriggerContext.OnKeyDown"/> so slot-based triggers (e.g. <see cref="Button"/> AsChild)
/// can apply <c>preventDefault</c> / <c>stopPropagation</c> for the same keydown event.
/// Prefer completing synchronously with Task.FromResult so Blazor reads flags in time.
/// </summary>
internal readonly struct TriggerKeyDownResult
{
    public bool PreventDefault { get; init; }

    public bool StopPropagation { get; init; }
}
