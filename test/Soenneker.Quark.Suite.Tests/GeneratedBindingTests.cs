using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark.Suite.Tests;

public sealed class GeneratedBindingTests : BunitContext
{
    public GeneratedBindingTests()
    {
        Services.AddLogging();
        Services.AddDefaultQuarkOptionsAsScoped();
        Services.AddSingleton<IInputInterop, InputSelectionTestInterop>();
    }

    [Test]
    public async Task Ordinary_bind_validates_nested_model_and_rebinds_replaced_owner()
    {
        var model = new GeneratedBindingContainer();
        var cut = Render<GeneratedBindingProbe>(p => p.Add(c => c.Model, model));
        var validation = cut.FindComponent<Validation>().Instance;
        validation.FieldIdentifier.Should().Be(new FieldIdentifier(model.Child, nameof(model.Child.Name)));
        await cut.InvokeAsync(validation.ValidateCurrent);
        validation.Status.Should().Be(ValidationStatus.Error);

        cut.Find("input").Input("valid");
        model.Child.Name.Should().Be("valid");
        cut.WaitForAssertion(() => validation.Status.Should().Be(ValidationStatus.Success));

        model.Child = new GeneratedBindingModel();
        cut.Render(p => p.Add(c => c.Model, model));
        validation.FieldIdentifier.Should().Be(new FieldIdentifier(model.Child, nameof(model.Child.Name)));
        await cut.InvokeAsync(validation.ValidateCurrent);
        validation.Status.Should().Be(ValidationStatus.Error);
    }

    [Test]
    public async Task Explicit_field_overrides_generated_binding()
    {
        var model = new GeneratedBindingContainer();
        var explicitModel = new GeneratedBindingModel { Name = "valid" };
        var field = new FieldIdentifier(explicitModel, nameof(explicitModel.Name));
        var cut = Render<GeneratedBindingProbe>(p => p.Add(c => c.Model, model).Add(c => c.ExplicitField, field));
        var validation = cut.FindComponent<Validation>().Instance;
        validation.FieldIdentifier.Should().Be(field);
        await cut.InvokeAsync(validation.ValidateCurrent);
        validation.Status.Should().Be(ValidationStatus.Success);
    }
}
