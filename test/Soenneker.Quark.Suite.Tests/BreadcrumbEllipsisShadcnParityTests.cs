using AwesomeAssertions;
using Bunit;
using System;
using System.IO;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void BreadcrumbEllipsis_matches_shadcn_base_classes()
    {
        var cut = Render<BreadcrumbEllipsis>();

        var classes = cut.Find("[data-slot='breadcrumb-ellipsis']").GetAttribute("class")!;

        classes.Should().Contain("flex");
        classes.Should().Contain("size-9");
        classes.Should().Contain("items-center");
        classes.Should().Contain("justify-center");
        classes.Should().Contain("[&>svg]:size-4");
        classes.Should().NotContain("size-5");
        classes.Should().NotContain("q-breadcrumb-ellipsis");
    }

    [Test]
    public void Breadcrumbs_collapsed_demo_uses_dropdown_trigger_for_ellipsis()
    {
        var source = File.ReadAllText(Path.Combine(GetSuiteRootForBreadcrumbEllipsis(), "test", "Soenneker.Quark.Suite.Demo", "Pages", "Components", "Breadcrumbs.razor"));
        var collapsedStart = source.IndexOf("Title=\"Collapsed\"", StringComparison.Ordinal);
        var linkComponentStart = source.IndexOf("Title=\"Link component\"", StringComparison.Ordinal);
        var collapsedDemo = source[collapsedStart..linkComponentStart];

        collapsedDemo.Should().Contain("<Dropdown>");
        collapsedDemo.Should().Contain("<DropdownToggle AsChild=\"true\">");
        collapsedDemo.Should().Contain("<BreadcrumbEllipsis />");
        collapsedDemo.Should().Contain("Toggle menu");
    }

    [Test]
    public void Breadcrumb_demo_examples_match_current_shadcn_docs_examples()
    {
        var source = File.ReadAllText(Path.Combine(GetSuiteRootForBreadcrumbEllipsis(), "test", "Soenneker.Quark.Suite.Demo", "Pages", "Components", "Breadcrumbs.razor"));
        var basicStart = source.IndexOf("Title=\"Basic\"", StringComparison.Ordinal);
        var customStart = source.IndexOf("Title=\"Custom separator\"", StringComparison.Ordinal);
        var dropdownStart = source.IndexOf("Title=\"Dropdown\"", StringComparison.Ordinal);
        var collapsedStart = source.IndexOf("Title=\"Collapsed\"", StringComparison.Ordinal);
        var linkComponentStart = source.IndexOf("Title=\"Link component\"", StringComparison.Ordinal);
        var rtlStart = source.IndexOf("Title=\"RTL\"", StringComparison.Ordinal);

        var basicDemo = source[basicStart..customStart];
        basicDemo.Should().Contain("A basic breadcrumb with a home link and a components link.");
        basicDemo.Should().Contain("<BreadcrumbLink To=\"#\">Home</BreadcrumbLink>");
        basicDemo.Should().Contain("<BreadcrumbLink To=\"#\">Components</BreadcrumbLink>");
        basicDemo.Should().NotContain("<Dropdown>");

        var customDemo = source[customStart..dropdownStart];
        customDemo.Should().Contain("Use a custom component as children for BreadcrumbSeparator to create a custom separator.");
        customDemo.Should().Contain("<Anchor To=\"/\">Home</Anchor>");
        customDemo.Should().Contain("<Anchor To=\"/components\">Components</Anchor>");

        var dropdownDemo = source[dropdownStart..collapsedStart];
        dropdownDemo.Should().Contain("You can compose BreadcrumbItem with a DropdownMenu to create a dropdown in the breadcrumb.");
        dropdownDemo.Should().Contain("<DropdownToggle Class=\"flex items-center gap-1\">");
        dropdownDemo.Should().Contain("LucideIcon.ChevronDown");

        var collapsedDemo = source[collapsedStart..linkComponentStart];
        collapsedDemo.Should().Contain("We provide a BreadcrumbEllipsis component to show a collapsed state when the breadcrumb is too long.");
        collapsedDemo.Should().Contain("<BreadcrumbEllipsis />");
        collapsedDemo.Should().Contain("<Anchor To=\"/docs/components\">Components</Anchor>");

        var linkComponentDemo = source[linkComponentStart..rtlStart];
        linkComponentDemo.Should().Contain("To use a custom link component from your routing library, you can use the AsChild prop on BreadcrumbLink.");
        linkComponentDemo.Should().Contain("<Anchor To=\"/\">Home</Anchor>");
        linkComponentDemo.Should().Contain("<Anchor To=\"/components\">Components</Anchor>");
    }

    private static string GetSuiteRootForBreadcrumbEllipsis()
    {
        var directory = AppContext.BaseDirectory;

        while (!File.Exists(Path.Combine(directory, "Soenneker.Quark.Suite.slnx")))
            directory = Directory.GetParent(directory)!.FullName;

        return directory;
    }
}
