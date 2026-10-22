using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using System;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public async Task CollectionAutoSave_tags_save_committed_add_remove_and_clear()
    {
        var saves = new List<string[]>();
        var cut = Render<TagInput>(p => p.Add(c => c.AutoSave, true).Add(c => c.AutoSaveDelay, 0)
            .Add(c => c.ClearAll, true)
            .Add(c => c.OnAutoSave, (value, _) => { saves.Add(value); return ValueTask.CompletedTask; }));
        saves.Should().BeEmpty();
        await cut.Find("input").InputAsync(new ChangeEventArgs { Value = "one" });
        saves.Should().BeEmpty();
        await cut.Find("input").KeyDownAsync(new KeyboardEventArgs { Key = "Enter" });
        cut.WaitForAssertion(() => saves.Should().ContainSingle());
        saves[0].Should().Equal("one");
        await cut.Find("input").InputAsync(new ChangeEventArgs { Value = "two" });
        await cut.Find("input").KeyDownAsync(new KeyboardEventArgs { Key = "Enter" });
        cut.WaitForAssertion(() => saves.Should().HaveCount(2));
        saves[0].Should().Equal("one");
        await cut.Find("input").KeyDownAsync(new KeyboardEventArgs { Key = "Backspace" });
        await cut.Find("input").KeyDownAsync(new KeyboardEventArgs { Key = "Backspace" });
        cut.WaitForAssertion(() => saves.Should().HaveCount(3));
        saves[2].Should().Equal("one");
        await cut.Find("[data-slot='tag-input-clear-all']").ClickAsync(new MouseEventArgs());
        cut.WaitForAssertion(() => saves.Should().HaveCount(4));
        saves[3].Should().BeEmpty();
    }

    [Test]
    public async Task CollectionAutoSave_questionnaire_saves_whole_answers_and_reset()
    {
        var saves = new List<IReadOnlyDictionary<string, IReadOnlyList<string>>>();
        var cut = Render<Questionnaire>(p => p.Add(c => c.AutoSave, true).Add(c => c.AutoSaveDelay, 0)
            .Add(c => c.ChildContent, BuildQuestionnaireContent())
            .Add(c => c.OnAutoSave, (value, _) => { saves.Add(value); return ValueTask.CompletedTask; }));
        saves.Should().BeEmpty();
        await cut.Find("input[value='alpha']").ChangeAsync(new ChangeEventArgs { Value = "alpha" });
        cut.WaitForAssertion(() => saves.Should().ContainSingle());
        saves[0]["first"].Should().Equal("alpha");
        await cut.Find("button[data-slot='questionnaire-next']").ClickAsync(new MouseEventArgs());
        saves.Should().ContainSingle();
        await cut.Find("input[value='gamma']").ChangeAsync(new ChangeEventArgs { Value = "gamma" });
        cut.WaitForAssertion(() => saves.Should().HaveCount(2));
        saves[1]["first"].Should().Equal("alpha");
        saves[1]["second"].Should().Equal("gamma");
        saves[0].Should().NotContainKey("second");
        await cut.Find("form").TriggerEventAsync("onreset", EventArgs.Empty);
        cut.WaitForAssertion(() => saves.Should().HaveCount(3));
        saves[2].Should().BeEmpty();
    }

    [Test]
    public async Task CollectionAutoSave_questionnaire_debounces_text_without_saving_initial_parameters()
    {
        var saves = new List<IReadOnlyDictionary<string, IReadOnlyList<string>>>();
        var cut = Render<Questionnaire>(p => p.Add(c => c.AutoSave, true).Add(c => c.AutoSaveDelay, 150)
            .Add(c => c.OnAutoSave, (value, _) => { saves.Add(value); return ValueTask.CompletedTask; })
            .AddChildContent<QuestionnaireItem>(item => item.Add(c => c.Name, "text")
                .AddChildContent<QuestionnaireInput>(input => input.Add(c => c.Value, "initial"))));
        saves.Should().BeEmpty();
        await cut.Find("input").InputAsync(new ChangeEventArgs { Value = "first" });
        await cut.Find("input").InputAsync(new ChangeEventArgs { Value = "latest" });
        cut.WaitForAssertion(() => saves.Should().ContainSingle());
        saves[0]["text"].Should().Equal("latest");
    }

    [Test]
    public async Task CollectionAutoSave_is_opt_in_and_reports_failure_then_recovery()
    {
        var attempts = 0;
        var cut = Render<TagInput>(p => p.Add(c => c.AutoSaveDelay, 0)
            .Add(c => c.OnAutoSave, (_, _) => ++attempts == 1
                ? ValueTask.FromException(new InvalidOperationException("Save failed")) : ValueTask.CompletedTask));
        await cut.Find("input").InputAsync(new ChangeEventArgs { Value = "one" });
        await cut.Find("input").KeyDownAsync(new KeyboardEventArgs { Key = "Enter" });
        attempts.Should().Be(0);
        cut.Render(p => p.Add(c => c.AutoSave, true));
        await cut.Find("input").InputAsync(new ChangeEventArgs { Value = "two" });
        await cut.Find("input").KeyDownAsync(new KeyboardEventArgs { Key = "Enter" });
        cut.WaitForAssertion(() => cut.Find("[data-slot='autosave-status']").GetAttribute("data-state").Should().Be("failed"));
        await cut.Find("input").InputAsync(new ChangeEventArgs { Value = "three" });
        await cut.Find("input").KeyDownAsync(new KeyboardEventArgs { Key = "Enter" });
        cut.WaitForAssertion(() => cut.Find("[data-slot='autosave-status']").GetAttribute("data-state").Should().Be("saved"));
        attempts.Should().Be(2);
    }

    [Test]
    public void Questionnaire_renders_one_active_item_with_progress_and_shortcuts()
    {
        var cut = RenderQuestionnaire();

        cut.Find("[data-slot='questionnaire-progress']").TextContent.Should().Be("Question 1 of 2");
        cut.FindAll("fieldset[data-slot='questionnaire-item']").Should().HaveCount(2);
        var items = cut.FindAll("fieldset[data-slot='questionnaire-item']");
        items[0].HasAttribute("hidden").Should().BeFalse();
        items[1].HasAttribute("hidden").Should().BeTrue();

        var inputs = cut.FindAll("input[data-slot='questionnaire-choice-input']");
        inputs[0].GetAttribute("aria-keyshortcuts").Should().Be("A");
        inputs[1].GetAttribute("aria-keyshortcuts").Should().Be("B");
    }

    [Test]
    public void Questionnaire_validates_then_advances_and_submits_answers()
    {
        QuestionnaireSubmitEventArgs? submitted = null;
        var cut = RenderQuestionnaire(args => submitted = args);

        cut.Find("button[data-slot='questionnaire-next']").Click();
        cut.Find("[data-slot='questionnaire-error']").HasAttribute("hidden").Should().BeFalse();
        cut.Find("[data-slot='questionnaire-progress']").TextContent.Should().Be("Question 1 of 2");

        cut.Find("input[value='alpha']").Change("alpha");
        cut.Find("button[data-slot='questionnaire-next']").Click();
        cut.Find("[data-slot='questionnaire-progress']").TextContent.Should().Be("Question 2 of 2");

        cut.Find("input[value='gamma']").Change("gamma");
        cut.Find("form[data-slot='questionnaire']").Submit();

        submitted.Should().NotBeNull();
        submitted!.Get("first").Should().Be("alpha");
        submitted.Get("second").Should().Be("gamma");
    }

    private IRenderedComponent<Questionnaire> RenderQuestionnaire(Action<QuestionnaireSubmitEventArgs>? onSubmit = null)
    {
        return Render<Questionnaire>(parameters =>
        {
            parameters.Add(p => p.Shortcuts, QuestionnaireShortcutMode.Letters);
            if (onSubmit is not null)
                parameters.Add(p => p.OnSubmit, onSubmit);
            parameters.Add(p => p.ChildContent, BuildQuestionnaireContent());
        });
    }

    private static RenderFragment BuildQuestionnaireContent() => builder =>
    {
        var sequence = 0;
        builder.OpenComponent<QuestionnaireProgress>(sequence++);
        builder.CloseComponent();

        AddItem(builder, ref sequence, "first", true, ("alpha", "Alpha"), ("beta", "Beta"));
        AddItem(builder, ref sequence, "second", true, ("gamma", "Gamma"));

        builder.OpenComponent<QuestionnaireActions>(sequence++);
        builder.AddAttribute(sequence++, nameof(QuestionnaireActions.ChildContent), (RenderFragment)(actions =>
        {
            actions.OpenComponent<QuestionnairePrevious>(0);
            actions.CloseComponent();
            actions.OpenComponent<QuestionnaireSkip>(1);
            actions.CloseComponent();
            actions.OpenComponent<QuestionnaireNext>(2);
            actions.CloseComponent();
            actions.OpenComponent<QuestionnaireSubmit>(3);
            actions.CloseComponent();
        }));
        builder.CloseComponent();
    };

    private static void AddItem(RenderTreeBuilder builder, ref int sequence, string name, bool required, params (string Value, string Label)[] choices)
    {
        builder.OpenComponent<QuestionnaireItem>(sequence++);
        builder.AddAttribute(sequence++, nameof(QuestionnaireItem.Name), name);
        builder.AddAttribute(sequence++, nameof(QuestionnaireItem.Required), required);
        builder.AddAttribute(sequence++, nameof(QuestionnaireItem.ChildContent), (RenderFragment)(item =>
        {
            var childSequence = 0;
            item.OpenComponent<QuestionnaireTitle>(childSequence++);
            item.AddAttribute(childSequence++, nameof(QuestionnaireTitle.ChildContent), (RenderFragment)(content => content.AddContent(0, name)));
            item.CloseComponent();
            item.OpenComponent<QuestionnaireChoices>(childSequence++);
            item.AddAttribute(childSequence++, nameof(QuestionnaireChoices.ChildContent), (RenderFragment)(choiceBuilder =>
            {
                var choiceSequence = 0;
                foreach ((var value, var label) in choices)
                {
                    choiceBuilder.OpenComponent<QuestionnaireChoice>(choiceSequence++);
                    choiceBuilder.AddAttribute(choiceSequence++, nameof(QuestionnaireChoice.Value), value);
                    choiceBuilder.AddAttribute(choiceSequence++, nameof(QuestionnaireChoice.ChildContent), (RenderFragment)(content => content.AddContent(0, label)));
                    choiceBuilder.CloseComponent();
                }
            }));
            item.CloseComponent();
            item.OpenComponent<QuestionnaireError>(childSequence++);
            item.CloseComponent();
        }));
        builder.CloseComponent();
    }
}
