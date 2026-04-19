using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Avatar_slots_match_shadcn_base_classes()
    {
        var avatar = Render<Avatar>(parameters => parameters
            .Add(p => p.ChildContent, "Avatar"));

        string avatarClasses = avatar.Find("[data-slot='avatar']").GetAttribute("class")!;

        avatarClasses.Should().Contain("group/avatar");
        avatarClasses.Should().Contain("relative");
        avatarClasses.Should().Contain("flex");
        avatarClasses.Should().Contain("size-8");
        avatarClasses.Should().Contain("shrink-0");
        avatarClasses.Should().Contain("rounded-full");
        avatarClasses.Should().Contain("select-none");
        avatarClasses.Should().Contain("after:border-border");
        avatarClasses.Should().Contain("data-[size=lg]:size-10");
        avatarClasses.Should().Contain("data-[size=sm]:size-6");
        avatarClasses.Should().NotContain("q-avatar");
    }
}

