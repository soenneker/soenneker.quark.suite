using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    private static readonly CascaderOption[] CascaderOptions =
    [
        new()
        {
            Value = "usa",
            Label = "USA",
            Children =
            [
                new()
                {
                    Value = "new_york",
                    Label = "New York",
                    Children = [new() { Value = "statue_of_liberty", Label = "Statue of Liberty" }]
                }
            ]
        },
        new()
        {
            Value = "france",
            Label = "France",
            Children = [new() { Value = "paris", Label = "Paris" }]
        }
    ];

    [Test]
    public void Cascader_renders_trigger_with_placeholder()
    {
        var cut = Render<Cascader>(parameters => parameters
            .Add(p => p.Options, CascaderOptions)
            .Add(p => p.Placeholder, "Please select"));

        cut.Find("[data-slot='cascader']").Should().NotBeNull();
        cut.Find("button").TextContent.Should().Contain("Please select");
    }

    [Test]
    public void Cascader_displays_default_value_path()
    {
        var cut = Render<Cascader>(parameters => parameters
            .Add(p => p.Options, CascaderOptions)
            .Add(p => p.DefaultValue, ["usa", "new_york", "statue_of_liberty"]));

        cut.Find("button").TextContent.Should().Contain("USA / New York / Statue of Liberty");
    }

    [Test]
    public async Task Cascader_selects_leaf_option()
    {
        string[]? selected = null;

        var cut = Render<Cascader>(parameters => parameters
            .Add(p => p.Options, CascaderOptions)
            .Add(p => p.ValueChanged, value =>
            {
                selected = value;
                return Task.CompletedTask;
            }));

        await cut.Find("button").ClickAsync(new MouseEventArgs());
        await cut.Find("[data-value='usa']").ClickAsync(new MouseEventArgs());
        await cut.Find("[data-value='new_york']").ClickAsync(new MouseEventArgs());
        await cut.Find("[data-value='statue_of_liberty']").ClickAsync(new MouseEventArgs());

        selected.Should().Equal("usa", "new_york", "statue_of_liberty");
    }

    [Test]
    public async Task Cascader_clear_button_clears_selection()
    {
        string[]? selected = ["usa", "new_york", "statue_of_liberty"];

        var cut = Render<Cascader>(parameters => parameters
            .Add(p => p.Options, CascaderOptions)
            .Add(p => p.Value, selected)
            .Add(p => p.ValueChanged, value =>
            {
                selected = value;
                return Task.CompletedTask;
            }));

        await cut.Find("[aria-label='Clear selection']").ClickAsync(new MouseEventArgs());

        selected.Should().BeEmpty();
    }
}
