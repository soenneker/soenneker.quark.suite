using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void AlertDialogFooter_matches_shadcn_default_component_contract()
    {
        var cut = Render<AlertDialogFooter>(parameters => parameters
            .Add(p => p.ChildContent, "Footer"));

        var classes = cut.Find("[data-slot='alert-dialog-footer']").GetAttribute("class")!;

        classes.Should().Contain("-mx-4");
        classes.Should().Contain("-mb-4");
        classes.Should().Contain("flex");
        classes.Should().Contain("flex-col-reverse");
        classes.Should().Contain("gap-2");
        classes.Should().Contain("rounded-b-xl");
        classes.Should().Contain("border-t");
        classes.Should().Contain("bg-muted/50");
        classes.Should().Contain("p-4");
        classes.Should().Contain("group-data-[size=sm]/alert-dialog-content:grid");
        classes.Should().Contain("group-data-[size=sm]/alert-dialog-content:grid-cols-2");
        classes.Should().Contain("sm:flex-row");
        classes.Should().Contain("sm:justify-end");
    }
}
