using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void TagInput_renders_inline_tags_and_input()
    {
        var cut = Render<TagInput>(parameters => parameters
            .Add(p => p.Placeholder, "Enter a topic")
            .Add(p => p.Values, ["Sports", "Programming", "Travel"]));

        cut.Find("[data-slot='tag-input']").Should().NotBeNull();
        cut.FindAll("[data-slot='tag-input-tag']").Should().HaveCount(3);
        cut.Find("input").GetAttribute("placeholder").Should().BeEmpty();
    }

    [Test]
    public async Task TagInput_adds_tag_from_enter_key()
    {
        string[] values = ["Sports"];

        var cut = Render<TagInput>(parameters => parameters
            .Add(p => p.Placeholder, "Enter a topic")
            .Add(p => p.Values, values)
            .Add(p => p.ValuesChanged, next =>
            {
                values = next;
                return Task.CompletedTask;
            }));

        var input = cut.Find("input");
        await input.InputAsync(new ChangeEventArgs { Value = "Programming" });
        await input.KeyDownAsync(new KeyboardEventArgs { Key = "Enter" });

        values.Should().Equal("Sports", "Programming");
    }

    [Test]
    public async Task TagInput_removes_tag_from_button()
    {
        string[] values = ["Sports", "Programming"];

        var cut = Render<TagInput>(parameters => parameters
            .Add(p => p.Values, values)
            .Add(p => p.ValuesChanged, next =>
            {
                values = next;
                return Task.CompletedTask;
            }));

        await cut.Find("[aria-label='Remove Sports']").ClickAsync(new MouseEventArgs());

        values.Should().Equal("Programming");
    }
}
