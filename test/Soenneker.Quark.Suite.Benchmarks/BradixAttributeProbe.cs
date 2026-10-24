using Soenneker.Bradix;

namespace Soenneker.Quark.Suite.Benchmarks;

public sealed partial class BradixAttributeProbe : BradixElement
{
    public object BuildArray() => BuildAttributes(new (string Key, object? Value)[]
    {
        ("role", "option"), ("aria-labelledby", "label"), ("aria-selected", "false"),
        ("aria-disabled", null), ("data-value", "value"), ("data-state", "unchecked"),
        ("data-highlighted", null), ("data-disabled", null), ("tabindex", "-1")
    });

    public object BuildSpan() => BuildAttributes(
        ("role", "option"), ("aria-labelledby", "label"), ("aria-selected", "false"),
        ("aria-disabled", null), ("data-value", "value"), ("data-state", "unchecked"),
        ("data-highlighted", null), ("data-disabled", null), ("tabindex", "-1"));
}
