using AwesomeAssertions;
using Bunit;
using System.Threading.Tasks;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public async Task ConsentManager_defaults_banner_to_bottom_center()
    {
        var cut = Render<ConsentManager>(parameters => parameters
            .Add(p => p.AutoInitialize, false));

        await cut.InvokeAsync(() => cut.Instance.Initialize().AsTask());

        var banner = cut.Find("[role='dialog']");
        banner.ClassList.Should().Contain("bottom-6");
        banner.ClassList.Should().Contain("max-sm:bottom-24");
        banner.ClassList.Should().Contain("left-1/2");
        banner.ClassList.Should().Contain("-translate-x-1/2");
        banner.ClassList.Should().NotContain("left-6");
        banner.ClassList.Should().NotContain("right-6");
    }

    [Test]
    public async Task ConsentManager_can_place_banner_on_left_side()
    {
        var cut = Render<ConsentManager>(parameters => parameters
            .Add(p => p.AutoInitialize, false)
            .Add(p => p.Side, ConsentManagerSide.Left));

        await cut.InvokeAsync(() => cut.Instance.Initialize().AsTask());

        var banner = cut.Find("[role='dialog']");
        banner.ClassList.Should().Contain("left-6");
        banner.ClassList.Should().NotContain("left-1/2");
        banner.ClassList.Should().NotContain("right-6");
    }

    [Test]
    public async Task ConsentManager_can_place_banner_on_right_side()
    {
        var cut = Render<ConsentManager>(parameters => parameters
            .Add(p => p.AutoInitialize, false)
            .Add(p => p.Side, ConsentManagerSide.Right));

        await cut.InvokeAsync(() => cut.Instance.Initialize().AsTask());

        var banner = cut.Find("[role='dialog']");
        banner.ClassList.Should().Contain("right-6");
        banner.ClassList.Should().NotContain("left-1/2");
        banner.ClassList.Should().NotContain("left-6");
    }

    [Test]
    public async Task ConsentManager_keeps_explicit_horizontal_position_class()
    {
        var cut = Render<ConsentManager>(parameters => parameters
            .Add(p => p.AutoInitialize, false)
            .Add(p => p.Side, ConsentManagerSide.Right)
            .Add(p => p.PositionClass, "absolute bottom-6 left-6"));

        await cut.InvokeAsync(() => cut.Instance.Initialize().AsTask());

        var banner = cut.Find("[role='dialog']");
        banner.ClassList.Should().Contain("left-6");
        banner.ClassList.Should().NotContain("right-6");
    }
}
