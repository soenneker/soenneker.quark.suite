using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Tests;

public class ThemeBuilderCompatibilityTests
{
    [Test]
    public void Generate_LeadpingTheme_ResolvesBuildersAndNestedSelectors()
    {
        var css = ComponentsCssGenerator.Generate(LeadpingThemeFixture.Build());
        css.Should().Contain("min-width: 42rem;");
        css.Should().Contain("background-color: transparent;");
        css.Should().Contain("background-color: var(--surface);");
        css.Should().Contain("padding-top: 0.75rem;");
        css.Should().Contain("padding-bottom: 0.75rem;");
        css.Should().Contain("margin-top: 0.125rem;");
        css.Should().Contain("padding-left: 0.875rem;");
        css.Should().Contain("padding-right: 0.875rem;");
        css.Should().Contain("line-height: 1.375;");
        css.Should().Contain("justify-content: space-between;");
        css.Should().Contain("transition-property: color, background-color");
        css.Should().Contain(":focus-visible {");
        css.Should().Contain("--tw-ring-shadow:");
        css.Should().Contain(":has(.q-datatable) {");
        css.Should().Contain("[data-active]");
        css.Should().Contain("[aria-selected='true']");
        css.Should().Contain("[data-state='active']");
        css.Should().NotContain("background-color: bg-");
    }

    [Test]
    public void Generate_ArbitrarySpacingAndChainedValues_PreserveAllDeclarations()
    {
        ComponentCssGenerator.Generate(new ComponentOptions
        {
            Padding = Padding.OnY.Is1.OnX.Token("[0.875rem]")
        }).Should().Be(":root {\n  padding-top: 0.25rem;\n  padding-bottom: 0.25rem;\n  padding-left: 0.875rem;\n  padding-right: 0.875rem;\n}");
    }

    [Test]
    public void Generate_AbsoluteSelector_DoesNotIncludeParent()
    {
        ComponentCssGenerator.Generate(new ComponentOptions
        {
            Selector = ".parent",
            Shadow = Shadow.None.WithSelector(".global", absolute: true)
        }).Should().Be(".global {\n  --tw-shadow: 0 0 #0000;\n  box-shadow: none;\n}");
    }

    [Test]
    public void Generate_TypedCssValue_IsAccepted()
    {
        CssValue<PaddingBuilder> padding = Padding.OnX.Is2;
        ComponentCssGenerator.Generate(new ComponentOptions { Padding = padding })
            .Should().Contain("padding-left: 0.5rem;");
    }

    [Test]
    public void Generate_BuilderSnapshot_DoesNotChangeWhenBuilderIsReused()
    {
        var builder = Padding.OnY.Is1;
        var options = new ComponentOptions { Padding = builder };
        _ = builder.OnX.Is3;
        ComponentCssGenerator.Generate(options).Should().NotContain("padding-left");
    }

    [Test]
    public void Generate_SelectorLists_ApplyEachStateToEachParent()
    {
        var css = ComponentCssGenerator.Generate(new ComponentOptions
        {
            Selector = ".one, .two",
            BackgroundColor = BackgroundColor.Transparent.WithSelector("&[data-active], &:not([data-state='a,b'])")
        });
        css.Should().Contain(".one[data-active], .two[data-active], .one:not([data-state='a,b']), .two:not([data-state='a,b']) {");
    }

    [Test]
    public void Generate_LiteralAndTypedValues_Coexist()
    {
        var css = ComponentCssGenerator.Generate(new InputOptions
        {
            Width = "calc(100% - 1rem)",
            Shadow = Shadow.None,
            Ring = Ring.OnFocusVisible.None
        });
        css.Should().Contain("width: calc(100% - 1rem);");
        css.Should().Contain("box-shadow: none;");
        css.Should().Contain("--tw-shadow: 0 0 #0000;");
        css.Should().Contain(":focus-visible {");
    }
}
