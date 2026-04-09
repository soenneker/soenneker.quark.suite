using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark;

/// <summary>
/// Shared keyboard dismissal for popover/dialog-style triggers (Escape / Enter / Space while open).
/// Uses synchronously completed tasks so Button AsChild can apply preventDefault in time.
/// </summary>
internal static class OverlayTriggerKeyDown
{
    /// <summary>Escape-only dismissal (e.g. collapsible disclosure, nested layers).</summary>
    public static Task<TriggerKeyDownResult> HandleEscapeCloseWhenOpen(KeyboardEventArgs e, bool isOpen, Func<Task> closeAsync)
    {
        if (!isOpen || e.Key != "Escape")
            return Task.FromResult(default(TriggerKeyDownResult));

        var result = new TriggerKeyDownResult
        {
            PreventDefault = true,
            StopPropagation = true
        };

        _ = closeAsync();
        return Task.FromResult(result);
    }

    public static Task<TriggerKeyDownResult> HandleDismissWhenOpen(KeyboardEventArgs e, bool isOpen, Func<Task> dismiss)
    {
        if (!isOpen)
            return Task.FromResult(default(TriggerKeyDownResult));

        if (e.Key is not ("Escape" or "Enter" or " "))
            return Task.FromResult(default(TriggerKeyDownResult));

        var result = new TriggerKeyDownResult
        {
            PreventDefault = true,
            StopPropagation = e.Key == "Escape"
        };

        _ = dismiss();
        return Task.FromResult(result);
    }
}
