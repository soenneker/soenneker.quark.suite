using AwesomeAssertions;
using Bunit;
using System;
using System.IO;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Avatar_slots_match_shadcn_base_classes()
    {
        var avatar = Render<Avatar>(parameters => parameters
            .Add(p => p.ChildContent, "Avatar"));

        var avatarClasses = avatar.Find("[data-slot='avatar']").GetAttribute("class")!;

        avatarClasses.Should().Contain("group/avatar");
        avatarClasses.Should().Contain("relative");
        avatarClasses.Should().Contain("flex");
        avatarClasses.Should().Contain("size-8");
        avatarClasses.Should().Contain("shrink-0");
        avatarClasses.Should().Contain("overflow-hidden");
        avatarClasses.Should().Contain("rounded-full");
        avatarClasses.Should().Contain("select-none");
        avatarClasses.Should().Contain("data-[size=lg]:size-10");
        avatarClasses.Should().Contain("data-[size=sm]:size-6");
        avatarClasses.Should().NotContain("after:border-border");
        avatarClasses.Should().NotContain("after:mix-blend-darken");
        avatarClasses.Should().NotContain("q-avatar");
    }

    [Test]
    public void Avatar_demo_examples_match_current_shadcn_docs_examples()
    {
        var source = File.ReadAllText(Path.Combine(GetSuiteRootForAvatar(), "test", "Soenneker.Quark.Suite.Demo", "Pages", "Components", "Avatars.razor"));

        source.Should().Contain("A basic avatar component with an image and a fallback.");
        source.Should().Contain("Use the AvatarBadge component to add a badge to the avatar.");
        source.Should().Contain("Use the size prop to change the size of the avatar.");
        source.Should().Contain("private const string BasicAvatarCode");
        source.Should().Contain("<AvatarFallback>CN</AvatarFallback>");
        source.Should().Contain("https://github.com/shadcn.png");
        source.Should().Contain("https://github.com/maxleiter.png");
        source.Should().Contain("https://github.com/evilrabbit.png");
        source.Should().Contain("<Avatar Size=\"sm\">");
        source.Should().Contain("<Avatar Size=\"lg\">");
    }

    private static string GetSuiteRootForAvatar()
    {
        var directory = AppContext.BaseDirectory;

        while (!File.Exists(Path.Combine(directory, "Soenneker.Quark.Suite.slnx")))
            directory = Directory.GetParent(directory)!.FullName;

        return directory;
    }
}

