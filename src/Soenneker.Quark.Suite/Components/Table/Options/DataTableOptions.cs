using System.Linq;
using System.Text.Json.Serialization;

namespace Soenneker.Quark;

/// <summary>
/// Configuration options for DataTable
/// </summary>
public sealed class DataTableOptions
{
    /// <summary>
    /// Gets or sets the default page size
    /// </summary>
    [JsonPropertyName("defaultPageSize")]
    public int DefaultPageSize { get; set; } = 10;

    /// <summary>
    /// Gets or sets whether DataTable should render a built-in page size selector in its bottom bar.
    /// </summary>
    [JsonPropertyName("showPageSizeSelector")]
    public bool ShowPageSizeSelector { get; set; }

    /// <summary>
    /// Gets or sets the selectable page sizes.
    /// </summary>
    [JsonPropertyName("pageSizeOptions")]
    public int[] PageSizeOptions { get; set; } = [10, 25, 50, 100];

    /// <summary>
    /// Gets or sets the singular item label used by the page size selector.
    /// </summary>
    [JsonPropertyName("pageSizeItemSingularText")]
    public string PageSizeItemSingularText { get; set; } = "item";

    /// <summary>
    /// Gets or sets the plural item label used by the page size selector.
    /// </summary>
    [JsonPropertyName("pageSizeItemPluralText")]
    public string PageSizeItemPluralText { get; set; } = "items";

    /// <summary>
    /// Gets or sets the record label used by the built-in page info text.
    /// </summary>
    [JsonPropertyName("pageInfoRecordText")]
    public string PageInfoRecordText { get; set; } = "records";

    /// <summary>
    /// Gets or sets optional text rendered before the page size selector.
    /// </summary>
    [JsonPropertyName("pageSizeSelectorLabel")]
    public string? PageSizeSelectorLabel { get; set; }

    /// <summary>
    /// Gets or sets optional text rendered after the page size selector.
    /// </summary>
    [JsonPropertyName("pageSizeSelectorSuffix")]
    public string? PageSizeSelectorSuffix { get; set; }

    /// <summary>
    /// Gets or sets the search debounce delay in milliseconds
    /// </summary>
    [JsonPropertyName("searchDebounceMs")]
    public int SearchDebounceMs { get; set; } = 300;

    /// <summary>
    /// Gets or sets whether to enable debug logging
    /// </summary>
    [JsonPropertyName("debug")]
    public bool Debug { get; set; }

    /// <summary>
    /// Creates a clone of the current options
    /// </summary>
    /// <returns>A new instance with the same values</returns>
    public DataTableOptions Clone()
    {
        return new DataTableOptions
        {
            DefaultPageSize = DefaultPageSize,
            ShowPageSizeSelector = ShowPageSizeSelector,
            PageSizeOptions = PageSizeOptions.ToArray(),
            PageSizeItemSingularText = PageSizeItemSingularText,
            PageSizeItemPluralText = PageSizeItemPluralText,
            PageInfoRecordText = PageInfoRecordText,
            PageSizeSelectorLabel = PageSizeSelectorLabel,
            PageSizeSelectorSuffix = PageSizeSelectorSuffix,
            SearchDebounceMs = SearchDebounceMs,
            Debug = Debug
        };
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object
    /// </summary>
    /// <param name="obj">The object to compare with the current object</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not DataTableOptions other)
            return false;

        return DefaultPageSize == other.DefaultPageSize &&
               ShowPageSizeSelector == other.ShowPageSizeSelector &&
               PageSizeOptions.SequenceEqual(other.PageSizeOptions) &&
               PageSizeItemSingularText == other.PageSizeItemSingularText &&
               PageSizeItemPluralText == other.PageSizeItemPluralText &&
               PageInfoRecordText == other.PageInfoRecordText &&
               PageSizeSelectorLabel == other.PageSizeSelectorLabel &&
               PageSizeSelectorSuffix == other.PageSizeSelectorSuffix &&
               SearchDebounceMs == other.SearchDebounceMs &&
               Debug == other.Debug;
    }

    /// <summary>
    /// Serves as the default hash function
    /// </summary>
    /// <returns>A hash code for the current object</returns>
    public override int GetHashCode()
    {
        unchecked
        {
            var hash = 17;
            hash = hash * 23 + DefaultPageSize.GetHashCode();
            hash = hash * 23 + ShowPageSizeSelector.GetHashCode();
            for (var i = 0; i < PageSizeOptions.Length; i++)
                hash = hash * 23 + PageSizeOptions[i].GetHashCode();
            hash = hash * 23 + PageSizeItemSingularText.GetHashCode();
            hash = hash * 23 + PageSizeItemPluralText.GetHashCode();
            hash = hash * 23 + PageInfoRecordText.GetHashCode();
            hash = hash * 23 + (PageSizeSelectorLabel?.GetHashCode() ?? 0);
            hash = hash * 23 + (PageSizeSelectorSuffix?.GetHashCode() ?? 0);
            hash = hash * 23 + SearchDebounceMs.GetHashCode();
            hash = hash * 23 + Debug.GetHashCode();
            return hash;
        }
    }
} 
