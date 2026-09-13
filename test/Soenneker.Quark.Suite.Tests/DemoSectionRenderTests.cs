using AwesomeAssertions;
using Bunit;
using Soenneker.Quark.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void DemoSection_creates_editor_only_when_code_is_expanded()
    {
        var code = "first\nsecond\nthird\nfourth\nfifth";
        var cut = Render<DemoSection>(parameters => parameters
            .Add(p => p.Code, code)
            .Add(p => p.ChildContent, "Preview"));

        cut.FindComponents<CodeEditor>().Should().BeEmpty();
        cut.Find("pre code").TextContent.Should().Be("first\nsecond\nthird\nfourth\n");
        cut.Find("[data-slot='code'] button").Click();
        cut.FindComponent<CodeEditor>().Instance.Text.Should().Be(code);
        cut.FindAll("pre code").Should().BeEmpty();
    }

    [Test]
    public void DemoSection_title_anchor_is_opt_in()
    {
        var cut = Render<DemoSection>(parameters => parameters
            .Add(p => p.Title, "Default")
            .Add(p => p.ChildContent, "Preview"));

        var heading = cut.Find("h2");

        heading.GetAttribute("id").Should().Be("default");
        cut.FindAll("h2 a[href='#default']").Should().BeEmpty();
    }

    [Test]
    public void DemoSection_show_title_anchor_renders_hash_link()
    {
        var cut = Render<DemoSection>(parameters => parameters
            .Add(p => p.Title, "Multiple")
            .Add(p => p.ShowTitleAnchor, true)
            .Add(p => p.ChildContent, "Preview"));

        var heading = cut.Find("h2");
        var anchor = cut.Find("h2 a[href='#multiple']");
        var hash = cut.Find("h2 a span[aria-hidden='true']");

        heading.GetAttribute("id").Should().Be("multiple");
        heading.GetAttribute("class").Should().Contain("scroll-m-24");
        anchor.TextContent.Should().Contain("Multiple");
        anchor.GetAttribute("class").Should().Contain("group/title-anchor");
        hash.TextContent.Should().Be("#");
        hash.GetAttribute("class").Should().Contain("opacity-0");
        hash.GetAttribute("class").Should().Contain("group-hover/title-anchor:opacity-100");
    }
}
