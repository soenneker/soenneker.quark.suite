using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Announcement_renders_pill_structure()
    {
        var cut = Render<Announcement>(parameters => parameters
            .Add(p => p.TagText, "New")
            .Add(p => p.Href, "#updates")
            .Add(p => p.ChildContent, "Introducing Quark Suite components"));

        var root = cut.Find("[data-slot='announcement']");
        root.TagName.Should().Be("A");
        root.GetAttribute("href").Should().Be("#updates");
        root.ClassList.Should().Contain("rounded-full");
        root.ClassList.Should().Contain("hover:shadow-md");
        cut.Find("[data-slot='announcement-tag']").TextContent.Should().Be("New");
        cut.Find("[data-slot='announcement-text']").TextContent.Should().Contain("Introducing Quark Suite components");
        cut.Find("[data-slot='announcement-chevron']").Should().NotBeNull();
    }

    [Test]
    public void Announcement_applies_themed_classes()
    {
        var cut = Render<Announcement>(parameters => parameters
            .Add(p => p.TagText, "Beta")
            .Add(p => p.Themed, true)
            .Add(p => p.ChildContent, "Try the updated component playground"));

        var root = cut.Find("[data-slot='announcement']");
        root.ClassList.Should().Contain("bg-primary/5");
        cut.Find("[data-slot='announcement-tag']").ClassList.Should().Contain("bg-primary");
    }

    [Test]
    public void Announcement_can_hide_chevron()
    {
        var cut = Render<Announcement>(parameters => parameters
            .Add(p => p.TagText, "Stable")
            .Add(p => p.ShowChevron, false)
            .Add(p => p.ChildContent, "All systems operational"));

        cut.FindAll("[data-slot='announcement-chevron']").Should().BeEmpty();
    }
}
