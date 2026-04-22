using AwesomeAssertions;


namespace Soenneker.Quark.Suite.Tests;

public class ComponentsCssGeneratorTests
{
    [Test]
    public void Generate_WithNullTheme_ReturnsEmptyString()
    {
        // Act
        var result = ComponentsCssGenerator.Generate(null!);

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public void Generate_WithNoComponentOptions_ReturnsEmptyString()
    {
        // Arrange
        var theme = new Theme();

        // Act
        var result = ComponentsCssGenerator.Generate(theme);

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public void Generate_WithDiv_ReturnsComponentCss()
    {
        // Arrange
        var theme = new Theme
        {
            Divs = new DivOptions
            {
                DecorationLine = DecorationLine.Underline
            }
        };

        // Act
        var result = ComponentsCssGenerator.Generate(theme);

        // Assert
        result.Should().Be("[data-slot='div'] {\n  text-decoration: underline;\n}");
    }

    [Test]
    public void Generate_WithSingleComponent_ReturnsComponentCss()
    {
        // Arrange
        var theme = new Theme
        {
            Anchors = new AnchorOptions
            {
                Selector = "a",
                DecorationLine = DecorationLine.Underline
            }
        };

        // Act
        var result = ComponentsCssGenerator.Generate(theme);

        // Assert
        result.Should().Be("a {\n  text-decoration: underline;\n}");
    }

    [Test]
    public void Generate_WithShrink_ReturnsFlexShrinkCss()
    {
        var theme = new Theme
        {
            Divs = new DivOptions
            {
                Shrink = Shrink.Is0
            }
        };

        var result = ComponentsCssGenerator.Generate(theme);

        result.Should().Be("[data-slot='div'] {\n  flex-shrink: 0;\n}");
    }

    [Test]
    public void Generate_WithFlexDirectionWrapAndGrow_ReturnsFlexCss()
    {
        var theme = new Theme
        {
            Divs = new DivOptions
            {
                FlexDirection = FlexDirection.Col,
                FlexWrap = FlexWrap.Wrap,
                Grow = Grow.Is0
            }
        };

        var result = ComponentsCssGenerator.Generate(theme);

        result.Should().Be("[data-slot='div'] {\n  display: flex;\n  flex-direction: column;\n  display: flex;\n  flex-wrap: wrap;\n  flex-grow: 0;\n}");
    }
}
