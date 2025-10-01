using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a step wizard component for multi-step processes.
/// </summary>
public interface ISteps : IElement
{
    /// <summary>
    /// Gets or sets the name of the currently selected step.
    /// </summary>
    string SelectedStep { get; set; }

    /// <summary>
    /// Gets or sets the render mode for step panels (default, lazy load, etc.).
    /// </summary>
    StepRenderMode RenderMode { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the selected step changes.
    /// </summary>
    EventCallback<string> SelectedStepChanged { get; set; }

    /// <summary>
    /// Gets or sets a function that determines whether navigation to a step is allowed.
    /// </summary>
    Func<StepNavigationContext, bool>? NavigationAllowed { get; set; }

    /// <summary>
    /// Gets or sets the template for rendering step items.
    /// </summary>
    RenderFragment? Items { get; set; }

    /// <summary>
    /// Gets or sets the template for rendering step content panels.
    /// </summary>
    RenderFragment? Content { get; set; }

    /// <summary>
    /// Selects a specific step by name.
    /// </summary>
    /// <param name="stepName">The name of the step to select.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SelectStep(string stepName);

    /// <summary>
    /// Navigates to the next step.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task NextStep();

    /// <summary>
    /// Navigates to the previous step.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task PreviousStep();

    /// <summary>
    /// Gets the index of a step by its name.
    /// </summary>
    /// <param name="name">The name of the step.</param>
    /// <returns>The index of the step (1-based).</returns>
    int IndexOfStep(string name);
}

