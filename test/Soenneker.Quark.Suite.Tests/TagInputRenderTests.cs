using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void TagInput_large_autocomplete_observes_mutated_values_and_case_insensitive_duplicates()
    {
        var values = new string[16];
        for (int i = 0; i < values.Length; i++) values[i] = $"Tag{i}";
        string[] options = ["TAG0", "Tag1", "Tag2", "Tag3", "Tag4", "Tag5", "Tag6", "New"];
        var cut = Render<TagInput>(p => p.Add(c => c.Values, values).Add(c => c.AutocompleteOptions, options)
            .Add(c => c.EnableAutocomplete, true));
        cut.Find("input").Focus();
        cut.FindAll("[data-slot='tag-input-autocomplete-item']").Should().HaveCount(1);
        values[0] = "Replacement";
        cut.Render(p => p.Add(c => c.Values, values));
        cut.FindAll("[data-slot='tag-input-autocomplete-item']").Should().HaveCount(2);
    }

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
    public async ValueTask TagInput_adds_tag_from_enter_key()
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
    public async ValueTask TagInput_removes_tag_from_button()
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

    [Test]
    public async ValueTask TagInput_removes_the_selected_duplicate_tag()
    {
        string[] values = ["Same", "Other", "Same"];

        var cut = Render<TagInput>(parameters => parameters
            .Add(p => p.AllowDuplicates, true)
            .Add(p => p.Values, values)
            .Add(p => p.ValuesChanged, next =>
            {
                values = next;
                return Task.CompletedTask;
            }));

        await cut.FindAll("[aria-label='Remove Same']")[1].ClickAsync(new MouseEventArgs());

        values.Should().Equal("Same", "Other");
    }
}
