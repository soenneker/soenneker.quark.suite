using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Bradix;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    [Arguments(typeof(TextInput))]
    [Arguments(typeof(PasswordInput))]
    [Arguments(typeof(NumericInput))]
    [Arguments(typeof(CurrencyInput))]
    [Arguments(typeof(DateInput))]
    public void Audit_input_wrappers_validate_once_per_native_event(Type componentType)
    {
        Services.AddScoped<IValidationInterop, FakeValidationInterop>();
        var values = new List<object?>();
        var cut = Render<Validation>(p => p
            .Add(c => c.AsyncFunc, (args, _) =>
            {
                values.Add(args.Value);
                args.Status = ValidationStatus.Success;
                return Task.CompletedTask;
            })
            .Add(c => c.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent(0, componentType);
                builder.CloseComponent();
            })));
        values.Clear();
        var text = componentType == typeof(DateInput) ? "2026-10-11" : "12";
        object expected = componentType == typeof(DateInput) ? new DateTime(2026, 10, 11)
            : componentType == typeof(NumericInput) || componentType == typeof(CurrencyInput) ? 12m : "12";
        cut.Find("input").Input(text);
        values.Should().ContainSingle().Which.Should().Be(expected);
        values.Clear();
        cut.Find("input").Change(text);
        values.Should().ContainSingle().Which.Should().Be(expected);

        if (componentType == typeof(NumericInput) || componentType == typeof(CurrencyInput) || componentType == typeof(DateInput))
        {
            values.Clear();
            cut.Find("input").Input("");
            values.Should().ContainSingle().Which.Should().BeNull();
        }
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async ValueTask Audit_native_input_rebinds_validation_owner_and_expression(bool memo, CancellationToken cancellationToken)
    {
        Services.AddScoped<IValidationInterop, FakeValidationInterop>();
        var model = new { First = "first", Second = "second" };
        var editContext = new EditContext(model);
        var first = Render<Validation>(p => p.AddCascadingValue(nameof(EditContext), editContext));
        var second = Render<Validation>(p => p.AddCascadingValue(nameof(EditContext), editContext));
        Expression<Func<string?>> expression = () => model.First;
        RenderFragment content = builder =>
        {
            builder.OpenComponent(0, memo ? typeof(MemoInput) : typeof(Input));
            builder.AddAttribute(1, "ValueExpression", expression);
            builder.CloseComponent();
        };
        var host = Render<CascadingValue<Validation>>(p => p.Add(c => c.Name, "ParentValidation")
            .Add(c => c.Value, first.Instance).Add(c => c.ChildContent, content));
        first.Instance.FieldIdentifier.FieldName.Should().Be("First");
        expression = () => model.Second;
        host.Render(p => p.Add(c => c.Value, second.Instance).Add(c => c.ChildContent, content));
        second.Instance.FieldIdentifier.FieldName.Should().Be("Second");
        int renders = memo ? host.FindComponent<MemoInput>().RenderCount : host.FindComponent<Input>().RenderCount;
        await host.InvokeAsync(() => first.Instance.NotifyValidationStatusChanged(ValidationStatus.Error));
        (memo ? host.FindComponent<MemoInput>().RenderCount : host.FindComponent<Input>().RenderCount).Should().Be(renders);
        await host.InvokeAsync(() => second.Instance.NotifyValidationStatusChanged(ValidationStatus.Error));
        host.WaitForAssertion(() => (memo ? host.FindComponent<MemoInput>().RenderCount : host.FindComponent<Input>().RenderCount).Should().BeGreaterThan(renders));
    }

    [Test]
    public async ValueTask Audit_Check_forwards_actual_click_before_value_change(CancellationToken cancellationToken)
    {
        MouseEventArgs? click = null;
        var callbacks = 0;
        var cut = Render<Check>(p => p.Add(c => c.OnClick, args => { click = args; callbacks++; }));
        await cut.Find("[role=checkbox]").ClickAsync(new MouseEventArgs { Detail = 2, CtrlKey = true });
        callbacks.Should().Be(1);
        click!.Detail.Should().Be(2);
        click.CtrlKey.Should().BeTrue();
        await cut.InvokeAsync(() => cut.FindComponent<BradixCheckbox>().Instance.HandleDelegatedKeyDown(new BradixDelegatedKeyboardEvent { Key = " " }));
        callbacks.Should().Be(1);
    }

    [Test]
    public void Audit_RadioGroup_updates_explicit_name()
    {
        var cut = Render<RadioGroup>(p => p.Add(c => c.Name, "first"));
        cut.Instance.EffectiveName.Should().Be("first");
        cut.Render(p => p.Add(c => c.Name, "second"));
        cut.FindComponent<BradixRadioGroup>().Instance.Name.Should().Be("second");
        cut.Render(p => p.Add(c => c.Name, (string?)null));
        cut.Instance.EffectiveName.Should().StartWith("quark-radiogroup");
    }

    [Test]
    public void Audit_Slider_explicit_direction_reaches_primitive()
    {
        var cut = Render<Slider>(p => p.Add(c => c.Dir, "rtl"));
        cut.FindComponent<BradixSlider>().Instance.Dir.Should().Be("rtl");
        cut.Render(p => p.Add(c => c.Dir, "ltr"));
        cut.FindComponent<BradixSlider>().Instance.Dir.Should().Be("ltr");
    }

    [Test]
    public async ValueTask Audit_ToggleGroupItem_forwards_click(CancellationToken cancellationToken)
    {
        var callbacks = 0;
        var cut = Render<ToggleGroup>(p => p.AddChildContent(builder =>
        {
            builder.OpenComponent<ToggleGroupItem>(0);
            builder.AddAttribute(1, nameof(ToggleGroupItem.Value), "bold");
            builder.AddAttribute(2, nameof(ToggleGroupItem.OnClick), EventCallback.Factory.Create<MouseEventArgs>(this, _ => callbacks++));
            builder.CloseComponent();
        }));
        await cut.Find("button").ClickAsync();
        callbacks.Should().Be(1);
    }

    [Test]
    public void Audit_CodeEditor_synchronizes_options_and_theme_subscriptions()
    {
        var cut = Render<CodeEditor>(p => p.Add(c => c.Theme, "vs").Add(c => c.Lazy, false));
        var interop = (FakeCodeEditorInterop)Services.GetRequiredService<ICodeEditorInterop>();
        interop.OptionUpdates.Should().HaveCount(1);
        cut.Render();
        interop.OptionUpdates.Should().HaveCount(1);
        cut.Render(p => p.Add(c => c.ReadOnly, true).Add(c => c.WordWrap, true).Add(c => c.Minimap, false)
            .Add(c => c.AutoHeight, true).Add(c => c.MaxLines, 5).Add(c => c.AriaLabel, "Editor source").Add(c => c.Theme, "auto"));
        interop.OptionUpdates.Should().HaveCount(2);
        interop.OptionUpdates[1].Should().Be((true, true, false, true, (int?)3, (int?)5, "Editor source"));
        interop.ThemeRegistrations.Should().Be(1);
        cut.Render(p => p.Add(c => c.AutoHeight, false).Add(c => c.Theme, "vs-dark"));
        interop.OptionUpdates[^1].AutoHeight.Should().BeFalse();
        interop.ThemeUnregistrations.Should().Be(1);
    }
}
