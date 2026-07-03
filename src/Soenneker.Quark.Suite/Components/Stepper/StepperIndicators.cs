using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Provides custom content for stepper indicator states.
/// </summary>
public sealed class StepperIndicators
{
    public RenderFragment? Active { get; set; }

    public RenderFragment? Completed { get; set; }

    public RenderFragment? Inactive { get; set; }

    public RenderFragment? Loading { get; set; }
}
