using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components.Forms;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public async Task Generated_generic_select_binding_identifies_model_without_explicit_type_or_field()
    {
        var model = new GeneratedBindingModel();
        var cut = Render<GeneratedSelectBindingProbe>(p => p.Add(c => c.Model, model));
        var validation = cut.FindComponent<Validation>().Instance;
        validation.FieldIdentifier.Should().Be(new FieldIdentifier(model, nameof(model.Name)));
        await cut.InvokeAsync(validation.ValidateCurrent);
        validation.Status.Should().Be(ValidationStatus.Error);
    }
}
