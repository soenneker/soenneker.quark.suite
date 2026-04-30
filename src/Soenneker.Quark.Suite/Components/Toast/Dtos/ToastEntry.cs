using System;
using System.Threading.Tasks;

namespace Soenneker.Quark;

public sealed class ToastEntry
{
    public string Id { get; init; } = string.Empty;

    public string Title { get; init; } = string.Empty;

    public string? Description { get; init; }

    public ToastVariant Variant { get; init; } = ToastVariant.Default;

    public string? ActionLabel { get; init; }

    public string? ActionAltText { get; init; }

    public Func<ValueTask>? Action { get; init; }

    public int? Duration { get; init; }

    public bool Open { get; set; } = true;
}
