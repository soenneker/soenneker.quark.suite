using AwesomeAssertions;
using Bunit;
using System;
using System.IO;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void ContextMenuSub_matches_shadcn_base_classes()
    {
        var sub = Render<ContextMenuSub>(parameters => parameters.Add(p => p.ChildContent, "Sub"));

        var subClasses = sub.Find("[data-slot='context-menu-sub']").GetAttribute("class")!;

        subClasses.Should().Contain("relative");
        subClasses.Should().NotContain("q-context-menu-sub");
    }

    [Test]
    public void ContextMenuSubTrigger_matches_shadcn_v4_state_and_icon_classes()
    {
        var source = ReadContextMenuSubSource("ContextMenuSubTrigger.razor");

        source.Should().Contain("Rounded.Sm");
        source.Should().Contain("Padding.Is2.OnX.Token(\"1.5\").OnY");
        source.Should().Contain("data-[inset]:pl-8");
        source.Should().Contain("data-[state=open]:bg-accent");
        source.Should().Contain("data-[state=open]:text-accent-foreground");
        source.Should().Contain("[&_svg:not([class*='text-'])]:text-muted-foreground");
        source.Should().Contain("Class=\"ml-auto size-4\"");
        source.Should().NotContain("data-open:");
        source.Should().NotContain("data-[inset]:pl-7");
        source.Should().NotContain("cn-rtl-flip");
    }

    private static string ReadContextMenuSubSource(string fileName)
    {
        return File.ReadAllText(Path.Combine(GetSuiteRootForContextMenuSub(), "src", "Soenneker.Quark.Suite", "Components", "ContextMenu", fileName));
    }

    private static string GetSuiteRootForContextMenuSub()
    {
        var directory = AppContext.BaseDirectory;

        for (var i = 0; i < 6; i++)
        {
            directory = Directory.GetParent(directory)!.FullName;
        }

        return directory;
    }
}
