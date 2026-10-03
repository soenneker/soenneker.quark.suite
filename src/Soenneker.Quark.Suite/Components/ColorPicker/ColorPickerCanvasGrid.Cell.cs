namespace Soenneker.Quark;

internal sealed partial class ColorPickerCanvasGrid
{
    private readonly record struct Cell(double Saturation, double Lightness, string Label, string Style);
}
