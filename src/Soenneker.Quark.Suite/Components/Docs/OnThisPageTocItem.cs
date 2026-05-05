namespace Soenneker.Quark;

/// <summary>
/// Represents a heading discovered in a docs article for the On This Page rail.
/// </summary>
public sealed record OnThisPageTocItem(string Id, string Title, int Level);
