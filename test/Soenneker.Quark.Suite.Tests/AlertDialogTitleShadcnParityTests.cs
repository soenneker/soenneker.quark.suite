using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void AlertDialogTitle_matches_shadcn_default_component_contract()
    {
        var cut = Render<AlertDialogTitle>(parameters => parameters
            .Add(p => p.ChildContent, "Delete item"));

        var classes = cut.Find("[data-slot='alert-dialog-title']").GetAttribute("class")!;

        classes.Should().Contain("text-lg");
        classes.Should().Contain("font-semibold");
        classes.Should().Contain("sm:group-data-[size=default]/alert-dialog-content:group-has-data-[slot=alert-dialog-media]/alert-dialog-content:col-start-2");
        classes.Should().NotContain("cn-font-heading");
    }
}
