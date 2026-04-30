using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void AvatarFallback_matches_shadcn_base_classes()
    {
        var fallback = Render<AvatarFallback>(parameters => parameters
            .Add(p => p.ChildContent, "AB"));

        var fallbackClasses = fallback.Find("[data-slot='avatar-fallback']").GetAttribute("class")!;

        fallbackClasses.Should().Contain("size-full");
        fallbackClasses.Should().Contain("bg-muted");
        fallbackClasses.Should().Contain("text-muted-foreground");
        fallbackClasses.Should().Contain("flex");
        fallbackClasses.Should().Contain("rounded-full");
        fallbackClasses.Should().Contain("text-sm");
        fallbackClasses.Should().Contain("items-center");
        fallbackClasses.Should().Contain("justify-center");
        fallbackClasses.Should().Contain("group-data-[size=sm]/avatar:text-xs");
        fallbackClasses.Should().NotContain("absolute");
        fallbackClasses.Should().NotContain("inset-0");
        fallbackClasses.Should().NotContain("q-avatar-fallback");
    }
}
