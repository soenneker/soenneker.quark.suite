using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void ComboboxGroup_matches_shadcn_default_component_contract()
    {
        var cut = Render<ComboboxGroup>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddContent(0, "Items"))));

        var classes = cut.Find("[data-slot='combobox-group']").GetAttribute("class")!;

        classes.Should().Contain("flex");
        classes.Should().Contain("flex-col");
        classes.Should().NotContain("q-combobox-group");
    }
}
