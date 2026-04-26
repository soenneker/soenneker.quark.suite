using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void AlertDialogMedia_matches_shadcn_default_component_contract()
    {
        var cut = Render<AlertDialogMedia>(parameters => parameters
            .Add(p => p.ChildContent, "Icon"));

        var classes = cut.Find("[data-slot='alert-dialog-media']").GetAttribute("class")!;

        classes.Should().Contain("mb-2");
        classes.Should().Contain("inline-flex");
        classes.Should().Contain("size-10");
        classes.Should().Contain("items-center");
        classes.Should().Contain("justify-center");
        classes.Should().Contain("rounded-md");
        classes.Should().Contain("bg-muted");
        classes.Should().Contain("sm:group-data-[size=default]/alert-dialog-content:row-span-2");
        classes.Should().Contain("*:[svg:not([class*='size-'])]:size-6");
    }
}
