using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Pill_renders_shadcn_pill_surface()
    {
        var cut = Render<Pill>(parameters => parameters
            .Add(p => p.ChildContent, "Online"));

        var root = cut.Find("[data-slot='pill']");
        root.TagName.Should().Be("SPAN");
        root.ClassList.Should().Contain("rounded-full");
        root.ClassList.Should().Contain("shadow-xs");
        root.ClassList.Should().Contain("gap-1.5");
        root.TextContent.Should().Contain("Online");
    }

    [Test]
    public void PillIndicator_renders_variant_and_pulse()
    {
        var cut = Render<PillIndicator>(parameters => parameters
            .Add(p => p.Variant, PillIndicatorVariant.Warning)
            .Add(p => p.Pulse, true));

        var root = cut.Find("[data-slot='pill-indicator']");
        root.GetAttribute("data-variant").Should().Be("warning");
        root.ClassList.Should().Contain("bg-amber-500");
        cut.Find("[data-slot='pill-indicator-pulse']").ClassList.Should().Contain("animate-ping");
    }

    [Test]
    public void PillDelta_and_close_render_composable_slots()
    {
        var cut = Render<Pill>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment) (builder =>
            {
                builder.AddContent(0, "Revenue");
                builder.OpenComponent<PillDelta>(1);
                builder.AddAttribute(2, nameof(PillDelta.Direction), PillDeltaDirection.Down);
                builder.AddAttribute(3, nameof(PillDelta.ChildContent), (RenderFragment) (deltaBuilder => deltaBuilder.AddContent(0, "8%")));
                builder.CloseComponent();
                builder.OpenComponent<PillClose>(4);
                builder.AddAttribute(5, nameof(PillClose.Label), "Remove revenue filter");
                builder.CloseComponent();
            })));

        cut.Find("[data-slot='pill-delta']").GetAttribute("data-direction").Should().Be("down");
        cut.Find("[data-slot='pill-delta']").ClassList.Should().Contain("text-red-600");
        cut.Find("[data-slot='pill-close']").TextContent.Should().Contain("Remove revenue filter");
    }
}
