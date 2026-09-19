using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Soenneker.Quark;

/// <summary>Wraps arbitrary content in a file drop target, forwarding files through a native Blazor file input.</summary>
public interface IFileDrop : IElement
{
    /// <summary>The built-in or externally owned input ID. Use this with a label's for attribute to offer a file picker. Generated when omitted.</summary>
    string InputId { get; set; }
    /// <summary>Renders the built-in hidden input. Set false to target an existing InputFile by InputId; its owner handles selection, disabled state, and stream lifetime.</summary>
    bool RenderInput { get; set; }
    /// <summary>File picker accept hint. Validate dropped file types and sizes in the callback.</summary>
    string? Accept { get; set; }
    /// <summary>Whether the file picker allows multiple selection. Validate drop counts in the callback.</summary>
    bool Multiple { get; set; }
    /// <summary>Disables drops and the built-in picker while leaving child content interactive. External inputs must be disabled by their owner.</summary>
    bool Disabled { get; set; }
    /// <summary>Shows a subtle translucent blur and shadow over the content during an enabled file drag. Defaults to false. The overlay does not intercept pointer events.</summary>
    bool ShowDragOverlay { get; set; }
    /// <summary>Receives selected or dropped files from the built-in input. External inputs use their own OnChange handler. Apply application limits before reading file streams.</summary>
    EventCallback<InputFileChangeEventArgs> OnFilesDropped { get; set; }
}
