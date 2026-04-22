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

        classes.Should().Contain("flex");
        classes.Should().Contain("flex-col");
        classes.Should().NotContain("q-combobox-list");
    }
}
