namespace Soenneker.Quark;

/// <summary>A numeric observation with an optional bubble area weight.</summary>
public sealed record ChartPoint(double X, double Y, double Size = 1);
