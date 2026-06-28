using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Represents the data table theme options.
/// </summary>
public sealed class DataTableThemeOptions : ComponentOptions
{
    private const string _anchorDefaultSelector = "[data-slot='anchor']";
    private const string _bottomBarDefaultSelector = ".q-datatable-bottom-bar";
    private const string _buttonDefaultSelector = "[data-slot='button']";
    private const string _dataTableInfoDefaultSelector = ".q-datatable-info";
    private const string _dataTableLoaderDefaultSelector = "[data-slot='datatable-loader']";
    private const string _dataTableNoDataDefaultSelector = "[data-slot='datatable-empty']";
    private const string _dataTablePageSizeSelectorDefaultSelector = ".q-datatable-page-size-selector";
    private const string _dataTablePaginationDefaultSelector = "[data-slot='datatable-pagination']";
    private const string _dataTableSearchDefaultSelector = ".q-datatable-search";
    private const string _divDefaultSelector = "[data-slot='div']";
    private const string _iconDefaultSelector = "[data-slot='icon']";
    private const string _inputDefaultSelector = "[data-slot='input']";
    private const string _leftDefaultSelector = ".q-datatable-left";
    private const string _paginationItemDefaultSelector = "[data-slot='pagination-item']";
    private const string _paginationLinkDefaultSelector = "[data-slot='pagination-link']";
    private const string _rightDefaultSelector = ".q-datatable-right";
    private const string _selectDefaultSelector = "[data-slot='select']";
    private const string _smallDefaultSelector = "[data-slot='small']";
    private const string _spanDefaultSelector = "[data-slot='span']";
    private const string _tbodyDefaultSelector = "[data-slot='table-body']";
    private const string _tdDefaultSelector = "[data-slot='table-cell']";
    private const string _theadDefaultSelector = "[data-slot='table-header']";
    private const string _thDefaultSelector = "[data-slot='table-head']";
    private const string _topBarDefaultSelector = ".q-datatable-top-bar";
    private const string _trDefaultSelector = "[data-slot='table-row']";

    public DataTableThemeOptions()
    {
        Selector = ".q-datatable";
    }

    /// <summary>
    /// Gets or sets anchor styling scoped to anchors inside the data table body.
    /// </summary>
    public AnchorOptions? Anchors { get; set; }

    /// <summary>
    /// Gets or sets styling for direct div children inside data table body anchors.
    /// </summary>
    public DivOptions? AnchorDivs { get; set; }

    /// <summary>
    /// Gets or sets styling for the first direct span child inside data table body anchors.
    /// </summary>
    public SpanOptions? AnchorLeadingSpans { get; set; }

    /// <summary>
    /// Gets or sets styling for small text inside direct div children inside data table body anchors.
    /// </summary>
    public SmallOptions? AnchorSmalls { get; set; }

    /// <summary>
    /// Gets or sets styling for spans inside direct div children inside data table body anchors.
    /// </summary>
    public SpanOptions? AnchorSpans { get; set; }

    /// <summary>
    /// Gets or sets button styling scoped to buttons inside the data table.
    /// </summary>
    public ButtonOptions? Buttons { get; set; }

    /// <summary>
    /// Gets or sets bottom bar styling scoped to the data table.
    /// </summary>
    public DataTableBottomBarOptions? BottomBars { get; set; }

    /// <summary>
    /// Gets or sets div styling scoped to divs inside the data table body.
    /// </summary>
    public DivOptions? Divs { get; set; }

    /// <summary>
    /// Gets or sets data table info styling scoped to the data table.
    /// </summary>
    public DataTableInfoOptions? Infos { get; set; }

    /// <summary>
    /// Gets or sets icon styling scoped to icons inside the data table body.
    /// </summary>
    public IconOptions? Icons { get; set; }

    /// <summary>
    /// Gets or sets input styling scoped to data table inputs.
    /// </summary>
    public InputOptions? Inputs { get; set; }

    /// <summary>
    /// Gets or sets left content styling scoped to the data table.
    /// </summary>
    public DataTableLeftOptions? Lefts { get; set; }

    /// <summary>
    /// Gets or sets loader styling scoped to the data table.
    /// </summary>
    public DataTableLoaderOptions? Loaders { get; set; }

    /// <summary>
    /// Gets or sets no-data styling scoped to the data table.
    /// </summary>
    public DataTableNoDataOptions? NoDatas { get; set; }

    /// <summary>
    /// Gets or sets page size selector styling scoped to the data table.
    /// </summary>
    public DataTablePageSizeSelectorOptions? PageSizeSelectors { get; set; }

    /// <summary>
    /// Gets or sets pagination styling scoped to the data table.
    /// </summary>
    public DataTablePaginationOptions? Paginations { get; set; }

    /// <summary>
    /// Gets or sets pagination item styling scoped to the data table pagination.
    /// </summary>
    public PaginationItemOptions? PaginationItems { get; set; }

    /// <summary>
    /// Gets or sets pagination link styling scoped to the data table pagination.
    /// </summary>
    public PaginationLinkOptions? PaginationLinks { get; set; }

    /// <summary>
    /// Gets or sets right content styling scoped to the data table.
    /// </summary>
    public DataTableRightOptions? Rights { get; set; }

    /// <summary>
    /// Gets or sets search styling scoped to the data table.
    /// </summary>
    public DataTableSearchOptions? Searches { get; set; }

    /// <summary>
    /// Gets or sets select styling scoped to data table selects.
    /// </summary>
    public SelectOptions? Selects { get; set; }

    /// <summary>
    /// Gets or sets small text styling scoped to small text inside the data table body.
    /// </summary>
    public SmallOptions? Smalls { get; set; }

    /// <summary>
    /// Gets or sets span styling scoped to spans inside the data table body.
    /// </summary>
    public SpanOptions? Spans { get; set; }

    /// <summary>
    /// Gets or sets table body styling scoped to the data table.
    /// </summary>
    public TbodyOptions? Tbodys { get; set; }

    /// <summary>
    /// Gets or sets table cell styling scoped to the data table body.
    /// </summary>
    public TdOptions? Tds { get; set; }

    /// <summary>
    /// Gets or sets table header group styling scoped to the data table.
    /// </summary>
    public TheadOptions? Theads { get; set; }

    /// <summary>
    /// Gets or sets table header cell styling scoped to the data table.
    /// </summary>
    public ThOptions? Ths { get; set; }

    /// <summary>
    /// Gets or sets top bar styling scoped to the data table.
    /// </summary>
    public DataTableTopBarOptions? TopBars { get; set; }

    /// <summary>
    /// Gets or sets table row styling scoped to the data table body.
    /// </summary>
    public TrOptions? Trs { get; set; }

    private protected override void CollectChildCssRules(List<ComponentCssRule> buffer, string baseSelector)
    {
        AddChildCssRules(buffer, Anchors, "tbody td > [data-slot='anchor']", _anchorDefaultSelector, baseSelector);
        AddChildCssRules(buffer, AnchorDivs, "tbody td > [data-slot='anchor'] > [data-slot='div']", _divDefaultSelector, baseSelector);
        AddChildCssRules(buffer, AnchorLeadingSpans, "tbody td > [data-slot='anchor'] > [data-slot='span']:first-child", _spanDefaultSelector, baseSelector);
        AddChildCssRules(buffer, AnchorSmalls, "tbody td > [data-slot='anchor'] > [data-slot='div'] > [data-slot='small']", _smallDefaultSelector, baseSelector);
        AddChildCssRules(buffer, AnchorSpans, "tbody td > [data-slot='anchor'] > [data-slot='div'] > [data-slot='span']", _spanDefaultSelector, baseSelector);
        AddChildCssRules(buffer, BottomBars, "[data-slot='datatable-bottom-bar']", _bottomBarDefaultSelector, baseSelector);
        AddChildCssRules(buffer, Buttons, "[data-slot='button']", _buttonDefaultSelector, baseSelector);
        AddChildCssRules(buffer, Divs, "tbody td [data-slot='div']", _divDefaultSelector, baseSelector);
        AddChildCssRules(buffer, Infos, "[data-slot='datatable-info']", _dataTableInfoDefaultSelector, baseSelector);
        AddChildCssRules(buffer, Icons, "tbody td [data-slot='icon']", _iconDefaultSelector, baseSelector);
        AddChildCssRules(buffer, Inputs, "[data-slot='datatable-search-input']", _inputDefaultSelector, baseSelector);
        AddChildCssRules(buffer, Lefts, "[data-slot='datatable-left']", _leftDefaultSelector, baseSelector);
        AddChildCssRules(buffer, Loaders, "[data-slot='datatable-loader']", _dataTableLoaderDefaultSelector, baseSelector);
        AddChildCssRules(buffer, NoDatas, "[data-slot='datatable-empty']", _dataTableNoDataDefaultSelector, baseSelector);
        AddChildCssRules(buffer, PageSizeSelectors, "[data-slot='datatable-page-size-selector']", _dataTablePageSizeSelectorDefaultSelector, baseSelector);
        AddChildCssRules(buffer, Paginations, "[data-slot='datatable-pagination']", _dataTablePaginationDefaultSelector, baseSelector);
        AddChildCssRules(buffer, PaginationItems, "[data-slot='datatable-pagination'] [data-slot='pagination-item']", _paginationItemDefaultSelector, baseSelector);
        AddChildCssRules(buffer, PaginationLinks, "[data-slot='datatable-pagination'] [data-slot='pagination-link']", _paginationLinkDefaultSelector, baseSelector);
        AddChildCssRules(buffer, Rights, "[data-slot='datatable-right']", _rightDefaultSelector, baseSelector);
        AddChildCssRules(buffer, Searches, "[data-slot='datatable-search']", _dataTableSearchDefaultSelector, baseSelector);
        AddChildCssRules(buffer, Selects, "[data-slot='datatable-page-size-select']", _selectDefaultSelector, baseSelector);
        AddChildCssRules(buffer, Smalls, "tbody td [data-slot='small']", _smallDefaultSelector, baseSelector);
        AddChildCssRules(buffer, Spans, "tbody td [data-slot='span']", _spanDefaultSelector, baseSelector);
        AddChildCssRules(buffer, Tbodys, "[data-slot='table-body']", _tbodyDefaultSelector, baseSelector);
        AddChildCssRules(buffer, Tds, "tbody [data-slot='table-cell']", _tdDefaultSelector, baseSelector);
        AddChildCssRules(buffer, Theads, "[data-slot='table-header']", _theadDefaultSelector, baseSelector);
        AddChildCssRules(buffer, Ths, "thead [data-slot='table-head']", _thDefaultSelector, baseSelector);
        AddChildCssRules(buffer, TopBars, "[data-slot='datatable-top-bar']", _topBarDefaultSelector, baseSelector);
        AddChildCssRules(buffer, Trs, "tbody [data-slot='table-row']", _trDefaultSelector, baseSelector);
    }
}
