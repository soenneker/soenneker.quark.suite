using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Utility_properties_emit_classes_without_converting_them_to_styles()
    {
        var cut = Render<Div>(p => p
            .Add(c => c.Padding, Padding.Is0)
            .Add(c => c.Width, Width.IsFull)
            .Add(c => c.Style, "padding: 20px"));

        var element = cut.Find("div");
        element.ClassList.Should().Contain("p-0");
        element.ClassList.Should().Contain("w-full");
        element.GetAttribute("style").Should().Be("padding: 20px");
    }

    [Test]
    public void Explicit_style_follows_builder_inline_values_without_resolving_duplicates()
    {
        var cut = Render<Div>(p => p
            .Add(c => c.Width, (CssValue<WidthBuilder>) 100)
            .Add(c => c.Style, "width: 200px; width: 300px"));

        cut.Find("div").GetAttribute("style").Should().Be("width: 100px; width: 200px; width: 300px");
    }

    [Test]
    public void Conflicting_utility_classes_are_preserved_in_input_order()
    {
        var cut = Render<Div>(p => p
            .Add(c => c.Padding, (CssValue<PaddingBuilder>) "p-0 p-4")
            .Add(c => c.Class, "p-8 p-0"));

        var element = cut.Find("div");
        element.GetAttribute("class").Should().Be("p-0 p-4 p-8 p-0");
        element.HasAttribute("style").Should().BeFalse();
    }
}
