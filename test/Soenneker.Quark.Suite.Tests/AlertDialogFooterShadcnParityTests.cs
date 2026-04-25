using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void AlertDialogFooter_matches_shadcn_default_component_contract()
    {
        var cut = Render<AlertDialogFooter>(parameters => parameters
            .Add(p => p.ChildContent, "Footer"));

        var classes = cut.Find("[data-slot='alert-dialog-footer']").GetAttribute("class")!;

        classes.Should().Contain("flex");
        classes.Should().Contain("flex-col-reverse");
        classes.Should().Contain("gap-2");
        classes.Should().Contain("group-data-[size=sm]/alert-dialog-content:grid");
        classes.Should().Contain("group-data-[size=sm]/alert-dialog-content:grid-cols-2");
        classes.Should().Contain("sm:flex-row");
        classes.Should().Contain("sm:justify-end");
        classes.Should().NotContain("-mx-4");
        classes.Should().NotContain("rounded-b-xl");
    }
}
