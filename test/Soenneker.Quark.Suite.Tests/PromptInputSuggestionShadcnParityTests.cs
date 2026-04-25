using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void PromptInput_and_Suggestions_use_ai_elements_slots()
    {
        var prompt = Render<PromptInput>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<PromptInputTextarea>(0);
                builder.CloseComponent();
                builder.OpenComponent<PromptInputHeader>(1);
                builder.AddAttribute(2, nameof(PromptInputHeader.ChildContent), (RenderFragment)(header =>
                {
                    header.OpenComponent<PromptInputAttachments>(0);
                    header.CloseComponent();
                }));
                builder.CloseComponent();
                builder.OpenComponent<PromptInputBody>(3);
                builder.AddAttribute(4, nameof(PromptInputBody.ChildContent), (RenderFragment)(body =>
                {
                    body.OpenComponent<PromptInputTextarea>(0);
                    body.CloseComponent();
                }));
                builder.CloseComponent();
                builder.OpenComponent<PromptInputFooter>(5);
                builder.AddAttribute(6, nameof(PromptInputFooter.ChildContent), (RenderFragment)(actions =>
                {
                    actions.OpenComponent<PromptInputTools>(0);
                    actions.AddAttribute(1, nameof(PromptInputTools.ChildContent), (RenderFragment)(group =>
                    {
                        group.OpenComponent<PromptInputActionMenu>(0);
                        group.AddAttribute(1, nameof(PromptInputActionMenu.ChildContent), (RenderFragment)(menu =>
                        {
                            menu.OpenComponent<PromptInputActionMenuTrigger>(0);
                            menu.CloseComponent();
                            menu.OpenComponent<PromptInputActionMenuContent>(1);
                            menu.AddAttribute(2, nameof(PromptInputActionMenuContent.ChildContent), (RenderFragment)(content =>
                            {
                                content.OpenComponent<PromptInputActionAddAttachments>(0);
                                content.CloseComponent();
                                content.OpenComponent<PromptInputActionAddScreenshot>(1);
                                content.CloseComponent();
                            }));
                            menu.CloseComponent();
                        }));
                        group.CloseComponent();
                        group.OpenComponent<PromptInputSelect<string>>(2);
                        group.AddAttribute(3, nameof(PromptInputSelect<string>.SelectedValue), "gpt-4o");
                        group.AddAttribute(4, nameof(PromptInputSelect<string>.ChildContent), (RenderFragment)(select =>
                        {
                            select.OpenComponent<PromptInputSelectTrigger>(0);
                            select.AddAttribute(1, nameof(PromptInputSelectTrigger.ChildContent), (RenderFragment)(trigger =>
                            {
                                trigger.OpenComponent<PromptInputSelectValue>(0);
                                trigger.CloseComponent();
                            }));
                            select.CloseComponent();
                            select.OpenComponent<PromptInputSelectContent>(2);
                            select.AddAttribute(3, nameof(PromptInputSelectContent.ChildContent), (RenderFragment)(content =>
                            {
                                content.OpenComponent<PromptInputSelectItem<string>>(0);
                                content.AddAttribute(1, nameof(PromptInputSelectItem<string>.ItemValue), "gpt-4o");
                                content.AddAttribute(2, nameof(PromptInputSelectItem<string>.Text), "GPT-4o");
                                content.CloseComponent();
                            }));
                            select.CloseComponent();
                        }));
                        group.CloseComponent();
                        group.OpenComponent<PromptInputActionAddAttachments>(5);
                        group.CloseComponent();
                    }));
                    actions.CloseComponent();
                    actions.OpenComponent<PromptInputTools>(2);
                    actions.AddAttribute(3, nameof(PromptInputTools.ChildContent), (RenderFragment)(group =>
                    {
                        group.OpenComponent<PromptInputSubmit>(0);
                        group.AddAttribute(1, nameof(PromptInputSubmit.ChildContent), (RenderFragment)(submit => submit.AddContent(0, "Send")));
                        group.CloseComponent();
                    }));
                    actions.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        prompt.Find("form[data-slot='prompt-input']").GetAttribute("role").Should().Be("group");
        prompt.Find("[data-slot='prompt-input-textarea']").GetAttribute("aria-label").Should().Be("Message input");
        prompt.Find("[data-slot='prompt-input-header']").Should().NotBeNull();
        prompt.Find("[data-slot='prompt-input-body']").Should().NotBeNull();
        prompt.Find("[data-slot='prompt-input-footer']").Should().NotBeNull();
        prompt.Find("[data-slot='prompt-input-tools']").Should().NotBeNull();
        prompt.Find("[data-slot='prompt-input-button']").Should().NotBeNull();
        prompt.Find("[data-slot='prompt-input-action-menu']").Should().NotBeNull();
        prompt.Find("[data-slot='prompt-input-action-menu-trigger']").Should().NotBeNull();
        prompt.Find("[data-slot='prompt-input-select']").Should().NotBeNull();
        prompt.Find("[data-slot='prompt-input-select-trigger']").Should().NotBeNull();
        prompt.Find("[data-slot='prompt-input-select-value']").Should().NotBeNull();
        prompt.Find("[data-slot='prompt-input-file-input']").GetAttribute("type").Should().Be("file");
        prompt.Find("[data-slot='prompt-input-submit']").Should().NotBeNull();

        var suggestions = Render<Suggestions>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<Suggestion>(0);
                builder.AddAttribute(1, nameof(Suggestion.ChildContent), (RenderFragment)(content => content.AddContent(0, "Summarize")));
                builder.CloseComponent();
            })));

        suggestions.Find("[data-slot='suggestions']").Should().NotBeNull();
        suggestions.Find("[data-slot='suggestion']").TextContent.Should().Contain("Summarize");
    }
}
