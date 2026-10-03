using AwesomeAssertions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark.Suite.Tests;

public sealed class InputOtpSanitizationTests : BunitContext
{
    public InputOtpSanitizationTests()
    {
        Services.AddLogging();
        Services.AddDefaultQuarkOptionsAsScoped();
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
