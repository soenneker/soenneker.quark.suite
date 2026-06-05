using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a shadcn-style sheet component.
/// </summary>
public interface ISheet : IElement
{
    /// <summary>
    /// Gets or sets a value indicating whether visible.
    /// </summary>
    bool Visible { get; set; }
    /// <summary>
    /// Gets or sets visible changed.
    /// </summary>
    EventCallback<bool> VisibleChanged { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether show backdrop.
    /// </summary>
    bool ShowBackdrop { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether blur backdrop.
    /// </summary>
    bool BlurBackdrop { get; set; }

    /// <summary>
    /// When <c>true</c>, Escape and backdrop do not dismiss (same idea as <see cref="Dialog.Static"/>).
    /// </summary>
    bool Static { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether close on escape.
    /// </summary>
    bool CloseOnEscape { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether close on backdrop click.
    /// </summary>
    bool CloseOnBackdropClick { get; set; }
    /// <summary>
    /// Gets or sets on show.
    /// </summary>
    EventCallback OnShow { get; set; }
    /// <summary>
    /// Gets or sets on hide.
    /// </summary>
    EventCallback OnHide { get; set; }
    /// <summary>
    /// Gets or sets on backdrop click.
    /// </summary>
    EventCallback OnBackdropClick { get; set; }
    /// <summary>
    /// Executes the show operation.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Show();
    /// <summary>
    /// Executes the hide operation.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Hide();
}
