using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Timeline_root_and_item_emit_accessible_local_contract()
    {
        var cut = Render<Timeline>(parameters => parameters
            .Add(p => p.TimelineAlignment, TimelineAlign.Left)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<TimelineItem>(0);
                builder.AddAttribute(1, nameof(TimelineItem.Status), (object) TimelineStatus.InProgress);
                builder.AddAttribute(2, nameof(TimelineItem.Title), "Deploying");
                builder.AddAttribute(3, nameof(TimelineItem.Time), "Now");
                builder.AddAttribute(4, nameof(TimelineItem.Description), "The release is rolling out.");
                builder.CloseComponent();
            })));

        var timeline = cut.Find("[data-slot='timeline']");
        var timelineClasses = timeline.GetAttribute("class")!;
        var item = cut.Find("[data-slot='timeline-item']");
        var content = cut.Find("[data-slot='timeline-content']");

        timeline.TagName.Should().Be("OL");
        timeline.GetAttribute("aria-label").Should().Be("Timeline");
        timeline.GetAttribute("data-align").Should().Be("left");
        timelineClasses.Should().Contain("relative");
        timelineClasses.Should().Contain("flex");
        timelineClasses.Should().Contain("flex-col");

        item.TagName.Should().Be("LI");
        item.GetAttribute("data-status").Should().Be("inprogress");
        item.GetAttribute("aria-current").Should().Be("step");
        item.GetAttribute("class")!.Should().Contain("w-full");
        item.QuerySelector(".aria-current-step").Should().BeNull();

        content.GetAttribute("class")!.Should().Contain("flex");
        content.GetAttribute("class")!.Should().Contain("flex-col");
        cut.Find("[data-slot='timeline-title']").TextContent.Should().Be("Deploying");
        cut.Find("[data-slot='timeline-description']").TextContent.Should().Be("The release is rolling out.");
    }

    [Test]
    public void TimelineMarker_preserves_marker_slot_when_composed_through_icon()
    {
        var cut = Render<Timeline>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<TimelineItem>(0);
                builder.AddAttribute(1, nameof(TimelineItem.ChildContent), (RenderFragment)(itemBuilder =>
                {
                    itemBuilder.OpenComponent<TimelineMarker>(0);
                    itemBuilder.AddAttribute(1, nameof(TimelineMarker.ChildContent), (RenderFragment)(markerBuilder => markerBuilder.AddContent(0, "M")));
                    itemBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        cut.Find("[data-slot='timeline-marker']");
    }

    [Test]
    public void TimelineIcon_centers_custom_icon_content()
    {
        var cut = Render<TimelineIcon>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "<svg></svg>"))));

        var innerClasses = cut.Find("[data-slot='timeline-icon'] > div").GetAttribute("class")!;

        innerClasses.Should().Contain("flex");
        innerClasses.Should().Contain("items-center");
        innerClasses.Should().Contain("justify-center");
    }
}
