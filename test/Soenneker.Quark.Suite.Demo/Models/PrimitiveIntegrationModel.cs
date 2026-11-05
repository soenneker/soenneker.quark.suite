using System.ComponentModel.DataAnnotations;

namespace Soenneker.Quark.Suite.Demo;

public sealed partial class PrimitiveIntegrationModel
{
    [Required(ErrorMessage = "Choose a city.")]
    public string? City { get; set; }
}
