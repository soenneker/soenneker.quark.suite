using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Tests;

public sealed class ColorBuilderShadcnTests
{
    [Test]
    public void TextColor_destructive_maps_to_text_destructive()
    {
        TextColor.Destructive.ToClass().Should().Be("text-destructive");
    }

    [Test]
    public void TextColor_primary_foreground_maps_to_text_primary_foreground()
    {
        TextColor.PrimaryForeground.ToClass().Should().Be("text-primary-foreground");
    }

    [Test]
    public void BackgroundColor_card_maps_to_bg_card()
    {
        BackgroundColor.Card.ToClass().Should().Be("bg-card");
    }

    [Test]
    public void BackgroundColor_danger_alias_maps_to_bg_destructive()
    {
        BackgroundColor.Destructive.ToClass().Should().Be("bg-destructive");
    }

    [Test]
    public void BorderColor_input_maps_to_border_input()
    {
        BorderColor.Input.ToClass().Should().Be("border-input");
    }

    [Test]
    public void RingColor_destructive_maps_to_ring_destructive()
    {
        RingColor.Destructive.ToClass().Should().Be("ring-destructive");
    }

    [Test]
    public void BackgroundColor_token_maps_to_raw_blue_500_suffix()
    {
        BackgroundColor.Token("blue-500").ToClass().Should().Be("blue-500");
    }

    [Test]
    public void BackgroundColor_utility_passthrough_maps_directly()
    {
        BackgroundColor.Utility("bg-primary/20").ToClass().Should().Be("bg-primary/20");
    }
}
