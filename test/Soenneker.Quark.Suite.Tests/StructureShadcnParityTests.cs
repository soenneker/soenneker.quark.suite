using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Structure_components_emit_semantic_tags_and_theme_slots()
    {
        var cut = Render<Div>(parameters => parameters.Add(p => p.ChildContent, (RenderFragment)(builder =>
        {
            builder.OpenComponent<Main>(0);
            builder.AddAttribute(1, "ChildContent", (RenderFragment)(mainBuilder =>
            {
                mainBuilder.OpenComponent<Section>(0);
                mainBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(sectionBuilder =>
                {
                    sectionBuilder.OpenComponent<Article>(0);
                    sectionBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(articleBuilder => articleBuilder.AddContent(0, "Article content")));
                    sectionBuilder.CloseComponent();

                    sectionBuilder.OpenComponent<Aside>(2);
                    sectionBuilder.AddAttribute(3, "ChildContent", (RenderFragment)(asideBuilder => asideBuilder.AddContent(0, "Aside content")));
                    sectionBuilder.CloseComponent();
                }));
                mainBuilder.CloseComponent();
            }));
            builder.CloseComponent();
        })));

        cut.Find("div[data-slot='div']").Should().NotBeNull();
        cut.Find("main[data-slot='main']").TextContent.Should().Contain("Article content");
        cut.Find("section[data-slot='section']").Should().NotBeNull();
        cut.Find("article[data-slot='article']").TextContent.Should().Contain("Article content");
        cut.Find("aside[data-slot='aside']").TextContent.Should().Contain("Aside content");
    }

    [Test]
    public void Figure_fieldset_and_details_preserve_native_accessibility_contracts()
    {
        var cut = Render<Div>(parameters => parameters.Add(p => p.ChildContent, (RenderFragment)(builder =>
        {
            builder.OpenComponent<Figure>(0);
            builder.AddAttribute(1, "ChildContent", (RenderFragment)(figureBuilder =>
            {
                figureBuilder.AddMarkupContent(0, "<img alt=\"Chart\" />");
                figureBuilder.OpenComponent<Figcaption>(1);
                figureBuilder.AddAttribute(2, "ChildContent", (RenderFragment)(captionBuilder => captionBuilder.AddContent(0, "Chart caption")));
                figureBuilder.CloseComponent();
            }));
            builder.CloseComponent();

            builder.OpenComponent<Fieldset>(3);
            builder.AddAttribute(4, nameof(Fieldset.Disabled), true);
            builder.AddAttribute(5, nameof(Fieldset.Form), "profile-form");
            builder.AddAttribute(6, "ChildContent", (RenderFragment)(fieldsetBuilder =>
            {
                fieldsetBuilder.OpenComponent<Legend>(0);
                fieldsetBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(legendBuilder => legendBuilder.AddContent(0, "Profile")));
                fieldsetBuilder.CloseComponent();
                fieldsetBuilder.AddMarkupContent(2, "<input name=\"first\" />");
            }));
            builder.CloseComponent();

            builder.OpenComponent<Details>(7);
            builder.AddAttribute(8, nameof(Details.Open), true);
            builder.AddAttribute(9, "ChildContent", (RenderFragment)(detailsBuilder =>
            {
                detailsBuilder.OpenComponent<Summary>(0);
                detailsBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(summaryBuilder => summaryBuilder.AddContent(0, "More")));
                detailsBuilder.CloseComponent();
                detailsBuilder.AddContent(2, "Native disclosure content");
            }));
            builder.CloseComponent();
        })));

        cut.Find("figure[data-slot='figure'] figcaption[data-slot='figcaption']").TextContent.Should().Be("Chart caption");

        var fieldset = cut.Find("fieldset[data-slot='field-set']");
        fieldset.GetAttribute("disabled").Should().NotBeNull();
        fieldset.GetAttribute("form").Should().Be("profile-form");
        fieldset.GetAttribute("class")!.Should().Contain("flex-col");
        cut.Find("legend[data-slot='field-legend']").TextContent.Should().Be("Profile");

        var details = cut.Find("details[data-slot='details']");
        details.GetAttribute("open").Should().NotBeNull();
        cut.Find("summary[data-slot='summary']").TextContent.Should().Be("More");
    }
}
