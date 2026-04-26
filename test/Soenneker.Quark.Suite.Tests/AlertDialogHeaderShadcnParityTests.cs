using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void AlertDialogHeader_matches_shadcn_default_component_contract()
    {
        var cut = Render<AlertDialogHeader>(parameters => parameters
            .Add(p => p.ChildContent, "Header"));

        var classes = cut.Find("[data-slot='alert-dialog-header']").GetAttribute("class")!;

        classes.Should().Contain("grid");
        classes.Should().Contain("grid-rows-[auto_1fr]");
        classes.Should().Contain("place-items-center");
        classes.Should().Contain("gap-1.5");
        classes.Should().Contain("text-center");
        classes.Should().Contain("has-data-[slot=alert-dialog-media]:grid-rows-[auto_auto_1fr]");
        classes.Should().Contain("has-data-[slot=alert-dialog-media]:gap-x-4");
    }
}
