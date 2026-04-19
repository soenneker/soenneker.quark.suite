using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Accordion_slots_match_shadcn_base_classes()
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

        string accordionClasses = cut.Find("[data-slot='accordion']").GetAttribute("class")!;
        string itemClasses = cut.Find("[data-slot='accordion-item']").GetAttribute("class")!;
        string triggerClasses = cut.Find("[data-slot='accordion-trigger']").GetAttribute("class")!;
        string contentClasses = cut.Find("[data-slot='accordion-content']").GetAttribute("class")!;
        string innerClasses = cut.Find("[data-slot='accordion-content-inner']").GetAttribute("class")!;

        accordionClasses.Should().Contain("flex");
        accordionClasses.Should().Contain("w-full");
        accordionClasses.Should().Contain("flex-col");
        accordionClasses.Should().NotContain("q-accordion");

        itemClasses.Should().Contain("not-last:border-b");
        itemClasses.Should().NotContain("q-accordion-item");
        itemClasses.Should().NotContain("last:border-b-0");

        triggerClasses.Should().Contain("group/accordion-trigger");
        triggerClasses.Should().Contain("relative");
        triggerClasses.Should().Contain("flex");
        triggerClasses.Should().Contain("flex-1");
        triggerClasses.Should().Contain("items-start");
        triggerClasses.Should().Contain("justify-between");
        triggerClasses.Should().Contain("rounded-lg");
        triggerClasses.Should().Contain("border");
        triggerClasses.Should().Contain("border-transparent");
        triggerClasses.Should().Contain("py-2.5");
        triggerClasses.Should().Contain("text-start");
        triggerClasses.Should().Contain("text-sm");
        triggerClasses.Should().Contain("font-medium");
        triggerClasses.Should().Contain("transition-all");
        triggerClasses.Should().Contain("outline-none");
        triggerClasses.Should().Contain("hover:underline");
        triggerClasses.Should().Contain("focus-visible:border-ring");
        triggerClasses.Should().Contain("focus-visible:ring-[3px]");
        triggerClasses.Should().Contain("focus-visible:ring-ring/50");
        triggerClasses.Should().Contain("**:data-[slot=accordion-trigger-icon]:ms-auto");
        triggerClasses.Should().Contain("**:data-[slot=accordion-trigger-icon]:size-4");
        triggerClasses.Should().Contain("**:data-[slot=accordion-trigger-icon]:text-muted-foreground");
        triggerClasses.Should().NotContain("q-accordion-trigger");
        triggerClasses.Should().NotContain("text-left");

        contentClasses.Should().Contain("overflow-hidden");
        contentClasses.Should().Contain("text-sm");
        contentClasses.Should().Contain("data-open:animate-accordion-down");
        contentClasses.Should().Contain("data-closed:animate-accordion-up");
        contentClasses.Should().NotContain("q-accordion-content");

        innerClasses.Should().Contain("h-(--radix-accordion-content-height)");
        innerClasses.Should().Contain("pt-0");
        innerClasses.Should().Contain("pb-2.5");
        innerClasses.Should().Contain("data-ending-style:h-0");
        innerClasses.Should().Contain("data-starting-style:h-0");
        innerClasses.Should().Contain("[&_a]:underline");
        innerClasses.Should().Contain("[&_a]:underline-offset-3");
        innerClasses.Should().Contain("[&_a]:hover:text-foreground");
        innerClasses.Should().Contain("[&_p:not(:last-child)]:mb-4");
    }
}

