using AwesomeAssertions;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public class ComponentsCssGeneratorTests
{
    [Fact]
    public void Generate_WithNullTheme_ReturnsEmptyString()
    {
        // Act
        var result = ComponentsCssGenerator.Generate(null!);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void Generate_WithNoComponentOptions_ReturnsEmptyString()
    {
        // Arrange
        var theme = new Theme();

        // Act
        var result = ComponentsCssGenerator.Generate(theme);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void Generate_WithDiv_ReturnsComponentCss()
    {
        // Arrange
        var theme = new Theme
        {
            Divs = new DivOptions()
            {
                TextDecoration = TextDecoration.Underline
            }
        };

        // Act
        var result = ComponentsCssGenerator.Generate(theme);

        // Assert
        result.Should().Be("div {\n  text-decoration: underline;\n}");
    }

    [Fact]
    public void Generate_WithSingleComponent_ReturnsComponentCss()
    {
        // Arrange
        var theme = new Theme
        {
            Anchors = new AnchorOptions
            {
                TextDecoration = TextDecoration.Underline
            }
        };

        // Act
        var result = ComponentsCssGenerator.Generate(theme);

        // Assert
        result.Should().Be("a {\n  text-decoration: underline;\n}");
    }

    [Fact]
    public void Generate_WithMultipleComponents_AppendsBlocksInThemeOrder()
    {
        // Arrange
        var theme = new Theme
        {
            Anchors = new AnchorOptions
            {
                TextDecoration = TextDecoration.Underline
            },
            Buttons = new ButtonOptions
            {
                Selector = ".btn",
                BackgroundColor = "#123456"
            }
        };

        // Act
        var result = ComponentsCssGenerator.Generate(theme);

        // Assert
        const string expected = "a {\n  text-decoration: underline;\n}\n.btn {\n  background-color: #123456;\n}";
        result.Should().Be(expected);
    }

    [Fact]
    public void Generate_WithCustomSelectorRule_UsesResolvedSelector()
    {
        // Arrange
        var hoverDecoration = TextDecoration.Underline.WithSelector("&:hover");
        CssValue<BackgroundColorBuilder> focusBackground = "#abcdef";

        var theme = new Theme
        {
            Anchors = new AnchorOptions
            {
                TextDecoration = hoverDecoration,
                BackgroundColor = focusBackground.WithSelector("&:focus")
            }
        };

        // Act
        var result = ComponentsCssGenerator.Generate(theme);

        // Assert
        result.Should().Contain("a:hover {\n  text-decoration: underline;\n}");
        result.Should().Contain("a:focus {\n  background-color: #abcdef;\n}");
    }
}