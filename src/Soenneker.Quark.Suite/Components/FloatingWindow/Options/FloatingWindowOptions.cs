using System.Text.Json.Serialization;

namespace Soenneker.Quark;

/// <summary>
/// Options for configuring a Quark floating window.
/// </summary>
public sealed class FloatingWindowOptions
{
    [JsonPropertyName("draggable")]
    public bool Draggable { get; set; } = true;

    [JsonPropertyName("resizable")]
    public bool Resizable { get; set; } = true;

    [JsonPropertyName("showCloseButton")]
    public bool ShowCloseButton { get; set; } = true;

    [JsonPropertyName("showTitleBar")]
    public bool ShowTitleBar { get; set; } = true;

    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; } = true;

    [JsonPropertyName("width")]
    public int? Width { get; set; } = 400;

    [JsonPropertyName("height")]
    public int? Height { get; set; } = 300;

    [JsonPropertyName("initialX")]
    public int? InitialX { get; set; } = 100;

    [JsonPropertyName("initialY")]
    public int? InitialY { get; set; } = 100;

    [JsonPropertyName("minWidth")]
    public int MinWidth { get; set; } = 200;

    [JsonPropertyName("minHeight")]
    public int MinHeight { get; set; } = 150;

    [JsonPropertyName("maxWidth")]
    public int? MaxWidth { get; set; }

    [JsonPropertyName("maxHeight")]
    public int? MaxHeight { get; set; }

    [JsonPropertyName("constrainToViewport")]
    public bool ConstrainToViewport { get; set; } = true;

    [JsonPropertyName("centerOnShow")]
    public bool CenterOnShow { get; set; } = true;

    [JsonPropertyName("focusOnShow")]
    public bool FocusOnShow { get; set; } = true;

    [JsonPropertyName("useCdn")]
    public bool UseCdn { get; set; } = true;

    [JsonPropertyName("autoSizeToContent")]
    public bool AutoSizeToContent { get; set; } = true;

    [JsonPropertyName("dynamicAutoSizeToContent")]
    public bool DynamicAutoSizeToContent { get; set; }

    [JsonPropertyName("recenterOnResize")]
    public bool RecenterOnResize { get; set; }

    [JsonPropertyName("zIndex")]
    public int ZIndex { get; set; } = 1000;
}
