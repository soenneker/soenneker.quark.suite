using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using System;
using System.IO;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void ModelSelector_uses_shadcn_playground_popover_command_contract()
    {
        var cut = Render<ModelSelector>(parameters => parameters
            .Add(p => p.Value, "gpt-5.5")
            .Add(p => p.Options, new[]
            {
                new ModelSelectorOption("gpt-5.5", "GPT-5.5", "OpenAI", "Most capable"),
                new ModelSelectorOption("gpt-5.4", "GPT-5.4", "OpenAI", "Balanced")
            })
            .Add(p => p.SubmenuOptions, new[]
            {
                new ModelSelectorOption("gemini-2.5-pro", "Gemini 2.5 Pro", "Google", "Reasoning")
            }));

        var trigger = cut.Find("[role='combobox']");
        trigger.GetAttribute("aria-label").Should().Be("Select a model");
        trigger.TextContent.Should().Contain("GPT-5.5");
        trigger.GetAttribute("class")!.Should().Contain("w-full");
        trigger.GetAttribute("class")!.Should().Contain("justify-between");

        var source = File.ReadAllText(Path.Combine(GetSuiteRootForModelSelector(), "src", "Soenneker.Quark.Suite", "Components", "ModelSelector", "ModelSelector.razor"));
        source.Should().Contain("<Popover");
        source.Should().Contain("<Command>");
        source.Should().Contain("<CommandInput Placeholder=\"@SearchPlaceholder\"");
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
