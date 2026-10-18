namespace Soenneker.Quark;
/// <summary>A server table query. PageIndex is zero-based; the provider applies all filters, search and sorting in State.</summary>
public sealed record DataTableRequest(QuarkViewState State, int PageIndex, int PageSize);
