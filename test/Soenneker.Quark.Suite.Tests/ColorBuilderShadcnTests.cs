using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed class ColorBuilderShadcnTests
{
    [Fact]
    public void TextColor_destructive_maps_to_text_destructive()
    {
        Assert.Equal("text-destructive", TextColor.Destructive.ToClass());
    }

    [Fact]
    public void TextColor_primary_foreground_maps_to_text_primary_foreground()
    {
        Assert.Equal("text-primary-foreground", TextColor.PrimaryForeground.ToClass());
    }

    [Fact]
    public void BackgroundColor_card_maps_to_bg_card()
    {
        Assert.Equal("bg-card", BackgroundColor.Card.ToClass());
    }

    [Fact]
    public void BackgroundColor_danger_alias_maps_to_bg_destructive()
    {
        Assert.Equal("bg-destructive", BackgroundColor.Danger.ToClass());
    }

    [Fact]
    public void BorderColor_input_maps_to_border_input()
    {
        Assert.Equal("border-input", BorderColor.Input.ToClass());
    }

    [Fact]
    public void Color_generic_maps_to_text_classes()
    {
        Assert.Equal("text-popover-foreground", Color.PopoverForeground.ToClass());
    }

    [Fact]
    public void FocusRing_destructive_maps_to_focus_visible_ring_destructive()
    {
        Assert.Equal("focus-visible:ring-destructive", FocusRing.Destructive.ToClass());
    }

    [Fact]
    public void BackgroundColor_from_css_falls_back_to_style()
    {
        Assert.Equal("#123456", BackgroundColor.FromCss("#123456").ToStyle());
    }
}
