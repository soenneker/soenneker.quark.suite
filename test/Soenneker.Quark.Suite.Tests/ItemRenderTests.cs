using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Item_preserves_explicit_spacing_across_size_changes_and_restores_defaults()
    {
        var cut = Render<Item>(p => p.Add(c => c.ItemSize, ItemSize.ExtraSmall)
            .Add(c => c.Padding, Padding.Is0).Add(c => c.Gap, Gap.Is4));

        cut.Find("[data-slot='item']").ClassList.Should().Contain("p-0").And.Contain("gap-4");
        cut.Find("[data-slot='item']").ClassList.Should().NotContain("px-2.5").And.NotContain("gap-2");

        cut.Render(p => p.Add(c => c.ItemSize, ItemSize.Default));
        cut.Find("[data-slot='item']").ClassList.Should().Contain("p-0").And.Contain("gap-4");
        cut.Find("[data-slot='item']").ClassList.Should().NotContain("px-3").And.NotContain("gap-2.5");

        cut.Render(p => p.Add(c => c.Padding, (CssValue<PaddingBuilder>?)null)
            .Add(c => c.Gap, (CssValue<GapBuilder>?)null));
        cut.Find("[data-slot='item']").ClassList.Should().Contain("px-3").And.Contain("py-2.5").And.Contain("gap-2.5");
    }
}
