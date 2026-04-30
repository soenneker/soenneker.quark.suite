using System;
using System.Threading.Tasks;

namespace Soenneker.Quark;

public sealed class ToastOptions
{
    public string? Description { get; set; }

    public ToastVariant Variant { get; set; } = ToastVariant.Default;

    public string? ActionLabel { get; set; }

    public string? ActionAltText { get; set; }

    public Func<ValueTask>? Action { get; set; }

    public int? Duration { get; set; }
}
