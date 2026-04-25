using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Switch_matches_shadcn_default_component_contract()
    {
        var cut = Render<Switch>();

        var root = cut.Find("[data-slot='switch']");
        var thumb = cut.Find("[data-slot='switch-thumb']");
        var rootClasses = root.GetAttribute("class")!;
        var thumbClasses = thumb.GetAttribute("class")!;

        cut.Markup.TrimStart().Should().StartWith("<button");

        rootClasses.Should().Contain("peer");
        rootClasses.Should().Contain("group/switch");
        rootClasses.Should().Contain("inline-flex");
        rootClasses.Should().Contain("shrink-0");
        rootClasses.Should().Contain("items-center");
        rootClasses.Should().Contain("rounded-full");
        rootClasses.Should().Contain("border-transparent");
        rootClasses.Should().Contain("transition-all");
        rootClasses.Should().Contain("after:absolute");
        rootClasses.Should().Contain("after:-inset-x-3");
        rootClasses.Should().Contain("after:-inset-y-2");
        rootClasses.Should().Contain("focus-visible:border-ring");
        rootClasses.Should().Contain("focus-visible:ring-3");
        rootClasses.Should().Contain("disabled:cursor-not-allowed");
        rootClasses.Should().Contain("data-[size=default]:h-[18.4px]");
        rootClasses.Should().Contain("data-[size=default]:w-[32px]");
        rootClasses.Should().Contain("data-[size=sm]:h-[14px]");
        rootClasses.Should().Contain("data-[size=sm]:w-[24px]");
        rootClasses.Should().Contain("data-checked:bg-primary");
        rootClasses.Should().Contain("data-unchecked:bg-input");
        rootClasses.Should().Contain("data-[state=checked]:bg-primary");
        rootClasses.Should().Contain("data-[state=unchecked]:bg-input");
        rootClasses.Should().NotContain("shadow-xs");
        rootClasses.Should().NotContain("focus-visible:ring-[3px]");
        rootClasses.Should().NotContain("data-[size=default]:h-[1.15rem]");
        rootClasses.Should().NotContain("data-[size=default]:w-8");

        thumbClasses.Should().Contain("group-data-[size=default]/switch:size-4");
        thumbClasses.Should().Contain("group-data-[size=sm]/switch:size-3");
        thumbClasses.Should().Contain("group-data-[size=default]/switch:data-checked:translate-x-[calc(100%-2px)]");
        thumbClasses.Should().Contain("group-data-[size=sm]/switch:data-checked:translate-x-[calc(100%-2px)]");
        thumbClasses.Should().Contain("data-[state=checked]:translate-x-[calc(100%-2px)]");
        thumbClasses.Should().Contain("data-[state=unchecked]:translate-x-0");
    }
}
