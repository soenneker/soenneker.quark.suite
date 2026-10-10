using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>A sortable comparison of entries across independently scaled numeric metrics.</summary>
public interface IComparisonChart
{
    /// <summary>Gets or sets the entries. Sorting never changes the supplied collection.</summary>
    IReadOnlyList<ComparisonChartRow> Rows { get; set; }

    /// <summary>Gets or sets the metrics defining the order and scales of row values.</summary>
    IReadOnlyList<ComparisonChartMetric> Metrics { get; set; }

    /// <summary>Gets or sets whether the first metric appears to the left of the names and grows toward the left. Defaults to true.</summary>
    bool MirrorFirstMetric { get; set; }

    /// <summary>Gets or sets whether metric controls can sort rows. The initial order matches the supplied entries.</summary>
    bool Sortable { get; set; }

    /// <summary>Gets or sets whether the legend is displayed. Legend buttons emphasize highlighted, reference, or other entries.</summary>
    bool ShowLegend { get; set; }

    /// <summary>Gets or sets the heading above entry names. Defaults to Entry.</summary>
    string EntryLabel { get; set; }

    /// <summary>Gets or sets whether rows reveal once as they enter the viewport and marks animate when values change. Sorting or recreating the chart restarts the reveal. Reduced-motion preferences take precedence.</summary>
    bool Animate { get; set; }

    /// <summary>Gets or sets the callback raised when an entry is selected by mouse, touch, or keyboard.</summary>
    EventCallback<ComparisonChartRow> OnSelect { get; set; }

    /// <summary>Gets or sets the accessible chart name.</summary>
    string? AriaLabel { get; set; }

    /// <summary>Gets or sets the message shown when there are no rows or metrics.</summary>
    string EmptyText { get; set; }
}
