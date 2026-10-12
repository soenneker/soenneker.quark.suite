using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Bradix;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Audit_interactive_explicit_open_is_controlled_without_change_callback()
    {
        var popover = Render<Popover>(p => p.Add(c => c.Open, true));
        popover.FindComponent<BradixPopover>().Instance.Open.Should().BeTrue();
        var hover = Render<HoverCard>(p => p.Add(c => c.Open, true));
        hover.FindComponent<BradixHoverCard>().Instance.Open.Should().BeTrue();
        var tooltip = Render<Tooltip>(p => p.Add(c => c.Open, true));
        tooltip.FindComponent<BradixTooltip>().Instance.Open.Should().BeTrue();

        var uncontrolled = Render<Popover>(p => p.Add(c => c.DefaultOpen, true));
        uncontrolled.FindComponent<BradixPopover>().Instance.Open.Should().BeNull();
    }

    [Test]
    public void Audit_interactive_static_overlays_disable_escape_dismissal()
    {
        var alert = Render<AlertDialog>(p => p.Add(c => c.Visible, true).Add(c => c.Static, true)
            .AddChildContent<AlertDialogContent>());
        alert.FindComponent<BradixAlertDialogContent>().Instance.CloseOnEscapeKeyDown.Should().BeFalse();

        var sheet = Render<Sheet>(p => p.Add(c => c.Visible, true).Add(c => c.Static, true)
            .AddChildContent<SheetContent>());
        sheet.FindComponent<BradixDialogContent>().Instance.CloseOnEscapeKeyDown.Should().BeFalse();
    }

    [Test]
    public async Task Audit_interactive_tab_click_callback_uses_click_instead_of_mousedown()
    {
        int clicks = 0;
        var cut = Render<Tabs>(p => p.AddChildContent<TabsList>(list => list
            .AddChildContent<TabsTrigger>(trigger => trigger.Add(c => c.Value, "one")
                .Add(c => c.OnClick, _ => clicks++).AddChildContent("One"))));
        await cut.Find("button").MouseDownAsync(new MouseEventArgs { Button = 2 });
        clicks.Should().Be(0);
        await cut.Find("button").ClickAsync();
        clicks.Should().Be(1);
    }

    [Test]
    public async Task Audit_interactive_button_links_preserve_browser_navigation()
    {
        var navigation = Services.GetRequiredService<NavigationManager>();
        string initialUri = navigation.Uri;
        var cut = Render<Button>(p => p.Add(c => c.Href, "/download").Add(c => c.Target, Target.Blank)
            .Add(c => c.Download, "report.csv").AddChildContent("Download"));
        cut.Find("a").GetAttribute("blazor:onclick:preventDefault").Should().NotBe("true");
        await cut.Find("a").ClickAsync(new MouseEventArgs { CtrlKey = true });
        navigation.Uri.Should().Be(initialUri);
    }

    [Test]
    public async Task Audit_interactive_collapsible_slot_preserves_attributes_callbacks_and_disabled_state()
    {
        int triggerClicks = 0;
        int buttonClicks = 0;
        var cut = Render<Collapsible>(p => p.AddChildContent<CollapsibleTrigger>(trigger => trigger
            .Add(c => c.AsChild, true).Add(c => c.Class, "trigger-class")
            .Add(c => c.Attributes, new Dictionary<string, object> { ["aria-label"] = "Toggle details" })
            .Add(c => c.OnClick, _ => triggerClicks++)
            .AddChildContent<Button>(button => button.Add(c => c.OnClick, _ => buttonClicks++).AddChildContent("Details"))));

        var button = cut.Find("button");
        button.ClassList.Should().Contain("trigger-class");
        button.GetAttribute("aria-label").Should().Be("Toggle details");
        await button.ClickAsync();
        triggerClicks.Should().Be(1);
        buttonClicks.Should().Be(1);
        cut.Find("button").GetAttribute("aria-expanded").Should().Be("true");

        cut.FindComponent<CollapsibleTrigger>().Render(p => p.Add(c => c.Disabled, true));
        cut.Find("button").HasAttribute("disabled").Should().BeTrue();
        await cut.Find("button").ClickAsync();
        triggerClicks.Should().Be(1);
        buttonClicks.Should().Be(1);
    }

    [Test]
    public async Task Audit_interactive_combobox_disabled_root_rejects_synthetic_selection()
    {
        int changes = 0;
        var cut = Render<Combobox>(p => p.Add(c => c.Disabled, true).Add(c => c.Open, true)
            .Add(c => c.ValueChanged, _ => changes++)
            .AddChildContent<ComboboxItem>(item => item.Add(c => c.Value, "one").AddChildContent("One")));
        await cut.Find("[role='option']").ClickAsync();
        changes.Should().Be(0);
    }

    [Test]
    public void Audit_interactive_disabled_styles_do_not_disable_enabled_controls()
    {
        var cut = Render<Button>(p => p.AddChildContent("Save"));
        var classes = cut.Find("button").ClassList;
        classes.Should().Contain("disabled:pointer-events-none").And.Contain("disabled:opacity-50");
        classes.Should().NotContain("pointer-events-none").And.NotContain("opacity-50");
    }
    [Test]
    public async Task Audit_interactive_input_group_emits_one_value_change_for_input_then_change()
    {
        int changes = 0;
        var cut = Render<InputGroupInput>(p => p.Add(c => c.ValueChanged, _ => changes++));
        await cut.Find("input").InputAsync(new ChangeEventArgs { Value = "hello" });
        await cut.Find("input").ChangeAsync(new ChangeEventArgs { Value = "hello" });
        changes.Should().Be(1);
        cut.Render(p => p.Add(c => c.ReadOnly, true));
        await cut.Find("input").InputAsync(new ChangeEventArgs { Value = "ignored" });
        cut.Instance.Value.Should().Be("hello");
        changes.Should().Be(1);
    }

    [Test]
    public async Task Audit_interactive_tag_removal_is_a_button_and_respects_minimum()
    {
        int changes = 0;
        var cut = Render<TagInput>(p => p.Add(c => c.Values, new[] { "one" }).Add(c => c.MinTags, 1)
            .Add(c => c.ClearAll, true).Add(c => c.ValuesChanged, _ => changes++));
        var remove = cut.Find("[data-slot='tag-input-tag-remove']");
        remove.GetAttribute("type").Should().Be("button");
        await remove.ClickAsync();
        await cut.Find("[data-slot='tag-input-clear-all']").ClickAsync();
        changes.Should().Be(0);
        cut.Instance.Values.Should().Equal("one");
    }

    [Test]
    public async Task Audit_interactive_color_picker_preserves_hex_alpha_and_rejects_nonfinite_coordinates()
    {
        string? changed = null;
        var cut = Render<ColorPicker>(p => p.Add(c => c.Inline, true).Add(c => c.Value, "#f008")
            .Add(c => c.ValueChanged, value => changed = value));
        await cut.InvokeAsync(() => cut.Instance.SetCanvasColor(100, 50));
        changed.Should().Be("#ff000088");
        changed = null;
        await cut.InvokeAsync(() => cut.Instance.SetCanvasColor(double.NaN, 50));
        changed.Should().BeNull();
        cut.Find("[data-slot='color-picker-canvas']").GetAttribute("style").Should().Contain("transparent 50%");
    }

    [Test]
    public async Task Audit_interactive_cascader_navigation_skips_disabled_options()
    {
        var cut = Render<Cascader>(p => p.Add(c => c.Options, new CascaderOption[]
        {
            new() { Value = "disabled", Label = "Disabled", Disabled = true },
            new() { Value = "one", Label = "One" },
            new() { Value = "two", Label = "Two" }
        }));
        await cut.InvokeAsync(() => cut.FindComponent<Popover>().Instance.OpenChanged.InvokeAsync(true));
        cut.Find("[data-value='one']").GetAttribute("tabindex").Should().Be("0");
        await cut.Find("[data-value='one']").KeyDownAsync(new KeyboardEventArgs { Key = "ArrowUp" });
        cut.Find("[data-value='one']").GetAttribute("tabindex").Should().Be("0");
        await cut.Find("[data-value='one']").KeyDownAsync(new KeyboardEventArgs { Key = "ArrowDown" });
        cut.Find("[data-value='two']").GetAttribute("tabindex").Should().Be("0");
    }

    [Test]
    public async Task Audit_interactive_sidebar_slot_actions_forward_their_click_callback()
    {
        int clicks = 0;
        var cut = Render<SidebarMenuAction>(p => p.Add(c => c.AsChild, true).Add(c => c.OnClick, _ => clicks++)
            .AddChildContent<Button>(button => button.AddChildContent("Action")));
        await cut.Find("button").ClickAsync();
        clicks.Should().Be(1);
    }

    [Test]
    public async Task Audit_interactive_false_disabled_attribute_does_not_suppress_button_click()
    {
        int clicks = 0;
        var cut = Render<Button>(p => p.Add(c => c.Attributes, new Dictionary<string, object> { ["disabled"] = false })
            .Add(c => c.OnClick, _ => clicks++));
        await cut.Find("button").ClickAsync();
        clicks.Should().Be(1);
    }

    [Test]
    public async Task Audit_interactive_context_menu_radio_group_is_exclusive_and_preserves_controlled_value()
    {
        string? changed = null;
        var cut = Render<ContextMenu>(p => p.AddChildContent<ContextMenuContent>(content => content
            .Add(c => c.ForceMount, true).AddChildContent<ContextMenuRadioGroup>(group => group
                .Add(c => c.Value, "one").Add(c => c.ValueChanged, value => changed = value)
                .AddChildContent<ContextMenuRadioItem>(item => item.Add(c => c.Value, "one").AddChildContent("One"))
                .AddChildContent<ContextMenuRadioItem>(item => item.Add(c => c.Value, "two").AddChildContent("Two")))));
        var items = cut.FindAll("[role='menuitemradio']");
        items[0].GetAttribute("aria-checked").Should().Be("true");
        items[1].GetAttribute("aria-checked").Should().Be("false");
        await items[1].ClickAsync();
        changed.Should().Be("two");
        cut.FindAll("[role='menuitemradio']")[0].GetAttribute("aria-checked").Should().Be("true");
        cut.FindAll("[role='menuitemradio']")[1].GetAttribute("aria-checked").Should().Be("false");
        cut.FindComponent<ContextMenuRadioGroup>().Render(p => p.Add(c => c.Value, "two"));
        cut.FindAll("[role='menuitemradio']")[0].GetAttribute("aria-checked").Should().Be("false");
        cut.FindAll("[role='menuitemradio']")[1].GetAttribute("aria-checked").Should().Be("true");
    }

    [Test]
    public async Task Audit_interactive_checks_and_switches_forward_attributes_and_legacy_switch_mode()
    {
        var cut = Render<Check>(p => p.Add(c => c.Class, "custom-check").Add(c => c.Style, "color:red")
            .Add(c => c.CheckSize, CheckSizes.Lg).Add(c => c.AriaLabel, "Accept")
            .Add(c => c.Attributes, new Dictionary<string, object> { ["class"] = "extra-check", ["style"] = "margin:1px" }));
        var control = cut.Find("[role='checkbox']");
        control.ClassList.Should().Contain("custom-check").And.Contain("extra-check").And.Contain("h-5");
        control.GetAttribute("style").Should().Contain("color:red").And.Contain("margin:1px");
        control.GetAttribute("aria-label").Should().Be("Accept");
        cut.Render(p => p.Add(c => c.Class, "updated-check").Add(c => c.Switch, true));
        cut.Find("[role='switch']").ClassList.Should().Contain("updated-check");
        await cut.Find("[role='switch']").ClickAsync();
        cut.Instance.Checked.Should().BeTrue();

        var toggle = Render<Switch>(p => p.Add(c => c.Class, "custom-switch").Add(c => c.Style, "color:blue")
            .Add(c => c.AriaLabel, "Notify").Add(c => c.AriaLabelledBy, "notify-label"));
        toggle.Find("[role='switch']").ClassList.Should().Contain("custom-switch");
        toggle.Find("[role='switch']").GetAttribute("style").Should().Contain("color:blue");
        toggle.Find("[role='switch']").GetAttribute("aria-label").Should().Be("Notify");
        toggle.Find("[role='switch']").GetAttribute("aria-labelledby").Should().Be("notify-label");
    }

    [Test]
    public async Task Audit_interactive_keyboard_resize_honors_pixel_limits_and_rtl()
    {
        var cut = Render<ResizablePanelGroup>(p => p.AddChildContent<ResizablePanel>(panel => panel.Add(c => c.MinSize, 490))
            .AddChildContent<ResizableHandle>()
            .AddChildContent<ResizablePanel>(panel => panel.Add(c => c.MinSize, 490)));
        await cut.InvokeAsync(() => cut.Instance.HandleKeyboardResize(0, "ArrowRight", true, 1000));
        cut.Find("[role='separator']").GetAttribute("aria-valuenow").Should().Be("51");
        await cut.InvokeAsync(() => cut.Instance.HandleKeyboardResize(0, "ArrowLeft", true, 1000));
        cut.Find("[role='separator']").GetAttribute("aria-valuenow").Should().Be("49");
        cut.Render(p => p.Add(c => c.Dir, "rtl"));
        await cut.InvokeAsync(() => cut.Instance.HandleKeyboardResize(0, "ArrowLeft", true, 1000));
        cut.Find("[role='separator']").GetAttribute("aria-valuenow").Should().Be("51");
    }

    [Test]
    public async Task Audit_interactive_otp_active_slot_tracks_native_selection()
    {
        var interop = (FakeInputInterop)Services.GetRequiredService<IInputInterop>();
        interop.Selection = new InputSelectionSnapshot { Start = 1, End = 1, Value = "1234" };
        var cut = Render<InputOtp>(p => p.Add(c => c.DefaultOtpValue, "1234").AddChildContent(builder =>
        {
            for (int index = 0; index < 6; index++)
            {
                builder.OpenComponent<InputOtpSlot>(0);
                builder.AddAttribute(1, nameof(InputOtpSlot.Index), index);
                builder.CloseComponent();
            }
        }));
        await cut.Find("input").FocusAsync();
        cut.FindAll("[data-slot='input-otp-slot']")[1].GetAttribute("data-active").Should().Be("true");
        interop.Selection.Start = 3;
        await cut.Find("input").KeyUpAsync(new KeyboardEventArgs { Key = "ArrowRight" });
        cut.FindAll("[data-slot='input-otp-slot']")[3].GetAttribute("data-active").Should().Be("true");
        interop.Selection.Start = 0;
        await cut.Find("input").ClickAsync();
        cut.FindAll("[data-slot='input-otp-slot']")[0].GetAttribute("data-active").Should().Be("true");
    }

    [Test]
    public async Task Audit_interactive_command_keyboard_matches_sorted_items_in_dom_group_order()
    {
        var cut = Render<Command>(p => p.AddChildContent<CommandGroup>(group => group.Add(c => c.Heading, "Z group")
                .AddChildContent<CommandItem>(item => item.Add(c => c.Id, "z-item").Add(c => c.SearchValue, "find z"))
                .AddChildContent<CommandItem>(item => item.Add(c => c.Id, "a-item").Add(c => c.SearchValue, "find a")))
            .AddChildContent<CommandGroup>(group => group.Add(c => c.Heading, "A group")
                .AddChildContent<CommandItem>(item => item.Add(c => c.Id, "next-group").Add(c => c.SearchValue, "find b"))));
        await cut.InvokeAsync(() => cut.Instance.SetSearch("find"));
        cut.Instance.ActiveItemId.Should().Be("a-item");
        await cut.InvokeAsync(() => cut.Instance.HandleInputNavigation("ArrowDown"));
        cut.Instance.ActiveItemId.Should().Be("z-item");
        await cut.InvokeAsync(() => cut.Instance.HandleInputNavigation("ArrowDown"));
        cut.Instance.ActiveItemId.Should().Be("next-group");
    }

    [Test]
    [Arguments("Check")]
    [Arguments("Switch")]
    [Arguments("Radio")]
    [Arguments("TextArea")]
    public async Task Audit_interactive_form_controls_rebind_validation_owner_and_expression(string control)
    {
        Services.AddScoped<IValidationInterop, FakeValidationInterop>();
        var boolModel = new { First = false, Second = true };
        var textModel = new { First = "first", Second = "second" };
        var editContext = new EditContext(control == "TextArea" ? (object)textModel : boolModel);
        var first = Render<Validation>(p => p.AddCascadingValue(nameof(EditContext), editContext));
        var second = Render<Validation>(p => p.AddCascadingValue(nameof(EditContext), editContext));
        object expression = control == "TextArea" ? (Expression<Func<string?>>)(() => textModel.First) : (Expression<Func<bool>>)(() => boolModel.First);
        Type type = control switch { "Check" => typeof(Check), "Switch" => typeof(Switch), "Radio" => typeof(Radio), _ => typeof(TextArea) };
        RenderFragment content = builder =>
        {
            builder.OpenComponent(0, type);
            builder.AddAttribute(1, control == "TextArea" ? "ValueExpression" : "CheckedExpression", expression);
            builder.CloseComponent();
        };
        var host = Render<CascadingValue<Validation>>(p => p.Add(c => c.Name, "ParentValidation")
            .Add(c => c.Value, first.Instance).Add(c => c.ChildContent, content));
        first.Instance.FieldIdentifier.FieldName.Should().Be("First");
        expression = control == "TextArea" ? (Expression<Func<string?>>)(() => textModel.Second) : (Expression<Func<bool>>)(() => boolModel.Second);
        host.Render(p => p.Add(c => c.Value, second.Instance).Add(c => c.ChildContent, content));
        second.Instance.FieldIdentifier.FieldName.Should().Be("Second");
        int Count() => control switch
        {
            "Check" => host.FindComponent<Check>().RenderCount,
            "Switch" => host.FindComponent<Switch>().RenderCount,
            "Radio" => host.FindComponent<Radio>().RenderCount,
            _ => host.FindComponent<TextArea>().RenderCount
        };
        int renders = Count();
        await host.InvokeAsync(() => first.Instance.NotifyValidationStatusChanged(ValidationStatus.Error));
        Count().Should().Be(renders);
        await host.InvokeAsync(() => second.Instance.NotifyValidationStatusChanged(ValidationStatus.Error));
        host.WaitForAssertion(() => Count().Should().BeGreaterThan(renders));
    }

    [Test]
    public void Audit_interactive_static_sidebar_preserves_root_attributes_without_desktop_hidden_classes()
    {
        var cut = Render<Sidebar>(p => p.Add(c => c.Collapsible, SidebarCollapsible.None)
            .Add(c => c.Class, "custom-sidebar").Add(c => c.Style, "color:red")
            .Add(c => c.Attributes, new Dictionary<string, object> { ["aria-label"] = "Tools", ["data-custom"] = "kept" }));
        var root = cut.Find("aside");
        root.ClassList.Should().Contain("custom-sidebar").And.NotContain("hidden");
        root.GetAttribute("style").Should().Contain("color:red");
        root.GetAttribute("aria-label").Should().Be("Tools");
        root.GetAttribute("data-custom").Should().Be("kept");
    }

}
