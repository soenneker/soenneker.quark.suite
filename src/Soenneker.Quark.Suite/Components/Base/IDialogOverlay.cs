using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
namespace Soenneker.Quark;

/// <summary>Common visibility commands and dismissal options for dialog-based overlays.</summary>
public interface IDialogOverlay : IElement
{
    /// <summary>Prevents outside dismissal. Sheets and drawers also prevent Escape dismissal. Explicit Hide remains available.</summary>
    bool Static { get; set; }
    /// <summary>Invoked on outside pointer interaction, including when dismissal is disabled.</summary>
    EventCallback OnBackdropClick { get; set; }
    /// <summary>Shows the overlay and raises visibility and lifecycle callbacks once per transition.</summary>
    Task Show();
    /// <summary>Hides the overlay and raises visibility and lifecycle callbacks once per transition.</summary>
    Task Hide();
}
