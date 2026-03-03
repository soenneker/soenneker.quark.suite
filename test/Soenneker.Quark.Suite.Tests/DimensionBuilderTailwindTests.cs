using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed class DimensionBuilderTailwindTests
{
    [Fact]
    public void Width_percent_alias_maps_to_tailwind_fraction()
    {
        Assert.Equal("w-1/4", Width.Is25.ToClass());
    }

    [Fact]
    public void Width_screen_maps_to_tailwind_width_screen()
    {
        Assert.Equal("w-screen", Width.IsScreen.ToClass());
    }

    [Fact]
    public void Width_custom_token_maps_to_tailwind_width_utility()
    {
        Assert.Equal("w-72", Width.Token("72").ToClass());
    }

    [Fact]
    public void Height_percent_alias_maps_to_tailwind_fraction()
    {
        Assert.Equal("h-1/2", Height.Is50.ToClass());
    }

    [Fact]
    public void Height_screen_maps_to_tailwind_height_screen()
    {
        Assert.Equal("h-screen", Height.IsScreen.ToClass());
    }

    [Fact]
    public void Height_custom_token_maps_to_tailwind_height_utility()
    {
        Assert.Equal("h-72", Height.Token("72").ToClass());
    }
}
