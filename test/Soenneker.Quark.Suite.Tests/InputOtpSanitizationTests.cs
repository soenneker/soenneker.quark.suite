using AwesomeAssertions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark.Suite.Tests;

public sealed class InputOtpSanitizationTests : BunitContext
{
    public InputOtpSanitizationTests()
    {
        Services.AddLogging();
        Services.AddScoped<IInputInterop, InputSelectionTestInterop>();
        Services.AddDefaultQuarkOptionsAsScoped();
    }

    [Test]
    public void Controlled_null_clears_the_value_instead_of_restoring_the_default()
    {
        var cut = Render<InputOtp>(p => p.Add(c => c.DefaultOtpValue, "123456")
            .Add(c => c.OtpValue, "654321").Add(c => c.OtpValueChanged, _ => { }));
        cut.Render(p => p.Add(c => c.OtpValue, (string?)null));
        cut.Find("input").GetAttribute("value").Should().BeNullOrEmpty();
    }

    [Test]
    [Arguments("123456", "123456")]
    [Arguments("a1b2c3", "123")]
    [Arguments("12a34b", "1234")]
    [Arguments("abc", "")]
    public void Filters_invalid_characters_without_losing_the_valid_prefix(string input, string expected)
    {
        var cut = Render<InputOtp>(p => p.Add(c => c.Pattern, "[0-9]").Add(c => c.DefaultOtpValue, input));
        cut.Find("input").GetAttribute("value").Should().Be(expected);
    }

    [Test]
    public void Long_filtered_values_can_exceed_the_stack_buffer()
    {
        string expected = new('1', 256);
        var cut = Render<InputOtp>(p => p.Add(c => c.Pattern, "[0-9]").Add(c => c.DefaultOtpValue, "x" + expected + "x"));
        cut.Find("input").GetAttribute("value").Should().Be(expected);
    }
}
