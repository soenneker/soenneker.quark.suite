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

    [Test]
    public void Generate_WithQuarkBuilderValues_ReturnsComponentCss()
    {
        var theme = new Theme
        {
            Anchors = new AnchorOptions
            {
                Selector = "a",
                DecorationLine = DecorationLine.None,
                TextColor = TextColor.Primary
            },
            Buttons = new ButtonOptions
            {
                BackgroundColor = BackgroundColor.Primary,
                TextColor = TextColor.Blue.Is100,
                Rounded = Rounded.Lg,
                Padding = Padding.OnY.Is2.OnX.Is4
            },
            Cards = new CardOptions
            {
                BackgroundColor = BackgroundColor.Card,
                Rounded = Rounded.Xl,
                Shadow = Shadow.Sm
            }
        };

        var result = ComponentsCssGenerator.Generate(theme);

        result.Should().Be("a {\n  color: var(--primary);\n  text-decoration: none;\n}\n[data-slot='button'] {\n  padding-top: 0.5rem;\n  padding-bottom: 0.5rem;\n  padding-left: 1rem;\n  padding-right: 1rem;\n  color: var(--color-blue-100);\n  background-color: var(--primary);\n  border-radius: 0.5rem;\n}\n[data-slot='card'] {\n  box-shadow: 0 1px 3px 0 rgb(0 0 0 / 0.1), 0 1px 2px -1px rgb(0 0 0 / 0.1);\n  background-color: var(--card);\n  border-radius: 0.75rem;\n}");
    }
}
