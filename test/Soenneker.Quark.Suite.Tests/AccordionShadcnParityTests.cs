using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using System;
using System.IO;


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

        accordionClasses.Should().NotContain("flex");
        accordionClasses.Should().NotContain("w-full");
        accordionClasses.Should().NotContain("flex-col");
        accordionClasses.Should().NotContain("q-accordion");

        itemClasses.Should().Contain("border-b");
        itemClasses.Should().Contain("last:border-b-0");
        itemClasses.Should().NotContain("not-last:border-b");
        itemClasses.Should().NotContain("q-accordion-item");

        triggerClasses.Should().NotContain("group/accordion-trigger");
        triggerClasses.Should().NotContain("relative");
        triggerClasses.Should().Contain("flex");
        triggerClasses.Should().Contain("flex-1");
        triggerClasses.Should().Contain("items-start");
        triggerClasses.Should().Contain("justify-between");
        triggerClasses.Should().Contain("gap-4");
        triggerClasses.Should().Contain("rounded-md");
        triggerClasses.Should().NotContain("border-transparent");
        triggerClasses.Should().Contain("py-4");
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
        triggerClasses.Should().NotContain("aria-disabled:pointer-events-none");
        triggerClasses.Should().NotContain("aria-disabled:opacity-50");
        triggerClasses.Should().NotContain("**:data-[slot=accordion-trigger-icon]:ml-auto");
        triggerClasses.Should().NotContain("py-2.5");
        triggerClasses.Should().NotContain("focus-visible:ring-3");
        triggerClasses.Should().Contain("[&[data-state=open]>svg]:rotate-180");
        triggerClasses.Should().NotContain("[&[data-state=open]>[data-slot=accordion-trigger-icon]]:rotate-180");
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
        innerClasses.Should().Contain("pb-4");
        innerClasses.Should().NotContain("pb-2.5");
    }

    [Test]
    public void Accordion_demo_examples_match_current_shadcn_docs_examples()
    {
        var source = File.ReadAllText(Path.Combine(GetSuiteRootForAccordion(), "test", "Soenneker.Quark.Suite.Demo", "Pages", "Components", "Accordions.razor"));

        source.Should().Contain("What are the key considerations when implementing a comprehensive enterprise-level authentication system?");
        source.Should().Contain("How does modern distributed system architecture handle eventual consistency and data synchronization across multiple regions?");
        source.Should().Contain("h-[70rem]");
        source.Should().Contain("Title=\"With Disabled\"");
        source.Should().Contain("Class=\"p-1 data-[state=open]:bg-muted/50\"");
        source.Should().Contain("Title=\"With Borders\"");
        source.Should().Contain("There are no hidden fees or setup costs.");
        source.Should().Contain("Our API documentation includes code examples in 10+ programming languages.");
        source.Should().Contain("Title=\"In Card\"");
        source.Should().Contain("Can I upgrade or downgrade my plan?");
        source.Should().Contain("What is your refund policy?");
        source.Should().Contain("<Button Size=\"ButtonSize.Sm\">");
        source.Should().Contain("h-[42rem]");
        source.Should().NotContain("DefaultValues=\"@MultipleDefaultValues\"");
        source.Should().NotContain("Notification Settings");
    }

    private static string GetSuiteRootForAccordion()
    {
        var directory = AppContext.BaseDirectory;

        while (!File.Exists(Path.Combine(directory, "Soenneker.Quark.Suite.slnx")))
            directory = Directory.GetParent(directory)!.FullName;

        return directory;
    }
}

