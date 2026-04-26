using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using System;
using System.IO;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void ModelSelector_uses_nexus_radio_dropdown_contract()
    {
        var cut = Render<ModelSelector>(parameters => parameters
            .Add(p => p.Value, "gpt-4")
            .Add(p => p.Options, new[]
            {
                new ModelSelectorOption("gpt-4", "GPT-4", "OpenAI", "Most capable, best for complex tasks"),
                new ModelSelectorOption("gpt-4o-mini", "GPT-4o Mini", "OpenAI", "Fast and affordable")
            }));

        var trigger = cut.Find("[role='button']");
        trigger.GetAttribute("aria-label").Should().Be("Select a model");
        trigger.TextContent.Should().Contain("GPT-4");
        trigger.GetAttribute("class")!.Should().Contain("rounded-full");
        trigger.GetAttribute("class")!.Should().Contain("bg-muted");

        var source = File.ReadAllText(Path.Combine(GetSuiteRootForModelSelector(), "src", "Soenneker.Quark.Suite", "Components", "ModelSelector", "ModelSelector.razor"));
        source.Should().Contain("<Popover");
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
