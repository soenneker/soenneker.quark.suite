using AwesomeAssertions;
using Bunit;
using Soenneker.Quark.Suite.Demo.Pages.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Date_picker_input_triggers_use_ghost_icon_buttons()
    {
        var cut = Render<DatePickers>();

        var triggers = cut.FindAll("button#date-picker");

        triggers.Should().HaveCount(2);
        triggers.Should().OnlyContain(trigger => trigger.GetAttribute("data-variant") == "ghost");
    }
}
