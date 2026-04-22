using System;

namespace Soenneker.Quark;

internal sealed class SelectContext
{
    public required Func<string> Size { get; init; }
    public required Func<bool> IsInvalid { get; init; }
    public required Func<bool> FillFieldWidth { get; init; }
    public required Func<string> EffectiveTriggerId { get; init; }
    public required Func<string?> AriaDescribedBy { get; init; }
    public required Func<string?> DefaultItemText { get; init; }
}
