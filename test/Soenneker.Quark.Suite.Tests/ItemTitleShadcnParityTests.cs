using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void ItemTitle_matches_shadcn_base_classes()
    {
        var title = Render<ItemTitle>(parameters => parameters
            .Add(p => p.ChildContent, "Title"));

        var titleClasses = title.Find("[data-slot='item-title']").GetAttribute("class")!;

        titleClasses.Should().Contain("line-clamp-1");
        titleClasses.Should().Contain("flex");
        titleClasses.Should().Contain("w-fit");
        titleClasses.Should().Contain("items-center");
        titleClasses.Should().Contain("gap-2");
        titleClasses.Should().Contain("text-sm");
        titleClasses.Should().Contain("leading-snug");
        titleClasses.Should().Contain("font-medium");
        titleClasses.Should().Contain("underline-offset-4");
    }
}
