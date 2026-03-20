using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

public sealed class SonnerToastOptions
{
    public string? ToasterId { get; set; }

    public string? Id { get; set; }

    public string? Description { get; set; }

    public RenderFragment? Content { get; set; }

    public SonnerPosition? Position { get; set; }

    public int? Duration { get; set; }

    public bool? CloseButton { get; set; }

    public bool Dismissible { get; set; } = true;

    public string? ActionLabel { get; set; }

    public Func<ValueTask>? Action { get; set; }

    public Func<ValueTask>? OnDismiss { get; set; }

    public Func<ValueTask>? OnAutoClose { get; set; }
}
