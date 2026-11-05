using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Soenneker.Bradix;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    [Arguments(typeof(Dialog), typeof(DialogTrigger), false)]
    [Arguments(typeof(Dialog), typeof(DialogTrigger), true)]
    [Arguments(typeof(AlertDialog), typeof(AlertDialogTrigger), false)]
    [Arguments(typeof(AlertDialog), typeof(AlertDialogTrigger), true)]
    [Arguments(typeof(Popover), typeof(PopoverTrigger), false)]
    [Arguments(typeof(Popover), typeof(PopoverTrigger), true)]
    [Arguments(typeof(Collapsible), typeof(CollapsibleTrigger), false)]
    [Arguments(typeof(Collapsible), typeof(CollapsibleTrigger), true)]
    public async Task Bradix_integration_triggers_cancel_and_activate_once(Type root, Type trigger, bool asChild)
    {
        int clicks = 0;
        int childClicks = 0;
        bool cancel = true;
        RenderFragment child = b =>
        {
            b.OpenComponent(0, trigger);
            b.AddAttribute(1, "AsChild", asChild);
            b.AddAttribute(2, "Class", "parent-trigger");
            b.AddAttribute(3, "OnClick", EventCallback.Factory.Create<MouseEventArgs>(this, _ => clicks++));
            b.AddAttribute(4, "OnActivate", EventCallback.Factory.Create<BradixActivationEventArgs>(this, args => { if (cancel) args.PreventDefault(); }));
            b.AddAttribute(5, "ChildContent", (RenderFragment)(c =>
            {
                if (asChild)
                {
                    c.OpenComponent<Button>(0);
                    c.AddAttribute(1, "Class", "child-button");
                    c.AddAttribute(2, "OnClick", EventCallback.Factory.Create<MouseEventArgs>(this, _ => childClicks++));
                    c.AddAttribute(3, "ChildContent", (RenderFragment)(text => text.AddContent(0, "Open")));
                    c.AddAttribute(4, "Id", "custom-child-trigger");
                    c.CloseComponent();
                }
                else c.AddContent(4, "Open");
            }));
            b.CloseComponent();
        };
        var cut = Render<DynamicComponent>(p => p.Add(c => c.Type, root)
            .Add(c => c.Parameters, new Dictionary<string, object> { ["ChildContent"] = child }));
        cut.FindAll("button").Count.Should().Be(1);
        cut.Find("button").ClassList.Should().Contain("parent-trigger");
        if (asChild) cut.Find("button").ClassList.Should().Contain("child-button");
        await cut.Find("button").ClickAsync(new MouseEventArgs());
        cut.Find("button").GetAttribute("aria-expanded").Should().Be("false");
        clicks.Should().Be(1);
        childClicks.Should().Be(asChild ? 1 : 0);
        cancel = false;
        await cut.Find("button").ClickAsync(new MouseEventArgs());
        cut.Find("button").GetAttribute("aria-expanded").Should().Be("true");
        clicks.Should().Be(2);
        childClicks.Should().Be(asChild ? 2 : 0);
        if (root == typeof(Dialog) || root == typeof(AlertDialog))
        {
            cut.FindComponent<BradixDialog>().Instance.TriggerElement.Id.Should().NotBeNullOrEmpty();
            if (asChild) cut.FindComponent<BradixDialog>().Instance.ResolvedTriggerId.Should().Be("custom-child-trigger");
        }
        if (asChild && root == typeof(Popover))
            cut.FindComponent<BradixPopover>().Instance.ResolvedTriggerId.Should().Be("custom-child-trigger");
    }

    [Test]
    public async Task Bradix_integration_select_renders_rich_values_and_preserves_placeholder()
    {
        var cut = Render<Select<string>>(p => p.Add(c => c.SelectedValue, "Paris")
            .AddChildContent<SelectValue>(v => v.Add(c => c.Placeholder, "Choose a city")
                .Add(c => c.ValueTemplate, value => b => { b.OpenElement(0, "strong"); b.AddContent(1, value); b.CloseElement(); })));
        cut.Find("strong").TextContent.Should().Be("Paris");
        cut.Render(p => p.Add(c => c.SelectedValue, "London"));
        cut.Find("strong").TextContent.Should().Be("London");
        cut.Render(p => p.Add(c => c.SelectedValue, ""));
        cut.FindAll("strong").Count.Should().Be(0);
        cut.Markup.Should().Contain("Choose a city");
        await Task.CompletedTask;
    }

    [Test]
    public async Task Bradix_integration_select_updates_edit_context_directly()
    {
        var model = new BradixIntegrationModel();
        var editContext = new EditContext(model);
        var identifier = new FieldIdentifier(model, nameof(model.City));
        var errors = new ValidationMessageStore(editContext);
        RenderFragment input = b =>
        {
            b.OpenComponent<Select<string>>(0);
            b.AddAttribute(1, "SelectedValue", model.City);
            b.AddAttribute(2, "SelectedValueChanged", EventCallback.Factory.Create<string?>(this, value => model.City = value));
            b.AddAttribute(3, "SelectedValueExpression", (Expression<Func<string?>>)(() => model.City));
            b.AddAttribute(4, "ChildContent", (RenderFragment)(t =>
            {
                t.OpenComponent<SelectTrigger>(0);
                t.CloseComponent();
            }));
            b.CloseComponent();
        };
        var cut = Render<CascadingValue<EditContext>>(p => p.Add(c => c.Value, editContext).AddChildContent(input));
        await cut.InvokeAsync(() => cut.FindComponent<BradixSelect>().Instance.ValueChanged.InvokeAsync("Paris"));
        model.City.Should().Be("Paris");
        editContext.IsModified(identifier).Should().BeTrue();
        await cut.InvokeAsync(() => { errors.Add(identifier, "Choose another city"); editContext.NotifyValidationStateChanged(); });
        cut.Find("[role=combobox]").GetAttribute("aria-invalid").Should().Be("true");
        cut.Find("[role=combobox]").ClassList.Should().Contain("invalid");
        await cut.InvokeAsync(() => { errors.Clear(); editContext.NotifyValidationStateChanged(); });
        cut.Find("[role=combobox]").HasAttribute("aria-invalid").Should().BeFalse();
    }

    [Test]
    [Arguments(typeof(Input), "input")]
    [Arguments(typeof(TextInput), "input")]
    [Arguments(typeof(PasswordInput), "input")]
    [Arguments(typeof(TextArea), "textarea")]
    [Arguments(typeof(MemoInput), "textarea")]
    public async Task Bradix_integration_inputs_track_changes_and_validation(Type control, string selector)
    {
        var model = new BradixIntegrationModel();
        var context = new EditContext(model);
        var field = new FieldIdentifier(model, nameof(model.City));
        var errors = new ValidationMessageStore(context);
        int changes = 0;
        context.OnFieldChanged += (_, _) => changes++;
        RenderFragment input = b =>
        {
            b.OpenComponent(0, control);
            b.AddAttribute(1, "Value", model.City);
            b.AddAttribute(2, "ValueChanged", EventCallback.Factory.Create<string?>(this, value => model.City = value));
            b.AddAttribute(3, "ValueExpression", (Expression<Func<string?>>)(() => model.City));
            b.CloseComponent();
        };
        var cut = Render<CascadingValue<EditContext>>(p => p.Add(c => c.Value, context).AddChildContent(input));
        await cut.Find(selector).InputAsync(new ChangeEventArgs { Value = "Paris" });
        model.City.Should().Be("Paris");
        context.IsModified(field).Should().BeTrue();
        changes.Should().Be(1);
        await cut.Find(selector).ChangeAsync(new ChangeEventArgs { Value = "Paris" });
        changes.Should().Be(1);
        await cut.InvokeAsync(() => { errors.Add(field, "Invalid city"); context.NotifyValidationStateChanged(); });
        cut.Find(selector).GetAttribute("aria-invalid").Should().Be("true");
        cut.Find(selector).ClassList.Should().Contain("invalid");
        await cut.InvokeAsync(() => { errors.Clear(); context.NotifyValidationStateChanged(); });
        cut.Find(selector).GetAttribute("aria-invalid").Should().NotBe("true");
    }

    [Test]
    [Arguments(typeof(NumericInput))]
    [Arguments(typeof(CurrencyInput))]
    public async Task Bradix_integration_numeric_inputs_notify_the_typed_field(Type control)
    {
        var model = new BradixIntegrationModel();
        var context = new EditContext(model);
        int changes = 0;
        context.OnFieldChanged += (_, _) => changes++;
        RenderFragment input = b =>
        {
            b.OpenComponent(0, control);
            b.AddAttribute(1, "Value", model.Amount);
            b.AddAttribute(2, "ValueChanged", EventCallback.Factory.Create<decimal?>(this, value => model.Amount = value));
            b.AddAttribute(3, "ValueExpression", (Expression<Func<decimal?>>)(() => model.Amount));
            b.CloseComponent();
        };
        var cut = Render<CascadingValue<EditContext>>(p => p.Add(c => c.Value, context).AddChildContent(input));
        await cut.Find("input").InputAsync(new ChangeEventArgs { Value = "12.5" });
        model.Amount.Should().Be(12.5m);
        context.IsModified(new FieldIdentifier(model, nameof(model.Amount))).Should().BeTrue();
        changes.Should().Be(1);
    }

    [Test]
    [Arguments(typeof(Check), "[role=checkbox]")]
    [Arguments(typeof(Switch), "[role=switch]")]
    public async Task Bradix_integration_toggles_notify_the_checked_field(Type control, string selector)
    {
        var model = new BradixIntegrationModel();
        var context = new EditContext(model);
        RenderFragment input = b =>
        {
            b.OpenComponent(0, control);
            b.AddAttribute(1, "Checked", model.Enabled);
            b.AddAttribute(2, "CheckedChanged", EventCallback.Factory.Create<bool>(this, value => model.Enabled = value));
            b.AddAttribute(3, "CheckedExpression", (Expression<Func<bool>>)(() => model.Enabled));
            b.CloseComponent();
        };
        var cut = Render<CascadingValue<EditContext>>(p => p.Add(c => c.Value, context).AddChildContent(input));
        await cut.Find(selector).ClickAsync(new MouseEventArgs());
        model.Enabled.Should().BeTrue();
        context.IsModified(new FieldIdentifier(model, nameof(model.Enabled))).Should().BeTrue();
    }

    [Test]
    public async Task Bradix_integration_inputs_rebind_when_form_model_changes()
    {
        var first = new BradixIntegrationModel();
        var second = new BradixIntegrationModel();
        var firstContext = new EditContext(first);
        var secondContext = new EditContext(second);
        var model = first;
        RenderFragment input = b =>
        {
            b.OpenComponent<Input>(0);
            b.AddAttribute(1, "Value", model.City);
            b.AddAttribute(2, "ValueChanged", EventCallback.Factory.Create<string?>(this, value => model.City = value));
            b.AddAttribute(3, "ValueExpression", (Expression<Func<string?>>)(() => model.City));
            b.CloseComponent();
        };
        var cut = Render<CascadingValue<EditContext>>(p => p.Add(c => c.Value, firstContext).AddChildContent(input));
        model = second;
        cut.Render(p => p.Add(c => c.Value, secondContext).AddChildContent(input));
        await cut.Find("input").InputAsync(new ChangeEventArgs { Value = "Paris" });
        firstContext.IsModified().Should().BeFalse();
        secondContext.IsModified(new FieldIdentifier(second, nameof(second.City))).Should().BeTrue();
        first.City.Should().Be("London");
        second.City.Should().Be("Paris");
        cut.Render(p => p.Add(c => c.Value, secondContext).AddChildContent((RenderFragment)(_ => { })));
        firstContext.NotifyValidationStateChanged();
        secondContext.NotifyValidationStateChanged();
    }

    [Test]
    public void Bradix_integration_announcements_share_regions_and_clean_up()
    {
        var cut = Render<AnnounceProvider>(p => p.AddChildContent<Announce>(a => a.AddChildContent("Saved")));
        cut.WaitForAssertion(() => cut.Find("[aria-live=polite]").TextContent.Should().Contain("Saved"));
        cut.FindAll("[aria-live]").Count.Should().Be(2);
        cut.Render(p => p.AddChildContent<Announce>(a => a.Add(c => c.Type, "assertive").AddChildContent("Failed")));
        cut.WaitForAssertion(() => cut.Find("[aria-live=assertive]").TextContent.Should().Contain("Failed"));
        cut.Find("[aria-live=polite]").TextContent.Trim().Should().BeEmpty();
        cut.Render(p => p.AddChildContent((RenderFragment)(_ => { })));
        cut.WaitForAssertion(() => cut.Find("[aria-live=assertive]").TextContent.Trim().Should().BeEmpty());
    }
}
