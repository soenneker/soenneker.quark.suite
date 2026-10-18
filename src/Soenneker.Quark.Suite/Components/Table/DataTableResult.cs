using System.Collections.Generic;
namespace Soenneker.Quark;
/// <summary>A server page and the exact total number of matching rows.</summary>
public sealed record DataTableResult<TItem>(IReadOnlyList<TItem> Items, int TotalCount);
