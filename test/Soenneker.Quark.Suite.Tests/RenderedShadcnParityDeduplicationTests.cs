using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Soenneker.Bradix;
namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public async Task Dedup_dialog_preserves_visibility_callbacks() => await VerifyOverlay<Dialog, DialogContent>(true);
    [Test]
    public async Task Dedup_sheet_preserves_visibility_callbacks() => await VerifyOverlay<Sheet, SheetContent>(false);
    [Test]
    public async Task Dedup_drawer_preserves_visibility_callbacks() => await VerifyOverlay<Drawer, DrawerContent>(false);

    private async Task VerifyOverlay<TOverlay, TContent>(bool staticEscape) where TOverlay : DialogOverlayElement where TContent : Microsoft.AspNetCore.Components.IComponent
    {
        var events = new List<string>();
        var cut = Render<TOverlay>(p => p.Add(c => c.Static, true)
            .Add(c => c.VisibleChanged, value => events.Add(value.ToString()))
            .Add(c => c.OnShow, () => events.Add("show"))
            .Add(c => c.OnHide, () => events.Add("hide"))
            .Add(c => c.OnBackdropClick, () => events.Add("outside"))
            .AddChildContent<TContent>());
        await cut.InvokeAsync(cut.Instance.Show);
        await cut.InvokeAsync(cut.Instance.Show);
        var content = cut.FindComponent<BradixDialogContent>().Instance;
        content.CloseOnPointerDownOutside.Should().BeFalse();
        content.CloseOnEscapeKeyDown.Should().Be(staticEscape);
        await cut.InvokeAsync(() => content.OnPointerDownOutside.InvokeAsync());
        cut.Instance.Visible.Should().BeTrue();
        await cut.InvokeAsync(cut.Instance.Hide);
        await cut.InvokeAsync(cut.Instance.Hide);
        events.Should().Equal("True", "show", "outside", "False", "hide");
    }

    [Test]
    public void Dedup_copyable_field_preserves_placeholder_copy_override_and_presets()
    {
        var textPreset = new QuarkPresetToken("text", c => c.TextSize = TextSize.Sm);
        var actionPreset = new QuarkPresetToken("action", c => c.Padding = Padding.Is2);
        var cut = Render<CopyableText>(p => p.Add(c => c.FieldAppearance, true).Add(c => c.Placeholder, "Not issued")
            .Add(c => c.TextPreset, textPreset).Add(c => c.CopyButtonPreset, actionPreset));
        cut.Find("[data-slot='copyable-text-value']").TextContent.Should().Be("Not issued");
        cut.FindComponent<CopyButton>().Instance.Text.Should().BeNull();
        cut.Find("button").HasAttribute("disabled").Should().BeTrue();
        cut.Render(p => p.Add(c => c.CopyText, "actual value"));
        cut.FindComponent<CopyButton>().Instance.Text.Should().Be("actual value");
        cut.Find("button").HasAttribute("disabled").Should().BeFalse();
        cut.FindComponent<CopyButton>().Instance.Preset.Should().Be(actionPreset);
        cut.Find("[data-slot='copyable-text-value']").ClassList.Should().Contain("text-sm");
    }

    [Test]
    public async Task Dedup_tag_autocomplete_wraps_and_keeps_free_form_entry()
    {
        var cut = Render<TagInput>(p => p.Add(c => c.EnableAutocomplete, true)
            .Add(c => c.AutocompleteOptions, new[] { "Alpha", "Beta" }));
        await cut.Find("input").FocusAsync();
        await cut.Find("input").KeyDownAsync(new KeyboardEventArgs { Key = "ArrowUp" });
        cut.Find("[data-active='true']").TextContent.Trim().Should().Be("Beta");
        await cut.Find("input").KeyDownAsync(new KeyboardEventArgs { Key = "Enter" });
        cut.Instance.Values.Should().Contain("Beta");
        await cut.Find("input").InputAsync(new ChangeEventArgs { Value = "Custom" });
        await cut.Find("input").KeyDownAsync(new KeyboardEventArgs { Key = "Enter" });
        cut.Instance.Values.Should().Contain("Custom");
    }
}
