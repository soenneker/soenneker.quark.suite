using AwesomeAssertions;
using Bunit;
using Soenneker.Bradix;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Label_matches_shadcn_base_classes()
    {
        var cut = Render<Label>(parameters => parameters
            .Add(p => p.For, "terms")
            .Add(p => p.ChildContent, "Accept"));

        var classes = cut.Find("[data-slot='label']").GetAttribute("class")!;
        var label = cut.Find("[data-slot='label']");

        label.GetAttribute("for").Should().Be("terms");
        classes.Should().Contain("flex");
        classes.Should().Contain("items-center");
        classes.Should().Contain("gap-2");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("leading-none");
        classes.Should().Contain("font-medium");
        classes.Should().Contain("select-none");
        classes.Should().Contain("group-data-[disabled=true]:pointer-events-none");
        classes.Should().Contain("group-data-[disabled=true]:opacity-50");
        classes.Should().Contain("peer-disabled:cursor-not-allowed");
        classes.Should().Contain("peer-disabled:opacity-50");
        classes.Should().NotContain("q-label");
    }

    [Test]
    public void Label_forwards_mouse_down_callback_to_radix_label_primitive()
    {
        var calls = 0;

        var cut = Render<Label>(parameters => parameters
            .Add(p => p.OnMouseDown, _ => calls++)
            .Add(p => p.ChildContent, "Accept"));

        cut.FindComponent<BradixLabel>().Instance.HandleMouseDownFromJs(new BradixDelegatedMouseEvent { Detail = 1 });

        calls.Should().Be(1);
    }
}
