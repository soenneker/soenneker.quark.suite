using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Textarea_matches_shadcn_base_classes()
    {
        var textarea = Render<TextArea>(parameters => parameters.Add(p => p.Placeholder, "Type your message here."));

        var textareaClasses = textarea.Find("[data-slot='textarea']").GetAttribute("class")!;

        textareaClasses.Should().Contain("flex");
        textareaClasses.Should().Contain("field-sizing-content");
        textareaClasses.Should().Contain("min-h-16");
        textareaClasses.Should().Contain("w-full");
        textareaClasses.Should().Contain("rounded-lg");
        textareaClasses.Should().Contain("border");
        textareaClasses.Should().Contain("border-input");
        textareaClasses.Should().Contain("bg-transparent");
        textareaClasses.Should().Contain("px-2.5");
        textareaClasses.Should().Contain("py-2");
        textareaClasses.Should().Contain("text-base");
        textareaClasses.Should().Contain("md:text-sm");
        textareaClasses.Should().Contain("transition-colors");
        textareaClasses.Should().Contain("focus-visible:ring-3");
        textareaClasses.Should().Contain("disabled:bg-input/50");
        textareaClasses.Should().Contain("dark:disabled:bg-input/80");
        textareaClasses.Should().Contain("aria-invalid:ring-3");
        textareaClasses.Should().Contain("aria-invalid:ring-destructive/20");
        textareaClasses.Should().Contain("dark:aria-invalid:border-destructive/50");
        textareaClasses.Should().NotContain("rounded-md");
        textareaClasses.Should().NotContain("px-3");
        textareaClasses.Should().NotContain("transition-[color,shadow]");
        textareaClasses.Should().NotContain("transition-[color,box-shadow]");
        textareaClasses.Should().NotContain("focus-visible:ring-[3px]");
        textareaClasses.Should().NotContain("shadow-xs");
        textareaClasses.Should().NotContain("q-textarea");
    }

    [Test]
    public void Textarea_matches_shadcn_field_and_rtl_contract()
    {
        var cut = Render<DirectionProvider>(parameters => parameters
            .Add(p => p.Dir, "rtl")
            .AddChildContent<Field>(field => field
                .AddChildContent<FieldLabel>(label => label
                    .Add(p => p.For, "feedback")
                    .AddChildContent("التعليقات"))
                .AddChildContent<TextArea>(textarea => textarea
                    .Add(p => p.Id, "feedback")
                    .Add(p => p.Rows, 4)
                    .Add(p => p.Placeholder, "تعليقاتك تساعدنا على التحسين..."))
                .AddChildContent<FieldDescription>(description => description
                    .AddChildContent("شاركنا أفكارك حول خدمتنا."))));

        var field = cut.Find("[data-slot='field']");
        var textarea = cut.Find("[data-slot='textarea']");

        field.GetAttribute("dir").Should().Be("rtl");
        textarea.GetAttribute("dir").Should().Be("rtl");
        textarea.GetAttribute("aria-describedby").Should().BeNull();
        textarea.GetAttribute("rows").Should().Be("4");
    }
}
