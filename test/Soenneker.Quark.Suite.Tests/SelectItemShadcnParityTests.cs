using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void SelectItem_matches_shadcn_base_classes()
    {
        var item = Render<SelectItem<string>>(parameters => parameters
            .Add(p => p.ItemValue, "apple")
            .Add(p => p.Text, "Apple"));

        string itemClasses = item.Find("[data-slot='select-item']").GetAttribute("class")!;

        itemClasses.Should().Contain("relative");
        itemClasses.Should().Contain("flex");
        itemClasses.Should().Contain("w-full");
        itemClasses.Should().Contain("cursor-default");
        itemClasses.Should().Contain("items-center");
        itemClasses.Should().Contain("gap-2");
        itemClasses.Should().Contain("rounded-sm");
        itemClasses.Should().Contain("py-1.5");
        itemClasses.Should().Contain("pr-8");
        itemClasses.Should().Contain("pl-2");
        itemClasses.Should().Contain("text-sm");
        itemClasses.Should().Contain("outline-hidden");
        itemClasses.Should().Contain("select-none");
        itemClasses.Should().Contain("data-[highlighted]:bg-accent");
        itemClasses.Should().Contain("data-[disabled]:pointer-events-none");
        itemClasses.Should().Contain("*:[span]:last:flex");
        itemClasses.Should().NotContain("q-select-item");
    }
}
