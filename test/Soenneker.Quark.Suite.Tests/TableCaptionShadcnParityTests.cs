using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void TableCaption_matches_shadcn_base_classes()
    {
        var caption = Render<TableCaption>(parameters => parameters.Add(p => p.ChildContent, "Caption"));

        var captionClasses = caption.Find("[data-slot='table-caption']").GetAttribute("class")!;

        captionClasses.Should().Contain("mt-4");
        captionClasses.Should().Contain("text-sm");
        captionClasses.Should().Contain("text-muted-foreground");
        captionClasses.Should().NotContain("q-table-caption");
    }
}
