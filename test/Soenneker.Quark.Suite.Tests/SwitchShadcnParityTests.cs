using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Switch_matches_shadcn_default_component_contract()
    {
        var cut = Render<Switch>();

        var root = cut.Find("[data-slot='switch']");
        var thumb = cut.Find("[data-slot='switch-thumb']");
        string rootClasses = root.GetAttribute("class")!;
        string thumbClasses = thumb.GetAttribute("class")!;

        cut.Markup.TrimStart().Should().StartWith("<button");

        rootClasses.Should().Contain("peer");
        rootClasses.Should().Contain("group/switch");
        rootClasses.Should().Contain("relative");
        rootClasses.Should().Contain("after:-inset-x-3");
        rootClasses.Should().Contain("after:-inset-y-2");
        rootClasses.Should().Contain("data-[size=default]:h-[18.4px]");
        rootClasses.Should().Contain("data-[size=default]:w-[32px]");
        rootClasses.Should().Contain("data-[size=sm]:h-[14px]");
        rootClasses.Should().Contain("data-[size=sm]:w-[24px]");
        rootClasses.Should().Contain("data-checked:bg-primary");
        rootClasses.Should().Contain("data-unchecked:bg-input");
        rootClasses.Should().Contain("data-disabled:cursor-not-allowed");
        rootClasses.Should().NotContain("shadow-xs");

        thumbClasses.Should().Contain("group-data-[size=default]/switch:size-4");
        thumbClasses.Should().Contain("group-data-[size=sm]/switch:size-3");
        thumbClasses.Should().Contain("group-data-[size=default]/switch:data-checked:translate-x-[calc(100%-2px)]");
        thumbClasses.Should().Contain("group-data-[size=sm]/switch:data-checked:translate-x-[calc(100%-2px)]");
        thumbClasses.Should().Contain("group-data-[size=default]/switch:data-unchecked:translate-x-0");
        thumbClasses.Should().Contain("group-data-[size=sm]/switch:data-unchecked:translate-x-0");
        thumbClasses.Should().NotContain("data-[state=checked]:translate-x");
    }
}
