using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void AvatarFallback_matches_shadcn_base_classes()
    {
        var fallback = Render<AvatarFallback>(parameters => parameters
            .Add(p => p.ChildContent, "AB"));

        var fallbackClasses = fallback.Find("[data-slot='avatar-fallback']").GetAttribute("class")!;

        fallbackClasses.Should().Contain("absolute");
        fallbackClasses.Should().Contain("inset-0");
        fallbackClasses.Should().Contain("bg-muted");
        fallbackClasses.Should().Contain("text-muted-foreground");
        fallbackClasses.Should().Contain("flex");
        fallbackClasses.Should().Contain("rounded-full");
        fallbackClasses.Should().Contain("items-center");
        fallbackClasses.Should().Contain("justify-center");
        fallbackClasses.Should().NotContain("q-avatar-fallback");
    }
}
