namespace Soenneker.Quark;

/// <summary>
/// Configures node editor viewport and interaction behavior.
/// </summary>
public interface INodeEditorOptions
{
    /// <summary>
    /// Gets or sets whether pointer and keyboard interactions can reposition nodes.
    /// </summary>
    bool DraggableNodes { get; set; }

    /// <summary>
    /// Gets or sets whether middle-button dragging empty editor space pans the viewport.
    /// </summary>
    bool PanEnabled { get; set; }

    /// <summary>
    /// Gets or sets whether dragging across empty editor space selects every selectable node intersecting the rectangle.
    /// Middle-button dragging continues to pan when <see cref="PanEnabled"/> is enabled.
    /// </summary>
    bool MarqueeSelectionEnabled { get; set; }

    /// <summary>
    /// Gets or sets whether wheel gestures and viewport controls can change zoom.
    /// </summary>
    bool ZoomEnabled { get; set; }

    /// <summary>
    /// Gets or sets whether users can create connections between source and target ports.
    /// </summary>
    bool ConnectionsEnabled { get; set; }

    /// <summary>
    /// Gets or sets whether Delete and Backspace raise <see cref="NodeEditor.OnDeleteRequested"/> for the current selection.
    /// </summary>
    bool DeleteKeyEnabled { get; set; }

    /// <summary>
    /// Gets or sets whether standard undo, redo, and duplicate keyboard shortcuts raise controlled command callbacks.
    /// </summary>
    bool CommandShortcutsEnabled { get; set; }

    /// <summary>
    /// Gets or sets whether double-clicking an edge, or pressing F2 while it is focused, opens its inline label editor.
    /// </summary>
    bool InlineEdgeLabelEditingEnabled { get; set; }

    /// <summary>
    /// Gets or sets the smallest allowed viewport scale. Must be finite and greater than zero.
    /// </summary>
    double MinZoom { get; set; }

    /// <summary>
    /// Gets or sets the largest allowed viewport scale. Must be finite and at least <see cref="MinZoom"/>.
    /// </summary>
    double MaxZoom { get; set; }

    /// <summary>
    /// Gets or sets the initial viewport scale. Values outside the allowed range are clamped.
    /// </summary>
    double InitialZoom { get; set; }

    /// <summary>
    /// Gets or sets the initial horizontal viewport translation in screen pixels.
    /// </summary>
    double InitialX { get; set; }

    /// <summary>
    /// Gets or sets the initial vertical viewport translation in screen pixels.
    /// </summary>
    double InitialY { get; set; }

    /// <summary>
    /// Gets or sets the graph-space grid interval used when <see cref="SnapToGrid"/> is enabled.
    /// </summary>
    double GridSize { get; set; }

    /// <summary>
    /// Gets or sets whether pointer and keyboard node movement snaps positions to <see cref="GridSize"/>.
    /// </summary>
    bool SnapToGrid { get; set; }

    /// <summary>
    /// Gets or sets whether the editor fits all initial nodes into the viewport after browser initialization.
    /// </summary>
    bool FitViewOnInitialize { get; set; }
}
