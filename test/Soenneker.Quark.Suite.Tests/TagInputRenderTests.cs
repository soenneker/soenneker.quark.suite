using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public async ValueTask TagInput_presets_color_individual_tags_and_preserve_removal(CancellationToken cancellationToken)
    {
        var red = new QuarkPresetToken("test-tag-red", context =>
        {
            context.BackgroundColor = BackgroundColor.Token("red-100");
            context.TextColor = TextColor.Token("red-900");
        });
        var blue = new QuarkPresetToken("test-tag-blue", context =>
        {
            context.BackgroundColor = BackgroundColor.Token("blue-100");
            context.TextColor = TextColor.Token("blue-900");
        });
        QuarkPresetToken? selectedPreset = red;
        string[] values = ["Colored", "Default"];
        var cut = Render<TagInput>(p => p.Add(c => c.Values, values)
            .Add(c => c.TagPresetSelector, tag => tag == "Colored" ? selectedPreset : null)
            .Add(c => c.ValuesChanged, next => values = next));

        var tags = cut.FindAll("[data-slot='tag-input-tag']");
        tags[0].ClassList.Should().Contain("bg-red-100").And.Contain("text-red-900");
        tags[0].ClassList.Should().NotContain("bg-secondary");
        tags[1].ClassList.Should().Contain("bg-secondary");

        selectedPreset = blue;
        cut.Render();
        cut.Find("[data-slot='tag-input-tag']").ClassList.Should().Contain("bg-blue-100").And.NotContain("bg-red-100");

        selectedPreset = null;
        cut.Render();
        cut.Find("[data-slot='tag-input-tag']").ClassList.Should().Contain("bg-secondary").And.NotContain("bg-blue-100");
        await cut.Find("[aria-label='Remove Colored']").ClickAsync(new MouseEventArgs());
        values.Should().Equal("Default");
    }

    [Test]
    public void TagInput_assigns_its_id_and_label_reference_only_to_the_input()
    {
        var cut = Render<TagInput>(p => p.Add(c => c.Id, "topics")
            .Add(c => c.AriaLabelledBy, "topics-label"));

        cut.FindAll("#topics").Should().ContainSingle();
        cut.Find("#topics").TagName.Should().Be("INPUT");
        cut.Find("input").GetAttribute("aria-labelledby").Should().Be("topics-label");
    }

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
    public async ValueTask TagInput_adds_tag_from_enter_key(CancellationToken cancellationToken)
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
    public async ValueTask TagInput_removes_tag_from_button(CancellationToken cancellationToken)
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
    public async ValueTask TagInput_removes_the_selected_duplicate_tag(CancellationToken cancellationToken)
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
