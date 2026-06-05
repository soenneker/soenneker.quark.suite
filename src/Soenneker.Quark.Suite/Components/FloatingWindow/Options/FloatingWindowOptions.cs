using System.Text.Json.Serialization;

namespace Soenneker.Quark;

/// <summary>
/// Options for configuring a Quark floating window.
/// </summary>
public sealed class FloatingWindowOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether draggable.
    /// </summary>
    [JsonPropertyName("draggable")]
    public bool Draggable { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether resizable.
    /// </summary>
    [JsonPropertyName("resizable")]
    public bool Resizable { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether show close button.
    /// </summary>
    [JsonPropertyName("showCloseButton")]
    public bool ShowCloseButton { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether show title bar.
    /// </summary>
    [JsonPropertyName("showTitleBar")]
    public bool ShowTitleBar { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether enabled.
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Gets or sets width.
    /// </summary>
    [JsonPropertyName("width")]
    public int? Width { get; set; } = 400;

    /// <summary>
    /// Gets or sets height.
    /// </summary>
    [JsonPropertyName("height")]
    public int? Height { get; set; } = 300;

    /// <summary>
    /// Gets or sets initial x.
    /// </summary>
    [JsonPropertyName("initialX")]
    public int? InitialX { get; set; } = 100;

    /// <summary>
    /// Gets or sets initial y.
    /// </summary>
    [JsonPropertyName("initialY")]
    public int? InitialY { get; set; } = 100;

    /// <summary>
    /// Gets or sets min width.
    /// </summary>
    [JsonPropertyName("minWidth")]
    public int MinWidth { get; set; } = 200;

    /// <summary>
    /// Gets or sets min height.
    /// </summary>
    [JsonPropertyName("minHeight")]
    public int MinHeight { get; set; } = 150;

    /// <summary>
    /// Gets or sets max width.
    /// </summary>
    [JsonPropertyName("maxWidth")]
    public int? MaxWidth { get; set; }

    /// <summary>
    /// Gets or sets max height.
    /// </summary>
    [JsonPropertyName("maxHeight")]
    public int? MaxHeight { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether constrain to viewport.
    /// </summary>
    [JsonPropertyName("constrainToViewport")]
    public bool ConstrainToViewport { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether center on show.
    /// </summary>
    [JsonPropertyName("centerOnShow")]
    public bool CenterOnShow { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether focus on show.
    /// </summary>
    [JsonPropertyName("focusOnShow")]
    public bool FocusOnShow { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether use cdn.
    /// </summary>
    [JsonPropertyName("useCdn")]
    public bool UseCdn { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether auto size to content.
    /// </summary>
    [JsonPropertyName("autoSizeToContent")]
    public bool AutoSizeToContent { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether dynamic auto size to content.
    /// </summary>
    [JsonPropertyName("dynamicAutoSizeToContent")]
    public bool DynamicAutoSizeToContent { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether recenter on resize.
    /// </summary>
    [JsonPropertyName("recenterOnResize")]
    public bool RecenterOnResize { get; set; }

    /// <summary>
    /// Gets or sets z index.
    /// </summary>
    [JsonPropertyName("zIndex")]
    public int ZIndex { get; set; } = 1000;
}
