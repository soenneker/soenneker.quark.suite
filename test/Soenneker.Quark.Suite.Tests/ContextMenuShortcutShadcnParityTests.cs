using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void ContextMenuShortcut_matches_shadcn_base_classes()
    {
        var shortcut = Render<ContextMenuShortcut>(parameters => parameters.Add(p => p.ChildContent, "⌘K"));

        string shortcutClasses = shortcut.Find("[data-slot='context-menu-shortcut']").GetAttribute("class")!;

        shortcutClasses.Should().Contain("ml-auto");
        shortcutClasses.Should().Contain("text-xs");
        shortcutClasses.Should().Contain("tracking-widest");
        shortcutClasses.Should().Contain("text-muted-foreground");
        shortcutClasses.Should().NotContain("q-context-menu-shortcut");
    }
}
