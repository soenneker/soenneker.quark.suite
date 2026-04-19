using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void ComboboxLabel_matches_shadcn_default_component_contract()
    {
        var cut = Render<ComboboxLabel>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddContent(0, "Suggestions"))));

        string classes = cut.Find("[data-slot='combobox-label']").GetAttribute("class")!;

        classes.Should().Contain("px-2");
        classes.Should().Contain("py-1.5");
        classes.Should().Contain("text-xs");
        classes.Should().Contain("font-medium");
        classes.Should().Contain("text-muted-foreground");
        classes.Should().NotContain("q-combobox-label");
    }
}
