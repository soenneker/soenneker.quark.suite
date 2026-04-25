using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void ComboboxEmpty_matches_shadcn_default_component_contract()
    {
        var cut = Render<ComboboxEmpty>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddContent(0, "No results found."))));

        var classes = cut.Find("[data-slot='combobox-empty']").GetAttribute("class")!;

        classes.Should().Contain("w-full");
        classes.Should().Contain("justify-center");
        classes.Should().Contain("py-2");
        classes.Should().Contain("text-center");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("text-muted-foreground");
        classes.Should().NotContain("q-combobox-empty");
    }
}
