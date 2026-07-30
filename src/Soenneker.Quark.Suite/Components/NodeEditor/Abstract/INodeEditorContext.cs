using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Exposes viewport commands to composed node editor controls.
/// </summary>
public interface INodeEditorContext
{
    /// <summary>
    /// Increases the viewport scale by one control step.
    /// </summary>
    /// <returns>A value task representing the asynchronous operation.</returns>
    ValueTask ZoomIn();

    /// <summary>
    /// Decreases the viewport scale by one control step.
    /// </summary>
    /// <returns>A value task representing the asynchronous operation.</returns>
    ValueTask ZoomOut();

    /// <summary>
    /// Fits all rendered nodes into the available viewport.
    /// </summary>
    /// <returns>A value task representing the asynchronous operation.</returns>
    ValueTask FitView();

    /// <summary>
    /// Restores the configured initial viewport.
    /// </summary>
    /// <returns>A value task representing the asynchronous operation.</returns>
    ValueTask ResetView();

    /// <summary>
    /// Gets whether an undo operation is available.
    /// </summary>
    bool CanUndo { get; }

    /// <summary>
    /// Gets whether a redo operation is available.
    /// </summary>
    bool CanRedo { get; }

    /// <summary>
    /// Requests an undo operation from the controlled host.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Undo();

    /// <summary>
    /// Requests a redo operation from the controlled host.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Redo();
}
