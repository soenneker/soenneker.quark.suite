using System.ComponentModel.DataAnnotations;

namespace Soenneker.Quark.Suite.Demo.Pages.Components;

public sealed class FormDemoModel
{
    [Required(ErrorMessage = "Enter your name.")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Enter your email address.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    public string Email { get; set; } = "";
}
