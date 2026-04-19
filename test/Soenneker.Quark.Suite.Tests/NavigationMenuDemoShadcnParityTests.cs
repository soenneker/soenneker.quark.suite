using AwesomeAssertions;
using Bunit;
using System.Linq;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void NavigationMenu_demo_page_uses_shadcn_preview_overrides()
    {
        var cut = Render<global::Soenneker.Quark.Suite.Demo.Pages.Components.NavigationMenuDemo>();
        var previewClasses = cut.FindAll("[data-slot='preview']")
            .Select(node => node.FirstElementChild!.GetAttribute("class")!)
            .ToList();

        previewClasses.Count(cls => cls.Contains("h-96")).Should().BeGreaterThanOrEqualTo(2);
    }
}
