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

        textareaClasses.Should().Contain("min-h-16");
        textareaClasses.Should().Contain("rounded-md");
        textareaClasses.Should().Contain("text-base");
        textareaClasses.Should().Contain("md:text-sm");
        textareaClasses.Should().Contain("aria-invalid:ring-3");
        textareaClasses.Should().NotContain("aria-invalid:ring-[3px]");
        textareaClasses.Should().NotContain("q-textarea");
    }
}
