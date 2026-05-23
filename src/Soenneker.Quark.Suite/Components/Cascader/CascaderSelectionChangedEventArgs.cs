using System.Collections.Generic;

namespace Soenneker.Quark;

public sealed class CascaderSelectionChangedEventArgs
{
    public required string[] Value { get; init; }

    public required IReadOnlyList<CascaderOption> SelectedOptions { get; init; }
}
