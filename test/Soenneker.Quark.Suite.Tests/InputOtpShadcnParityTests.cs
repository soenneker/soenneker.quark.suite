using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void InputOtp_slots_match_shadcn_base_classes()
    {
        var inputOtp = Render<InputOtp>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<InputOtpGroup>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(groupBuilder =>
                {
                    groupBuilder.OpenComponent<InputOtpSlot>(0);
                    groupBuilder.AddAttribute(1, "Index", 0);
                    groupBuilder.CloseComponent();

                    groupBuilder.OpenComponent<InputOtpSlot>(2);
                    groupBuilder.AddAttribute(3, "Index", 1);
                    groupBuilder.CloseComponent();
                }));
                builder.CloseComponent();

                builder.OpenComponent<InputOtpSeparator>(2);
                builder.CloseComponent();

                builder.OpenComponent<InputOtpInput>(3);
                builder.CloseComponent();
            })));

        var inputOtpClasses = inputOtp.Find("[data-slot='input-otp']").GetAttribute("class")!;
        var inputOtpGroupClasses = inputOtp.Find("[data-slot='input-otp-group']").GetAttribute("class")!;
        var inputOtpSlotClasses = inputOtp.Find("[data-slot='input-otp-slot']").GetAttribute("class")!;
        var inputOtpSeparatorClasses = inputOtp.Find("[data-slot='input-otp-separator']").GetAttribute("class")!;

        inputOtpClasses.Should().Contain("flex");
        inputOtpClasses.Should().Contain("items-center");
        inputOtpClasses.Should().Contain("gap-2");
        inputOtpClasses.Should().Contain("has-disabled:opacity-50");
        inputOtpClasses.Should().NotContain("cn-input-otp");
        inputOtpClasses.Should().NotContain("q-input-otp");

        inputOtpGroupClasses.Should().Contain("flex");
        inputOtpGroupClasses.Should().Contain("items-center");
        inputOtpGroupClasses.Should().NotContain("rounded-lg");
        inputOtpGroupClasses.Should().NotContain("has-aria-invalid:border-destructive");
        inputOtpGroupClasses.Should().NotContain("has-aria-invalid:ring-3");
        inputOtpGroupClasses.Should().NotContain("q-input-otp-group");

        inputOtpSlotClasses.Should().Contain("relative");
        inputOtpSlotClasses.Should().Contain("flex");
        inputOtpSlotClasses.Should().Contain("h-9");
        inputOtpSlotClasses.Should().Contain("w-9");
        inputOtpSlotClasses.Should().Contain("items-center");
        inputOtpSlotClasses.Should().Contain("justify-center");
        inputOtpSlotClasses.Should().Contain("border-input");
        inputOtpSlotClasses.Should().Contain("text-sm");
        inputOtpSlotClasses.Should().Contain("shadow-xs");
        inputOtpSlotClasses.Should().Contain("transition-all");
        inputOtpSlotClasses.Should().Contain("outline-none");
        inputOtpSlotClasses.Should().Contain("first:rounded-l-md");
        inputOtpSlotClasses.Should().Contain("last:rounded-r-md");
        inputOtpSlotClasses.Should().Contain("data-[active=true]:ring-[3px]");
        inputOtpSlotClasses.Should().NotContain("size-8");
        inputOtpSlotClasses.Should().NotContain("text-center");
        inputOtpSlotClasses.Should().NotContain("first:rounded-l-lg");
        inputOtpSlotClasses.Should().NotContain("data-[active=true]:ring-3");
        inputOtpSlotClasses.Should().NotContain("focus:ring-3");
        inputOtpSlotClasses.Should().NotContain("q-input-otp-slot");

        inputOtpSeparatorClasses.Should().NotContain("flex");
        inputOtpSeparatorClasses.Should().NotContain("items-center");
        inputOtpSeparatorClasses.Should().NotContain("[&_svg:not([class*='size-'])]:size-4");
        inputOtpSeparatorClasses.Should().NotContain("q-input-otp-separator");
    }
}
