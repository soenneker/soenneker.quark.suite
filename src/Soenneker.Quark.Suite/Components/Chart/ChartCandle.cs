namespace Soenneker.Quark;

/// <summary>An OHLC observation in display order.</summary>
public sealed record ChartCandle(string Label, double Open, double High, double Low, double Close);
