using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components.Forms;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public async Task FileDrop_forwards_files_and_updates_disabled_and_picker_parameters()
    {
        Services.AddQuarkFileDropAsScoped();
        var module = JSInterop.SetupModule("./_content/Soenneker.Quark.Suite/js/filedropinterop.js");
        module.SetupVoid("register", _ => true).SetVoidResult();
        module.SetupVoid("unregister", _ => true).SetVoidResult();
        var count = 0;
        var cut = Render<FileDrop>(p => p.Add(c => c.InputId, "attachments")
            .Add(c => c.Accept, "image/*")
            .Add(c => c.OnFilesDropped, args => count += args.FileCount)
            .AddChildContent("<textarea></textarea>"));
        cut.Find("textarea").HasAttribute("disabled").Should().BeFalse();
        cut.Find("input").Id.Should().Be("attachments");
        cut.FindComponent<InputFile>().UploadFiles(InputFileContent.CreateFromText("file", "test.txt"));
        count.Should().Be(1);
        cut.Render(p => p.Add(c => c.Disabled, true).Add(c => c.Multiple, false).Add(c => c.Accept, ".pdf"));
        cut.Find("input").HasAttribute("disabled").Should().BeTrue();
        cut.Find("input").HasAttribute("multiple").Should().BeFalse();
        cut.Find("input").GetAttribute("accept").Should().Be(".pdf");
        cut.Find("[data-slot='file-drop']").GetAttribute("data-file-drop-disabled").Should().Be("true");
        cut.FindComponent<InputFile>().UploadFiles(InputFileContent.CreateFromText("file", "test.txt"));
        count.Should().Be(1);
        await cut.Instance.DisposeAsync();
        module.VerifyInvoke("unregister");
    }
}
