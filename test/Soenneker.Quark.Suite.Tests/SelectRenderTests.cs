using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Soenneker.Bradix;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Popper_select_content_does_not_force_viewport_to_trigger_height()
    {
        var cut = Render(CreatePopperSelect());

        var viewport = cut.Find("[data-radix-select-viewport][data-position='popper']");
        string? viewportClass = viewport.GetAttribute("class");

        viewportClass.Should().Contain("data-[position=popper]:w-full");
        viewportClass.Should().Contain("data-[position=popper]:min-w-(--radix-select-trigger-width)");
        viewportClass.Should().NotContain("data-[position=popper]:h-(--radix-select-trigger-height)");
    }

    private static RenderFragment CreatePopperSelect()
    {
        return builder =>
        {
            builder.OpenComponent<Select<string>>(0);
            builder.AddAttribute(1, nameof(Select<string>.SelectedValue), "professional");
            builder.AddAttribute(2, nameof(Select<string>.ChildContent), (RenderFragment)(select =>
            {
                select.OpenComponent<SelectTrigger>(0);
                select.AddAttribute(1, nameof(SelectTrigger.ChildContent), (RenderFragment)(trigger =>
                {
                    trigger.OpenComponent<SelectValue>(0);
                    trigger.CloseComponent();
                }));
                select.CloseComponent();

                select.OpenComponent<SelectContent>(2);
                select.AddAttribute(3, nameof(SelectContent.ContentPosition), (object)SelectPosition.Popper);
                select.AddAttribute(4, nameof(SelectContent.ForceMount), true);
                select.AddAttribute(5, nameof(SelectContent.ChildContent), (RenderFragment)(content =>
                {
                    content.OpenComponent<SelectItem<string>>(0);
                    content.AddAttribute(1, nameof(SelectItem<string>.ItemValue), "postal");
                    content.AddAttribute(2, nameof(SelectItem<string>.Text), "Postal and Delivery");
                    content.CloseComponent();

                    content.OpenComponent<SelectItem<string>>(3);
                    content.AddAttribute(4, nameof(SelectItem<string>.ItemValue), "professional");
                    content.AddAttribute(5, nameof(SelectItem<string>.Text), "Professional Services");
                    content.CloseComponent();
                }));
                select.CloseComponent();
            }));
            builder.CloseComponent();
        };
    }
}
