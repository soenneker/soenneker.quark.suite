using Microsoft.AspNetCore.Components.Forms;

namespace Soenneker.Quark;

/// <summary>Connects an input's generated binding to its validation owner.</summary>
public interface IValidationFieldBinding
{
    /// <summary>Gets or sets the model field supplied by build-time Razor binding generation.</summary>
    FieldIdentifier? ValidationField { get; set; }
}
