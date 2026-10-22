using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>Optional persistence and feedback for an editable control.</summary>
public interface IAutoSaveInput<TValue>
{
    /// <summary>Enables saving committed edits automatically. Defaults to false.</summary>
    bool AutoSave { get; set; }

    /// <summary>Debounce delay in milliseconds before saving an edit. Defaults to 750.</summary>
    int AutoSaveDelay { get; set; }

    /// <summary>Shows saving, saved, and failed feedback when autosave is configured.</summary>
    bool ShowAutoSaveStatus { get; set; }

    /// <summary>Persists the edited value. Throw on failure to display failed status; honor cancellation.</summary>
    Func<TValue, CancellationToken, ValueTask>? OnAutoSave { get; set; }

    /// <summary>Raised when the autosave state changes.</summary>
    EventCallback<AutoSaveState> AutoSaveStateChanged { get; set; }

    /// <summary>Additional classes for the autosave status.</summary>
    string? AutoSaveStatusClass { get; set; }

    /// <summary>Additional classes for the saving state.</summary>
    string? AutoSaveSavingClass { get; set; }

    /// <summary>Additional classes for the saved state.</summary>
    string? AutoSaveSavedClass { get; set; }

    /// <summary>Additional classes for the failed state.</summary>
    string? AutoSaveFailedClass { get; set; }

}
