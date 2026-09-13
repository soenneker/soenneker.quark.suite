using AwesomeAssertions;
using Bunit;
using System.Linq;
using Soenneker.Quark.Suite.Demo.Pages.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Tree_collapse_retains_rows_but_removes_them_from_navigation()
    {
        var cut = Render<Trees>();
        var tree = cut.Find("[role='tree']");
        var root = tree.QuerySelector("[role='treeitem']")!;
        var count = tree.QuerySelectorAll("[role='treeitem']").Length;
        root.Click();
        cut.WaitForAssertion(() =>
        {
            tree = cut.Find("[role='tree']");
            root = tree.QuerySelector("[role='treeitem']")!;
            root.GetAttribute("aria-expanded").Should().Be("false");
            tree.QuerySelectorAll("[role='treeitem']").Length.Should().Be(count);
            var hidden = tree.QuerySelectorAll("[inert] [role='treeitem']");
            hidden.Length.Should().Be(count - 1);
            hidden.All(row => row.GetAttribute("tabindex") == "-1").Should().BeTrue();
        });
        root.Click();
        cut.WaitForAssertion(() =>
        {
            tree = cut.Find("[role='tree']");
            root = tree.QuerySelector("[role='treeitem']")!;
            root.GetAttribute("aria-expanded").Should().Be("true");
            tree.QuerySelectorAll("[inert] [role='treeitem']").Length.Should().BeLessThan(count - 1);
        });
    }
}
