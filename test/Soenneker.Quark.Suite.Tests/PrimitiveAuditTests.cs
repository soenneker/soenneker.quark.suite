using System.Collections.Generic;
using System.Globalization;
using System.Threading;
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
    public void Audit_core_command_checkmark_styles_are_conditional()
    {
        var cut = Render<CommandItem>();
        var icon = cut.Find("svg");
        icon.ClassList.Should().Contain("opacity-0").And.NotContain("opacity-100")
            .And.Contain("group-data-[checked=true]/command-item:opacity-100")
            .And.NotContain("hidden");
    }

    [Test]
    public void Audit_core_password_visibility_respects_controlled_and_notification_only_parameters()
    {
        var controlled = Render<PasswordInput>(p => p.Add(c => c.Value, "secret").Add(c => c.ShowPassword, true));
        controlled.Find("input").GetAttribute("type").Should().BeNull();
        controlled.Find("button").Click();
        controlled.Find("input").GetAttribute("type").Should().BeNull();
        bool? notified = null;
        var observed = Render<PasswordInput>(p => p.Add(c => c.Value, "secret").Add(c => c.ShowPasswordChanged, value => notified = value));
        observed.Find("button").Click();
        notified.Should().BeTrue();
        observed.Find("input").GetAttribute("type").Should().BeNull();
    }
    [Test]
    public async Task Audit_core_validation_gates_automatic_errors_until_explicit_validation()
    {
        var cut = Render<Validations>(p => p.Add(c => c.GateErrorsUntilValidate, true)
            .AddChildContent<Validation>(v => v.Add(c => c.Validator, new RequiredValidator())
                .AddChildContent<Input>().AddChildContent<ValidationError>()));
        cut.Find("input").Input(" ");
        var validation = cut.FindComponent<Validation>();
        validation.Instance.Status.Should().Be(ValidationStatus.Error);
        validation.Instance.ShouldShowInvalid.Should().BeFalse();
        cut.FindAll("[role=alert]").Should().BeEmpty();
        await cut.InvokeAsync(() => cut.Instance.Validate());
        cut.Find("[role=alert]").TextContent.Should().Contain("required");
        await cut.InvokeAsync(() => cut.Instance.ClearAll());
        validation.Instance.ShouldShowInvalid.Should().BeFalse();
    }

    [Test]
    public void Audit_core_email_validator_accepts_long_top_level_domains()
    {
        new EmailValidator().Validate("user@example.technology").Status.Should().Be(ValidationStatus.Success);
        new EmailValidator().Validate("invalid@@example.technology").Status.Should().Be(ValidationStatus.Error);
    }

    [Test]
    public void Audit_core_date_input_accepts_unbound_values_using_invariant_browser_dates()
    {
        var previous = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ar-SA");
            var cut = Render<DateInput>();
            cut.Find("input").Input("2026-10-11");
            cut.Instance.Date.Should().Be(new System.DateTime(2026, 10, 11));
        }
        finally { CultureInfo.CurrentCulture = previous; }
    }

    [Test]
    public void Audit_core_input_wrappers_forward_attributes_builders_and_events()
    {
        VerifyInputWrapper<TextInput>();
        VerifyInputWrapper<NumericInput>();
        VerifyInputWrapper<PasswordInput>();
        VerifyInputWrapper<CurrencyInput>();
        VerifyInputWrapper<DateInput>();
    }

    private void VerifyInputWrapper<T>() where T : FormControlElementBase
    {
        var clicks = 0;
        var focuses = 0;
        var attributes = new Dictionary<string, object> { ["data-extra"] = "first" };
        var cut = Render<T>(p => p.Add(c => c.Class, "custom-input").Add(c => c.Style, "color: red;")
            .Add(c => c.Width, Width.Is64).Add(c => c.AriaLabelledBy, "label-id").Add(c => c.Attributes, attributes)
            .Add(c => c.OnClick, () => clicks++).Add(c => c.OnFocus, () => focuses++));
        var input = cut.Find("input");
        input.ClassList.Should().Contain("custom-input").And.Contain("w-64");
        input.GetAttribute("style").Should().Contain("color: red;");
        input.GetAttribute("aria-labelledby").Should().Be("label-id");
        input.GetAttribute("data-extra").Should().Be("first");
        input.Click();
        input.Focus();
        clicks.Should().Be(1);
        focuses.Should().Be(1);
        attributes.Clear();
        cut.Render(p => p.Add(c => c.Attributes, attributes).Add(c => c.Class, "updated-input"));
        cut.Find("input").HasAttribute("data-extra").Should().BeFalse();
        cut.Find("input").ClassList.Should().Contain("updated-input").And.NotContain("custom-input");
    }

    [Test]
    public void Audit_core_currency_precision_is_bounded_to_decimal_capacity()
    {
        var cut = Render<CurrencyInput>(p => p.Add(c => c.DecimalPlaces, int.MaxValue).Add(c => c.CultureName, "en-US"));
        cut.Find("input").Input("1.25");
        cut.Instance.Value.Should().Be(1.25m);
        cut.Find("input").GetAttribute("value").Should().Be("1.2500000000000000000000000000");
        cut.Render(p => p.Add(c => c.DecimalPlaces, -10));
        cut.Find("input").Input("1.75");
        cut.Instance.Value.Should().Be(2m);
    }

    [Test]
    public void Audit_core_precode_preserves_custom_classes_styles_and_max_height_utilities()
    {
        var cut = Render<PreCode>(p => p.Add(c => c.Class, "custom-code").Add(c => c.Style, "color: red;")
            .Add(c => c.MaxHeight, MaxHeight.Token("64")));
        cut.Find("pre").ClassList.Should().Contain("custom-code").And.Contain("max-h-64");
        cut.Find("pre").GetAttribute("style").Should().Contain("color: red;").And.NotContain("max-h-");
    }

    [Test]
    public void Audit_core_progress_reports_values_completion_and_indeterminate_transitions()
    {
        var cut = Render<Progress>(p => p.Add(c => c.ProgressValue, 25));
        cut.Find("[role=progressbar]").GetAttribute("aria-valuenow").Should().Be("25");
        cut.Find("[role=progressbar]").GetAttribute("data-state").Should().Be("loading");
        cut.Render(p => p.Add(c => c.ProgressValue, 250).Add(c => c.Max, 200));
        cut.Find("[role=progressbar]").GetAttribute("aria-valuenow").Should().Be("200");
        cut.Find("[role=progressbar]").GetAttribute("data-state").Should().Be("complete");
        cut.Render(p => p.Add(c => c.Indeterminate, true));
        cut.Find("[role=progressbar]").HasAttribute("aria-valuenow").Should().BeFalse();
        cut.Render(p => p.Add(c => c.Indeterminate, false).Add(c => c.ProgressValue, -1).Add(c => c.Max, 0));
        cut.Find("[role=progressbar]").GetAttribute("aria-valuenow").Should().Be("0");
        cut.Find("[role=progressbar]").GetAttribute("aria-valuemax").Should().Be("100");
    }

    [Test]
    public void Audit_core_progress_retains_fractional_precision_in_css()
    {
        var previous = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("fr-FR");
            var cut = Render<Progress>(p => p.Add(c => c.ProgressValue, 1).Add(c => c.Max, 8));
            cut.Find("[data-slot=progress-indicator]").GetAttribute("style").Should().Contain("-87.5%");
        }
        finally { CultureInfo.CurrentCulture = previous; }
    }

    [Test]
    public void Audit_core_breadcrumb_link_preserves_updates_and_removes_attributes()
    {
        var attributes = new Dictionary<string, object> { ["data-test"] = "first" };
        var cut = Render<BreadcrumbLink>(p => p.Add(c => c.Href, "/first").Add(c => c.Id, "crumb")
            .Add(c => c.Class, "custom").Add(c => c.AriaLabel, "First").Add(c => c.Attributes, attributes));
        var link = cut.Find("a");
        link.Id.Should().Be("crumb");
        link.ClassList.Should().Contain("custom");
        link.GetAttribute("aria-label").Should().Be("First");
        attributes["data-test"] = "second";
        cut.Render(p => p.Add(c => c.Attributes, attributes));
        cut.Find("a").GetAttribute("data-test").Should().Be("second");
        attributes.Clear();
        cut.Render(p => p.Add(c => c.Attributes, attributes));
        cut.Find("a").HasAttribute("data-test").Should().BeFalse();
    }

    [Test]
    public void Audit_core_layout_defaults_follow_updated_orientation_and_keep_explicit_overrides()
    {
        var stack = Render<Stack>();
        stack.Render(p => p.Add(c => c.Inline, true).Add(c => c.Orientation, StackOrientation.Horizontal));
        stack.Find("div").ClassList.Should().Contain("inline-flex").And.Contain("flex-row").And.NotContain("flex-col");
        stack.Render(p => p.Add(c => c.Display, Display.Grid).Add(c => c.FlexDirection, FlexDirection.Col));
        stack.Find("div").ClassList.Should().Contain("grid").And.Contain("flex-col").And.NotContain("inline-flex");
        var grid = Render<Grid>();
        grid.Render(p => p.Add(c => c.Inline, true));
        grid.Find("div").ClassList.Should().Contain("inline-grid").And.NotContain("grid");
    }

    [Test]
    public void Audit_core_item_and_empty_variants_clear_previous_visual_defaults()
    {
        var item = Render<Item>(p => p.Add(c => c.Variant, ItemVariant.Muted));
        item.Render(p => p.Add(c => c.Variant, ItemVariant.Outline));
        item.Find("[data-slot=item]").ClassList.Should().Contain("border-border").And.NotContain("bg-muted/50");
        var media = Render<ItemMedia>(p => p.Add(c => c.Variant, ItemMediaVariant.Image));
        media.Render(p => p.Add(c => c.Variant, ItemMediaVariant.Icon));
        media.Find("div").ClassList.Should().NotContain("size-10").And.NotContain("overflow-hidden");
        var empty = Render<EmptyMedia>(p => p.Add(c => c.Variant, EmptyMediaVariant.Icon));
        empty.Render(p => p.Add(c => c.Variant, EmptyMediaVariant.Default));
        empty.Find("div").ClassList.Should().Contain("bg-transparent").And.NotContain("size-8").And.NotContain("bg-muted");
    }

    [Test]
    public void Audit_core_pill_colors_follow_parameter_changes()
    {
        var indicator = Render<PillIndicator>();
        indicator.Render(p => p.Add(c => c.Variant, PillIndicatorVariant.Error));
        indicator.Find("[data-slot=pill-indicator]").ClassList.Should().Contain("bg-red-500").And.NotContain("bg-green-500");
        var delta = Render<PillDelta>();
        delta.Render(p => p.Add(c => c.Direction, PillDeltaDirection.Down));
        delta.Find("[data-slot=pill-delta]").ClassList.Should().Contain("text-red-600").And.NotContain("text-green-600");
    }

    [Test]
    public void Audit_core_scroll_area_preserves_explicit_direction()
    {
        var cut = Render<ScrollArea>(p => p.Add(c => c.Dir, "rtl"));
        cut.FindComponent<BradixScrollArea>().Instance.Dir.Should().Be("rtl");
        cut.Render(p => p.Add(c => c.Dir, "ltr"));
        cut.FindComponent<BradixScrollArea>().Instance.Dir.Should().Be("ltr");
    }

    [Test]
    public void Audit_core_header_action_applies_interaction_styles_only_in_their_state()
    {
        var cut = Render<Header.HeaderActionLink>(p => p.Add(c => c.Href, "/test"));
        cut.Find("a").ClassList.Should().Contain("disabled:pointer-events-none").And.NotContain("pointer-events-none")
            .And.Contain("disabled:opacity-50").And.NotContain("opacity-50").And.Contain("hover:text-accent-foreground");
    }

    [Test]
    public void Audit_core_closed_collapse_is_inert_and_hidden_from_accessibility_tree()
    {
        var cut = Render<Collapse>();
        cut.Find("div").HasAttribute("inert").Should().BeTrue();
        cut.Find("div").GetAttribute("aria-hidden").Should().Be("true");
        cut.Render(p => p.Add(c => c.Expanded, true));
        cut.Find("div").HasAttribute("inert").Should().BeFalse();
        cut.Find("div").HasAttribute("aria-hidden").Should().BeFalse();
    }

    [Test]
    public async ValueTask Audit_core_carousel_uses_vertical_keys_and_notifies_only_selection_changes(CancellationToken cancellationToken)
    {
        var calls = 0;
        var apiCalls = 0;
        var cut = Render<Carousel>(p => p.Add(c => c.Orientation, Orientation.Vertical)
            .Add(c => c.SetApi, _ => apiCalls++)
            .Add(c => c.SelectedIndexChanged, _ => calls++)
            .AddChildContent(builder =>
            {
                builder.OpenComponent<CarouselItem>(0);
                builder.CloseComponent();
                builder.OpenComponent<CarouselItem>(1);
                builder.CloseComponent();
            }));
        calls.Should().Be(0);
        apiCalls.Should().Be(1);
        await cut.Find("[data-slot=carousel]").KeyDownAsync(new KeyboardEventArgs { Key = "ArrowDown" });
        calls.Should().Be(1);
        apiCalls.Should().Be(1);
        await cut.Find("[data-slot=carousel]").KeyDownAsync(new KeyboardEventArgs { Key = "ArrowDown" });
        calls.Should().Be(1);
    }
}
