using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void DropdownShortcut_matches_shadcn_base_classes()
    {
        var shortcut = Render<DropdownShortcut>(parameters => parameters.Add(p => p.ChildContent, "⌘K"));

        var shortcutClasses = shortcut.Find("[data-slot='dropdown-menu-shortcut']").GetAttribute("class")!;

        shortcutClasses.Should().Contain("ml-auto");
        shortcutClasses.Should().Contain("text-xs");
        shortcutClasses.Should().Contain("tracking-widest");
        shortcutClasses.Should().Contain("text-muted-foreground");
        shortcutClasses.Should().NotContain("q-dropdown-shortcut");
    }
}
