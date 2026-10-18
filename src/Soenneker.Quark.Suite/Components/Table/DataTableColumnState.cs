namespace Soenneker.Quark;
/// <summary>Serializable column preferences. Width is clamped to 64–1200 pixels by the grid.</summary>
public sealed record DataTableColumnState(string Key, bool Visible = true, int Width = 180, DataTableColumnPin Pin = DataTableColumnPin.None);
