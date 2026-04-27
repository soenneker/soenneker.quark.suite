using AwesomeAssertions;
using Bunit;
using System;
using System.IO;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void MenubarSub_matches_shadcn_base_classes()
    {
        var sub = Render<MenubarSub>(parameters => parameters.Add(p => p.ChildContent, "Sub"));

        var subClasses = sub.Find("[data-slot='menubar-sub']").GetAttribute("class")!;

        subClasses.Should().Contain("relative");
        subClasses.Should().NotContain("q-menubar-sub");
    }

    [Test]
    public void MenubarSubTrigger_and_SubContent_match_shadcn_v4_classes()
    {
        var trigger = ReadMenubarSubSource("MenubarSubTrigger.razor");
        var content = ReadMenubarSubSource("MenubarSubContent.razor");

        trigger.Should().Contain("Rounded.Sm");
        trigger.Should().Contain("data-[inset]:pl-8");
        trigger.Should().Contain("data-[state=open]:bg-accent");
        trigger.Should().Contain("Class=\"ml-auto h-4 w-4\"");
        trigger.Should().NotContain("cn-rtl-flip");
        trigger.Should().NotContain("data-open:");
        trigger.Should().NotContain("data-[inset]:pl-7");

        content.Should().Contain("ForceMount");
        content.Should().Contain("CollisionBoundarySelector=\"@CollisionBoundarySelector\"");
        content.Should().Contain("Sticky=\"@Sticky\"");
        content.Should().Contain("Border.Default");
        content.Should().Contain("MinWidth ??= Quark.MinWidth.Token(\"[8rem]\")");
        content.Should().Contain("data-[state=closed]:animate-out");
        content.Should().Contain("data-[state=open]:animate-in");
        content.Should().NotContain("ring-1");
        content.Should().NotContain("duration-100");
        content.Should().NotContain("data-open:");
        content.Should().NotContain("data-closed:");
    }

    private static string ReadMenubarSubSource(string fileName)
    {
        return File.ReadAllText(Path.Combine(GetSuiteRootForMenubarSub(), "src", "Soenneker.Quark.Suite", "Components", "Menubar", fileName));
    }

    private static string GetSuiteRootForMenubarSub()
    {
        var directory = AppContext.BaseDirectory;

        for (var i = 0; i < 6; i++)
        {
            directory = Directory.GetParent(directory)!.FullName;
        }

        return directory;
    }
}
