using System;
using System.Linq;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Timeline_emits_reui_step_slots_and_orientation_classes()
    {
        var cut = Render<Timeline>(parameters => parameters
            .Add(component => component.Value, 2)
            .Add(component => component.ChildContent, BuildTimelineItems()));

        var timeline = cut.Find("[data-slot='timeline']");
        var items = cut.FindAll("[data-slot='timeline-item']");
        var indicator = cut.Find("[data-slot='timeline-indicator']");
        var separator = cut.Find("[data-slot='timeline-separator']");
        var date = cut.Find("[data-slot='timeline-date']");
        var title = cut.Find("[data-slot='timeline-title']");
        var content = cut.Find("[data-slot='timeline-content']");

        timeline.TagName.Should().Be("DIV");
        timeline.GetAttribute("data-orientation").Should().Be("vertical");
        timeline.GetAttribute("class").Should().Contain("group/timeline");
        timeline.GetAttribute("class").Should().Contain("data-[orientation=vertical]:flex-col");

        items.Should().HaveCount(2);
        items[0].GetAttribute("data-completed").Should().NotBeNull();
        items[1].GetAttribute("data-completed").Should().NotBeNull();
        items[1].GetAttribute("aria-current").Should().Be("step");
        items[0].GetAttribute("class").Should().Contain("group/timeline-item");

        indicator.GetAttribute("class").Should().Contain("group-data-completed/timeline-item:border-primary");
        separator.GetAttribute("class").Should().Contain("group-last/timeline-item:hidden");
        date.GetAttribute("class").Should().Contain("text-xs");
        title.GetAttribute("class").Should().Contain("text-sm font-medium");
        content.GetAttribute("class").Should().Contain("text-muted-foreground text-sm");
    }

    [Test]
    public void Event_timeline_vertical_renders_details_newest_first_and_connects_only_adjacent_events()
    {
        var when = new DateTimeOffset(2026, 9, 20, 19, 0, 0, TimeSpan.FromHours(-5));
        var cut = Render<EventTimeline>(p => p
            .Add(c => c.Layout, EventTimelineLayout.Vertical)
            .Add(c => c.NewestFirst, true)
            .Add(c => c.DenseTimelineThreshold, 1)
            .Add(c => c.Items, [
                new EventTimelineItem { Label = "Earlier", When = when },
                new EventTimelineItem { Label = "Later", When = when.AddHours(1), Tone = SemanticTone.Success }])
            .Add(c => c.ItemTemplate, item => builder => builder.AddContent(0, $"Action: {item.Label}")));

        cut.FindAll("[role='listitem']").Select(e => e.TextContent).First().Should().Contain("Action: Later");
        cut.FindAll("[role='listitem']").Should().HaveCount(2);
        cut.FindAll("[data-slot='event-timeline-connector']").Should().HaveCount(1);
        cut.FindAll("[data-slot='event-timeline-marker']").Should().HaveCount(2);
        cut.Find("time").GetAttribute("datetime").Should().Be(when.AddHours(1).ToString("O"));
        cut.FindAll("button").Should().BeEmpty();
        cut.FindAll("[data-slot='event-timeline-marker']")[0].ClassName.Should().Contain("emerald");
    }

    [Test]
    public void Event_timeline_vertical_defaults_to_label_and_handles_empty_items()
    {
        var cut = Render<EventTimeline>(p => p
            .Add(c => c.Layout, EventTimelineLayout.Vertical)
            .Add(c => c.Items, [new EventTimelineItem { Label = "Reindexed" }]));
        cut.Find("[role='listitem']").TextContent.Should().Contain("Reindexed");
        cut.FindAll("[data-slot='event-timeline-connector']").Should().BeEmpty();
        cut.Render(p => p.Add(c => c.Items, []));
        cut.Markup.Should().Contain("No timeline events yet.");
        cut.FindAll("[role='listitem']").Should().BeEmpty();
    }

    private static RenderFragment BuildTimelineItems()
    {
        return builder =>
        {
            builder.OpenComponent<TimelineItem>(0);
            builder.AddAttribute(1, nameof(TimelineItem.Step), 1);
            builder.AddAttribute(2, nameof(TimelineItem.ChildContent), BuildTimelineItemContent("March 2024", "Project Initialized", "Repository and architecture setup completed."));
            builder.CloseComponent();

            builder.OpenComponent<TimelineItem>(4);
            builder.AddAttribute(5, nameof(TimelineItem.Step), 2);
            builder.AddAttribute(6, nameof(TimelineItem.ChildContent), BuildTimelineItemContent("April 2024", "Beta Release", "Early tester rollout started."));
            builder.CloseComponent();
        };
    }

    private static RenderFragment BuildTimelineItemContent(string date, string title, string content)
    {
        return builder =>
        {
            builder.OpenComponent<TimelineHeader>(0);
            builder.AddAttribute(1, nameof(TimelineHeader.ChildContent), (RenderFragment) (headerBuilder =>
            {
                headerBuilder.OpenComponent<TimelineDate>(0);
                headerBuilder.AddAttribute(1, nameof(TimelineDate.ChildContent), (RenderFragment) (dateBuilder => dateBuilder.AddContent(0, date)));
                headerBuilder.CloseComponent();

                headerBuilder.OpenComponent<TimelineTitle>(2);
                headerBuilder.AddAttribute(3, nameof(TimelineTitle.ChildContent), (RenderFragment) (titleBuilder => titleBuilder.AddContent(0, title)));
                headerBuilder.CloseComponent();
            }));
            builder.CloseComponent();

            builder.OpenComponent<TimelineIndicator>(2);
            builder.CloseComponent();

            builder.OpenComponent<TimelineSeparator>(3);
            builder.CloseComponent();

            builder.OpenComponent<TimelineContent>(4);
            builder.AddAttribute(5, nameof(TimelineContent.ChildContent), (RenderFragment) (contentBuilder => contentBuilder.AddContent(0, content)));
            builder.CloseComponent();
        };
    }
}
