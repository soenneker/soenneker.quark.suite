using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
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

    private sealed class FakeValidationInterop : IValidationInterop
    {
        public ValueTask Initialize(CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}
