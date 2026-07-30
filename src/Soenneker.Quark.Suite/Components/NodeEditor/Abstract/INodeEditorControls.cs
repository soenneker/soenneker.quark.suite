namespace Soenneker.Quark;

/// <summary>
/// Represents the built-in node editor viewport and history controls.
/// </summary>
public interface INodeEditorControls : IElement
{
    /// <summary>
    /// Gets or sets the cascaded editor command context.
    /// </summary>
    NodeEditorContext? EditorContext { get; set; }

    /// <summary>
    /// Gets or sets whether the undo action is available.
    /// </summary>
    bool CanUndo { get; set; }

    /// <summary>
    /// Gets or sets whether the redo action is available.
    /// </summary>
    bool CanRedo { get; set; }
}
