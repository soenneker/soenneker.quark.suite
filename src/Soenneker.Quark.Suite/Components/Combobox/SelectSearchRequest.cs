namespace Soenneker.Quark;
/// <summary>A debounced query with an opaque continuation token and requested page size.</summary>
public sealed record SelectSearchRequest(string Search, string? ContinuationToken, int PageSize);
