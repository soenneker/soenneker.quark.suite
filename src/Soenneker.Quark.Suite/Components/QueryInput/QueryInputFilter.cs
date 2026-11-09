namespace Soenneker.Quark;

/// <summary>A parsed field/value pair, optionally excluded using a leading minus.</summary>
public sealed record QueryInputFilter(string Field, string Value, bool Excluded = false);
