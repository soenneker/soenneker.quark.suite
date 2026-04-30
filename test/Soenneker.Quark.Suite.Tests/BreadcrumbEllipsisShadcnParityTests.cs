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

    private static string GetSuiteRootForBreadcrumbEllipsis()
    {
        var directory = AppContext.BaseDirectory;

        while (!File.Exists(Path.Combine(directory, "Soenneker.Quark.Suite.slnx")))
            directory = Directory.GetParent(directory)!.FullName;

        return directory;
    }
}
