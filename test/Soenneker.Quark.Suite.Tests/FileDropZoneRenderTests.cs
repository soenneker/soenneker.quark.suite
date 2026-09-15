using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components.Forms;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public async Task FileDropZone_reordering_preserves_file_state_and_publishes_order()
    {
        var first = new FileDropZoneFile { Id = "a", Name = "a.pdf", State = FileDropZoneState.Uploading, Progress = 42 };
        var second = new FileDropZoneFile { Id = "b", Name = "b.pdf", ServerId = "saved" };
        var third = new FileDropZoneFile { Id = "c", Name = "c.pdf", State = FileDropZoneState.Processing };
        string[]? published = null;
        var cut = Render<FileDropZone>(p => p.Add(c => c.Files, [first, second, third])
            .Add(c => c.FilesChanged, files => published = files.Select(file => file.Id).ToArray()));
        await cut.InvokeAsync(() => cut.Instance.MoveFile("c", "a"));
        published.Should().Equal("c", "a", "b");
        cut.Instance.Files[1].Should().BeSameAs(first);
        cut.Instance.Files[1].Progress.Should().Be(42);
        await cut.InvokeAsync(() => cut.Instance.MoveFile("c", null));
        published.Should().Equal("a", "b", "c");
        await cut.InvokeAsync(() => cut.Instance.MoveFile("a", "missing"));
        published.Should().Equal("a", "b", "c");
        cut.Render(p => p.Add(c => c.AllowReorder, false));
        await cut.InvokeAsync(() => cut.Instance.MoveFile("c", "a"));
        cut.Instance.Files.Select(file => file.Id).Should().Equal("a", "b", "c");
        cut.FindAll("[data-file-drag-handle]").Should().BeEmpty();
    }

    [Test]
    public async Task FileDropZone_external_drop_inserts_new_files_at_the_requested_position()
    {
        var cut = Render<FileDropZone>(p => p.Add(c => c.Files, [new() { Id = "existing", Name = "existing.txt" }])
            .Add(c => c.Upload, (_, _) => ValueTask.FromResult(new FileDropZoneUploadResult { ServerId = "new" })));
        await cut.InvokeAsync(() => cut.Instance.SetDropTarget("existing"));
        cut.FindComponent<InputFile>().UploadFiles(InputFileContent.CreateFromText("new", "new.txt"));
        cut.Instance.Files.Select(file => file.Name).Should().Equal("new.txt", "existing.txt");
        cut.FindComponents<InputFile>().Last().UploadFiles(InputFileContent.CreateFromText("last", "last.txt"));
        cut.Instance.Files.Select(file => file.Name).Should().Equal("new.txt", "existing.txt", "last.txt");
    }

    [Test]
    public async Task FileDropZone_failure_collapses_preview_and_labeled_retry_restores_it()
    {
        var module = JSInterop.SetupModule("./_content/Soenneker.Quark.Suite/js/filedropzoneinterop.js");
        module.Setup<string>("createPreview", _ => true).SetResult("blob:retry-image");
        var fail = true;
        var cut = Render<FileDropZone>(p => p.Add(c => c.Upload, (_, _) =>
        {
            if (fail) throw new InvalidOperationException("Connection interrupted.");
            return ValueTask.FromResult(new FileDropZoneUploadResult { ServerId = "image" });
        }));
        cut.FindComponent<InputFile>().UploadFiles(InputFileContent.CreateFromText("image", "image.png", contentType: "image/png"));
        cut.FindAll("img").Should().BeEmpty();
        cut.Find("[role='alert']").TextContent.Should().Contain("Connection interrupted.");
        cut.Find("button[aria-label='Retry upload of image.png']").TextContent.Should().Contain("Retry");
        fail = false;
        await cut.Find("button[aria-label='Retry upload of image.png']").ClickAsync();
        cut.FindAll("[role='alert']").Should().BeEmpty();
        cut.Find("img").GetAttribute("src").Should().Be("blob:retry-image");
    }

    [Test]
    public async Task FileDropZone_local_preview_survives_upload_and_is_released_after_deletion()
    {
        var module = JSInterop.SetupModule("./_content/Soenneker.Quark.Suite/js/filedropzoneinterop.js");
        module.Setup<string>("createPreview", _ => true).SetResult("blob:local-image");
        var cut = Render<FileDropZone>(p => p
            .Add(c => c.Upload, (_, _) => ValueTask.FromResult(new FileDropZoneUploadResult { ServerId = "image" }))
            .Add(c => c.Delete, (_, _) => ValueTask.CompletedTask));
        cut.FindComponent<InputFile>().UploadFiles(InputFileContent.CreateFromText("image", "image.png", contentType: "image/png"));
        cut.WaitForAssertion(() => cut.Instance.Files.Single().State.Should().Be(FileDropZoneState.Complete));
        cut.Find("img").GetAttribute("src").Should().Be("blob:local-image");
        cut.FindComponents<InputFile>().Should().HaveCount(1);
        await cut.Find("button[aria-label='Delete image.png']").ClickAsync();
        cut.FindAll("img").Should().BeEmpty();
        module.Invocations.Should().Contain(invocation => invocation.Identifier == "retain");
    }

    [Test]
    public void FileDropZone_media_previews_support_existing_images_videos_and_pdf_fallback()
    {
        var cut = Render<FileDropZone>(p => p.Add(c => c.ReadOnly, true).Add(c => c.Files, [
            new() { Name = "photo.png", PreviewUrl = "/media/photo.png" },
            new() { Name = "clip.mp4", PreviewUrl = "/media/clip.mp4" },
            new() { Name = "paper.pdf", PreviewUrl = "/media/paper.pdf" }
        ]));
        cut.Find("img").GetAttribute("alt").Should().Be("photo.png");
        cut.Find("video").HasAttribute("controls").Should().BeTrue();
        cut.Find("video").HasAttribute("autoplay").Should().BeFalse();
        cut.Find("object").GetAttribute("type").Should().Be("application/pdf");
        cut.Find("a").TextContent.Should().Be("Open PDF");
        cut.Render(p => p.Add(c => c.ShowPreviews, false));
        cut.FindAll("[data-slot='file-drop-zone-preview']").Should().BeEmpty();
    }

    [Test]
    public void FileDropZone_preview_rejects_active_urls_and_explicit_nonmedia_content()
    {
        var cut = Render<FileDropZone>(p => p.Add(c => c.Files, [
            new() { Name = "image.png", PreviewUrl = "javascript:alert(1)" },
            new() { Name = "fake.png", ContentType = "text/html", PreviewUrl = "/file" }
        ]));
        cut.FindAll("[data-slot='file-drop-zone-preview']").Should().BeEmpty();
    }

    [Test]
    public void FileDropZone_existing_files_and_external_processing_updates_need_no_browser_file()
    {
        var file = new FileDropZoneFile { Name = "saved.pdf", ServerId = "server-1" };
        var cut = Render<FileDropZone>(p => p.Add(c => c.Files, [file]).Add(c => c.ReadOnly, true));
        cut.Markup.Should().Contain("Ready");
        cut.FindAll("input").Should().BeEmpty();
        cut.Render(p => p.Add(c => c.Files, [file with { State = FileDropZoneState.Processing, StatusText = "Checking for viruses" }]));
        cut.Find("[role='progressbar']").HasAttribute("aria-valuenow").Should().BeFalse();
        cut.Find("[role='progressbar']").GetAttribute("aria-valuetext").Should().Be("Checking for viruses");
        cut.Render(p => p.Add(c => c.Files, [file with { State = FileDropZoneState.Processing, Progress = 160 }]));
        cut.Find("[role='progressbar']").GetAttribute("aria-valuenow").Should().Be("100");
    }

    [Test]
    public async Task FileDropZone_delete_waits_for_server_and_keeps_failed_deletions_retryable()
    {
        var completion = new TaskCompletionSource();
        var file = new FileDropZoneFile { Name = "saved.pdf", ServerId = "server-1" };
        var cut = Render<FileDropZone>(p => p.Add(c => c.Files, [file]).Add(c => c.Delete, async (item, ct) =>
        {
            item.ServerId.Should().Be("server-1");
            await completion.Task;
        }));
        var deletion = cut.Find("button[aria-label='Delete saved.pdf']").ClickAsync();
        cut.Find("[data-state='deleting']").Should().NotBeNull();
        cut.Instance.Files.Should().HaveCount(1);
        completion.SetException(new InvalidOperationException("Delete failed. Try again."));
        await deletion;
        cut.Find("[role='alert']").TextContent.Should().Contain("Delete failed");
        cut.Instance.Files.Single().ServerId.Should().Be("server-1");
        cut.Render(p => p.Add(c => c.Delete, (_, _) => ValueTask.CompletedTask));
        await cut.Find("button[aria-label='Delete saved.pdf']").ClickAsync();
        cut.Instance.Files.Should().BeEmpty();
    }

    [Test]
    public void FileDropZone_saved_files_without_delete_handler_have_no_delete_action()
    {
        var cut = Render<FileDropZone>(p => p.Add(c => c.Files, [new() { Name = "saved.pdf", ServerId = "server-1" }]));
        cut.FindAll("button").Should().BeEmpty();
        cut.Find("input").HasAttribute("disabled").Should().BeTrue();
    }

    [Test]
    public void FileDropZone_rejects_invalid_files_before_calling_transport()
    {
        var calls = 0;
        var cut = Render<FileDropZone>(p => p.Add(c => c.Accept, ".pdf").Add(c => c.MaxFileSize, 4)
            .Add(c => c.Upload, (_, _) => { calls++; return ValueTask.FromResult(new FileDropZoneUploadResult { ServerId = "id" }); }));
        cut.FindComponent<InputFile>().UploadFiles(InputFileContent.CreateFromText("12345", "big.pdf"), InputFileContent.CreateFromText("abc", "bad.txt"));
        cut.WaitForAssertion(() => cut.Find("[role='alert']").TextContent.Should().Contain("big.pdf").And.Contain("bad.txt"));
        calls.Should().Be(0);
        cut.Instance.Files.Should().BeEmpty();
    }

    [Test]
    public void FileDropZone_reports_stages_and_accepts_background_processing_result()
    {
        var cut = Render<FileDropZone>(p => p.Add(c => c.Upload, async (request, ct) =>
        {
            await request.ReportUploadProgress(100);
            await request.ReportProcessingProgress("Checking for viruses");
            return new FileDropZoneUploadResult { ServerId = "scan-1", State = FileDropZoneState.Processing, StatusText = "Checking for viruses" };
        }));
        cut.FindComponent<InputFile>().UploadFiles(InputFileContent.CreateFromText("abc", "scan.pdf"));
        cut.WaitForAssertion(() => cut.Instance.Files.Single().ServerId.Should().Be("scan-1"));
        cut.Instance.Files.Single().State.Should().Be(FileDropZoneState.Processing);
        cut.Find("[role='progressbar']").HasAttribute("aria-valuenow").Should().BeFalse();
        cut.FindComponents<InputFile>().Should().HaveCount(1);
    }

    [Test]
    public async Task FileDropZone_failed_upload_can_retry_after_another_selection()
    {
        var fail = true;
        var cut = Render<FileDropZone>(p => p.Add(c => c.Upload, (request, ct) =>
        {
            if (fail) throw new InvalidOperationException("Try again");
            return ValueTask.FromResult(new FileDropZoneUploadResult { ServerId = request.File.Name });
        }));
        cut.FindComponent<InputFile>().UploadFiles(InputFileContent.CreateFromText("one", "one.pdf"));
        cut.WaitForAssertion(() => cut.Find("button[aria-label='Retry upload of one.pdf']").Should().NotBeNull());
        fail = false;
        cut.FindComponents<InputFile>().Last().UploadFiles(InputFileContent.CreateFromText("two", "two.pdf"));
        cut.WaitForAssertion(() => cut.Instance.Files.Last().ServerId.Should().Be("two.pdf"));
        cut.FindComponents<InputFile>().Should().HaveCount(2);
        await cut.Find("button[aria-label='Retry upload of one.pdf']").ClickAsync();
        cut.Instance.Files.First().ServerId.Should().Be("one.pdf");
        cut.FindComponents<InputFile>().Should().HaveCount(1);
    }

    [Test]
    public async Task FileDropZone_cancellation_reaches_transport_and_preserves_retry()
    {
        var cut = Render<FileDropZone>(p => p.Add(c => c.Upload, async (_, ct) =>
        {
            await Task.Delay(Timeout.Infinite, ct);
            return new FileDropZoneUploadResult { ServerId = "never" };
        }));
        var input = cut.FindComponent<InputFile>();
        var upload = Task.Run(() => input.UploadFiles(InputFileContent.CreateFromText("abc", "cancel.pdf")));
        cut.WaitForAssertion(() => cut.Find("button[aria-label='Cancel upload of cancel.pdf']").Should().NotBeNull());
        await cut.Find("button[aria-label='Cancel upload of cancel.pdf']").ClickAsync();
        await upload.WaitAsync(TimeSpan.FromSeconds(10));
        cut.WaitForAssertion(() => cut.Instance.Files.Single().State.Should().Be(FileDropZoneState.Canceled));
        cut.Find("button[aria-label='Retry upload of cancel.pdf']").Should().NotBeNull();
    }
}
