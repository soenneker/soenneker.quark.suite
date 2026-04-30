using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void ComboboxList_matches_shadcn_default_component_contract()
    {
        var cut = Render<ComboboxList>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddContent(0, "Items"))));

        var classes = cut.Find("[data-slot='combobox-list']").GetAttribute("class")!;

        classes.Should().Contain("max-h-[min(calc(--spacing(96)---spacing(9)),calc(var(--available-height)---spacing(9)))]");
        classes.Should().Contain("scroll-py-1");
        classes.Should().Contain("overflow-y-auto");
        classes.Should().Contain("p-1");
        classes.Should().Contain("data-empty:p-0");
        classes.Should().NotContain("flex-col");
        classes.Should().NotContain("q-combobox-list");
    }
}
