using System.Collections.Generic;
namespace Soenneker.Quark;
/// <summary>A page of stable value/label choices and an optional continuation token.</summary>
public sealed record SelectSearchResult(IReadOnlyList<SelectOption> Items, string? ContinuationToken = null);
