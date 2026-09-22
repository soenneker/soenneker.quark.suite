using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task AsChild_tooltip_triggers_forward_events_without_repeated_hover_renders(bool useSpan)
    {
        var cut = Render<Tooltip>(p => p.Add(c => c.DelayDuration, 0).Add(c => c.ChildContent, builder =>
        {
            builder.OpenComponent<TooltipTrigger>(0);
            builder.AddAttribute(1, nameof(TooltipTrigger.AsChild), true);
            builder.AddAttribute(2, nameof(TooltipTrigger.ChildContent), (RenderFragment)(content =>
            {
                content.OpenComponent(0, useSpan ? typeof(Span) : typeof(Button));
                content.AddAttribute(1, "ChildContent", (RenderFragment)(label => label.AddContent(0, "Details")));
                content.CloseComponent();
            }));
            builder.CloseComponent();
        }));
        string selector = useSpan ? "span[data-slot='tooltip-trigger']" : "button";
        await cut.Find(selector).FocusInAsync(new FocusEventArgs());
        cut.Find(selector).GetAttribute("data-state").Should().Be("instant-open");
        await cut.Find(selector).MouseMoveAsync(new MouseEventArgs());
        int renders = cut.RenderCount;
        for (int i = 0; i < 20; i++)
            await cut.Find(selector).MouseMoveAsync(new MouseEventArgs());
        cut.RenderCount.Should().Be(renders);
        await cut.Find(selector).FocusOutAsync(new FocusEventArgs());
        cut.Find(selector).GetAttribute("data-state").Should().Be("closed");
    }

    [Test]
    public async Task Resizing_updates_panel_styles_without_rebuilding_the_group_and_ignores_identical_moves()
    {
        int groupRenders = 0;
        int panelRenders = 0;
        var cut = Render<ResizablePanelGroup>(p => p.Add(c => c.ChildContent, builder =>
        {
            groupRenders++;
            builder.OpenComponent<ResizablePanel>(0);
            builder.AddAttribute(1, nameof(ResizablePanel.ChildContent), (RenderFragment)(content =>
            {
                panelRenders++;
                content.AddContent(0, "Left");
            }));
            builder.CloseComponent();
            builder.OpenComponent<ResizableHandle>(2);
            builder.CloseComponent();
            builder.OpenComponent<ResizablePanel>(3);
            builder.CloseComponent();
        }));
        await cut.InvokeAsync(() => Task.CompletedTask);
        int renders = groupRenders;
        await cut.InvokeAsync(() => cut.Instance.HandlePointerDragMove(0, 60, 1000));
        groupRenders.Should().Be(renders);
        cut.FindAll("[data-slot='resizable-panel']")[0].GetAttribute("style").Should().Contain("flex: 60 1 0px");
        cut.Find("[role='separator']").GetAttribute("aria-valuenow").Should().Be("60");
        int changedPanelRenders = panelRenders;
        for (int i = 0; i < 20; i++)
            await cut.InvokeAsync(() => cut.Instance.HandlePointerDragMove(0, 60, 1000));
        panelRenders.Should().Be(changedPanelRenders);
        groupRenders.Should().Be(renders);
    }

    [Test]
    public async Task Combobox_registration_and_repeated_hover_do_not_render_all_options()
    {
        int contentRenders = 0;
        var cut = Render<Combobox>(p => p.Add(c => c.AutoHighlight, true).Add(c => c.ChildContent, builder =>
        {
            contentRenders++;
            for (int i = 0; i < 30; i++)
            {
                builder.OpenComponent<ComboboxItem>(0);
                builder.AddAttribute(1, nameof(ComboboxItem.Value), $"option-{i}");
                builder.CloseComponent();
            }
        }));
        await cut.InvokeAsync(() => Task.CompletedTask);
        contentRenders.Should().BeLessThanOrEqualTo(3);
        int renders = contentRenders;
        var first = cut.FindComponents<ComboboxItem>()[0];
        int itemRenders = first.RenderCount;
        for (int i = 0; i < 20; i++)
            await first.Find("[role='option']").MouseEnterAsync(new MouseEventArgs());
        contentRenders.Should().Be(renders);
        first.RenderCount.Should().Be(itemRenders);

        await cut.FindComponents<ComboboxItem>()[1].Find("[role='option']").MouseEnterAsync(new MouseEventArgs());
        cut.FindAll("[role='option']")[0].HasAttribute("data-highlighted").Should().BeFalse();
        cut.FindAll("[role='option']")[1].HasAttribute("data-highlighted").Should().BeTrue();
    }

    [Test]
    public void Button_reused_child_content_still_observes_owner_state()
    {
        string label = "Before";
        RenderFragment content = builder => builder.AddContent(0, label);
        var cut = Render<Button>(p => p.Add(c => c.ChildContent, content));
        label = "After";
        cut.Render(p => p.Add(c => c.ChildContent, content));
        cut.Find("button").TextContent.Should().Be("After");
    }

    [Test]
    public async Task DataTable_column_registration_does_not_render_existing_rows()
    {
        int rowRenders = 0;
        var cut = Render<DataTable>(p => p.Add(c => c.TableContent, builder =>
        {
            rowRenders++;
            builder.OpenElement(0, "thead");
            builder.OpenElement(1, "tr");
            for (int i = 0; i < 20; i++)
            {
                builder.OpenComponent<Th>(2);
                builder.AddAttribute(3, nameof(Th.Data), $"column-{i}");
                builder.AddAttribute(4, nameof(Th.ChildContent), (RenderFragment)(cell => cell.AddContent(0, "Header")));
                builder.CloseComponent();
            }
            builder.CloseElement();
            builder.CloseElement();
        }));
        await cut.InvokeAsync(() => Task.CompletedTask);
        rowRenders.Should().Be(1);
        cut.FindAll("th").Should().HaveCount(20);
    }
}
