using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Soenneker.Quark.Forms;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void FormItem_matches_shadcn_form_item_default_contract()
    {
        var cut = Render<FormItem>(parameters => parameters
            .AddChildContent("Email"));

        var item = cut.Find("[data-slot='form-item']");
        var classes = item.GetAttribute("class")!;

        item.HasAttribute("role").Should().BeFalse();
        classes.Should().Contain("grid");
        classes.Should().Contain("gap-2");
        classes.Should().NotContain("flex w-full gap-2");
    }

    [Test]
    public void FormControl_has_shadcn_slot_without_extra_layout_classes()
    {
        var cut = Render<FormControl>(parameters => parameters
            .AddChildContent<Input>(input => input
                .Add(p => p.Placeholder, "name@example.com")));

        var control = cut.Find("[data-slot='form-control']");

        control.GetAttribute("class").Should().BeNullOrEmpty();
        control.QuerySelector("input")!.GetAttribute("placeholder").Should().Be("name@example.com");
    }

    [Test]
    public void FormItem_wires_label_description_message_and_invalid_state_to_control()
    {
        var cut = Render<FormItem>(parameters => parameters
            .Add(p => p.IsInvalid, true)
            .AddChildContent(BuildFormItemContent()));

        var label = cut.Find("[data-slot='form-label']");
        var input = cut.Find("input");
        var description = cut.Find("[data-slot='form-description']");
        var message = cut.Find("[data-slot='form-message']");

        label.GetAttribute("data-error").Should().Be("true");
        label.GetAttribute("for").Should().Be(input.GetAttribute("id"));
        label.GetAttribute("class")!.Should().Contain("data-[error=true]:text-destructive");

        input.GetAttribute("aria-invalid").Should().Be("true");
        input.GetAttribute("aria-describedby")!.Split(' ').Should().Contain(description.Id);
        input.GetAttribute("aria-describedby")!.Split(' ').Should().Contain(message.Id);

        description.GetAttribute("class")!.Should().Contain("text-sm");
        description.GetAttribute("class")!.Should().Contain("text-muted-foreground");
        message.TagName.Should().Be("P");
        message.GetAttribute("class")!.Should().Contain("text-sm");
        message.GetAttribute("class")!.Should().Contain("text-destructive");
    }

    private static RenderFragment BuildFormItemContent()
    {
        return builder =>
        {
            builder.OpenComponent<FormLabel>(0);
            builder.AddAttribute(1, nameof(FormLabel.ChildContent), (RenderFragment)(labelBuilder => labelBuilder.AddContent(0, "Email")));
            builder.CloseComponent();

            builder.OpenComponent<Input>(2);
            builder.AddAttribute(3, nameof(Input.Placeholder), "name@example.com");
            builder.CloseComponent();

            builder.OpenComponent<FormDescription>(4);
            builder.AddAttribute(5, nameof(FormDescription.ChildContent), (RenderFragment)(descriptionBuilder => descriptionBuilder.AddContent(0, "Use your work email.")));
            builder.CloseComponent();

            builder.OpenComponent<FormMessage>(6);
            builder.AddAttribute(7, nameof(FormMessage.ChildContent), (RenderFragment)(messageBuilder => messageBuilder.AddContent(0, "Email is required.")));
            builder.CloseComponent();
        };
    }
}
