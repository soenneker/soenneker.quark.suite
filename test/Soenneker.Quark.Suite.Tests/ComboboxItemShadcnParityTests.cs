using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void ComboboxItem_matches_shadcn_default_component_contract()
    {
        var cut = Render<ComboboxItem>(parameters => parameters
            .Add(p => p.Value, "calendar")
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddContent(0, "Calendar"))));

        var classes = cut.Find("[data-slot='combobox-item']").GetAttribute("class")!;

        classes.Should().Contain("relative");
        classes.Should().Contain("flex");
        classes.Should().Contain("cursor-default");
        classes.Should().Contain("select-none");
        classes.Should().Contain("items-center");
        classes.Should().Contain("gap-2");
        classes.Should().Contain("rounded-sm");
        classes.Should().Contain("pl-2");
        classes.Should().Contain("pr-8");
        classes.Should().Contain("py-1.5");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("outline-hidden");
        classes.Should().Contain("data-[highlighted]:bg-accent");
        classes.Should().Contain("data-[disabled]:pointer-events-none");
        classes.Should().Contain("[&_svg:not([class*='size-'])]:size-4");
        classes.Should().NotContain("hover:bg-accent");
        classes.Should().NotContain("q-combobox-item");
    }
}
