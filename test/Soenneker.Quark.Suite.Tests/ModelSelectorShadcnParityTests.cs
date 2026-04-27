using AwesomeAssertions;
using System;
using System.IO;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void ModelSelector_uses_nexus_radio_dropdown_contract()
    {
        var source = File.ReadAllText(Path.Combine(GetSuiteRootForModelSelector(), "src", "Soenneker.Quark.Suite", "Components", "ModelSelector", "ModelSelector.razor"));
        source.Should().Contain("<Popover");
        source.Should().Contain("AriaLabel=\"@AriaLabel\"");
        source.Should().Contain("Select a model");
        source.Should().Contain("rounded-full");
        source.Should().Contain("bg-muted");
        source.Should().Contain("data-slot=\"model-selector-radio-item\"");
        source.Should().Contain("data-slot=\"model-selector-label\"");
        source.Should().NotContain("<Command>");
        source.Should().NotContain("<CommandInput");
        source.Should().Contain("<HoverCard");
        source.Should().NotContain("<Dropdown");
        source.Should().NotContain("<DropdownRadioItem");
    }

    private static string GetSuiteRootForModelSelector()
    {
        var directory = AppContext.BaseDirectory;

        for (var i = 0; i < 6; i++)
        {
            directory = Directory.GetParent(directory)!.FullName;
        }

        return directory;
    }
}
