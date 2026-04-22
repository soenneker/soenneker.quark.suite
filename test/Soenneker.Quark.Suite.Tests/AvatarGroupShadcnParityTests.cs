using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void AvatarGroup_matches_shadcn_base_classes()
    {
        var group = Render<AvatarGroup>(parameters => parameters
            .Add(p => p.ChildContent, "Group"));

        var groupClasses = group.Find("[data-slot='avatar-group']").GetAttribute("class")!;

        groupClasses.Should().Contain("group/avatar-group");
        groupClasses.Should().Contain("flex");
        groupClasses.Should().Contain("-space-x-2");
        groupClasses.Should().Contain("*:data-[slot=avatar]:ring-2");
        groupClasses.Should().Contain("*:data-[slot=avatar]:ring-background");
        groupClasses.Should().NotContain("q-avatar-group");
    }
}
