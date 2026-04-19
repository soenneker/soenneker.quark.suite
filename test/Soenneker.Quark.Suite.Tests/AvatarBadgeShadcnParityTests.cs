using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void AvatarBadge_matches_shadcn_base_classes()
    {
        var badge = Render<AvatarBadge>(parameters => parameters
            .Add(p => p.ChildContent, string.Empty));

        string badgeClasses = badge.Find("[data-slot='avatar-badge']").GetAttribute("class")!;

        badgeClasses.Should().Contain("absolute");
        badgeClasses.Should().Contain("right-0");
        badgeClasses.Should().Contain("bottom-0");
        badgeClasses.Should().Contain("inline-flex");
        badgeClasses.Should().Contain("items-center");
        badgeClasses.Should().Contain("justify-center");
        badgeClasses.Should().Contain("rounded-full");
        badgeClasses.Should().Contain("text-primary-foreground");
        badgeClasses.Should().Contain("bg-blend-color");
        badgeClasses.Should().Contain("ring-2");
        badgeClasses.Should().Contain("ring-background");
        badgeClasses.Should().Contain("group-data-[size=default]/avatar:size-2.5");
        badgeClasses.Should().NotContain("q-avatar-badge");
    }
}
