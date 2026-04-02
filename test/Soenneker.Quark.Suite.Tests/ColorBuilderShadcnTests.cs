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
        Assert.Equal("bg-destructive", BackgroundColor.Destructive.ToClass());
    }

    [Fact]
    public void BorderColor_input_maps_to_border_input()
    {
        Assert.Equal("border-input", BorderColor.Input.ToClass());
    }

    [Fact]
    public void RingColor_destructive_maps_to_ring_destructive()
    {
        Assert.Equal("ring-destructive", RingColor.Destructive.ToClass());
    }

    [Fact]
    public void BackgroundColor_token_maps_to_bg_blue_500()
    {
        Assert.Equal("bg-blue-500", BackgroundColor.Token("blue-500").ToClass());
    }

    [Fact]
    public void BackgroundColor_utility_passthrough_maps_directly()
    {
        Assert.Equal("bg-primary/20", BackgroundColor.Utility("bg-primary/20").ToClass());
    }
}
