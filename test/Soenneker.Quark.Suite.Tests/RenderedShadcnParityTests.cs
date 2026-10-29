using System;
using System.Linq;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using System.Threading;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Component_size_variants_use_enum_values_and_update_rendered_attributes()
    {
        var toggle = Render<Switch>(p => p.Add(c => c.SwitchSize, SwitchSize.Sm));
        toggle.Find("[role=switch]").GetAttribute("data-size").Should().Be("sm");
        toggle.Render(p => p.Add(c => c.SwitchSize, SwitchSize.Default));
        toggle.Find("[role=switch]").GetAttribute("data-size").Should().Be("default");

        var select = Render<Select<string>>(p => p.Add(c => c.SelectSize, SelectSize.Sm)
            .AddChildContent<SelectTrigger>());
        select.Find("[data-slot=select-trigger]").GetAttribute("data-size").Should().Be("sm");
        select.Render(p => p.Add(c => c.SelectSize, SelectSize.Default));
        select.Find("[data-slot=select-trigger]").GetAttribute("data-size").Should().Be("default");
    }

    [Test]
    public void Radio_and_checkbox_use_typed_responsive_size()
    {
        var radio = Render<Radio>(p => p.Add(c => c.Size, Size.Is4.OnMd.Is5));
        radio.Find("[data-slot=radio-group-item]").ClassList.Should().Contain("size-4").And.Contain("md:size-5");
        var check = Render<Check>(p => p.Add(c => c.Size, Size.Is5.OnLg.Is6));
        check.Find("[role=checkbox]").ClassList.Should().Contain("size-5").And.Contain("lg:size-6").And.NotContain("size-4");

        var preset = new QuarkPresetToken("control-size", context => context.Size = Size.Is6.OnMd.Is7);
        var presetRadio = Render<Radio>(p => p.Add(c => c.Preset, preset));
        presetRadio.Find("[data-slot=radio-group-item]").ClassList.Should().Contain("size-6").And.Contain("md:size-7").And.NotContain("size-4");
        var presetCheck = Render<Check>(p => p.Add(c => c.Preset, preset));
        presetCheck.Find("[role=checkbox]").ClassList.Should().Contain("size-6").And.Contain("md:size-7").And.NotContain("size-4");
    }

    [Test]
    public void Slider_sizing_matches_shadcn_and_supports_typed_width()
    {
        var cut = Render<Slider>(p => p.Add(c => c.SliderValue, 50)
            .Add(c => c.Width, Width.IsFull.OnMd.Is64));
        cut.Find("[data-slot=slider]").ClassList.Should().Contain("w-full").And.Contain("md:w-64");
        cut.Find("[data-slot=slider-track]").ClassList.Should().Contain("data-[orientation=horizontal]:h-1.5")
            .And.Contain("data-[orientation=vertical]:w-1.5");
        cut.Find("[data-slot=slider-thumb]").ClassList.Should().Contain("size-4");
    }

    [Test]
    public void Dialog_layout_honors_top_alignment_and_fullscreen_sizes()
    {
        var cut = Render<Dialog>(p => p.Add(c => c.Visible, true).Add(c => c.Centered, false)
            .AddChildContent<DialogContent>());
        cut.Find("[role=dialog]").ClassList.Should().Contain("top-4").And.NotContain("top-1/2");
        cut.Render(p => p.Add(c => c.Centered, true).Add(c => c.DialogSize, DialogSize.Fullscreen));
        var classes = cut.Find("[role=dialog]").ClassList;
        classes.Should().Contain("top-0!").And.Contain("h-dvh!").And.Contain("max-w-none!");
        cut.Render(p => p.Add(c => c.DialogSize, DialogSize.FullscreenMdDown));
        classes = cut.Find("[role=dialog]").ClassList;
        classes.Should().Contain("max-md:h-dvh!").And.NotContain("h-dvh!");
        cut.Render(p => p.Add(c => c.DialogSize, DialogSize.Large));
        classes = cut.Find("[role=dialog]").ClassList;
        classes.Should().Contain("sm:max-w-2xl").And.NotContain("sm:max-w-sm");
    }

    [Test]
    public void Composition_searchable_select_preserves_explicit_input_id_for_labels()
    {
        var cut = Render<SearchableSelect>(p => p
            .Add(c => c.Choices, [new SelectOption("test", "Test")])
            .AddUnmatched("Id", "industry"));

        cut.Find("input[role='combobox']").Id.Should().Be("industry");
        cut.Find("input").GetAttribute("aria-controls").Should().NotBeNullOrEmpty();
    }

    [Test]
    public async ValueTask Composition_searchable_select_keeps_duplicate_labels_and_empty_values_distinct(CancellationToken cancellationToken)
    {
        string? selected = null;
        var cut = Render<SearchableSelect>(p => p
            .Add(c => c.Choices, [new SelectOption("first", "Same label"), new SelectOption("second", "Same label"), new SelectOption("", "None")])
            .Add(c => c.Value, "first")
            .Add(c => c.ValueChanged, value => selected = value));

        cut.Find("input").GetAttribute("value").Should().Be("Same label");
        await cut.Find("input").FocusAsync();
        await cut.Find("[data-value='option:second']").ClickAsync();
        selected.Should().Be("second");
        cut.Find("input").GetAttribute("value").Should().Be("Same label");

        await cut.Find("input").FocusAsync();
        await cut.Find("[data-value='option:']").ClickAsync();
        selected.Should().BeEmpty();
        cut.Find("input").GetAttribute("value").Should().Be("None");
    }

    [Test]
    public async ValueTask Composition_searchable_select_filters_labels_and_rejects_disabled_choices(CancellationToken cancellationToken)
    {
        string? selected = null;
        var cut = Render<SearchableSelect>(p => p
            .Add(c => c.Choices, [new SelectOption("a", "Alpha"), new SelectOption("b", "Beta", true)])
            .Add(c => c.ValueChanged, value => selected = value));

        await cut.Find("input").FocusAsync();
        await cut.Find("input").InputAsync(new ChangeEventArgs { Value = "Beta" });
        cut.FindAll("[data-value='option:a']").Should().BeEmpty();
        await cut.Find("[data-value='option:b']").ClickAsync();
        selected.Should().BeNull();
    }

    [Test]
    public async ValueTask Composition_event_timeline_orders_events_formats_timestamps_and_switches_layout(CancellationToken cancellationToken)
    {
        var later = new DateTimeOffset(2026, 9, 14, 12, 0, 0, TimeSpan.Zero);
        var cut = Render<EventTimeline>(p => p
            .Add(c => c.Items, [
                new EventTimelineItem { Label = "Later", When = later },
                new EventTimelineItem { Label = "Earlier", When = later.AddHours(-1) },
                new EventTimelineItem { Label = "", When = later.AddHours(-2) }])
            .Add(c => c.DenseTimelineThreshold, 1)
            .Add(c => c.FormatTimestamp, value => value.ToString("HH:mm")));

        cut.FindAll("time").Select(t => t.TextContent.Trim()).Should().Equal("11:00", "12:00");
        cut.FindAll("div.absolute[aria-hidden='true']").Count.Should().Be(1);
        cut.Find(".overflow-x-auto").ClassList.Should().Contain("overflow-y-hidden");
        cut.FindAll("button")[0].GetAttribute("aria-pressed").Should().Be("true");
        await cut.FindAll("button")[1].ClickAsync();
        cut.FindAll("button")[1].GetAttribute("aria-pressed").Should().Be("true");
        cut.FindAll("time")[0].ParentElement!.ParentElement!.ClassList.Should().Contain("basis-[6.5rem]").And.Contain("md:basis-[12rem]");
    }

    [Test]
    public void Composition_event_timeline_respects_explicit_tone_even_for_created_labels()
    {
        var cut = Render<EventTimeline>(p => p.Add(c => c.Items,
            [new EventTimelineItem { Label = "Created", When = DateTimeOffset.UtcNow, Tone = SemanticTone.Danger }]));

        cut.FindAll("span").Single(s => s.TextContent == "Created").ClassName.Should().Contain("--destructive");
    }

    [Test]
    public void Composition_details_only_offer_copy_when_requested()
    {
        var plain = Render<DetailsItem>(p => p.Add(c => c.Label, "Name").Add(c => c.Content, "Example"));
        plain.FindComponents<CopyableText>().Should().BeEmpty();
        plain.Markup.Should().Contain("Example");

        var copyable = Render<DetailsItem>(p => p.Add(c => c.Content, "Example").Add(c => c.Copyable, true));
        copyable.FindComponents<CopyableText>().Should().ContainSingle();
    }

    [Test]
    public void Composition_code_viewer_formats_json_and_preserves_invalid_input()
    {
        var formatted = Render<CodeViewer>(p => p.Add(c => c.Text, "{\"count\":1}"));
        formatted.FindComponent<CodeEditor>().Instance.Text.Should().Contain("\n");
        formatted.FindComponent<CodeEditor>().Instance.ReadOnly.Should().BeTrue();

        var invalid = Render<CodeViewer>(p => p.Add(c => c.Text, "{broken"));
        invalid.FindComponent<CodeEditor>().Instance.Text.Should().Be("{broken");

        var unformatted = Render<CodeViewer>(p => p.Add(c => c.Text, "{\"count\":1}").Add(c => c.FormatJson, false));
        unformatted.FindComponent<CodeEditor>().Instance.Text.Should().Be("{\"count\":1}");
    }

    [Test]
    public void Composition_chart_card_prioritizes_loading_then_error_then_empty()
    {
        RenderFragment loading = b => b.AddContent(0, "Loading slot");
        RenderFragment error = b => b.AddContent(0, "Error slot");
        RenderFragment empty = b => b.AddContent(0, "Empty slot");
        var cut = Render<ChartCard>(p => p.Add(c => c.IsLoading, true).Add(c => c.ErrorMessage, "Failure")
            .Add(c => c.IsEmpty, true).Add(c => c.LoadingContent, loading).Add(c => c.ErrorContent, error).Add(c => c.EmptyContent, empty));
        cut.Markup.Should().Contain("Loading slot").And.NotContain("Error slot").And.NotContain("Empty slot");
    }

    [Test]
    public void Composition_empty_state_omits_absent_slots_and_renders_custom_actions()
    {
        var empty = Render<EmptyState>(p => p.Add(c => c.Title, "Nothing here"));
        empty.FindAll("[data-slot='empty-content']").Should().BeEmpty();
        empty.FindAll("[data-slot='empty-media']").Should().BeEmpty();
        var action = Render<EmptyState>(p => p.Add(c => c.Actions, b => b.AddContent(0, "Retry")));
        action.Find("[data-slot='empty-content']").TextContent.Should().Be("Retry");
    }

    [Test]
    public void Composition_copyable_text_field_accepts_presets_and_retains_accessibility_and_empty_state()
    {
        var preset = new QuarkPresetToken("test-copy-field", c => c.Width = Width.IsFull);
        var cut = Render<CopyableText>(p => p.Add(c => c.Preset, preset).Add(c => c.AriaLabel, "Reference")
            .Add(c => c.Placeholder, "Not issued"));
        cut.Find("[data-slot='copyable-text']").GetAttribute("aria-label").Should().Be("Reference");
        cut.Find("button").HasAttribute("disabled").Should().BeTrue();
        cut.Markup.Should().Contain("Not issued");
    }

    [Test]
    public void Composition_badge_tone_does_not_override_explicit_preset_colors()
    {
        var preset = new QuarkPresetToken("test-tone", c => c.TextColor = TextColor.Primary);
        var cut = Render<Badge>(p => p.Add(c => c.Tone, SemanticTone.Danger).Add(c => c.Preset, preset));
        var badge = cut.Find("[data-slot='badge']");
        badge.ClassList.Should().Contain("text-primary").And.NotContain("text-destructive");
        badge.GetAttribute("data-tone").Should().Be("danger");
    }

    [Test]
    public void Deep_audit_textarea_rows_and_explicit_sizing_override_auto_size()
    {
        var cut = Render<TextArea>(p => p.Add(c => c.Rows, 6));
        cut.Find("textarea").GetAttribute("rows").Should().Be("6");
        cut.Find("textarea").ClassList.Should().Contain("field-sizing-fixed").And.NotContain("field-sizing-content");
        cut.Render(p => p.Add(c => c.Rows, (int?)null));
        cut.Find("textarea").ClassList.Should().Contain("field-sizing-content").And.NotContain("field-sizing-fixed");
        cut.Render(p => p.Add(c => c.FieldSizing, FieldSizing.Fixed));
        cut.Find("textarea").ClassList.Should().Contain("field-sizing-fixed").And.NotContain("field-sizing-content");
    }

    [Test]
    public void Deep_audit_separator_stretches_only_without_explicit_cross_size_or_alignment()
    {
        var cut = Render<Separator>(p => p.Add(c => c.Orientation, SeparatorOrientation.Vertical));
        cut.Find("[data-slot='separator']").GetAttribute("data-auto-stretch").Should().Be("true");
        cut.Render(p => p.Add(c => c.Height, Height.Is4));
        cut.Find("[data-slot='separator']").HasAttribute("data-auto-stretch").Should().BeFalse();
        cut.Render(p => p.Add(c => c.Height, (CssValue<HeightBuilder>?)null).Add(c => c.SelfAlign, Self.Center));
        cut.Find("[data-slot='separator']").HasAttribute("data-auto-stretch").Should().BeFalse();
    }

    [Test]
    public void Deep_audit_closed_offcanvas_is_inert_but_icon_sidebar_remains_interactive()
    {
        var cut = Render<SidebarProvider>(p => p.Add(c => c.Open, false).Add(c => c.IsMobile, false)
            .Add(c => c.PersistState, false).AddChildContent<Sidebar>());
        cut.Find("[data-slot='sidebar-container']").HasAttribute("inert").Should().BeTrue();
        cut.Find("[data-slot='sidebar-container']").GetAttribute("aria-hidden").Should().Be("true");
        cut.Render(p => p.Add(c => c.Open, true));
        cut.Find("[data-slot='sidebar-container']").HasAttribute("inert").Should().BeFalse();
        cut.Render(p => p.Add(c => c.Open, false).AddChildContent<Sidebar>(s => s.Add(c => c.Collapsible, SidebarCollapsible.Icon)));
        cut.Find("[data-slot='sidebar-container']").HasAttribute("inert").Should().BeFalse();
    }

    [Test]
    public void Deep_audit_item_group_exposes_list_items_and_preserves_slotted_link_semantics()
    {
        var cut = Render<ItemGroup>(p => p.AddChildContent<Item>(i => i.AddChildContent("First")));
        cut.Find("[role='list'] [role='listitem']").TextContent.Should().Be("First");
        var standalone = Render<Item>();
        standalone.Find("[data-slot='item']").HasAttribute("role").Should().BeFalse();
        var linked = Render<ItemGroup>(p => p.AddChildContent<Item>(i => i.Add(c => c.AsChild, true)
            .AddChildContent<Anchor>(a => a.Add(c => c.Href, "/destination").AddChildContent("Destination"))));
        linked.Find("[role='listitem'] a").HasAttribute("role").Should().BeFalse();
    }

    [Test]
    public void Deep_audit_spinner_status_contains_announced_text_and_decorative_spinner_is_silent()
    {
        var cut = Render<Spinner>(p => p.Add(c => c.AriaLabel, "Saving"));
        cut.Find("[role='status'] .sr-only").TextContent.Should().Be("Saving");
        cut.Render(p => p.Add(c => c.Decorative, true));
        cut.FindAll(".sr-only").Should().BeEmpty();
        cut.FindAll("[role='status']").Should().BeEmpty();
    }

    [Test]
    public void Deep_audit_menubar_trigger_has_closed_keyboard_focus_style()
    {
        var cut = Render<Menubar>(p => p.AddChildContent<MenubarMenu>(m => m.AddChildContent<MenubarTrigger>(t => t.AddChildContent("File"))));
        cut.Find("[data-slot='menubar-trigger']").ClassList.Should().Contain("focus-visible:ring-2").And.Contain("focus-visible:ring-ring");
    }

    [Test]
    public void Deep_audit_switch_checked_thumb_reverses_in_rtl()
    {
        var cut = Render<Switch>(p => p.Add(c => c.Checked, true).Add(c => c.Dir, "rtl"));
        cut.Find("[data-slot='switch-thumb']").ClassList.Should().Contain("rtl:data-[state=checked]:-translate-x-[calc(100%-2px)]");
    }

    [Test]
    public void Deep_audit_otp_keeps_numeric_order_inside_rtl_layouts()
    {
        var cut = Render<InputOtp>(p => p.Add(c => c.Dir, "rtl"));
        cut.Find("[data-input-otp-container]").GetAttribute("dir").Should().Be("ltr");
        cut.Find("input").GetAttribute("dir").Should().Be("ltr");
    }

    [Test]
    public void Deep_audit_native_select_has_theme_aware_native_popup_colors()
    {
        var cut = Render<NativeSelect>();
        cut.Find("select").ClassList.Should().Contain("scheme-light").And.Contain("dark:scheme-dark");
    }

    [Test]
    public void Deep_audit_navigation_dropdown_has_stacking_priority()
    {
        var cut = Render<NavigationMenuContent>(p => p.Add(c => c.ForceMount, true));
        cut.Find("[data-slot='navigation-menu-content']").ClassList.Should().Contain("z-50");
    }

    [Test]
    public void Deep_audit_sizing_presets_are_not_overridden_by_component_fallbacks()
    {
        var areaPreset = new QuarkPresetToken("audit-area", c => c.FieldSizing = FieldSizing.Fixed);
        var area = Render<TextArea>(p => p.Add(c => c.Preset, areaPreset));
        area.Find("textarea").ClassList.Should().Contain("field-sizing-fixed").And.NotContain("field-sizing-content");
        var separatorPreset = new QuarkPresetToken("audit-separator", c => c.Height = Height.Is4);
        var separator = Render<Separator>(p => p.Add(c => c.Orientation, SeparatorOrientation.Vertical).Add(c => c.Preset, separatorPreset));
        separator.Find("[data-slot='separator']").HasAttribute("data-auto-stretch").Should().BeFalse();
    }

    [Test]
    public async System.Threading.Tasks.Task Deep_audit_carousel_announces_manual_slide_changes()
    {
        var cut = Render<Carousel>(p => p.Add(c => c.AriaLabel, "Product photos").AddChildContent(builder =>
        {
            builder.OpenComponent<CarouselItem>(0); builder.CloseComponent();
            builder.OpenComponent<CarouselItem>(1); builder.CloseComponent();
        }));
        cut.Find("[data-slot='carousel']").GetAttribute("aria-label").Should().Be("Product photos");
        cut.Find("[data-slot='carousel-status']").TextContent.Trim().Should().Be("Slide 1 of 2");
        await cut.Find("[data-slot='carousel']").KeyDownAsync(new Microsoft.AspNetCore.Components.Web.KeyboardEventArgs { Key = "ArrowRight" });
        cut.Find("[data-slot='carousel-status']").TextContent.Trim().Should().Be("Slide 2 of 2");
        cut.Find("[data-slot='carousel-status']").GetAttribute("aria-live").Should().Be("polite");
        cut.Render(p => p.Add(c => c.Autoplay, true));
        cut.Find("[data-slot='carousel-status']").GetAttribute("aria-live").Should().Be("off");
    }

    [Test]
    public void Deep_audit_checkbox_switch_variant_also_supports_rtl()
    {
        var cut = Render<Check>(p => p.Add(c => c.Switch, true).Add(c => c.Checked, true).Add(c => c.Dir, "rtl"));
        cut.Find("[data-slot='switch-thumb']").ClassList.Should().Contain("rtl:data-[state=checked]:-translate-x-[calc(100%-2px)]");
    }

    [Test]
    public void Deep_audit_inline_combobox_empty_state_tracks_available_items()
    {
        var cut = Render<Combobox>(p => p.AddChildContent<ComboboxEmpty>(e => e.AddChildContent("No matches")));
        cut.Find("[data-slot='combobox-empty']").TextContent.Should().Be("No matches");
        cut.Find("[data-slot='combobox-empty']").ClassList.Should().NotContain("hidden");
        cut.Render(p => p.AddChildContent(builder =>
        {
            builder.OpenComponent<ComboboxEmpty>(0); builder.CloseComponent();
            builder.OpenComponent<ComboboxItem>(1); builder.AddAttribute(2, "Value", "option"); builder.CloseComponent();
        }));
        cut.WaitForAssertion(() => cut.FindAll("[data-slot='combobox-empty']").Should().BeEmpty());
        cut.Render(p => p.AddChildContent<ComboboxEmpty>(e => e.AddChildContent("No matches")));
        cut.WaitForAssertion(() => cut.Find("[data-slot='combobox-empty']").TextContent.Should().Be("No matches"));
    }

    [Test]
    public void Deep_audit_command_selected_styles_require_true_not_attribute_presence()
    {
        var cut = Render<CommandItem>(p => p.AddChildContent<CommandShortcut>(s => s.AddChildContent("Ctrl+K")));
        var item = cut.Find("[cmdk-item]");
        item.GetAttribute("data-selected").Should().Be("false");
        item.ClassList.Should().Contain("data-[selected=true]:bg-muted").And.NotContain("data-selected:bg-muted");
        cut.Find("[data-slot='command-shortcut']").ClassList.Should().Contain("group-data-[selected=true]/command-item:text-foreground");
    }

    [Test]
    public void Deep_audit_dropdown_sizes_to_content_unless_width_is_explicit()
    {
        var cut = Render<Dropdown>(p => p.Add(c => c.Visible, true).AddChildContent<DropdownMenu>());
        var menu = cut.Find("[data-slot='dropdown-menu-content']");
        menu.ClassList.Should().Contain("min-w-32").And.NotContain("w-(--radix-dropdown-menu-trigger-width)");
        cut.Render(p => p.AddChildContent<DropdownMenu>(m => m.Add(c => c.Width, Width.Is64)));
        cut.Find("[data-slot='dropdown-menu-content']").ClassList.Should().Contain("w-64");
    }

    [Test]
    public void Deep_audit_invalid_controls_reserve_ring_width_for_keyboard_focus()
    {
        var cut = Render<Div>(p => p.AddChildContent(builder =>
        {
            var sequence = 0;
            foreach (var type in new[] { typeof(Button), typeof(Check), typeof(Switch), typeof(Input), typeof(TextArea), typeof(MemoInput), typeof(NativeSelect) })
            {
                builder.OpenComponent(sequence++, type);
                builder.AddAttribute(sequence++, "aria-invalid", "true");
                builder.CloseComponent();
            }
        }));
        var controls = cut.FindAll("[aria-invalid='true']");
        controls.Count.Should().BeGreaterThanOrEqualTo(7);
        foreach (var control in controls)
        {
            control.ClassList.Should().NotContain("aria-invalid:ring-3");
            control.ClassList.Should().Contain("aria-invalid:border-destructive");
            control.ClassList.Any(c => c is "focus-visible:ring-3" or "focus-visible:ring-[3px]").Should().BeTrue();
        }
    }

}
