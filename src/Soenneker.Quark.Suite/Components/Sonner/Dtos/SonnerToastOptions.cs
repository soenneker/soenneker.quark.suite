using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents the sonner toast options.
/// </summary>
public sealed class SonnerToastOptions
{
    /// <summary>
    /// Gets or sets toaster id.
    /// </summary>
    public string? ToasterId { get; set; }

    /// <summary>
    /// Gets or sets id.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets content.
    /// </summary>
    public RenderFragment? Content { get; set; }

    /// <summary>
    /// Gets or sets position.
    /// </summary>
    public SonnerPosition? Position { get; set; }

    /// <summary>
    /// Gets or sets duration.
    /// </summary>
    public int? Duration { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether close button.
    /// </summary>
    public bool? CloseButton { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether dismissible.
    /// </summary>
    public bool Dismissible { get; set; } = true;

    /// <summary>
    /// Gets or sets action label.
    /// </summary>
    public string? ActionLabel { get; set; }

    /// <summary>
    /// Gets or sets action.
    /// </summary>
    public Func<ValueTask>? Action { get; set; }

    /// <summary>
    /// Gets or sets on dismiss.
    /// </summary>
    public Func<ValueTask>? OnDismiss { get; set; }

    /// <summary>
    /// Gets or sets on auto close.
    /// </summary>
    public Func<ValueTask>? OnAutoClose { get; set; }
}
