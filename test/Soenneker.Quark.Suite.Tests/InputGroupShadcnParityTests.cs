using AwesomeAssertions;
using Bunit;
using System.Collections.Generic;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void InputGroup_matches_shadcn_base_classes()
    {
        var inputGroup = Render<InputGroup>(parameters => parameters
            .AddChildContent("<input data-slot=\"input-group-control\" />"));

        var groupClasses = inputGroup.Find("[data-slot='input-group']").GetAttribute("class")!;

        groupClasses.Should().Contain("group/input-group");
        groupClasses.Should().Contain("relative");
        groupClasses.Should().Contain("flex");
        groupClasses.Should().Contain("h-9");
        groupClasses.Should().Contain("w-full");
        groupClasses.Should().Contain("min-w-0");
        groupClasses.Should().Contain("items-center");
        groupClasses.Should().Contain("rounded-md");
        groupClasses.Should().Contain("border");
        groupClasses.Should().Contain("border-input");
        groupClasses.Should().Contain("shadow-xs");
        groupClasses.Should().Contain("transition-[color,box-shadow]");
        groupClasses.Should().Contain("has-[[data-slot=input-group-control]:focus-visible]:ring-[3px]");
        groupClasses.Should().Contain("has-[[data-slot][aria-invalid=true]]:border-destructive");
        groupClasses.Should().Contain("has-[[data-slot][aria-invalid=true]]:ring-destructive/20");
        groupClasses.Should().Contain("has-[>[data-align=inline-start]]:[&>input]:pl-2");
        groupClasses.Should().Contain("has-[>[data-align=inline-end]]:[&>input]:pr-2");
        groupClasses.Should().NotContain("rounded-lg");
        groupClasses.Should().NotContain("transition-[color,shadow]");
        groupClasses.Should().NotContain("h-8");
        groupClasses.Should().NotContain("has-[[data-slot=input-group-control]:focus-visible]:ring-3");
        groupClasses.Should().NotContain("q-input-group");
    }

    [Test]
    public void InputGroupInput_forwards_form_control_attributes_and_matches_shadcn_classes()
    {
        var cut = Render<InputGroupInput>(parameters => parameters
            .Add(p => p.Id, "url")
            .Add(p => p.Name, "site")
            .Add(p => p.Required, true)
            .Add(p => p.Placeholder, "example.com")
            .Add(p => p.Attributes, new Dictionary<string, object> { ["aria-invalid"] = "true" }));

        var input = cut.Find("input");
        var classes = input.GetAttribute("class")!;

        input.GetAttribute("data-slot").Should().Be("input-group-control");
        input.GetAttribute("id").Should().Be("url");
        input.GetAttribute("name").Should().Be("site");
        input.HasAttribute("required").Should().BeTrue();
        input.GetAttribute("placeholder").Should().Be("example.com");
        input.GetAttribute("aria-invalid").Should().Be("true");
        classes.Should().Contain("flex-1");
        classes.Should().Contain("rounded-none");
        classes.Should().Contain("border-0");
        classes.Should().Contain("bg-transparent");
        classes.Should().Contain("shadow-none");
        classes.Should().Contain("focus-visible:ring-0");
        classes.Should().Contain("dark:bg-transparent");
        classes.Should().NotContain("file:h-6");
    }

    [Test]
    public void InputGroupTextarea_forwards_rows_and_matches_shadcn_classes()
    {
        var cut = Render<InputGroupTextarea>(parameters => parameters
            .Add(p => p.Rows, 4)
            .Add(p => p.Placeholder, "Message"));

        var textarea = cut.Find("textarea");
        var classes = textarea.GetAttribute("class")!;

        textarea.GetAttribute("data-slot").Should().Be("input-group-control");
        textarea.GetAttribute("rows").Should().Be("4");
        textarea.GetAttribute("placeholder").Should().Be("Message");
        classes.Should().Contain("flex-1");
        classes.Should().Contain("resize-none");
        classes.Should().Contain("rounded-none");
        classes.Should().Contain("border-0");
        classes.Should().Contain("bg-transparent");
        classes.Should().Contain("py-3");
        classes.Should().Contain("shadow-none");
        classes.Should().Contain("focus-visible:ring-0");
        classes.Should().Contain("dark:bg-transparent");
        classes.Should().NotContain("disabled:bg-transparent");
        classes.Should().NotContain("aria-invalid:ring-0");
    }

    [Test]
    public void InputGroupButton_matches_shadcn_size_contract()
    {
        var cut = Render<InputGroupButton>(parameters => parameters
            .Add(p => p.ChildContent, "Go"));

        var button = cut.Find("button");
        var classes = button.GetAttribute("class")!;

        button.GetAttribute("data-slot").Should().Be("input-group-button");
        button.GetAttribute("data-size").Should().Be("xs");
        button.GetAttribute("data-variant").Should().Be("ghost");
        button.GetAttribute("type").Should().Be("button");
        classes.Should().Contain("shadow-none");
        classes.Should().Contain("h-6");
        classes.Should().Contain("gap-1");
        classes.Should().Contain("rounded-[calc(var(--radius)-5px)]");
        classes.Should().Contain("px-2");
        classes.Should().Contain("has-[>svg]:px-2");
        classes.Should().Contain("[&>svg:not([class*='size-'])]:size-3.5");
    }
}
