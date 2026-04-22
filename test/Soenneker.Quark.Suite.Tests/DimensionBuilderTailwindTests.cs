using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Tests;

public sealed class DimensionBuilderTailwindTests
{
    [Test]
    public void Width_percent_alias_maps_to_tailwind_fraction()
    {
        Width.Is25.ToClass().Should().Be("w-1/4");
    }

    [Test]
    public void Width_screen_maps_to_tailwind_width_screen()
    {
        Width.IsScreen.ToClass().Should().Be("w-screen");
    }

    [Test]
    public void Width_custom_token_maps_to_tailwind_width_utility()
    {
        Width.Token("72").ToClass().Should().Be("w-72");
    }

    [Test]
    public void Height_percent_alias_maps_to_tailwind_fraction()
    {
        Height.Is50.ToClass().Should().Be("h-1/2");
    }

    [Test]
    public void Height_screen_maps_to_tailwind_height_screen()
    {
        Height.IsScreen.ToClass().Should().Be("h-screen");
    }

    [Test]
    public void Height_custom_token_maps_to_tailwind_height_utility()
    {
        Height.Token("72").ToClass().Should().Be("h-72");
    }
}
