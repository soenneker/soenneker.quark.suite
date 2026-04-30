using AwesomeAssertions;
using Bunit;
using System;
using System.IO;
using System.Linq;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Checks_demo_page_uses_shadcn_preview_overrides()
    {
        var cut = Render<global::Soenneker.Quark.Suite.Demo.Pages.Components.Checks>();
        var previews = cut.FindAll("[data-slot='preview']");
        var previewInners = previews
            .Select(node => node.FirstElementChild!)
            .ToList();

        previewInners.Should().Contain(node => node.GetAttribute("class")!.Contains("h-80"));
        previewInners.Should().Contain(node => node.GetAttribute("class")!.Contains("p-4 md:p-8"));

        var rtlPreview = previews.First(node => node.GetAttribute("dir") == "rtl");
        rtlPreview.GetAttribute("data-lang").Should().Be("ar");
        rtlPreview.FirstElementChild!.GetAttribute("class")!.Should().Contain("h-80");
    }

    [Test]
    public void Checks_demo_page_matches_current_shadcn_examples()
    {
        var source = File.ReadAllText(Path.Combine(GetSuiteRootForChecks(), "test", "Soenneker.Quark.Suite.Demo", "Pages", "Components", "Checks.razor"));

        source.Should().Contain("A control that allows the user to toggle between checked and not checked.");
        source.Should().Contain("Pair the checkbox with Field and FieldLabel for proper layout and labeling.");
        source.Should().Contain("Use FieldContent and FieldDescription for helper text.");
        source.Should().Contain("By clicking this checkbox, you agree to the terms and conditions.");
        source.Should().Contain("Use the disabled prop to prevent interaction and add the data-disabled attribute to the Field component for disabled styles.");
        source.Should().Contain("Set aria-invalid on the checkbox and data-invalid on the field wrapper to show the invalid styles.");
        source.Should().Contain("Use multiple fields to create a checkbox list.");
        source.Should().Contain("toggle-checkbox-2-rtl");
        source.Should().Contain("يمكنك تفعيل أو إلغاء تفعيل الإشعارات في أي وقت.");
        source.Should().NotContain("By clicking this checkbox, you agree to the terms.");
        source.Should().NotContain("بالنقر على هذا المربع، فإنك توافق على الشروط.</FieldDescription>");
    }

    private static string GetSuiteRootForChecks()
    {
        var directory = AppContext.BaseDirectory;

        while (!File.Exists(Path.Combine(directory, "Soenneker.Quark.Suite.slnx")))
            directory = Directory.GetParent(directory)!.FullName;

        return directory;
    }
}
