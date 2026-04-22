using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Tests;

public sealed class SizeBuilderShadcnTests
{
    [Test]
    public void Size_is5_maps_to_tailwind_size_5()
    {
        Size.Is5.ToClass().Should().Be("size-5");
    }

    [Test]
    public void Size_builder_is5_maps_to_tailwind_size_5()
    {
        Size.Token("5").ToClass().Should().Be("size-5");
    }
}
