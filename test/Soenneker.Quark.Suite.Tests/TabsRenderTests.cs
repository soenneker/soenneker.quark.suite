using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Composed_justified_tabs_list_expands_to_full_width()
    {
        var cut = Render<Tabs>(parameters => parameters
            .Add(component => component.Composed, true)
            .Add(component => component.Justified, true)
            .Add(component => component.ChildContent, BuildTabsList()));

        var listClass = cut.Find("[data-slot='tabs-list']").GetAttribute("class")!;

        listClass.Should().Contain("w-full");
        listClass.Should().Contain("[&>[data-slot=tabs-trigger]]:flex-1");
        listClass.Should().NotContain("w-fit");
    }

    private static RenderFragment BuildTabsList()
    {
        return builder =>
        {
            builder.OpenComponent<TabsList>(0);
            builder.AddAttribute(1, nameof(TabsList.ChildContent), (RenderFragment) (listBuilder =>
            {
                listBuilder.OpenComponent<TabsTrigger>(0);
                listBuilder.AddAttribute(1, nameof(TabsTrigger.Value), "explorer");
                listBuilder.AddAttribute(2, nameof(TabsTrigger.ChildContent), (RenderFragment) (triggerBuilder => triggerBuilder.AddContent(0, "Explorer")));
                listBuilder.CloseComponent();

                listBuilder.OpenComponent<TabsTrigger>(3);
                listBuilder.AddAttribute(4, nameof(TabsTrigger.Value), "outline");
                listBuilder.AddAttribute(5, nameof(TabsTrigger.ChildContent), (RenderFragment) (triggerBuilder => triggerBuilder.AddContent(0, "Outline")));
                listBuilder.CloseComponent();
            }));
            builder.CloseComponent();
        };
    }
}
