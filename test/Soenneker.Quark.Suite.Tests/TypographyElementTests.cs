using AwesomeAssertions;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed class TypographyElementTests
{
    [Fact]
    public void TypographyElement_exposes_typography_aliases()
    {
        typeof(TypographyElement).GetProperty(nameof(TypographyElement.Decoration))!.PropertyType.Should().Be(typeof(CssValue<TextDecorationBuilder>?));
        typeof(TypographyElement).GetProperty(nameof(TypographyElement.Leading))!.PropertyType.Should().Be(typeof(CssValue<LeadingBuilder>?));
        typeof(TypographyElement).GetProperty(nameof(TypographyElement.Tracking))!.PropertyType.Should().Be(typeof(CssValue<TrackingBuilder>?));
        typeof(TypographyElement).GetProperty(nameof(TypographyElement.WhiteSpace))!.PropertyType.Should().Be(typeof(CssValue<WhitespaceBuilder>?));
        typeof(TypographyElement).GetProperty(nameof(TypographyElement.Wrap))!.PropertyType.Should().Be(typeof(CssValue<TextWrapBuilder>?));
        typeof(TypographyElement).GetProperty(nameof(TypographyElement.Break))!.PropertyType.Should().Be(typeof(CssValue<TextBreakBuilder>?));
        typeof(TypographyElement).GetProperty(nameof(TypographyElement.Numeric))!.PropertyType.Should().Be(typeof(CssValue<FontVariantNumericBuilder>?));
    }
}
