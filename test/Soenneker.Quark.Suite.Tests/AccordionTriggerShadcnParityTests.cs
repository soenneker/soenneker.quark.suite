using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void AccordionTrigger_matches_shadcn_text_alignment()
    {
        var cut = Render<Accordion>(parameters => parameters
            .Add(p => p.DefaultValue, "shipping")
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<AccordionItem>(0);
                builder.AddAttribute(1, "Value", "shipping");
                builder.AddAttribute(2, "ChildContent", (RenderFragment)(itemBuilder =>
                {
                    itemBuilder.OpenComponent<AccordionTrigger>(0);
                    itemBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Question")));
                    itemBuilder.CloseComponent();

                    itemBuilder.OpenComponent<AccordionContent>(2);
                    itemBuilder.AddAttribute(3, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Answer")));
                    itemBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        var classes = cut.Find("[data-slot='accordion-trigger']").GetAttribute("class")!;

        classes.Should().Contain("text-left");
        classes.Should().NotContain("text-start");
    }
}
