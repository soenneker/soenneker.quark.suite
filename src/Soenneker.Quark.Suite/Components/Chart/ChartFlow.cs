namespace Soenneker.Quark;

/// <summary>A positive weighted link in a directed acyclic flow diagram.</summary>
public sealed record ChartFlow(string Source, string Target, double Value);
