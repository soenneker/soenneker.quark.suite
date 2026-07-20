using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Badge_uses_default_variant_colors_when_typed_colors_are_not_set()
    {
        var cut = Render<Badge>(parameters => parameters
            .Add(component => component.ChildContent, "Active"));

        var badge = cut.Find("[data-slot='badge']");

        badge.ClassList.Should().Contain("border-transparent");
        badge.ClassList.Should().Contain("bg-primary");
        badge.ClassList.Should().Contain("text-primary-foreground");
    }

    [Test]
    public void Badge_typed_colors_replace_default_variant_colors()
    {
        var cut = Render<Badge>(parameters => parameters
            .Add(component => component.BackgroundColor, BackgroundColor.Secondary)
            .Add(component => component.TextColor, TextColor.Destructive)
            .Add(component => component.BorderColor, BorderColor.Primary)
            .Add(component => component.ChildContent, "Error"));

        var badge = cut.Find("[data-slot='badge']");

        badge.ClassList.Should().Contain("bg-secondary");
        badge.ClassList.Should().Contain("text-destructive");
        badge.ClassList.Should().Contain("border-primary");
        badge.ClassList.Should().NotContain("bg-primary");
        badge.ClassList.Should().NotContain("text-primary-foreground");
        badge.ClassList.Should().NotContain("border-transparent");
    }

    [Test]
    public void Badge_preset_colors_replace_default_variant_colors()
    {
        var colors = new QuarkPresetToken("status-colors", static context =>
        {
            context.BackgroundColor = BackgroundColor.Token("[var(--status-bg)]");
            context.TextColor = TextColor.Token("[var(--status-text)]");
            context.BorderColor = BorderColor.Token("[var(--status-border)]");
        });

        var cut = Render<Badge>(parameters => parameters
            .Add(component => component.Preset, colors)
            .Add(component => component.ChildContent, "Error"));

        var badge = cut.Find("[data-slot='badge']");

        badge.ClassList.Should().Contain("bg-[var(--status-bg)]");
        badge.ClassList.Should().Contain("text-[var(--status-text)]");
        badge.ClassList.Should().Contain("border-[var(--status-border)]");
        badge.ClassList.Should().NotContain("bg-primary");
        badge.ClassList.Should().NotContain("text-primary-foreground");
        badge.ClassList.Should().NotContain("border-transparent");
    }

    [Test]
    public void Anchor_preserves_aria_current()
    {
        var cut = Render<Anchor>(parameters => parameters
            .Add(p => p.Href, "/docs")
            .Add(p => p.AriaCurrent, "page")
            .Add(p => p.ChildContent, "Docs"));

        var anchor = cut.Find("a");

        anchor.GetAttribute("href").Should().Be("/docs");
        anchor.GetAttribute("aria-current").Should().Be("page");
    }

    [Test]
    public void Anchor_preserves_aria_hidden()
    {
        var cut = Render<Anchor>(parameters => parameters
            .Add(p => p.Href, "/docs")
            .Add(p => p.AriaHidden, true)
            .Add(p => p.ChildContent, "Docs"));

        var anchor = cut.Find("a");

        anchor.GetAttribute("href").Should().Be("/docs");
        anchor.GetAttribute("aria-hidden").Should().Be("true");
    }
}
