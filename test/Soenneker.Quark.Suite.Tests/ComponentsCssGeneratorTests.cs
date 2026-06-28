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

    [Test]
    public void Generate_WithDataTableThemeUtilities_ReturnsComponentCss()
    {
        var theme = new Theme
        {
            Anchors = new AnchorOptions
            {
                Selector = ".q-datatable tbody td > a",
                Display = Display.InlineFlex,
                MaxWidth = Width.IsFull,
                MinWidth = Width.Is0,
                ItemsAlign = Items.Center,
                Gap = Gap.Is2,
                Overflow = Overflow.Hidden,
                DecorationLine = DecorationLine.None
            },
            Divs = new DivOptions
            {
                Selector = ".q-datatable tbody td > a > div",
                MinWidth = Width.Is0,
                Overflow = Overflow.Hidden
            },
            Spans = new SpanOptions
            {
                Selector = ".q-datatable tbody td > a > div > span",
                Display = Display.Block,
                Overflow = Overflow.Hidden,
                TextOverflow = TextOverflow.Ellipsis,
                Whitespace = Whitespace.Nowrap
            },
            Trs = new TrOptions
            {
                Selector = ".q-datatable tbody tr",
                Border = Border.FromBottom.Is1,
                BorderColor = BorderColor.Token("[var(--border)]")
            },
            Tds = new TdOptions
            {
                Selector = ".q-datatable tbody td",
                MinWidth = Width.Is0,
                Padding = Padding.OnY.Is3,
                VerticalAlign = VerticalAlign.Top
            },
            Ths = new ThOptions
            {
                Selector = ".q-datatable thead th",
                TextSize = TextSize.Sm,
                FontWeight = FontWeight.Semibold
            },
            DataTableTopBars = new DataTableTopBarOptions
            {
                BackgroundColor = BackgroundColor.Token("[var(--surface)]")
            },
            DataTables = new DataTableThemeOptions
            {
                Width = Width.IsFull,
                MinWidth = Width.Token("[42rem]"),
                BackgroundColor = BackgroundColor.Transparent
            }
        };

        var result = ComponentsCssGenerator.Generate(theme);

        result.Should().Be(".q-datatable tbody td > a {\n  display: inline-flex;\n  min-width: 0;\n  max-width: 100%;\n  overflow: hidden;\n  gap: 0.5rem;\n  text-decoration: none;\n  align-items: center;\n}\n.q-datatable tbody td > a > div {\n  min-width: 0;\n  overflow: hidden;\n}\n.q-datatable tbody td > a > div > span {\n  display: block;\n  text-overflow: ellipsis;\n  overflow: hidden;\n  white-space: nowrap;\n}\n.q-datatable tbody tr {\n  border-bottom-width: 1px;\n  border-bottom-style: solid;\n  border-color: var(--border);\n}\n.q-datatable tbody td {\n  vertical-align: top;\n  padding-top: 0.75rem;\n  padding-bottom: 0.75rem;\n  min-width: 0;\n}\n.q-datatable thead th {\n  font-size: var(--text-sm);\n  font-weight: 600;\n}\n.q-datatable-top-bar {\n  background-color: var(--surface);\n}\n.q-datatable {\n  width: 100%;\n  min-width: 42rem;\n  background-color: transparent;\n}");
    }
}
