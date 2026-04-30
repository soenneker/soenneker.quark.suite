using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void DialogFooter_matches_shadcn_default_component_contract()
    {
        var footer = Render<DialogFooter>(parameters => parameters
            .Add(p => p.ChildContent, "Footer"));

        var footerClasses = footer.Find("[data-slot='dialog-footer']").GetAttribute("class")!;

        footerClasses.Should().Contain("flex");
        footerClasses.Should().Contain("flex-col-reverse");
        footerClasses.Should().Contain("gap-2");
        footerClasses.Should().Contain("sm:flex-row");
        footerClasses.Should().Contain("sm:justify-end");
        footerClasses.Should().NotContain("bg-muted");
        footerClasses.Should().NotContain("rounded-b-xl");
        footerClasses.Should().NotContain("border-t");
    }

    [Test]
    public void DialogFooter_can_render_current_shadcn_close_button_option()
    {
        var cut = Render<Dialog>(parameters => parameters
            .Add(p => p.Visible, true)
            .AddChildContent<DialogContent>(contentParameters => contentParameters
                .Add(p => p.ShowCloseButton, false)
                .AddChildContent<DialogFooter>(footerParameters => footerParameters
                    .Add(p => p.ShowCloseButton, true)
                    .Add(p => p.ChildContent, "Actions"))));

        var footer = cut.Find("[data-slot='dialog-footer']");
        footer.TextContent.Should().Contain("Actions");
        footer.TextContent.Should().Contain("Close");

        var close = footer.QuerySelector("[data-slot='dialog-close']");
        close.Should().NotBeNull();
        close!.GetAttribute("class").Should().Contain("border");
        close.GetAttribute("class").Should().Contain("bg-background");
    }
}
