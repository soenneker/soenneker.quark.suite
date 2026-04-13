using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

internal sealed class SelectContext
{
    public required Func<string> Size { get; init; }
    public required Func<bool> IsInvalid { get; init; }
    public required Func<string> EffectiveTriggerId { get; init; }
    public required Func<string?> AriaDescribedBy { get; init; }
    public required Func<string?> DefaultItemText { get; init; }
}
