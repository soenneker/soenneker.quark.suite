using AwesomeAssertions;
using Bunit;
using System;
using System.IO;
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

    [Test]
    public void NavigationMenus_demo_examples_match_current_shadcn_docs_examples()
    {
        var source = File.ReadAllText(Path.Combine(GetSuiteRootForNavigationMenusPage(), "test", "Soenneker.Quark.Suite.Demo", "Pages", "Components", "NavigationMenus.razor"));

        source.Should().Contain("Title=\"With Viewport\"");
        source.Should().Contain("Title=\"Without Viewport\"");
        source.Should().Contain("<NavigationMenu Viewport=\"false\"");
        source.Should().Contain("Getting started");
        source.Should().Contain("Components");
        source.Should().Contain("Documentation");
        source.Should().Contain("Simple List");
        source.Should().Contain("With Icon");
        source.Should().Contain("Backlog");
        source.Should().Contain("To Do");
        source.Should().Contain("Done");
        source.Should().Contain("Browse all components in the library.");
        source.Should().Contain("Learn how to use the library.");
        source.Should().Contain("Read our latest blog posts.");
        source.Should().NotContain("Docs");
    }

    private static string GetSuiteRootForNavigationMenusPage()
    {
        var directory = AppContext.BaseDirectory;

        while (!File.Exists(Path.Combine(directory, "Soenneker.Quark.Suite.slnx")))
            directory = Directory.GetParent(directory)!.FullName;

        return directory;
    }
}
