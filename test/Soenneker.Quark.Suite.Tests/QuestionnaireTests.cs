using System;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
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

        cut.Find("input[value='alpha']").Click();
        cut.Find("button[data-slot='questionnaire-next']").Click();
        cut.Find("[data-slot='questionnaire-progress']").TextContent.Should().Be("Question 2 of 2");

        cut.Find("input[value='gamma']").Click();
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
