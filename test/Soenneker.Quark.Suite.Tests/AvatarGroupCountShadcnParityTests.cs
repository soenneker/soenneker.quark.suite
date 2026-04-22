using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void AvatarGroupCount_matches_shadcn_base_classes()
    {
        var count = Render<AvatarGroupCount>(parameters => parameters
            .Add(p => p.ChildContent, "+3"));

        var countClasses = count.Find("[data-slot='avatar-group-count']").GetAttribute("class")!;

        countClasses.Should().Contain("relative");
        countClasses.Should().Contain("flex");
        countClasses.Should().Contain("size-8");
        countClasses.Should().Contain("rounded-full");
        countClasses.Should().Contain("bg-muted");
        countClasses.Should().Contain("text-muted-foreground");
        countClasses.Should().Contain("shrink-0");
        countClasses.Should().Contain("items-center");
        countClasses.Should().Contain("justify-center");
        countClasses.Should().Contain("ring-2");
        countClasses.Should().Contain("ring-background");
        countClasses.Should().Contain("group-has-data-[size=lg]/avatar-group:size-10");
        countClasses.Should().Contain("group-has-data-[size=sm]/avatar-group:size-6");
        countClasses.Should().NotContain("q-avatar-group-count");
    }
}
