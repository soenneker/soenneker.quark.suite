using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Kanban_renders_board_surface()
    {
        var cut = Render<Kanban>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment) (builder =>
            {
                builder.OpenComponent<KanbanBoard>(0);
                builder.AddAttribute(1, nameof(KanbanBoard.ListId), "board");
                builder.AddAttribute(2, nameof(KanbanBoard.ChildContent), (RenderFragment) (boardBuilder =>
                {
                    boardBuilder.OpenComponent<KanbanColumn>(0);
                    boardBuilder.AddAttribute(1, nameof(KanbanColumn.Value), "todo");
                    boardBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        var root = cut.Find("[data-slot='kanban']");
        root.ClassList.Should().Contain("overflow-hidden");

        var board = cut.Find("[data-slot='kanban-board']");
        board.GetAttribute("data-sortable-list-id").Should().Be("board");
        board.ClassList.Should().Contain("flex-row");
        board.ClassList.Should().NotContain("flex-col");
        board.ClassList.Should().Contain("overflow-x-auto");
    }

    [Test]
    public void Kanban_column_and_item_emit_sortable_markers()
    {
        var cut = Render<KanbanColumn>(parameters => parameters
            .Add(p => p.Value, "in-progress")
            .Add(p => p.ChildContent, (RenderFragment) (builder =>
            {
                builder.OpenComponent<KanbanColumnHandle>(0);
                builder.AddAttribute(1, nameof(KanbanColumnHandle.ChildContent), (RenderFragment) (handleBuilder => handleBuilder.AddContent(0, "In Progress")));
                builder.CloseComponent();
                builder.OpenComponent<KanbanItem>(2);
                builder.AddAttribute(3, nameof(KanbanItem.Value), "task-1");
                builder.AddAttribute(4, nameof(KanbanItem.ChildContent), (RenderFragment) (itemBuilder =>
                {
                    itemBuilder.OpenComponent<KanbanItemHandle>(0);
                    itemBuilder.AddAttribute(1, nameof(KanbanItemHandle.ChildContent), (RenderFragment) (handleBuilder => handleBuilder.AddContent(0, "Task")));
                    itemBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        var column = cut.Find("[data-slot='kanban-column']");
        column.GetAttribute("data-sortable-id").Should().Be("in-progress");
        column.HasAttribute("data-kanban-column").Should().BeTrue();

        cut.Find("[data-slot='kanban-column-handle']").HasAttribute("data-kanban-column-handle").Should().BeTrue();
        cut.Find("[data-slot='kanban-item']").GetAttribute("data-sortable-id").Should().Be("task-1");
        cut.Find("[data-slot='kanban-item-handle']").HasAttribute("data-kanban-item-handle").Should().BeTrue();
    }

    [Test]
    public void Kanban_column_content_uses_stable_column_list_id()
    {
        var cut = Render<KanbanColumnContent>(parameters => parameters
            .Add(p => p.Value, "done")
            .Add(p => p.HandleOnly, true)
            .Add(p => p.ChildContent, "Done items"));

        var content = cut.Find("[data-slot='kanban-column-content']");
        content.GetAttribute("data-sortable-list-id").Should().Be("done");
        content.ClassList.Should().Contain("overflow-y-auto");
    }
}
