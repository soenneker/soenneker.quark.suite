using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void TextInput_edit_mask_registers_pattern_validation()
    {
        Services.AddScoped<IValidationInterop, FakeValidationInterop>();

        const string editMask = @"^\d{2}$";

        var cut = Render<Validation>(parameters => parameters
            .Add(component => component.ChildContent, (RenderFragment) (builder =>
            {
                builder.OpenComponent<TextInput>(0);
                builder.AddAttribute(1, nameof(TextInput.EditMask), editMask);
                builder.CloseComponent();
            })));

        var input = cut.Find("input");
        input.GetAttribute("pattern").Should().Be(editMask);

        input.Input("abc");
        cut.WaitForAssertion(() => cut.Instance.Status.Should().Be(ValidationStatus.Error));

        input.Input("12");
        cut.WaitForAssertion(() => cut.Instance.Status.Should().Be(ValidationStatus.Success));
    }

    [Test]
    public void TextInput_time_passes_native_attributes_and_defers_value_binding_until_blur()
    {
        var value = "10:30:00";
        var changes = new List<string?>();

        var cut = Render<TextInput>(parameters => parameters
            .Add(component => component.Type, "time")
            .Add(component => component.Value, value)
            .Add(component => component.ValueChanged, next =>
            {
                value = next;
                changes.Add(next);
                return Task.CompletedTask;
            })
            .AddUnmatched("step", "1"));

        var input = cut.Find("input");
        input.GetAttribute("type").Should().Be("time");
        input.GetAttribute("step").Should().Be("1");

        input.Input("01:30:00");
        input.Change("19:30:00");

        changes.Should().BeEmpty();

        input.Blur();

        changes.Should().ContainSingle().Which.Should().Be("19:30:00");
        value.Should().Be("19:30:00");
    }

    private sealed class FakeValidationInterop : IValidationInterop
    {
        public ValueTask Initialize(CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}
