using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void DemoSection_preview_shell_matches_shadcn_docs_structure_more_closely()
    {
        var cut = Render<Components.DemoSection>(parameters => parameters
            .Add(p => p.Title, "Demo")
            .Add(p => p.Description, "Description")
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "<div>Preview</div>"))));

        var previewCard = cut.Find("[data-slot='component-preview']");
        var preview = cut.Find("[data-slot='preview']");
        var previewInner = preview.FirstElementChild!;
        var contentInner = previewInner.QuerySelector(".mx-auto");
        var previewCardClasses = previewCard.GetAttribute("class")!;
        var previewClasses = previewInner.GetAttribute("class")!;

        previewCardClasses.Should().Contain("group");
        previewCardClasses.Should().Contain("relative");
        previewCardClasses.Should().Contain("mb-12");
        previewCardClasses.Should().Contain("overflow-hidden");
        previewCardClasses.Should().Contain("rounded-xl");
        previewCardClasses.Should().Contain("border");

        preview.GetAttribute("dir").Should().Be("ltr");
        previewInner.GetAttribute("data-align").Should().Be("center");
        previewInner.GetAttribute("data-chromeless").Should().Be("false");
        previewClasses.Should().Contain("preview");
        previewClasses.Should().Contain("h-72");
        previewClasses.Should().Contain("w-full");
        previewClasses.Should().Contain("justify-center");
        previewClasses.Should().Contain("p-10");
        previewClasses.Should().Contain("data-[align=start]:items-start");
        contentInner.Should().NotBeNull();
        contentInner!.GetAttribute("class")!.Should().Contain("items-center");
        contentInner.GetAttribute("class")!.Should().Contain("justify-center");
    }

    [Test]
    public void DemoSection_can_render_shadcn_rtl_preview_toolbar()
    {
        var cut = Render<Components.DemoSection>(parameters => parameters
            .Add(p => p.PreviewDir, "rtl")
            .Add(p => p.PreviewLang, "ar")
            .Add(p => p.ShowPreviewToolbar, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "<div>Preview</div>"))));

        var preview = cut.Find("[data-slot='preview']");
        var toolbar = cut.Find("[data-slot='preview-toolbar']");
        var languageSelect = toolbar.QuerySelector("select[aria-label='Language']");

        preview.GetAttribute("dir").Should().Be("rtl");
        preview.GetAttribute("data-lang").Should().Be("ar");
        toolbar.GetAttribute("dir").Should().Be("ltr");
        toolbar.GetAttribute("class")!.Should().Contain("h-14");
        toolbar.GetAttribute("class")!.Should().Contain("border-b");
        languageSelect.Should().NotBeNull();
        languageSelect!.GetAttribute("dir").Should().Be("ltr");
        languageSelect.TextContent.Should().Contain("Arabic (العربية)");
    }
}
