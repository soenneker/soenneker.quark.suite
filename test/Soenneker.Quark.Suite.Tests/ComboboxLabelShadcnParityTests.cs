using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void ComboboxLabel_matches_shadcn_default_component_contract()
    {
        var cut = Render<ComboboxLabel>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddContent(0, "Suggestions"))));

        var classes = cut.Find("[data-slot='combobox-label']").GetAttribute("class")!;

        classes.Should().Contain("px-2");
        classes.Should().Contain("py-1.5");
        classes.Should().Contain("text-xs");
        classes.Should().Contain("text-muted-foreground");
        classes.Should().Contain("pointer-coarse:px-3");
        classes.Should().Contain("pointer-coarse:py-2");
        classes.Should().Contain("pointer-coarse:text-sm");
        classes.Should().NotContain("font-medium");
        classes.Should().NotContain("q-combobox-label");
    }
}
