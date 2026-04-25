using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void TreeView_slots_use_data_slot_hooks_instead_of_legacy_q_classes()
    {
        var cut = Render<TreeView<string>>(parameters => parameters
            .Add(p => p.Nodes, new[] { "Root" })
            .Add(p => p.SelectedNode, "Root")
            .Add(p => p.HasChildNodes, node => node == "Root")
            .Add(p => p.GetChildNodes, node => node == "Root" ? new[] { "Child" } : [])
            .Add(p => p.NodeContent, (RenderFragment<string>)(node => builder => builder.AddContent(0, node))));

        var tree = cut.Find("[data-slot='tree-view']");
        var node = cut.Find("[data-slot='tree-view-node']");
        var row = cut.Find("[data-slot='tree-view-row']");
        var content = cut.Find("[data-slot='tree-view-node-content']");
        var title = cut.Find("[data-slot='tree-view-node-title']");
        var item = cut.Find("[role='treeitem']");

        tree.GetAttribute("class")!.Should().Contain("block");
        tree.GetAttribute("role").Should().Be("tree");
        tree.GetAttribute("aria-label").Should().Be("Tree view");
        node.GetAttribute("class")!.Should().Contain("block");
        row.GetAttribute("class")!.Should().Contain("flex");
        row.GetAttribute("class")!.Should().Contain("items-center");
        row.GetAttribute("class")!.Should().Contain("gap-1");
        content.GetAttribute("class")!.Should().Contain("cursor-pointer");
        title.GetAttribute("class")!.Should().Contain("inline-flex");
        item.GetAttribute("aria-expanded").Should().Be("false");
        item.GetAttribute("aria-selected").Should().Be("true");
        item.GetAttribute("tabindex").Should().Be("0");
        cut.Markup.Should().NotContain("q-tree-view");
        cut.Markup.Should().NotContain("q-tree-view-node");
        cut.Markup.Should().NotContain("q-tree-view-node-content");
    }
}
