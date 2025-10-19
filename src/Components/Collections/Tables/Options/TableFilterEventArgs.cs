using System.Collections.Generic;
using Soenneker.DataTables.Dtos.ServerSideRequest;

namespace Soenneker.Quark;

/// <summary>
/// Event arguments for Table filter change events
/// </summary>
public sealed class TableFilterEventArgs
{
    /// <summary>
    /// Gets or sets the current search term
    /// </summary>
    public string? SearchTerm { get; set; }

    /// <summary>
    /// Gets or sets the current page number
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Gets or sets the current page size
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the current list of orders
    /// </summary>
    public List<DataTableOrderRequest>? Orders { get; set; }
} 
