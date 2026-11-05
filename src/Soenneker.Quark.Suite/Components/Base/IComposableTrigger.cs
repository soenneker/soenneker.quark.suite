using Microsoft.AspNetCore.Components;
using Soenneker.Bradix;

namespace Soenneker.Quark;

/// <summary>Composition and cancelable activation shared by Quark disclosure triggers.</summary>
public interface IComposableTrigger
{
    /// <summary>Applies trigger behavior to a child Quark control without rendering another button.</summary>
    bool AsChild { get; set; }

    /// <summary>Replaces the default button when AsChild is false. Forward the context's attributes, click callback, and DOM element reference.</summary>
    RenderFragment<BradixTriggerContext>? TriggerTemplate { get; set; }

    /// <summary>Runs after the trigger's OnClick callback. Call PreventDefault to cancel opening or closing, including keyboard activation.</summary>
    EventCallback<BradixActivationEventArgs> OnActivate { get; set; }
}
