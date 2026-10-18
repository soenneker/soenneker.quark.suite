using AwesomeAssertions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Issue_audit_combobox_retains_selection_across_unrelated_parameter_updates()
    {
        var changes = new System.Collections.Generic.List<string?>();
        var cut = Render<Combobox>(p => p.Add(c => c.Value, "one")
            .Add(c => c.ValueChanged, value => changes.Add(value)).AddChildContent<ComboboxInput>());
        cut.Render(p => p.Add(c => c.Id, "updated-parent-parameter"));
        cut.Instance.Value.Should().Be("one");
        changes.Should().BeEmpty();
    }

    [Test]
    public void Issue_audit_toggle_disabled_guard_and_blur_autosave()
    {
        var changes = new System.Collections.Generic.List<bool>();
        var saved = new System.Collections.Generic.List<bool?>();
        var cut = Render<Toggle>(p => p.Add(c => c.Disabled, true)
            .Add(c => c.AutoSave, true).Add(c => c.AutoSaveDelay, 60000)
            .Add(c => c.PressedChanged, value => changes.Add(value))
            .Add(c => c.OnAutoSave, (value, _) => { saved.Add(value); return System.Threading.Tasks.ValueTask.CompletedTask; }));
        cut.Find("button").Click();
        changes.Should().BeEmpty();
        cut.Render(p => p.Add(c => c.Disabled, false));
        cut.Find("button").Click();
        changes.Should().Equal(true);
        saved.Should().BeEmpty();
        cut.Find("button").Blur();
        cut.WaitForAssertion(() => saved.Should().Equal(true));
    }

    [Test]
    public void Issue_audit_tooltip_toggle_keeps_pressed_state_after_pointer_leaves()
    {
        var cut = Render<Tooltip>(p => p.Add(c => c.DelayDuration, 0)
            .AddChildContent<TooltipTrigger>(trigger => trigger.Add(c => c.AsChild, true)
                .AddChildContent<Toggle>(toggle => toggle.AddChildContent("Toggle hint")))
            .AddChildContent<TooltipContent>(content => content.AddChildContent("Explanation")));
        cut.Find("button").PointerMove();
        cut.WaitForAssertion(() => cut.Find("button").GetAttribute("aria-describedby").Should().NotBeNullOrEmpty());
        cut.Find("button").Click();
        cut.Find("button").PointerLeave();
        cut.Find("button").GetAttribute("aria-pressed").Should().Be("true");
        cut.Find("button").GetAttribute("data-state").Should().Be("on");
    }

    [Test]
    public void Issue_audit_dialog_close_labels_follow_live_localization()
    {
        var localizer = Services.GetRequiredService<QuarkLocalizer>();
        var close = Render<DialogCloseButton>(p => p.Add(c => c.AutoClose, false));
        var footer = Render<DialogFooter>(p => p.Add(c => c.ShowCloseButton, true));
        close.Find("button").TextContent.Should().Contain("Close");
        localizer.SetTranslations("es", new System.Collections.Generic.Dictionary<string, string> { ["Close"] = "Cerrar" });
        localizer.SetCulture(System.Globalization.CultureInfo.GetCultureInfo("es"));
        close.WaitForAssertion(() => close.Find("button").TextContent.Should().Contain("Cerrar"));
        footer.WaitForAssertion(() => footer.Find("button").TextContent.Should().Contain("Cerrar"));
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public void Issue_audit_grouped_controls_inherit_field_accessibility(bool textarea)
    {
        var cut = Render<Field>(p => p.Add(c => c.IsInvalid, true).AddChildContent(builder =>
        {
            builder.OpenComponent<FieldLabel>(0);
            builder.AddAttribute(1, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)(b => b.AddContent(0, "Email")));
            builder.CloseComponent();
            builder.OpenComponent<InputGroup>(2);
            builder.AddAttribute(3, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)(b =>
            {
                b.OpenComponent(0, textarea ? typeof(InputGroupTextarea) : typeof(InputGroupInput));
                b.CloseComponent();
            }));
            builder.CloseComponent();
            builder.OpenComponent<FieldDescription>(4);
            builder.AddAttribute(5, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)(b => b.AddContent(0, "Description")));
            builder.CloseComponent();
            builder.OpenComponent<FieldError>(6);
            builder.AddAttribute(7, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)(b => b.AddContent(0, "Error")));
            builder.CloseComponent();
        }));
        cut.WaitForAssertion(() =>
        {
            var control = cut.Find(textarea ? "textarea" : "input");
            control.Id.Should().Be(cut.Find("label").GetAttribute("for"));
            control.GetAttribute("aria-invalid").Should().Be("true");
            control.GetAttribute("aria-describedby").Should().Contain(cut.Find("[data-slot=field-description]").Id!);
            control.GetAttribute("aria-describedby").Should().Contain(cut.Find("[data-slot=field-error]").Id!);
        });
        cut.Render(p => p.Add(c => c.IsInvalid, false));
        cut.Find(textarea ? "textarea" : "input").GetAttribute("aria-invalid").Should().NotBe("true");
    }

    [Test]
    public async System.Threading.Tasks.Task Issue_audit_autoplay_pauses_until_both_pointer_and_focus_leave(System.Threading.CancellationToken cancellationToken)
    {
        var changes = 0;
        var cut = Render<Carousel>(p => p.Add(c => c.Loop, true)
            .Add(c => c.AutoplayDelay, System.TimeSpan.FromMilliseconds(50))
            .Add(c => c.SelectedIndexChanged, _ => changes++)
            .AddChildContent(builder =>
            {
                builder.OpenComponent<CarouselItem>(0); builder.CloseComponent();
                builder.OpenComponent<CarouselItem>(1); builder.CloseComponent();
            }));
        var root = cut.Find("[data-slot=carousel]");
        await root.MouseEnterAsync(new Microsoft.AspNetCore.Components.Web.MouseEventArgs());
        await root.FocusInAsync(new Microsoft.AspNetCore.Components.Web.FocusEventArgs());
        cut.Render(p => p.Add(c => c.Autoplay, true));
        await root.MouseLeaveAsync(new Microsoft.AspNetCore.Components.Web.MouseEventArgs());
        await System.Threading.Tasks.Task.Delay(180, cancellationToken);
        changes.Should().Be(0);
        cut.Find("[data-slot=carousel-status]").GetAttribute("aria-live").Should().Be("polite");
        await root.FocusOutAsync(new Microsoft.AspNetCore.Components.Web.FocusEventArgs());
        cut.WaitForAssertion(() => changes.Should().BeGreaterThan(0));
        await root.MouseEnterAsync(new Microsoft.AspNetCore.Components.Web.MouseEventArgs());
        var pausedChanges = changes;
        await System.Threading.Tasks.Task.Delay(180, cancellationToken);
        changes.Should().Be(pausedChanges);
    }

    [Test]
    public void Issue_audit_programmatic_select_changes_do_not_emit_empty_selection()
    {
        var changes = new System.Collections.Generic.List<string?>();
        var cut = Render<Select<string>>(p => p.Add(c => c.SelectedValue, "pending")
            .Add(c => c.SelectedValueChanged, value => changes.Add(value))
            .AddChildContent<SelectTrigger>(t => t.AddChildContent<SelectValue>()));
        cut.Render(p => p.Add(c => c.SelectedValue, "active"));
        cut.Instance.SelectedValue.Should().Be("active");
        changes.Should().BeEmpty();
    }

    [Test]
    public void Issue_audit_scalar_slider_keeps_one_thumb_after_value_change()
    {
        var cut = Render<Slider>(p => p.Add(c => c.SliderValue, 25));
        cut.FindAll("[role=slider]").Should().ContainSingle();
        cut.Find("[role=slider]").GetAttribute("aria-valuenow").Should().Be("25");
        cut.Render(p => p.Add(c => c.SliderValue, 75));
        cut.FindAll("[role=slider]").Should().ContainSingle();
        cut.Find("[role=slider]").GetAttribute("aria-valuenow").Should().Be("75");
    }

    [Test]
    public void Issue_audit_multiple_combobox_disables_input_and_chip_removal()
    {
        var cut = Render<Combobox>(p => p.Add(c => c.Multiple, true).Add(c => c.Disabled, true)
            .Add(c => c.Values, ["one"])
            .AddChildContent<ComboboxChips>(chips => chips
                .AddChildContent<ComboboxChip>(chip => chip.Add(c => c.Value, "one").AddChildContent("One"))
                .AddChildContent<ComboboxChipsInput>()));
        cut.Find("input").HasAttribute("disabled").Should().BeTrue();
        cut.Find("[data-slot=combobox-chip-remove]").HasAttribute("disabled").Should().BeTrue();
        cut.Render(p => p.Add(c => c.Disabled, false));
        cut.Find("input").HasAttribute("disabled").Should().BeFalse();
        cut.Find("[data-slot=combobox-chip-remove]").HasAttribute("disabled").Should().BeFalse();
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public void Issue_audit_external_checkbox_form_reaches_submission_input(bool asSwitch)
    {
        var cut = Render<Check>(p => p.Add(c => c.Switch, asSwitch).Add(c => c.Name, "consent")
            .Add(c => c.Checked, true).AddUnmatched("form", "external-form"));
        cut.Find("input[type=checkbox]").GetAttribute("form").Should().Be("external-form");
        cut.Render(p => p.AddUnmatched("form", "other-form"));
        cut.Find("input[type=checkbox]").GetAttribute("form").Should().Be("other-form");
    }

    [Test]
    public void Issue_audit_external_standalone_switch_form_reaches_submission_input()
    {
        var cut = Render<Switch>(p => p.Add(c => c.Name, "enabled").Add(c => c.Checked, true)
            .AddUnmatched("form", "external-form"));
        cut.Find("input[type=checkbox]").GetAttribute("form").Should().Be("external-form");
        cut.Render(p => p.AddUnmatched("form", "other-form"));
        cut.Find("input[type=checkbox]").GetAttribute("form").Should().Be("other-form");
    }

    [Test]
    public void Issue_audit_invalid_grouped_inputs_keep_distinct_focus_ring()
    {
        var group = Render<InputGroup>(p => p.AddChildContent<InputGroupInput>(i => i.AddUnmatched("aria-invalid", "true")));
        group.Find("[data-slot=input-group]").ClassList.Should()
            .NotContain("has-[[data-slot][aria-invalid=true]]:ring-3")
            .And.Contain("has-[[data-slot=input-group-control]:focus-visible]:ring-3");
        var chips = Render<ComboboxChips>();
        chips.Find("[data-slot=combobox-chips]").ClassList.Should()
            .NotContain("has-aria-invalid:ring-[3px]").And.Contain("focus-within:ring-[3px]");
    }
}
