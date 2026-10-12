using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public async Task Audit_calendar_keeps_a_tab_stop_and_navigates_disabled_days_across_months()
    {
        DateOnly? selected = null;
        var cut = Render<Calendar>(p => p.Add(c => c.DisplayMonth, new DateOnly(2026, 1, 1))
            .Add(c => c.DisabledDates, [new DateOnly(2026, 2, 1)])
            .Add(c => c.SelectedDateChanged, value => selected = value));
        cut.FindAll("button[data-calendar-date][tabindex='0']").Should().ContainSingle();
        await cut.InvokeAsync(() => cut.Instance.NavigateCalendar("2026-01-31", "ArrowRight", false));
        cut.Find("button[data-calendar-date][tabindex='0']").GetAttribute("data-calendar-date").Should().Be("2026-02-02");
        selected.Should().BeNull();
        await cut.InvokeAsync(() => cut.Instance.NavigateCalendar("2026-02-02", "PageDown", false));
        cut.Find("button[data-calendar-date][tabindex='0']").GetAttribute("data-calendar-date").Should().Be("2026-03-02");
    }

    [Test]
    public void Audit_calendar_handles_date_boundaries_and_readonly_selection()
    {
        var first = Render<Calendar>(p => p.Add(c => c.DisplayMonth, DateOnly.MinValue).Add(c => c.FixedWeeks, true));
        first.Find(".rdp-button_previous").GetAttribute("aria-disabled").Should().Be("true");
        var last = Render<Calendar>(p => p.Add(c => c.DisplayMonth, DateOnly.MaxValue).Add(c => c.NumberOfMonths, 2).Add(c => c.FixedWeeks, true));
        last.FindAll("table[role='grid']").Should().ContainSingle();
        last.Find(".rdp-button_next").GetAttribute("aria-disabled").Should().Be("true");
        DateOnly? selected = null;
        var readOnly = Render<Calendar>(p => p.Add(c => c.ReadOnly, true).Add(c => c.DisplayMonth, new DateOnly(2026, 1, 1))
            .Add(c => c.SelectedDateChanged, value => selected = value));
        readOnly.Find("button[data-calendar-date='2026-01-15']").Click();
        selected.Should().BeNull();
    }

    [Test]
    public void Audit_calendar_culture_bounds_and_secondary_month_dropdown_are_safe()
    {
        var gregorian = Render<Calendar>(p => p.Add(c => c.DisplayMonth, DateOnly.MinValue)
            .Add(c => c.CultureName, "fa-IR").Add(c => c.ShowWeekNumber, true));
        gregorian.Find("button[data-calendar-date='0001-01-01']").GetAttribute("aria-label").Should().NotBeNullOrWhiteSpace();
        var bounded = Render<Calendar>(p => p.Add(c => c.DisplayMonth, DateOnly.MinValue)
            .Add(c => c.UseCultureCalendar, true).Add(c => c.CultureName, "ar-SA"));
        bounded.FindAll("table[role='grid']").Should().ContainSingle();
        var cut = Render<Calendar>(p => p.Add(c => c.DisplayMonth, new DateOnly(2026, 1, 1))
            .Add(c => c.NumberOfMonths, 2).Add(c => c.CaptionLayout, "dropdown"));
        cut.FindAll("select.rdp-months_dropdown")[1].Change("2");
        cut.FindAll("select.rdp-months_dropdown")[0].GetAttribute("value").Should().Be("1");
        cut.FindAll("select.rdp-months_dropdown")[1].GetAttribute("value").Should().Be("2");
    }

    [Test]
    public async Task Audit_date_picker_rebinds_validation_owner_and_expression()
    {
        Services.AddScoped<IValidationInterop, FakeValidationInterop>();
        var model = new { First = (DateOnly?)new DateOnly(2026, 1, 1), Second = (DateOnly?)new DateOnly(2026, 2, 1) };
        var context = new EditContext(model);
        var first = Render<Validation>(p => p.AddCascadingValue(nameof(EditContext), context));
        var second = Render<Validation>(p => p.AddCascadingValue(nameof(EditContext), context));
        Expression<Func<DateOnly?>> expression = () => model.First;
        RenderFragment content = builder =>
        {
            builder.OpenComponent<DatePicker>(0);
            builder.AddAttribute(1, nameof(DatePicker.SelectedDateExpression), expression);
            builder.CloseComponent();
        };
        var host = Render<CascadingValue<Validation>>(p => p.Add(c => c.Name, "ParentValidation")
            .Add(c => c.Value, first.Instance).Add(c => c.ChildContent, content));
        first.Instance.FieldIdentifier.FieldName.Should().Be("First");
        expression = () => model.Second;
        host.Render(p => p.Add(c => c.Value, second.Instance).Add(c => c.ChildContent, content));
        second.Instance.FieldIdentifier.FieldName.Should().Be("Second");
        var picker = host.FindComponent<DatePicker>();
        var renders = picker.RenderCount;
        await host.InvokeAsync(() => first.Instance.NotifyValidationStatusChanged(ValidationStatus.Error));
        picker.RenderCount.Should().Be(renders);
        await host.InvokeAsync(() => second.Instance.NotifyValidationStatusChanged(ValidationStatus.Error));
        picker.RenderCount.Should().BeGreaterThan(renders);
        picker.Find("button").GetAttribute("aria-invalid").Should().Be("true");
    }

    [Test]
    public void Audit_date_picker_preserves_style_and_disables_readonly_trigger()
    {
        var cut = Render<DatePicker>(p => p.Add(c => c.ReadOnly, true).Add(c => c.Style, "color:red").Add(c => c.AriaLabel, "Due date"));
        var button = cut.Find("button");
        button.GetAttribute("style").Should().Contain("color:red");
        button.GetAttribute("aria-label").Should().Be("Due date");
        button.HasAttribute("disabled").Should().BeTrue();
    }

    [Test]
    public void Audit_date_range_picker_preserves_controlled_open_on_partial_updates()
    {
        var cut = Render<PresetDateRangePicker>(p => p.Add(c => c.Open, false).Add(c => c.DefaultOpen, true));
        cut.Render(p => p.Add(c => c.Style, "color:red"));
        cut.Find("button[data-slot='preset-date-range-picker-trigger']").GetAttribute("aria-expanded").Should().Be("false");
    }

    [Test]
    public void Audit_date_presets_clamp_large_offsets_to_supported_dates()
    {
        PresetDateRangePickerOption.LastDays(int.MaxValue).GetStart(DateOnly.MinValue).Should().Be(DateOnly.MinValue);
        new PresetDateRangePickerOption("future", "Future", 0, int.MaxValue).GetEnd(DateOnly.MaxValue).Should().Be(DateOnly.MaxValue);
        PresetDateRangePickerUtil.GetSelectableMin(DateOnly.MinValue, new DateOnly(1, 1, 2), int.MaxValue, true).Should().Be(DateOnly.MinValue);
    }

    [Test]
    public void Audit_stepper_controlled_selection_waits_for_its_owner()
    {
        var requested = 0;
        var cut = Render<Stepper>(p => p.Add(c => c.Value, 1)
            .Add(c => c.ValueChanged, value => requested = value)
            .Add(c => c.ChildContent, BuildStepper()));

        cut.FindAll("[role='tab']")[1].Click();

        requested.Should().Be(2);
        cut.FindAll("[role='tab']")[0].GetAttribute("aria-selected").Should().Be("true");
        cut.FindAll("[role='tab']")[1].GetAttribute("aria-selected").Should().Be("false");
        cut.Render(p => p.Add(c => c.Value, 2));
        cut.FindAll("[role='tab']")[1].GetAttribute("aria-selected").Should().Be("true");
    }

    [Test]
    public void Audit_stepper_defaults_are_initial_only_and_instances_have_distinct_ids()
    {
        var first = Render<Stepper>(p => p.Add(c => c.DefaultValue, 1).Add(c => c.ChildContent, BuildStepper()));
        var second = Render<Stepper>(p => p.Add(c => c.ChildContent, BuildStepper()));
        first.Find("[role='tab']").Id.Should().NotBe(second.Find("[role='tab']").Id);
        first.FindAll("[role='tab']")[1].Click();
        first.Render(p => p.Add(c => c.DefaultValue, 1));
        first.FindAll("[role='tab']")[1].GetAttribute("aria-selected").Should().Be("true");
    }

    [Test]
    public void Audit_chain_step_closes_when_work_completes_and_can_be_reopened()
    {
        var cut = Render<ChainOfThoughtStep>(p => p.Add(c => c.HasContent, true)
            .Add(c => c.Status, ChainOfThoughtStatus.Running)
            .Add(c => c.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<ChainOfThoughtStepTitle>(0);
                builder.AddAttribute(1, nameof(ChainOfThoughtStepTitle.ChildContent), (RenderFragment)(content => content.AddContent(0, "Work")));
                builder.CloseComponent();
            })));
        cut.Find("[data-slot='chain-of-thought-step']").GetAttribute("data-state").Should().Be("open");
        cut.Render(p => p.Add(c => c.Status, ChainOfThoughtStatus.Completed));
        cut.Find("[data-slot='chain-of-thought-step']").GetAttribute("data-state").Should().Be("closed");
        cut.Find("button").Click();
        cut.Find("[data-slot='chain-of-thought-step']").GetAttribute("data-state").Should().Be("open");
    }

    [Test]
    public void Audit_chain_closes_only_after_all_steps_complete()
    {
        var secondComplete = false;
        RenderFragment content = builder =>
        {
            builder.OpenComponent<ChainOfThoughtStep>(0);
            builder.AddAttribute(1, nameof(ChainOfThoughtStep.Status), ChainOfThoughtStatus.Completed);
            builder.CloseComponent();
            builder.OpenComponent<ChainOfThoughtStep>(2);
            builder.AddAttribute(3, nameof(ChainOfThoughtStep.Status), secondComplete ? ChainOfThoughtStatus.Completed : ChainOfThoughtStatus.Running);
            builder.CloseComponent();
        };
        var cut = Render<ChainOfThought>(p => p.Add(c => c.ChildContent, content));
        cut.Find("[data-slot='chain-of-thought']").GetAttribute("data-state").Should().Be("open");
        secondComplete = true;
        cut.Render(p => p.Add(c => c.ChildContent, content));
        cut.WaitForAssertion(() => cut.Find("[data-slot='chain-of-thought']").GetAttribute("data-state").Should().Be("closed"));
    }

    [Test]
    public void Audit_attachment_renders_its_custom_icon()
    {
        var cut = Render<Attachment>(p => p.Add(c => c.Icon, (RenderFragment)(builder => builder.AddContent(0, "Custom preview"))));
        cut.Find("[data-slot='attachment-preview']").TextContent.Should().Contain("Custom preview");
    }

    [Test]
    public void Audit_image_action_forwards_attributes_and_events()
    {
        var clicked = false;
        var cut = Render<ImageAction>(p => p.Add(c => c.Id, "download-image")
            .Add(c => c.Style, "color:red")
            .AddUnmatched("aria-label", "Download image")
            .AddUnmatched("onclick", EventCallback.Factory.Create<MouseEventArgs>(this, _ => clicked = true)));
        var button = cut.Find("button");
        button.Id.Should().Be("download-image");
        button.GetAttribute("style").Should().Contain("color:red");
        button.GetAttribute("aria-label").Should().Be("Download image");
        button.Click();
        clicked.Should().BeTrue();
    }

    [Test]
    public void Audit_iframe_preserves_an_empty_restrictive_sandbox()
    {
        var cut = Render<IFrame>(p => p.Add(c => c.Sandbox, "").Add(c => c.SrcDoc, ""));
        cut.Find("iframe").GetAttribute("sandbox").Should().BeEmpty();
        cut.Find("iframe").GetAttribute("srcdoc").Should().BeEmpty();
    }

    [Test]
    public void Audit_prompt_submit_uses_initial_value_and_modified_enter_does_not_submit()
    {
        var submissions = new List<string?>();
        var cut = Render<PromptInput>(p => p.Add(c => c.OnSubmit, text => submissions.Add(text))
            .Add(c => c.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<PromptInputTextarea>(0);
                builder.AddAttribute(1, nameof(PromptInputTextarea.Value), "Initial message");
                builder.CloseComponent();
            })));
        cut.Find("textarea").KeyDown(new KeyboardEventArgs { Key = "Enter", CtrlKey = true });
        cut.Find("textarea").KeyDown(new KeyboardEventArgs { Key = "Enter", AltKey = true });
        submissions.Should().BeEmpty();
        cut.Find("form").Submit();
        submissions.Should().Equal("Initial message");
    }

    [Test]
    public async Task Audit_prompt_single_attachment_and_disabled_input_are_enforced()
    {
        IReadOnlyList<PromptInputAttachmentInfo>? attachments = null;
        var cut = Render<PromptInput>(p => p.Add(c => c.AttachmentsChanged, value => attachments = value)
            .Add(c => c.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<PromptInputActionAddAttachments>(0);
                builder.CloseComponent();
            })));
        var action = cut.FindComponent<PromptInputActionAddAttachments>();
        await cut.InvokeAsync(() => action.Instance.AddAttachments([
            new() { Id = "first", Name = "first.txt" }, new() { Id = "second", Name = "second.txt" }
        ]));
        attachments.Should().ContainSingle().Which.Id.Should().Be("first");
        action.Render(p => p.Add(c => c.Disabled, true));
        await cut.InvokeAsync(() => action.Instance.AddAttachments([new() { Id = "third", Name = "third.txt" }]));
        attachments.Should().ContainSingle().Which.Id.Should().Be("first");
        action.Find("input[type=file]").HasAttribute("disabled").Should().BeTrue();
    }

    [Test]
    public void Audit_console_keeps_custom_actions_when_builtin_actions_are_hidden()
    {
        Services.AddQuarkConsolePanelAsScoped();
        var cut = Render<ConsolePanel>(p => p.Add(c => c.ShowCopy, false).Add(c => c.ShowDownload, false)
            .Add(c => c.Actions, (RenderFragment)(builder => builder.AddContent(0, "Custom action"))));
        cut.Find("[data-slot='console-actions']").TextContent.Should().Contain("Custom action");
    }

    [Test]
    public void Audit_questionnaire_prefilled_input_counts_as_an_answer()
    {
        QuestionnaireItemStatus? status = null;
        var cut = Render<QuestionnaireItem>(p => p.Add(c => c.Name, "answer")
            .Add(c => c.StatusChanged, value => status = value)
            .Add(c => c.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<QuestionnaireInput>(0);
                builder.AddAttribute(1, nameof(QuestionnaireInput.Value), "Existing answer");
                builder.CloseComponent();
            })));
        status.Should().Be(QuestionnaireItemStatus.Answered);
    }

    [Test]
    public void Audit_comparison_chart_renders_its_empty_state_without_metrics()
    {
        Services.AddQuarkComparisonChartAsScoped();
        var cut = Render<ComparisonChart>();
        cut.Find("[role='status']").TextContent.Should().Be("No data to display");
    }

    [Test]
    public void Audit_pagination_clamps_invalid_page_and_button_count()
    {
        var cut = Render<DataTablePagination>(p => p.Add(c => c.CurrentPage, 20)
            .Add(c => c.TotalPages, 3).Add(c => c.MaxPageButtons, 0));
        cut.Instance.StartPage.Should().Be(3);
        cut.Instance.EndPage.Should().Be(3);
        cut.Find("[aria-label='Go to next page']").HasAttribute("disabled").Should().BeTrue();
    }
}
