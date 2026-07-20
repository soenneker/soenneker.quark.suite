using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Switch_styles_target_the_state_attribute_emitted_by_bradix()
    {
        var cut = Render<Switch>(parameters => parameters.Add(p => p.Checked, true));

        var control = cut.Find("[role='switch']");
        var thumb = control.QuerySelector("[data-slot='switch-thumb']")!;

        control.GetAttribute("data-state").Should().Be("checked");
        control.GetAttribute("class").Should().Contain("data-[state=checked]:bg-primary");
        control.GetAttribute("class").Should().Contain("data-[state=unchecked]:bg-input");
        control.GetAttribute("class").Should().NotContain("data-checked:");
        control.GetAttribute("class").Should().NotContain("data-unchecked:");

        thumb.GetAttribute("data-state").Should().Be("checked");
        thumb.GetAttribute("class").Should().Contain("data-[state=checked]:translate-x-[calc(100%-2px)]");
        thumb.GetAttribute("class").Should().Contain("data-[state=unchecked]:translate-x-0");
        thumb.GetAttribute("class").Should().NotContain("data-checked:");
        thumb.GetAttribute("class").Should().NotContain("data-unchecked:");
    }
}
