using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Attachments_match_ai_elements_slot_and_variant_contract()
    {
        var cut = Render<Attachments>(parameters => parameters
            .Add(p => p.Variant, "grid")
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<Attachment>(0);
                builder.AddAttribute(1, nameof(Attachment.Name), "screenshot.png");
                builder.AddAttribute(2, nameof(Attachment.Description), "Uploading");
                builder.AddAttribute(3, nameof(Attachment.Progress), 64);
                builder.CloseComponent();
            })));

        var attachments = cut.Find("[data-slot='attachments']");
        attachments.GetAttribute("data-variant").Should().Be("grid");
        attachments.GetAttribute("class")!.Should().Contain("data-[variant=grid]:grid");

        var attachment = cut.Find("[data-slot='attachment']");
        attachment.TextContent.Should().Contain("screenshot.png");
        cut.Find("[data-slot='attachment-info']").Should().NotBeNull();
        cut.Find("[data-slot='attachment-name']").TextContent.Should().Be("screenshot.png");
        cut.Find("[data-slot='attachment-description']").TextContent.Should().Be("Uploading");

        var progress = cut.Find("[data-slot='attachment-progress']");
        progress.GetAttribute("role").Should().Be("progressbar");
        progress.GetAttribute("aria-valuenow").Should().Be("64");
    }

    [Test]
    public void Attachment_subcomponents_support_composed_ai_elements_slots()
    {
        var cut = Render<Attachments>(parameters => parameters
            .Add(p => p.Variant, "inline")
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<Attachment>(0);
                builder.AddAttribute(1, nameof(Attachment.Variant), "detailed");
                builder.AddAttribute(2, nameof(Attachment.ChildContent), (RenderFragment)(attachmentBuilder =>
                {
                    attachmentBuilder.OpenComponent<AttachmentRemove>(0);
                    attachmentBuilder.AddAttribute(1, nameof(AttachmentRemove.ChildContent), (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Remove")));
                    attachmentBuilder.CloseComponent();
                }));
                builder.CloseComponent();

                builder.OpenComponent<Attachment>(10);
                builder.AddAttribute(11, nameof(Attachment.Name), "manual.txt");
                builder.AddAttribute(12, nameof(Attachment.Description), "Composed metadata");
                builder.AddAttribute(13, nameof(Attachment.Progress), 140);
                builder.CloseComponent();

                builder.OpenComponent<AttachmentsEmpty>(20);
                builder.AddAttribute(21, nameof(AttachmentsEmpty.ChildContent), (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "No files")));
                builder.CloseComponent();
            })));

        cut.Find("[data-slot='attachments']").GetAttribute("data-variant").Should().Be("inline");
        cut.Find("[data-slot='attachment-remove']").TextContent.Should().Be("Remove");
        cut.Find("[data-slot='attachments-empty']").TextContent.Should().Be("No files");

        var progress = cut.Find("[data-slot='attachment-progress']");
        progress.GetAttribute("aria-valuenow").Should().Be("100");
    }
}
