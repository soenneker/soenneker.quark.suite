using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Bradix;
using System.Threading.Tasks;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public async Task Manual_validation_revalidates_input_after_explicit_validation()
    {
        Services.AddScoped<IValidationInterop, FakeValidationInterop>();

        var accepted = false;

        var cut = Render<Validations>(parameters => parameters
            .Add(component => component.Mode, ValidationMode.Manual)
            .Add(component => component.GateErrorsUntilValidate, true)
            .Add(component => component.ChildContent, (RenderFragment) (builder =>
            {
                builder.OpenComponent<Validation>(0);
                builder.AddAttribute(1, nameof(Validation.Validator), Validators.IsChecked);
                builder.AddAttribute(2, nameof(Validation.ChildContent), (RenderFragment) (child =>
                {
                    child.OpenComponent<Check>(0);
                    child.AddAttribute(1, nameof(Check.Checked), accepted);
                    child.AddAttribute(2, nameof(Check.CheckedChanged), EventCallback.Factory.Create<bool>(this, value => accepted = value));
                    child.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        (await cut.Instance.Validate()).Should().BeFalse();
        cut.FindComponent<Validation>().Instance.Status.Should().Be(ValidationStatus.Error);

        IRenderedComponent<BradixCheckbox> checkbox = cut.FindComponent<BradixCheckbox>();
        await checkbox.InvokeAsync(() => checkbox.Instance.CheckedChanged.InvokeAsync(BradixCheckboxCheckedState.Checked));

        cut.WaitForAssertion(() =>
        {
            accepted.Should().BeTrue();
            cut.FindComponent<Validation>().Instance.Status.Should().Be(ValidationStatus.Success);
        });
    }
}
