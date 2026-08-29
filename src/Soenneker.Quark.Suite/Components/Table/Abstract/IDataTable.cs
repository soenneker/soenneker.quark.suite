using System.Collections.Generic;
using System.Threading.Tasks;
using Soenneker.DataTables.Dtos.ServerSideRequest;

namespace Soenneker.Quark;

/// <summary>
/// Interface for the DataTable component
/// </summary>
public interface IDataTable : ICancellableElement
{
    /// <summary>
    /// Gets the current page number
    /// </summary>
    int CurrentPage { get; }

    /// <summary>
    /// Gets the current page size
    /// </summary>
    int PageSize { get; }

    /// <summary>
    /// Gets the total number of pages
    /// </summary>
    int TotalPages { get; }

    /// <summary>
    /// Gets the total number of records
    /// </summary>
    int TotalRecordsCount { get; }

    /// <summary>
    /// Gets whether the data table has loaded data at least once
    /// </summary>
    bool HasLoadedOnce { get; }

    /// <summary>
    /// Gets the current search term
    /// </summary>
    string? SearchTerm { get; }

    /// <summary>
    /// Gets the current sort by field
    /// </summary>
    string? SortBy { get; }

    /// <summary>
    /// Gets the current sort direction
    /// </summary>
    string? SortDirection { get; }

    /// <summary>
    /// Gets the data table options
    /// </summary>
    DataTableOptions Options { get; }

    /// <summary>
    /// Handles the column sort callback.
    /// </summary>
    /// <param name="columnIndex">Column Index for the handle column sort operation.</param>
    /// <returns>A task that completes when the handle column sort operation is complete.</returns>
    ValueTask HandleColumnSort(int columnIndex);

    /// <summary>
    /// Registers a column header component and returns its index
    /// </summary>
    /// <param name="columnHeader">The column header component to register</param>
    /// <returns>The column index</returns>
    int RegisterColumn(Th columnHeader);

    /// <summary>
    /// Handles search from child components
    /// </summary>
    /// <param name="searchTerm">Search Term for the handle search operation.</param>
    /// <returns>A task that completes when the handle search operation is complete.</returns>
    ValueTask HandleSearch(string searchTerm);

    /// <summary>
    /// Handles navigation to a specific page
    /// </summary>
    /// <param name="page">Browser page to inspect or control.</param>
    /// <returns>A task that completes when the handle go to page operation is complete.</returns>
    ValueTask HandleGoToPage(int page);

    /// <summary>
    /// Handles changing the number of records requested per page.
    /// </summary>
    /// <param name="pageSize">Maximum number of items to request per page.</param>
    /// <returns>A task that completes when the handle page size changed operation is complete.</returns>
    ValueTask HandlePageSizeChanged(int pageSize);

    /// <summary>
    /// Navigates to a specific page
    /// </summary>
    /// <param name="page">Browser page to inspect or control.</param>
    /// <returns>A task that completes when the go to page operation is complete.</returns>
    ValueTask GoToPage(int page);

    /// <summary>
    /// Clears all current sorting and resets to first page
    /// </summary>
    /// <returns>A task that completes when the Data Table has been cleared.</returns>
    ValueTask ClearSorting();

    /// <summary>
    /// Resets the table to its initial state (clears sorting and goes to first page)
    /// </summary>
    /// <returns>A task that completes when the reset operation is complete.</returns>
    ValueTask Reset();

    /// <summary>
    /// Gets the current list of orders
    /// </summary>
    /// <returns>A copy of the current orders</returns>
    List<DataTableOrderRequest> GetCurrentOrders();

    /// <summary>
    /// Sets the orders programmatically and triggers a reload
    /// </summary>
    /// <param name="orders">orders to process.</param>
    /// <returns>A task that completes when the orders has been stored.</returns>
    ValueTask SetOrders(List<DataTableOrderRequest> orders);

    /// <summary>
    /// Gets the current sort direction for a column index
    /// </summary>
    /// <param name="columnIndex">The column index</param>
    /// <returns>The sort direction ("asc", "desc", or null if not sorted)</returns>
    string? GetSortDirection(int columnIndex);

    /// <summary>
    /// Gets the CSS class for a column index based on its sort state
    /// </summary>
    /// <param name="columnIndex">The column index</param>
    /// <returns>The CSS class for the sort state</returns>
    string GetSortClassByIndex(int columnIndex);

    /// <summary>
    /// Gets the sort indicator for a column index
    /// </summary>
    /// <param name="columnIndex">The column index</param>
    /// <returns>The sort indicator (↑, ↓, or ↕)</returns>
    string GetSortIndicatorByIndex(int columnIndex);

    /// <summary>
    /// Cancels any ongoing operations and resets the loading state
    /// </summary>
    /// <returns>A task that completes when the cancel operations operation is complete.</returns>
    ValueTask CancelOperations();

    /// <summary>
    /// Updates the continuation token paging with response data
    /// </summary>
    /// <param name="recordCount">The number of records in the current response</param>
    /// <param name="continuationToken">The continuation token from the response</param>
    /// <param name="tokenUsedForCurrentPage">The continuation token that was used to reach the current page</param>
    void UpdateContinuationTokenPaging(int recordCount, string? continuationToken, string? tokenUsedForCurrentPage = null);
} 
