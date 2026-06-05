using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Soenneker.Blazor.Utils.Ids;

namespace Soenneker.Quark;

/// <summary>
/// Represents the sonner toast.
/// </summary>
public sealed class SonnerToast
{
    /// <summary>
    /// Gets or sets toaster id.
    /// </summary>
    public string ToasterId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets id.
    /// </summary>
    public string Id { get; set; } = BlazorIdGenerator.New("quark-sonner-toast");

    /// <summary>
    /// Gets or sets title.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets content.
    /// </summary>
    public RenderFragment? Content { get; set; }

    /// <summary>
    /// Gets or sets type.
    /// </summary>
    public SonnerToastType Type { get; set; } = SonnerToastType.Default;

    /// <summary>
    /// Gets or sets position.
    /// </summary>
    public SonnerPosition Position { get; set; } = SonnerPosition.TopCenter;

    /// <summary>
    /// Gets or sets duration.
    /// </summary>
    public int Duration { get; set; } = 4000;

    /// <summary>
    /// Gets or sets a value indicating whether dismissible.
    /// </summary>
    public bool Dismissible { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether close button.
    /// </summary>
    public bool CloseButton { get; set; }

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

    /// <summary>
    /// Gets or sets a value indicating whether mounted.
    /// </summary>
    public bool Mounted { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether removed.
    /// </summary>
    public bool Removed { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether promise.
    /// </summary>
    public bool Promise { get; set; }

    /// <summary>
    /// Gets or sets created at.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
