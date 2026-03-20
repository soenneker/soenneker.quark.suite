using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

public sealed class SonnerToast
{
    public string ToasterId { get; set; } = string.Empty;

    public string Id { get; set; } = Guid.NewGuid().ToString("N");

    public string? Title { get; set; }

    public string? Description { get; set; }

    public RenderFragment? Content { get; set; }

    public SonnerToastType Type { get; set; } = SonnerToastType.Default;

    public SonnerPosition Position { get; set; } = SonnerPosition.TopCenter;

    public int Duration { get; set; } = 4000;

    public bool Dismissible { get; set; } = true;

    public bool CloseButton { get; set; }

    public string? ActionLabel { get; set; }

    public Func<ValueTask>? Action { get; set; }

    public Func<ValueTask>? OnDismiss { get; set; }

    public Func<ValueTask>? OnAutoClose { get; set; }

    public bool Mounted { get; set; } = true;

    public bool Removed { get; set; }

    public bool Promise { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
