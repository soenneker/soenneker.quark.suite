using AwesomeAssertions;


namespace Soenneker.Quark.Suite.Tests;

public class ComponentsCssGeneratorTests
{
    [Test]
    public void Generate_EmitsLiteralValuesWithoutUtilityInterpretation()
    {
        var options = new ComponentOptions
        {
            Width = "calc(100% - var(--sidebar-width))",
            Height = "100dvh",
            MinHeight = "4rem",
            MaxHeight = "none",
            BackgroundColor = "oklch(0.7 0.15 30)"
        };
        ComponentCssGenerator.Generate(options).Should().Be(
            ":root {\n  width: calc(100% - var(--sidebar-width));\n  height: 100dvh;\n  min-height: 4rem;\n  max-height: none;\n  background-color: oklch(0.7 0.15 30);\n}");
    }

    [Test]
    public void Generate_DoesNotRepairClassTokensPassedAsCssValues()
    {
        ComponentCssGenerator.Generate(new ComponentOptions { Width = "w-0", Padding = "p-2 md:p-4" })
            .Should().Be(":root {\n  padding: p-2 md:p-4;\n  width: w-0;\n}");
    }

    [Test]
    public void Generate_PreservesCustomDeclarationsDuplicatesAndNestedSelectors()
    {
        var options = new ComponentOptions
        {
            Selector = ".card",
            Width = "20rem",
            Declarations = [new("--surface", "var(--card)"), new("width", "30rem"), new("width", "30rem")],
            Rules = [new() { Selector = "&:hover", TextColor = "var(--primary)" },
                new() { Selector = ".title", FontWeight = "600" }]
        };
        ComponentCssGenerator.Generate(options).Should().Be(
            ".card {\n  width: 20rem;\n  --surface: var(--card);\n  width: 30rem;\n  width: 30rem;\n}\n" +
            ".card:hover {\n  color: var(--primary);\n}\n.card .title {\n  font-weight: 600;\n}");
    }

    [Test]
    public void Generate_PreservesSemicolonsInsideCssValues()
    {
        var options = new ComponentOptions
        {
            Declarations = [new("content", "\"a;b\""), new("background-image", "url('data:image/svg+xml;base64,PHN2Zy8+')")]
        };
        ComponentCssGenerator.Generate(options).Should().Be(
            ":root {\n  content: \"a;b\";\n  background-image: url('data:image/svg+xml;base64,PHN2Zy8+');\n}");
    }

    [Test]
    public void Generate_WithEmptyOptions_ReturnsEmptyString()
    {
        ComponentCssGenerator.Generate(new ComponentOptions()).Should().BeEmpty();
    }

    [Test]
    public void Generate_WithRepeatedChildSelectors_PreservesBlockAndDeclarationOrder()
    {
        var options = new CardOptions
        {
            Selector = ".card",
            Display = "block",
            Anchors = new AnchorOptions { Selector = "& .shared", Display = "flex" },
            Bodies = new CardBodyOptions { Selector = "&", Display = "grid" },
            Buttons = new ButtonOptions { Selector = "& .other", Display = "inline" },
            Descriptions = new CardDescriptionOptions { Selector = "& .shared", Display = "block" }
        };

        ComponentCssGenerator.Generate(options).Should().Be(
            ".card {\n  display: block;\n  display: grid;\n}\n" +
            ".card .shared {\n  display: flex;\n  display: block;\n}\n" +
            ".card .other {\n  display: inline;\n}");
    }

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
                DecorationLine = "underline"
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
                DecorationLine = "underline"
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
                Shrink = "0"
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
                FlexDirection = "column",
                FlexWrap = "wrap",
                Grow = "0"
            }
        };

        var result = ComponentsCssGenerator.Generate(theme);

        result.Should().Be("[data-slot='div'] {\n  flex-direction: column;\n  flex-wrap: wrap;\n  flex-grow: 0;\n}");
    }

    [Test]
    public void Generate_WithFlexWrap_PreservesInlineFlexDisplay()
    {
        var theme = new Theme
        {
            Divs = new DivOptions
            {
                Display = "inline-flex",
                FlexDirection = "column",
                FlexWrap = "wrap"
            }
        };

        var result = ComponentsCssGenerator.Generate(theme);
        result.Should().Contain("display: inline-flex;");
        result.Should().Contain("flex-wrap: wrap;");
        result.Should().NotContain("display: flex;");
    }
    [Test]
    public void Generate_WithExplicitCssValues_ReturnsComponentCss()
    {
        var theme = new Theme
        {
            Anchors = new AnchorOptions
            {
                Selector = "a",
                DecorationLine = "none",
                TextColor = "var(--primary)"
            },
            Buttons = new ButtonOptions
            {
                BackgroundColor = "var(--primary)",
                TextColor = "var(--color-blue-100)",
                Rounded = "0.5rem",
                Padding = "0.5rem 1rem"
            },
            Cards = new CardOptions
            {
                BackgroundColor = "var(--card)",
                Rounded = "0.75rem",
                Shadow = "0 1px 3px 0 rgb(0 0 0 / 0.1), 0 1px 2px -1px rgb(0 0 0 / 0.1)"
            }
        };

        var result = ComponentsCssGenerator.Generate(theme);

        result.Should().Be("a {\n  color: var(--primary);\n  text-decoration: none;\n}\n[data-slot='button'] {\n  padding: 0.5rem 1rem;\n  color: var(--color-blue-100);\n  background-color: var(--primary);\n  border-radius: 0.5rem;\n}\n[data-slot='card'] {\n  box-shadow: 0 1px 3px 0 rgb(0 0 0 / 0.1), 0 1px 2px -1px rgb(0 0 0 / 0.1);\n  background-color: var(--card);\n  border-radius: 0.75rem;\n}");
    }

    [Test]
    public void Generate_WithDataTableCssValues_ReturnsComponentCss()
    {
        var theme = new Theme
        {
            Anchors = new AnchorOptions
            {
                Selector = ".q-datatable tbody td > a",
                Display = "inline-flex",
                MaxWidth = "100%",
                MinWidth = "0",
                ItemsAlign = "center",
                Gap = "0.5rem",
                Overflow = "hidden",
                DecorationLine = "none"
            },
            Divs = new DivOptions
            {
                Selector = ".q-datatable tbody td > a > div",
                MinWidth = "0",
                Overflow = "hidden"
            },
            Spans = new SpanOptions
            {
                Selector = ".q-datatable tbody td > a > div > span",
                Display = "block",
                Overflow = "hidden",
                TextOverflow = "ellipsis",
                Whitespace = "nowrap"
            },
            Trs = new TrOptions
            {
                Selector = ".q-datatable tbody tr",
                Declarations = [new("border-bottom-width", "1px"), new("border-bottom-style", "solid")],
                BorderColor = "var(--border)"
            },
            Tds = new TdOptions
            {
                Selector = ".q-datatable tbody td",
                MinWidth = "0",
                Declarations = [new("padding-top", "0.75rem"), new("padding-bottom", "0.75rem")],
                VerticalAlign = "top"
            },
            Ths = new ThOptions
            {
                Selector = ".q-datatable thead th",
                TextSize = "var(--text-sm)",
                FontWeight = "600"
            },
            DataTableTopBars = new DataTableTopBarOptions
            {
                BackgroundColor = "var(--surface)"
            },
            DataTables = new DataTableThemeOptions
            {
                Width = "100%",
                MinWidth = "42rem",
                BackgroundColor = "transparent"
            }
        };

        var result = ComponentsCssGenerator.Generate(theme);

        result.Should().Be(".q-datatable tbody td > a {\n  display: inline-flex;\n  min-width: 0;\n  max-width: 100%;\n  overflow: hidden;\n  gap: 0.5rem;\n  text-decoration: none;\n  align-items: center;\n}\n.q-datatable tbody td > a > div {\n  min-width: 0;\n  overflow: hidden;\n}\n.q-datatable tbody td > a > div > span {\n  display: block;\n  text-overflow: ellipsis;\n  overflow: hidden;\n  white-space: nowrap;\n}\n.q-datatable tbody tr {\n  border-color: var(--border);\n  border-bottom-width: 1px;\n  border-bottom-style: solid;\n}\n.q-datatable tbody td {\n  vertical-align: top;\n  min-width: 0;\n  padding-top: 0.75rem;\n  padding-bottom: 0.75rem;\n}\n.q-datatable thead th {\n  font-size: var(--text-sm);\n  font-weight: 600;\n}\n.q-datatable-top-bar {\n  background-color: var(--surface);\n}\n.q-datatable {\n  width: 100%;\n  min-width: 42rem;\n  background-color: transparent;\n}");
    }
}
