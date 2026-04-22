using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void DialogFooter_matches_shadcn_default_component_contract()
    {
        var footer = Render<DialogFooter>(parameters => parameters
            .Add(p => p.ChildContent, "Footer"));

        var footerClasses = footer.Find("[data-slot='dialog-footer']").GetAttribute("class")!;

        footerClasses.Should().Contain("-mx-4");
        footerClasses.Should().Contain("-mb-4");
        footerClasses.Should().Contain("flex");
        footerClasses.Should().Contain("flex-col-reverse");
        footerClasses.Should().Contain("gap-2");
        footerClasses.Should().Contain("rounded-b-xl");
        footerClasses.Should().Contain("border-t");
        footerClasses.Should().Contain("bg-muted/50");
        footerClasses.Should().Contain("p-4");
        footerClasses.Should().Contain("sm:flex-row");
        footerClasses.Should().Contain("sm:justify-end");
    }
}
