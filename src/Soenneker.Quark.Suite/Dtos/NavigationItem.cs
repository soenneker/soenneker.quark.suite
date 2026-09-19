namespace Soenneker.Quark;

/// <summary>
/// Simple navigation item model for reusable navigation components.
/// </summary>
public sealed record NavigationItem(string Title, string? Href = null, bool IsNew = false, bool ExactMatch = false);
