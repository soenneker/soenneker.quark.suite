using System;
using System.Threading.Tasks;

namespace Soenneker.Quark;

internal sealed class DropdownRadioGroupContext
{
    public string? Value { get; init; }

    public Func<string, Task>? SetValue { get; init; }
}
