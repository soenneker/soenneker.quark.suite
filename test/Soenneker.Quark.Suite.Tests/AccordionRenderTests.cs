using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public async Task Accordion_does_not_animate_default_content_on_initial_render()
    {
        var cut = Render(CreateAccordion());

        var initialContent = cut.Find("[data-slot='accordion-content']");
        initialContent.ClassList.Should().NotContain("data-[state=open]:animate-accordion-down");

        await cut.FindAll("button")[1].ClickAsync();

        var openedContent = cut.Find("[data-slot='accordion-content'][data-state='open']");
        openedContent.ClassList.Should().Contain("data-[state=open]:animate-accordion-down");
        openedContent.ClassList.Should().Contain("data-[state=closed]:animate-accordion-up");
    }

    private static RenderFragment CreateAccordion() => builder =>
    {
        builder.OpenComponent<Accordion>(0);
        builder.AddAttribute(1, nameof(Accordion.Collapsible), true);
        builder.AddAttribute(2, nameof(Accordion.DefaultValue), "first");
        builder.AddAttribute(3, nameof(Accordion.ChildContent), (RenderFragment)(contentBuilder =>
        {
            AddAccordionItem(contentBuilder, 0, "first", "First");
            AddAccordionItem(contentBuilder, 10, "second", "Second");
        }));
        builder.CloseComponent();
    };

    private static void AddAccordionItem(RenderTreeBuilder builder, int sequence, string value, string label)
    {
        builder.OpenComponent<AccordionItem>(sequence);
        builder.AddAttribute(sequence + 1, nameof(AccordionItem.Value), value);
        builder.AddAttribute(sequence + 2, nameof(AccordionItem.ChildContent), (RenderFragment)(itemBuilder =>
        {
            itemBuilder.OpenComponent<AccordionTrigger>(0);
            itemBuilder.AddAttribute(1, nameof(AccordionTrigger.ShowChevron), false);
            itemBuilder.AddAttribute(2, nameof(AccordionTrigger.ChildContent), (RenderFragment)(triggerBuilder => triggerBuilder.AddContent(0, label)));
            itemBuilder.CloseComponent();

            itemBuilder.OpenComponent<AccordionContent>(3);
            itemBuilder.AddAttribute(4, nameof(AccordionContent.ChildContent), (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, $"{label} content")));
            itemBuilder.CloseComponent();
        }));
        builder.CloseComponent();
    }
}
