using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void FieldLabel_matches_shadcn_base_classes()
    {
        var cut = Render<FieldLabel>(parameters => parameters
            .Add(p => p.For, "email")
            .Add(p => p.ChildContent, "Email"));

        string classes = cut.Find("[data-slot='field-label']").GetAttribute("class")!;

        classes.Should().Contain("items-center");
        classes.Should().Contain("group/field-label");
        classes.Should().Contain("peer/field-label");
        classes.Should().Contain("has-data-checked:border-primary/30");
        classes.Should().Contain("has-[>[data-slot=field]]:rounded-lg");
        classes.Should().Contain("*:data-[slot=field]:p-2.5");
        classes.Should().NotContain("has-data-[state=checked]");
        classes.Should().NotContain("rounded-md");
        classes.Should().NotContain("p-4");
    }
}
