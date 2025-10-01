using AwesomeAssertions;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public class BootstrapCssGeneratorTests
{
    [Fact]
    public void GenerateRootCss_WithNullVariables_ReturnsEmptyString()
    {
        // Act
        var result = BootstrapCssGenerator.GenerateRootCss((BootstrapCssVariables)null!);

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
}