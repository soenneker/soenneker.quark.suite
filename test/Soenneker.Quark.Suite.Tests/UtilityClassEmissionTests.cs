using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Width_and_explicit_class_preserve_identical_tokens()
    {
        var cut = Render<Div>(p => p
            .Add(c => c.Width, Width.Is0)
            .Add(c => c.Class, "w-0"));

        cut.Find("div").GetAttribute("class").Should().Be("w-0 w-0");
        cut.Find("div").HasAttribute("style").Should().BeFalse();
    }

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
    public void Explicit_style_preserves_duplicates()
    {
        var cut = Render<Div>(p => p
            .Add(c => c.Width, Width.Token("[100px]"))
            .Add(c => c.Style, "width: 200px; width: 300px"));

        cut.Find("div").GetAttribute("style").Should().Be("width: 200px; width: 300px");
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
    [Test]
    public void Raw_size_classes_do_not_suppress_component_defaults()
    {
        var icon = Render<Icon>(p => p.Add(c => c.Name, Soenneker.Lucide.Enums.Icons.LucideIcon.Check).Add(c => c.Class, "size-8"));
        icon.Find("svg").ClassList.Should().Contain("size-4").And.Contain("size-8");
        var simple = Render<SimpleIcon>(p => p.Add(c => c.Name, Soenneker.SimpleIcons.Enums.Icons.SimpleIconEnum.Github).AddUnmatched("class", "size-8"));
        simple.Find("svg").ClassList.Should().Contain("size-4").And.Contain("size-8");
        var spinner = Render<Spinner>(p => p.Add(c => c.Class, "size-8"));
        spinner.Find("[data-slot='spinner']").ClassList.Should().Contain("size-4").And.Contain("size-8");
    }

    [Test]
    public void Typed_size_replaces_the_default_without_interpreting_raw_classes()
    {
        var cut = Render<Icon>(p => p.Add(c => c.Name, Soenneker.Lucide.Enums.Icons.LucideIcon.Check)
            .Add(c => c.Size, Size.Is8).Add(c => c.Class, "size-8"));
        var classes = cut.Find("svg").GetAttribute("class");
        classes.Should().NotContain("size-4");
        System.Linq.Enumerable.Count(classes!.Split(' '), c => c == "size-8").Should().Be(2);
    }

    [Test]
    public void Avatar_badge_keeps_raw_background_and_uses_typed_default()
    {
        var cut = Render<AvatarBadge>(p => p.Add(c => c.Class, "bg-secondary"));
        cut.Find("span").ClassList.Should().Contain("bg-primary").And.Contain("bg-secondary");
        cut.Render(p => p.Add(c => c.BackgroundColor, BackgroundColor.Destructive));
        cut.Find("span").ClassList.Should().Contain("bg-destructive").And.Contain("bg-secondary").And.NotContain("bg-primary");
    }

}
