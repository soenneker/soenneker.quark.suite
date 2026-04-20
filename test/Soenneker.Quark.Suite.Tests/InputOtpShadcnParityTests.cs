using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
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

        inputOtpClasses.Should().Contain("cn-input-otp");
        inputOtpClasses.Should().Contain("flex");
        inputOtpClasses.Should().Contain("items-center");
        inputOtpClasses.Should().Contain("has-disabled:opacity-50");
        inputOtpClasses.Should().NotContain("q-input-otp");

        inputOtpGroupClasses.Should().Contain("flex");
        inputOtpGroupClasses.Should().Contain("items-center");
        inputOtpGroupClasses.Should().Contain("rounded-lg");
        inputOtpGroupClasses.Should().Contain("has-aria-invalid:ring-3");
        inputOtpGroupClasses.Should().NotContain("q-input-otp-group");

        inputOtpSlotClasses.Should().Contain("size-8");
        inputOtpSlotClasses.Should().Contain("border-input");
        inputOtpSlotClasses.Should().Contain("first:rounded-l-lg");
        inputOtpSlotClasses.Should().Contain("data-[active=true]:ring-3");
        inputOtpSlotClasses.Should().NotContain("q-input-otp-slot");

        inputOtpSeparatorClasses.Should().Contain("flex");
        inputOtpSeparatorClasses.Should().Contain("items-center");
        inputOtpSeparatorClasses.Should().Contain("[&_svg:not([class*='size-'])]:size-4");
        inputOtpSeparatorClasses.Should().NotContain("q-input-otp-separator");
    }
}
