using AwesomeAssertions;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public class BootstrapCssGeneratorTests
{
    [Fact]
    public void GenerateRootCss_WithNullVariables_ReturnsEmptyString()
    {
        // Act
        var result = BootstrapCssGenerator.GenerateRootCss(null!);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void GenerateRootCss_WithEmptyVariables_ReturnsEmptyString()
    {
        // Arrange
        var variables = new BootstrapCssVariables();

        // Act
        var result = BootstrapCssGenerator.GenerateRootCss(variables);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void GenerateRootCss_WithValidColors_GeneratesCorrectCss()
    {
        // Arrange
        var variables = new BootstrapCssVariables
        {
            Colors = new BootstrapColorsCssVariables
            {
                Primary = "#2563eb",
                Secondary = "#7c3aed"
            }
        };

        // Act
        var result = BootstrapCssGenerator.GenerateRootCss(variables);

        // Assert
        result.Should().Contain("--bs-primary: #2563eb;");
        result.Should().Contain("--bs-secondary: #7c3aed;");
        result.Should().StartWith(":root {");
        result.Should().EndWith("}");
    }

    [Fact]
    public void GenerateRootCss_WithBasicColors_GeneratesCorrectCss()
    {
        // Arrange
        var variables = new BootstrapCssVariables
        {
            Colors = new BootstrapColorsCssVariables
            {
                Blue = "#0d6efd",
                Indigo = "#6610f2",
                Purple = "#6f42c1",
                Pink = "#d63384",
                Red = "#dc3545",
                Orange = "#fd7e14",
                Yellow = "#ffc107",
                Green = "#198754",
                Teal = "#20c997",
                Cyan = "#0dcaf0",
                Black = "#000",
                White = "#fff",
                Gray = "#6c757d"
            }
        };

        // Act
        var result = BootstrapCssGenerator.GenerateRootCss(variables);

        // Assert
        result.Should().Contain("--bs-blue: #0d6efd;");
        result.Should().Contain("--bs-indigo: #6610f2;");
        result.Should().Contain("--bs-purple: #6f42c1;");
        result.Should().Contain("--bs-pink: #d63384;");
        result.Should().Contain("--bs-red: #dc3545;");
        result.Should().Contain("--bs-orange: #fd7e14;");
        result.Should().Contain("--bs-yellow: #ffc107;");
        result.Should().Contain("--bs-green: #198754;");
        result.Should().Contain("--bs-teal: #20c997;");
        result.Should().Contain("--bs-cyan: #0dcaf0;");
        result.Should().Contain("--bs-black: #000;");
        result.Should().Contain("--bs-white: #fff;");
        result.Should().Contain("--bs-gray: #6c757d;");
    }

    [Fact]
    public void GenerateRootCss_WithThemeColors_GeneratesCorrectCss()
    {
        // Arrange
        var variables = new BootstrapCssVariables
        {
            Colors = new BootstrapColorsCssVariables
            {
                Primary = "#0d6efd",
                Secondary = "#6c757d",
                Success = "#198754",
                Info = "#0dcaf0",
                Warning = "#ffc107",
                Danger = "#dc3545",
                Light = "#f8f9fa",
                Dark = "#212529"
            }
        };

        // Act
        var result = BootstrapCssGenerator.GenerateRootCss(variables);

        // Assert
        result.Should().Contain("--bs-primary: #0d6efd;");
        result.Should().Contain("--bs-secondary: #6c757d;");
        result.Should().Contain("--bs-success: #198754;");
        result.Should().Contain("--bs-info: #0dcaf0;");
        result.Should().Contain("--bs-warning: #ffc107;");
        result.Should().Contain("--bs-danger: #dc3545;");
        result.Should().Contain("--bs-light: #f8f9fa;");
        result.Should().Contain("--bs-dark: #212529;");
    }

    [Fact]
    public void GenerateRootCss_WithButtonVariables_GeneratesClassSpecificCss()
    {
        // Arrange
        var variables = new BootstrapCssVariables
        {
            PrimaryButton = new BootstrapPrimaryButtonCssVariables
            {
                Color = "#fff",
                Background = "#0d6efd",
                BorderColor = "#0d6efd",
                HoverColor = "#fff",
                HoverBackground = "#0b5ed7",
                HoverBorderColor = "#0a58ca"
            }
        };

        // Act
        var result = BootstrapCssGenerator.GenerateRootCss(variables);

        // Assert
        result.Should().Contain(".btn-primary {");
        result.Should().Contain("  --bs-btn-color: #fff;");
        result.Should().Contain("  --bs-btn-bg: #0d6efd;");
        result.Should().Contain("  --bs-btn-border-color: #0d6efd;");
        result.Should().Contain("  --bs-btn-hover-color: #fff;");
        result.Should().Contain("  --bs-btn-hover-bg: #0b5ed7;");
        result.Should().Contain("  --bs-btn-hover-border-color: #0a58ca;");
    }

    [Fact]
    public void GenerateRootCss_WithMixedVariables_GeneratesBothRootAndClassCss()
    {
        // Arrange
        var variables = new BootstrapCssVariables
        {
            Colors = new BootstrapColorsCssVariables
            {
                Primary = "#0d6efd",
                Secondary = "#6c757d"
            },
            PrimaryButton = new BootstrapPrimaryButtonCssVariables
            {
                Color = "#fff",
                Background = "#0d6efd"
            }
        };

        // Act
        var result = BootstrapCssGenerator.GenerateRootCss(variables);

        // Assert
        // Should contain both :root and .btn-primary selectors
        result.Should().Contain(":root {");
        result.Should().Contain(".btn-primary {");
        result.Should().Contain("  --bs-primary: #0d6efd;");
        result.Should().Contain("  --bs-secondary: #6c757d;");
        result.Should().Contain("  --bs-btn-color: #fff;");
        result.Should().Contain("  --bs-btn-bg: #0d6efd;");
    }

    [Fact]
    public void GenerateRootCss_WithSecondaryButtonVariables_GeneratesCorrectCss()
    {
        // Arrange
        var variables = new BootstrapCssVariables
        {
            SecondaryButton = new BootstrapSecondaryButtonCssVariables
            {
                Color = "#fff",
                Background = "#6c757d",
                BorderColor = "#6c757d",
                HoverColor = "#fff",
                HoverBackground = "#5c636a",
                HoverBorderColor = "#565e64"
            }
        };

        // Act
        var result = BootstrapCssGenerator.GenerateRootCss(variables);

        // Assert
        result.Should().Contain(".btn-secondary {");
        result.Should().Contain("  --bs-btn-color: #fff;");
        result.Should().Contain("  --bs-btn-bg: #6c757d;");
        result.Should().Contain("  --bs-btn-border-color: #6c757d;");
        result.Should().Contain("  --bs-btn-hover-color: #fff;");
        result.Should().Contain("  --bs-btn-hover-bg: #5c636a;");
        result.Should().Contain("  --bs-btn-hover-border-color: #565e64;");
    }

    [Fact]
    public void GenerateRootCss_WithLinkVariables_GeneratesRootOverrides()
    {
        // Arrange
        var variables = new BootstrapCssVariables
        {
            Link = new BootstrapLinkCssVariables
            {
                LinkColorRgb = "31, 41, 55",
                LinkOpacity = "1",
                LinkHoverColorRgb = "33, 37, 41",
                LinkHoverOpacity = "0",
                LinkUnderlineOpacity = "0"
            }
        };

        // Act
        var result = BootstrapCssGenerator.GenerateRootCss(variables);

        // Assert - Should generate :root overrides, NOT a {} selectors
        result.Should().Contain(":root {");
        result.Should().NotContain("a {");
        result.Should().NotContain("a:hover {");
        result.Should().Contain("  --bs-link-color-rgb: 31, 41, 55;");
        result.Should().Contain("  --bs-link-opacity: 1;");
        result.Should().Contain("  --bs-link-hover-color-rgb: 33, 37, 41;");
        result.Should().Contain("  --bs-link-hover-opacity: 0;");
        result.Should().Contain("  --bs-link-underline-opacity: 0;");
    }

    [Fact]
    public void GenerateRootCss_WithLinkVariables_DoesNotGenerateSelectorSpecificCss()
    {
        // Arrange
        var variables = new BootstrapCssVariables
        {
            Link = new BootstrapLinkCssVariables
            {
                LinkColorRgb = "13, 110, 253",
                LinkOpacity = "1"
            }
        };

        // Act
        var result = BootstrapCssGenerator.GenerateRootCss(variables);

        // Assert - Verify it does NOT create selector-specific blocks
        result.Should().NotContain("a {");
        result.Should().NotContain("a:hover {");
        result.Should().Contain(":root {");
    }

    [Fact]
    public void GenerateRootCss_WithLinkAndTextDecoration_GeneratesDirectCssPropertyInRoot()
    {
        // Arrange
        var variables = new BootstrapCssVariables
        {
            Link = new BootstrapLinkCssVariables
            {
                LinkColorRgb = "31, 41, 55",
                TextDecoration = "none"
            }
        };

        // Act
        var result = BootstrapCssGenerator.GenerateRootCss(variables);

        // Assert - Direct CSS properties should also be in :root
        result.Should().Contain(":root {");
        result.Should().NotContain("a {");
        result.Should().Contain("  --bs-link-color-rgb: 31, 41, 55;");
        result.Should().Contain("  text-decoration: none;");
    }

    [Fact]
    public void GenerateRootCss_WithCompleteLinkSetup_GeneratesOnlyRootOverrides()
    {
        // Arrange - All link variables should be in :root only
        var variables = new BootstrapCssVariables
        {
            Link = new BootstrapLinkCssVariables
            {
                LinkColorRgb = "31, 41, 55",
                LinkOpacity = "1",
                LinkHoverColorRgb = "33, 37, 41",
                LinkHoverOpacity = "0",
                LinkUnderlineOpacity = "0"
            }
        };

        // Act
        var result = BootstrapCssGenerator.GenerateRootCss(variables);

        // Assert - Verify only :root block is generated, no selector-specific blocks
        result.Should().Contain(":root {");
        result.Should().NotContain("a {");
        result.Should().NotContain("a:hover {");

        // Verify all variables are in the root block
        result.Should().Contain("  --bs-link-color-rgb: 31, 41, 55;");
        result.Should().Contain("  --bs-link-opacity: 1;");
        result.Should().Contain("  --bs-link-hover-color-rgb: 33, 37, 41;");
        result.Should().Contain("  --bs-link-hover-opacity: 0;");
        result.Should().Contain("  --bs-link-underline-opacity: 0;");
    }

    [Fact]
    public void GenerateRootCss_WithLinkAndColorVariables_GeneratesSingleRootBlock()
    {
        // Arrange - Mix link and color variables
        var variables = new BootstrapCssVariables
        {
            Colors = new BootstrapColorsCssVariables
            {
                Primary = "#0d6efd"
            },
            Link = new BootstrapLinkCssVariables
            {
                LinkColorRgb = "13, 110, 253",
                LinkOpacity = "1"
            }
        };

        // Act
        var result = BootstrapCssGenerator.GenerateRootCss(variables);

        // Assert - All should be in single :root block
        result.Should().Contain(":root {");
        result.Should().NotContain("a {");
        result.Should().Contain("  --bs-primary: #0d6efd;");
        result.Should().Contain("  --bs-link-color-rgb: 13, 110, 253;");
        result.Should().Contain("  --bs-link-opacity: 1;");
    }

    [Fact]
    public void AnchorOptions_WithTextDecoration_CanBeSet()
    {
        // Arrange
        var anchorOptions = new AnchorOptions
        {
            TextDecoration = TextDecoration.None
        };

        // Act & Assert
        anchorOptions.TextDecoration.Should().NotBeNull();
        anchorOptions.TextDecoration.Value.IsEmpty.Should().BeFalse();
    }

    [Fact]
    public void AnchorOptions_WithTextDecorationNone_GeneratesCorrectCss()
    {
        // Arrange
        var builder = TextDecoration.None;
        var anchorOptions = new AnchorOptions
        {
            TextDecoration = builder
        };

        // Act
        var cssClass = builder.ToClass();
        var cssStyle = builder.ToStyle();

        // Assert
        cssClass.Should().Contain("text-decoration-none");
        cssStyle.Should().Contain("text-decoration: none");
        anchorOptions.TextDecoration.Value.ToString().Should().Contain("text-decoration-none");
    }

    [Fact]
    public void AnchorOptions_WithTextDecorationUnderline_GeneratesCorrectCss()
    {
        // Arrange
        var builder = TextDecoration.Underline;
        var anchorOptions = new AnchorOptions
        {
            TextDecoration = builder
        };

        // Act
        var cssClass = builder.ToClass();
        var cssStyle = builder.ToStyle();

        // Assert
        cssClass.Should().Contain("text-decoration-underline");
        cssStyle.Should().Contain("text-decoration: underline");
        anchorOptions.TextDecoration.Value.ToString().Should().Contain("text-decoration-underline");
    }

    [Fact]
    public void AnchorOptions_WithTextDecorationLineThrough_GeneratesCorrectCss()
    {
        // Arrange
        var builder = TextDecoration.LineThrough;
        var anchorOptions = new AnchorOptions
        {
            TextDecoration = builder
        };

        // Act
        var cssClass = builder.ToClass();
        var cssStyle = builder.ToStyle();

        // Assert
        cssClass.Should().Contain("text-decoration-line-through");
        cssStyle.Should().Contain("text-decoration: line-through");
        anchorOptions.TextDecoration.Value.ToString().Should().Contain("text-decoration-line-through");
    }

    [Fact]
    public void AnchorOptions_WithComplexTextDecoration_GeneratesCorrectCss()
    {
        // Arrange - Create a complex text decoration with style, color, and thickness
        var builder = TextDecoration.Underline.Wavy.Primary.Medium;
        var anchorOptions = new AnchorOptions
        {
            TextDecoration = builder
        };

        // Act
        var cssStyle = builder.ToStyle();

        // Assert - Verify all parts are in the generated CSS
        cssStyle.Should().Contain("text-decoration:");
        cssStyle.Should().Contain("underline");
        cssStyle.Should().Contain("wavy");
        cssStyle.Should().Contain("var(--bs-primary)");
        cssStyle.Should().Contain("2px"); // Medium thickness

        // Verify it was set on the options
        anchorOptions.TextDecoration.Should().NotBeNull();
    }

    [Fact]
    public void AnchorOptions_WithTextDecorationAndStyle_GeneratesFullCss()
    {
        // Arrange - Test with underline, solid style, and danger color
        var builder = TextDecoration.Underline.Solid.Danger;
        var anchorOptions = new AnchorOptions
        {
            TextDecoration = builder
        };

        // Act
        var cssStyle = builder.ToStyle();

        // Assert
        cssStyle.Should().Contain("text-decoration:");
        cssStyle.Should().Contain("underline");
        cssStyle.Should().Contain("solid");
        cssStyle.Should().Contain("var(--bs-danger)");

        // Verify it was set on the options
        anchorOptions.TextDecoration.Should().NotBeNull();
    }

    [Fact]
    public void AnchorOptions_WithInheritedProperties_HasTextDecorationProperty()
    {
        // Arrange
        var anchorOptions = new AnchorOptions();

        // Act - Verify that TextDecoration property exists (inherited from ComponentOptions)
        var property = anchorOptions.GetType().GetProperty("TextDecoration");

        // Assert
        property.Should().NotBeNull("AnchorOptions should inherit TextDecoration from ComponentOptions");
        property!.PropertyType.Should().Be(typeof(CssValue<TextDecorationBuilder>?));
    }

    [Fact]
    public void AnchorOptions_MultiplePropertiesIncludingTextDecoration_AllWork()
    {
        // Arrange - Set multiple properties to verify they all work together
        var textDecorationBuilder = TextDecoration.None;

        var anchorOptions = new AnchorOptions
        {
            TextDecoration = textDecorationBuilder,
            TextAlignment = TextAlignment.Center
        };

        // Act & Assert
        anchorOptions.TextDecoration.Should().NotBeNull();
        anchorOptions.TextAlignment.Should().NotBeNull();

        // Verify TextDecoration was set correctly
        var textDecorationClass = textDecorationBuilder.ToClass();
        textDecorationClass.Should().Contain("text-decoration-none");
        anchorOptions.TextDecoration.Value.ToString().Should().Contain("text-decoration-none");
    }

    [Fact]
    public void ComponentCssGenerator_WithAnchorOptions_GeneratesAnchorCss()
    {
        // Arrange
        var builder = TextDecoration.Underline;
        var anchorOptions = new AnchorOptions
        {
            TextDecoration = builder
        };

        // Act
        var css = ComponentCssGenerator.GenerateSelectorCss(anchorOptions);

        // Assert
        css.Should().Contain("a {");
        css.Should().Contain("text-decoration: underline");
        css.Should().Contain("}");
    }

    [Fact]
    public void ComponentCssGenerator_WithAnchorOptionsNone_GeneratesCorrectCss()
    {
        // Arrange
        var anchorOptions = new AnchorOptions
        {
            TextDecoration = TextDecoration.None
        };

        // Act
        var css = ComponentCssGenerator.GenerateSelectorCss(anchorOptions);

        // Assert
        css.Should().Contain("a {");
        css.Should().Contain("text-decoration: none");
        css.Should().Contain("}");
    }

    [Fact]
    public void ComponentCssGenerator_WithComplexTextDecoration_GeneratesFullCssRule()
    {
        // Arrange - Complex text decoration with style, color, thickness
        var anchorOptions = new AnchorOptions
        {
            TextDecoration = TextDecoration.Underline.Wavy.Primary.Medium
        };

        // Act
        var css = ComponentCssGenerator.GenerateSelectorCss(anchorOptions);

        // Assert
        css.Should().Contain("a {");
        css.Should().Contain("text-decoration:");
        css.Should().Contain("underline");
        css.Should().Contain("wavy");
        css.Should().Contain("var(--bs-primary)");
        css.Should().Contain("2px");
        css.Should().Contain("}");
    }

    [Fact]
    public void ComponentCssGenerator_WithLineThrough_GeneratesCorrectCssRule()
    {
        // Arrange
        var anchorOptions = new AnchorOptions
        {
            TextDecoration = TextDecoration.LineThrough
        };

        // Act
        var css = ComponentCssGenerator.GenerateSelectorCss(anchorOptions);

        // Assert
        css.Should().Contain("a {");
        css.Should().Contain("text-decoration: line-through");
        css.Should().Contain("}");
    }

    [Fact]
    public void ComponentCssGenerator_WithMultipleProperties_GeneratesAllCssRules()
    {
        // Arrange
        var anchorOptions = new AnchorOptions
        {
            TextDecoration = TextDecoration.None,
            TextAlignment = TextAlignment.Center
        };

        // Act
        var css = ComponentCssGenerator.GenerateSelectorCss(anchorOptions);

        // Assert
        css.Should().Contain("a {");
        css.Should().Contain("text-decoration: none");
        css.Should().Contain("text-align:");
        css.Should().Contain("}");
    }

    [Fact]
    public void ComponentCssGenerator_WithEmptyOptions_ReturnsEmptyString()
    {
        // Arrange
        var anchorOptions = new AnchorOptions();

        // Act
        var css = ComponentCssGenerator.GenerateSelectorCss(anchorOptions);

        // Assert
        css.Should().BeEmpty();
    }

    [Fact]
    public void ComponentCssGenerator_WithNullOptions_ReturnsEmptyString()
    {
        // Act
        var css = ComponentCssGenerator.GenerateSelectorCss(null!);

        // Assert
        css.Should().BeEmpty();
    }
}