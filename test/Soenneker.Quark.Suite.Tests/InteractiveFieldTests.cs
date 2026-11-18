using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public async Task Interactive_fields_transfer_only_highlighted_options()
    {
        IReadOnlyList<string> selected = ["b"];
        var cut = Render<TransferList>(p => p
            .Add(c => c.Options, [new SelectOption("a", "Alpha"), new SelectOption("b", "Bravo")])
            .Add(c => c.Value, selected)
            .Add(c => c.ValueChanged, value => selected = value));
        await cut.Find("select").ChangeAsync(new ChangeEventArgs { Value = new[] { "a" } });
        await cut.Find("button").ClickAsync();
        selected.Should().Equal("b", "a");
    }

    [Test]
    public async Task Interactive_fields_select_parent_and_child_independently()
    {
        IReadOnlyList<string> selected = [];
        var cut = Render<TreeSelect>(p => p.Add(c => c.Multiple, true)
            .Add(c => c.Nodes, [new TreeSelectNode("p", "Parent", [new TreeSelectNode("c", "Child")])])
            .Add(c => c.ValueChanged, value => selected = value));
        await cut.Find("button").ClickAsync();
        await cut.Find("[aria-label=Parent]").ClickAsync();
        await cut.Find("button[aria-pressed]").ClickAsync();
        selected.Should().Equal("p");
        cut.Render(p => p.Add(c => c.Value, selected));
        await cut.FindAll("button[aria-pressed]").Last().ClickAsync();
        selected.Should().Equal("p", "c");
    }

    [Test]
    public async Task Interactive_fields_expand_data_records()
    {
        var data = new DataTableData<string>
        {
            Items = ["parent", "child"],
            RowKey = key => key,
            ParentKey = key => key == "child" ? "parent" : null,
            Columns = [new DataTableColumn<string> { Key = "name", Title = "Name", Value = key => key }]
        };
        var cut = Render<DataTable>(p => p.Add(c => c.Data, data));
        cut.FindAll("tbody tr").Count.Should().Be(1);
        await cut.Find("button[aria-label=parent]").ClickAsync();
        cut.Find("tr[aria-level='2']").TextContent.Should().Contain("child");
        await cut.Find("button[aria-label=parent]").ClickAsync();
        cut.FindAll("tbody tr").Count.Should().Be(1);
    }
    [Test]
    public async Task Interactive_fields_dashboard_emits_new_layout_and_blocks_overlap()
    {
        DashboardWidget[] original = [new("one", 1, 1), new("two", 2, 1)];
        IReadOnlyList<DashboardWidget> changed = [];
        var cut = Render<DashboardGrid>(p => p.Add(c => c.Layout, original)
            .Add(c => c.WidgetTemplate, w => b => b.AddContent(0, w.Key))
            .Add(c => c.LayoutChanged, layout => changed = layout));
        cut.Find("button[aria-label='Move right']").HasAttribute("disabled").Should().BeTrue();
        await cut.Find("button[aria-label=Taller]").ClickAsync();
        changed[0].Height.Should().Be(2);
        original[0].Height.Should().Be(1);
    }

    [Test]
    public async Task Interactive_fields_rating_binds_and_renders_read_only()
    {
        int value = 0;
        var cut = Render<Rating>(p => p.Add(c => c.ValueChanged, score => value = score));
        await cut.FindAll("input")[2].ChangeAsync(new ChangeEventArgs { Value = true });
        value.Should().Be(3);
        cut.Render(p => p.Add(c => c.Value, value).Add(c => c.ReadOnly, true));
        cut.FindAll("input").Should().BeEmpty();
        cut.Find("span").TextContent.Should().Be("★★★☆☆");
    }

    [Test]
    public async Task Interactive_fields_schema_revalidates_model_on_change()
    {
        var model = new Dictionary<string, string?>();
        var field = new SchemaField<Dictionary<string, string?>>("name", "Name", m => m.GetValueOrDefault("name"),
            (m, value) => m["name"] = value, m => string.IsNullOrEmpty(m.GetValueOrDefault("name")) ? "Enter a name" : null);
        var cut = Render<SchemaForm<Dictionary<string, string?>>>(p => p.Add(c => c.Model, model).Add(c => c.Fields, [field]));
        await cut.Find("input").ChangeAsync(new ChangeEventArgs { Value = "" });
        cut.Find("[role=alert]").TextContent.Should().Be("Enter a name");
        await cut.Find("input").ChangeAsync(new ChangeEventArgs { Value = "Ada" });
        cut.FindAll("[role=alert]").Should().BeEmpty();
        model["name"].Should().Be("Ada");
    }

    [Test]
    public async Task Interactive_fields_mask_binds_digits_and_formats_external_value()
    {
        string? raw = null;
        var cut = Render<MaskedInput>(p => p.Add(c => c.Mask, "(###) ###-####")
            .Add(c => c.Value, "1234567").Add(c => c.ValueChanged, value => raw = value));
        cut.Find("input").GetAttribute("value").Should().Be("(123) 456-7");
        await cut.Find("input").InputAsync(new ChangeEventArgs { Value = "(987) 654-3210" });
        raw.Should().Be("9876543210");
    }
}
