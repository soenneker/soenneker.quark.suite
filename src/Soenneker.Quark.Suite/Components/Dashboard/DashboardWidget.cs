namespace Soenneker.Quark;

/// <summary>Position and size of a widget in grid cells, using one-based coordinates.</summary>
public sealed record DashboardWidget(string Key, int Column, int Row, int Width = 1, int Height = 1);
