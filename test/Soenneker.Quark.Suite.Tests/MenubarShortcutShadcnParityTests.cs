using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void MenubarShortcut_matches_shadcn_base_classes()
    {
        var shortcut = Render<MenubarShortcut>(parameters => parameters.Add(p => p.ChildContent, "⌘K"));

        var shortcutClasses = shortcut.Find("[data-slot='menubar-shortcut']").GetAttribute("class")!;

        shortcutClasses.Should().Contain("ml-auto");
        shortcutClasses.Should().Contain("text-xs");
        shortcutClasses.Should().Contain("tracking-widest");
        shortcutClasses.Should().Contain("text-muted-foreground");
        shortcutClasses.Should().NotContain("q-menubar-shortcut");
    }
}
