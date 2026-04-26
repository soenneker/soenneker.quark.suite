using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
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

        var accordionClasses = cut.Find("[data-slot='accordion']").GetAttribute("class")!;
        var itemClasses = cut.Find("[data-slot='accordion-item']").GetAttribute("class")!;
        var triggerClasses = cut.Find("[data-slot='accordion-trigger']").GetAttribute("class")!;
        var contentClasses = cut.Find("[data-slot='accordion-content']").GetAttribute("class")!;
        var innerClasses = cut.Find("[data-slot='accordion-content-inner']").GetAttribute("class")!;

        accordionClasses.Should().Contain("flex");
        accordionClasses.Should().Contain("w-full");
        accordionClasses.Should().Contain("flex-col");
        accordionClasses.Should().NotContain("q-accordion");

        itemClasses.Should().Contain("border-b");
        itemClasses.Should().Contain("last:border-b-0");
        itemClasses.Should().NotContain("not-last:border-b");
        itemClasses.Should().NotContain("q-accordion-item");

        triggerClasses.Should().Contain("group/accordion-trigger");
        triggerClasses.Should().Contain("relative");
        triggerClasses.Should().Contain("flex");
        triggerClasses.Should().Contain("flex-1");
        triggerClasses.Should().Contain("items-start");
        triggerClasses.Should().Contain("justify-between");
        triggerClasses.Should().Contain("rounded-lg");
        triggerClasses.Should().Contain("border-transparent");
        triggerClasses.Should().Contain("py-2.5");
        triggerClasses.Should().Contain("text-left");
        triggerClasses.Should().Contain("text-sm");
        triggerClasses.Should().Contain("font-medium");
        triggerClasses.Should().Contain("transition-all");
        triggerClasses.Should().Contain("outline-none");
        triggerClasses.Should().Contain("hover:underline");
        triggerClasses.Should().Contain("focus-visible:border-ring");
        triggerClasses.Should().Contain("focus-visible:ring-[3px]");
        triggerClasses.Should().Contain("focus-visible:ring-ring/50");
        triggerClasses.Should().NotContain("focus-visible:after:border-ring");
        triggerClasses.Should().Contain("aria-disabled:pointer-events-none");
        triggerClasses.Should().Contain("aria-disabled:opacity-50");
        triggerClasses.Should().NotContain("**:data-[slot=accordion-trigger-icon]:ml-auto");
        triggerClasses.Should().NotContain("gap-4");
        triggerClasses.Should().NotContain("focus-visible:ring-3");
        triggerClasses.Should().Contain("[&[data-state=open]>[data-slot=accordion-trigger-icon]]:rotate-180");
        triggerClasses.Should().NotContain("q-accordion-trigger");
        triggerClasses.Should().NotContain("text-start");

        contentClasses.Should().Contain("overflow-hidden");
        contentClasses.Should().Contain("text-sm");
        contentClasses.Should().Contain("data-[state=open]:animate-accordion-down");
        contentClasses.Should().Contain("data-[state=closed]:animate-accordion-up");
        contentClasses.Should().NotContain("data-open:animate-accordion-down");
        contentClasses.Should().NotContain("data-closed:animate-accordion-up");
        contentClasses.Should().NotContain("q-accordion-content");

        innerClasses.Should().Contain("pt-0");
        innerClasses.Should().Contain("pb-2.5");
        innerClasses.Should().NotContain("pb-4");
    }
}

