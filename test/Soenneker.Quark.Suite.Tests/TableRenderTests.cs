using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void DataTable_renders_native_table_from_table_content()
    {
        var cut = Render<DataTable>(parameters => parameters
            .Add(p => p.TableContent, BasicTableContent));

        var table = cut.Find("table[data-slot='table']");
        table.TextContent.Should().Contain("Cell");
        table.ClassList.Should().Contain("q-datatable");
        table.ClassList.Should().Contain("w-full");
        table.ClassList.Should().Contain("text-sm");
        table.ClassList.Should().Contain("caption-bottom");
    }

    [Test]
    public void Anchor_and_span_render_themeable_data_slots()
    {
        var anchor = Render<Anchor>(parameters => parameters
            .Add(p => p.Href, "/details")
            .Add(p => p.ChildContent, "Details"));

        anchor.Find("a").GetAttribute("data-slot").Should().Be("anchor");

        var span = Render<Span>(parameters => parameters
            .Add(p => p.ChildContent, "Label"));

        span.Find("span").GetAttribute("data-slot").Should().Be("span");
    }

    [Test]
    public void DataTable_preserves_table_child_content_path()
    {
        var cut = Render<DataTable>(parameters => parameters
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<Table>(0);
                builder.AddAttribute(1, nameof(Table.ChildContent), BasicTableContent);
                builder.CloseComponent();
            }));

        cut.FindAll("table").Should().HaveCount(1);
        cut.Find("table[data-slot='table']").TextContent.Should().Contain("Cell");
    }

    private static readonly RenderFragment BasicTableContent = builder =>
    {
        builder.OpenElement(0, "tbody");
        builder.OpenElement(1, "tr");
        builder.OpenElement(2, "td");
        builder.AddContent(3, "Cell");
        builder.CloseElement();
        builder.CloseElement();
        builder.CloseElement();
    };
}
