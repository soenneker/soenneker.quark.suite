using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Sortable_components_emit_accessible_list_and_handle_contract()
    {
        var cut = Render<SortableList>(parameters => parameters
            .Add(p => p.HandleOnly, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<SortableItem>(0);
                builder.AddAttribute(1, nameof(SortableItem.ItemId), "alpha");
                builder.AddAttribute(2, "ChildContent", (RenderFragment)(itemBuilder =>
                {
                    itemBuilder.OpenComponent<SortableHandle>(0);
                    itemBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(handleBuilder => handleBuilder.AddContent(0, "Drag")));
                    itemBuilder.CloseComponent();
                    itemBuilder.AddContent(2, "Alpha");
                }));
                builder.CloseComponent();

                builder.OpenComponent<SortableItem>(3);
                builder.AddAttribute(4, nameof(SortableItem.ItemId), "beta");
                builder.AddAttribute(5, nameof(SortableItem.Disabled), true);
                builder.AddAttribute(6, "ChildContent", (RenderFragment)(itemBuilder => itemBuilder.AddContent(0, "Beta")));
                builder.CloseComponent();
            })));

        var list = cut.Find("[data-slot='sortable-list']");
        var listClasses = list.GetAttribute("class")!;
        var items = cut.FindAll("[data-slot='sortable-item']");
        var handle = cut.Find("[data-slot='sortable-handle']");

        list.GetAttribute("role").Should().Be("list");
        list.GetAttribute("data-sortable-list-id").Should().StartWith("quark-sortable");
        list.GetAttribute("data-sortable-keyboard").Should().Be("true");
        listClasses.Should().Contain("flex");
        listClasses.Should().Contain("flex-col");
        listClasses.Should().Contain("gap-2");

        items[0].GetAttribute("role").Should().Be("listitem");
        items[0].GetAttribute("data-sortable-id").Should().Be("alpha");
        items[1].GetAttribute("role").Should().Be("listitem");
        items[1].GetAttribute("data-disabled").Should().Be("true");
        items[1].GetAttribute("aria-disabled").Should().Be("true");

        handle.TagName.ToLowerInvariant().Should().Be("button");
        handle.GetAttribute("type").Should().Be("button");
        handle.GetAttribute("aria-label").Should().Be("Drag handle");
        handle.GetAttribute("aria-keyshortcuts").Should().Contain("ArrowDown");
        handle.GetAttribute("data-sortable-handle").Should().Be(string.Empty);
        handle.GetAttribute("class")!.Should().Contain("cursor-grab");
        handle.GetAttribute("class")!.Should().Contain("touch-none");
    }
}
