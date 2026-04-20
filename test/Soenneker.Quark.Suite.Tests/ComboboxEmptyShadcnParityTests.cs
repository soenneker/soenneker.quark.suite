using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void ComboboxEmpty_matches_shadcn_default_component_contract()
    {
        var cut = Render<ComboboxEmpty>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddContent(0, "No results found."))));

        string classes = cut.Find("[data-slot='combobox-empty']").GetAttribute("class")!;

        classes.Should().Contain("py-6");
        classes.Should().Contain("text-center");
        classes.Should().Contain("text-sm");
        classes.Should().NotContain("q-combobox-empty");
    }
}
