using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a draggable and keyboard-activatable node palette item.
/// </summary>
public interface INodeEditorPaletteItem : IElement
{
    /// <summary>
    /// Gets or sets the consumer-defined palette type emitted when this item is dropped onto a node editor.
    /// </summary>
    string Type { get; set; }

    /// <summary>
    /// Gets or sets optional opaque consumer data carried with the drop request.
    /// </summary>
    string? Data { get; set; }

    /// <summary>
    /// Gets or sets the callback raised when the palette item is clicked or keyboard-activated.
    /// This provides an accessible alternative to dragging; the consumer chooses the insertion position.
    /// </summary>
    EventCallback OnActivated { get; set; }
}
