using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed class SizeBuilderShadcnTests
{
    [Fact]
    public void Size_is5_maps_to_tailwind_size_5()
    {
        Assert.Equal("size-5", Size.Is5.ToClass());
    }

    [Fact]
    public void Size_builder_is5_maps_to_tailwind_size_5()
    {
        Assert.Equal("size-5", Size.Default.Is5.ToClass());
    }

    [Fact]
    public void Size_sm_remains_sm_for_legacy_component_size_switches()
    {
        Assert.Equal("sm", Size.Small.ToClass());
    }
}
