using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a shadcn-style sheet component.
/// </summary>
public interface ISheet : IElement
{
    bool Visible { get; set; }
    EventCallback<bool> VisibleChanged { get; set; }
    bool ShowBackdrop { get; set; }
    bool CloseOnEscape { get; set; }
    bool CloseOnBackdropClick { get; set; }
    EventCallback OnShow { get; set; }
    EventCallback OnHide { get; set; }
    EventCallback OnBackdropClick { get; set; }
    Task Show();
    Task Hide();
}
