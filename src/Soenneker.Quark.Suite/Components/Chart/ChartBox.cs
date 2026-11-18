namespace Soenneker.Quark;

/// <summary>A five-number summary in ascending order.</summary>
public sealed record ChartBox(string Label, double Minimum, double FirstQuartile, double Median, double ThirdQuartile, double Maximum);
