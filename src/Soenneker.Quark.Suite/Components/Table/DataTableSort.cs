namespace Soenneker.Quark;
/// <summary>A stable column sort; earlier entries take precedence.</summary>
public sealed record DataTableSort(string Key, bool Descending = false);
