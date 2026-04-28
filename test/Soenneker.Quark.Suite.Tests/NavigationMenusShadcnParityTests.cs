using AwesomeAssertions;
using Bunit;
using System.Linq;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void NavigationMenus_page_uses_shadcn_overrides()
    {
        var cut = Render<global::Soenneker.Quark.Suite.Demo.Pages.Components.NavigationMenus>();
        var previewClasses = cut.FindAll("[data-slot='preview']")
            .Select(node => node.FirstElementChild!.GetAttribute("class")!)
            .ToList();

        previewClasses.Count(cls => cls.Contains("h-96")).Should().BeGreaterThanOrEqualTo(2);
    }
}
