using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

///<inheritdoc cref="IThemeProvider"/>
public sealed class ThemeProvider : IThemeProvider
{
    public string? CurrentTheme { get; set; } = "Default";

    public Dictionary<string, Theme>? Themes { get; set; }

    public Dictionary<string, Func<Theme, ComponentOptions?>> ComponentOptions { get; set; } = new()
    {
        ["Alert"] = theme => theme.Alerts,
        ["Accordion"] = theme => theme.Accordions,
        ["AccordionItem"] = theme => theme.AccordionItems,
        ["AccordionHeader"] = theme => theme.AccordionHeaders,
        ["AccordionBody"] = theme => theme.AccordionBodys,
        ["Anchor"] = theme => theme.Anchors,
        ["Badge"] = theme => theme.Badges,
        ["Bar"] = theme => theme.Bars,
        ["Breadcrumb"] = theme => theme.Breadcrumbs,
        ["Button"] = theme => theme.Buttons,
        ["ButtonGroup"] = theme => theme.ButtonGroups,
        ["Card"] = theme => theme.Cards,
        ["Check"] = theme => theme.Checks,
        ["Code"] = theme => theme.Codes,
        ["Collapse"] = theme => theme.Collapses,
        ["Column"] = theme => theme.Columns,
        ["Container"] = theme => theme.Containers,
        ["DateEdit"] = theme => theme.DateEdits,
        ["Datepicker"] = theme => theme.Datepickers,
        ["Div"] = theme => theme.Divs,
        ["Field"] = theme => theme.Fields,
        ["Heading"] = theme => theme.Headings,
        ["Icon"] = theme => theme.Icons,
        ["Image"] = theme => theme.Images,
        ["Label"] = theme => theme.Labels,
        ["Layout"] = theme => theme.Layouts,
        ["ListItem"] = theme => theme.UnorderedListItems,
        ["MemoEdit"] = theme => theme.MemoEdits,
        ["Modal"] = theme => theme.Modals,
        ["Nav"] = theme => theme.Navs,
        ["NumericEdit"] = theme => theme.NumericEdits,
        ["Offcanvas"] = theme => theme.Offcanvases,
        ["OrderedList"] = theme => theme.OrderedLists,
        ["Pagination"] = theme => theme.Paginations,
        ["Paragraph"] = theme => theme.Paragraphs,
        ["Progress"] = theme => theme.Progresses,
        ["Radio"] = theme => theme.Radios,
        ["Row"] = theme => theme.Rows,
        ["Section"] = theme => theme.Sections,
        ["Slider"] = theme => theme.Sliders,
        ["Snackbar"] = theme => theme.Snackbars,
        ["Span"] = theme => theme.Spans,
        ["Step"] = theme => theme.Steps,
        ["Strong"] = theme => theme.Strongs,
        ["Switch"] = theme => theme.Switches,
        ["Table"] = theme => theme.Tables,
        ["Tab"] = theme => theme.Tabs,
        ["TextEdit"] = theme => theme.TextEdits,
        ["Text"] = theme => theme.Texts,
        ["UnorderedList"] = theme => theme.UnorderedLists,
        
        // Dropdown components
        ["Dropdown"] = theme => theme.Dropdowns,
        ["DropdownToggle"] = theme => theme.DropdownToggles,
        ["DropdownMenu"] = theme => theme.DropdownMenus,
        ["DropdownItem"] = theme => theme.DropdownItems,
        ["DropdownDivider"] = theme => theme.DropdownDividers,
        
        // Validation components
        ["ValidationSuccess"] = theme => theme.ValidationSuccess,
        ["Validations"] = theme => theme.Validations,
        ["ValidationErrors"] = theme => theme.ValidationErrors,
        ["ValidationError"] = theme => theme.ValidationError,
        
        // Table components
        ["QuarkTr"] = theme => theme.QuarkTrs,
        ["QuarkThead"] = theme => theme.QuarkTheads,
        ["QuarkTd"] = theme => theme.QuarkTds,
        ["QuarkTbody"] = theme => theme.QuarkTbodys,
        ["QuarkTableSearch"] = theme => theme.QuarkTableSearches,
        ["QuarkTablePagination"] = theme => theme.QuarkTablePaginations,
        ["QuarkTablePageSizeSelector"] = theme => theme.QuarkTablePageSizeSelectors,
        ["QuarkTableNoData"] = theme => theme.QuarkTableNoDatas,
        ["QuarkTableLoader"] = theme => theme.QuarkTableLoaders,
        ["QuarkTableInfo"] = theme => theme.QuarkTableInfos,
        ["QuarkTableElement"] = theme => theme.QuarkTableElements,
        
        // Snackbar components
        ["SnackbarHeader"] = theme => theme.SnackbarHeaders,
        ["SnackbarFooter"] = theme => theme.SnackbarFooters,
        ["SnackbarBody"] = theme => theme.SnackbarBodys,
        ["SnackbarAction"] = theme => theme.SnackbarActions,
        ["SnackbarStack"] = theme => theme.SnackbarStacks,
        
        // Navigation components
        ["Tabs"] = theme => theme.Tabs,
        ["Steps"] = theme => theme.Steps,
        ["StepPanel"] = theme => theme.StepPanels,
        ["StepContent"] = theme => theme.StepContents,
        ["PaginationLink"] = theme => theme.PaginationLinks,
        ["PaginationItem"] = theme => theme.PaginationItems,
        ["BreadcrumbItem"] = theme => theme.BreadcrumbItems,
        
        // Modal components
        ["OffcanvasHeader"] = theme => theme.OffcanvasHeaders,
        ["OffcanvasBody"] = theme => theme.OffcanvasBodys,
        ["ModalTitle"] = theme => theme.ModalTitles,
        ["ModalHeader"] = theme => theme.ModalHeaders,
        ["ModalFooter"] = theme => theme.ModalFooters,
        ["ModalContent"] = theme => theme.ModalContents,
        ["ModalCloseButton"] = theme => theme.ModalCloseButtons,
        ["ModalBody"] = theme => theme.ModalBodys,
        
        // Form components
        ["FieldLabel"] = theme => theme.FieldLabels,
        ["FieldHelp"] = theme => theme.FieldHelps,
        ["FieldBody"] = theme => theme.FieldBodys,
        
        // Content components
        ["Small"] = theme => theme.Smalls,
        ["PreCode"] = theme => theme.PreCodes,
        
        // Bar components
        ["BarToggler"] = theme => theme.BarTogglers,
        ["BarStart"] = theme => theme.BarStarts,
        ["BarMenu"] = theme => theme.BarMenus,
        ["BarLink"] = theme => theme.BarLinks,
        ["BarLabel"] = theme => theme.BarLabels,
        ["BarItem"] = theme => theme.BarItems,
        ["BarIcon"] = theme => theme.BarIcons,
        ["BarEnd"] = theme => theme.BarEnds,
        ["BarDropdownToggle"] = theme => theme.BarDropdownToggles,
        ["BarDropdown"] = theme => theme.BarDropdowns,
        ["BarDropdownMenu"] = theme => theme.BarDropdownMenus,
        ["BarDropdownItem"] = theme => theme.BarDropdownItems,
        ["BarDropdownDivider"] = theme => theme.BarDropdownDividers,
        ["BarBrand"] = theme => theme.BarBrands
    };

    public void AddTheme(Theme theme)
    {
        Themes ??= new Dictionary<string, Theme>();
        Themes[theme.Name] = theme;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string? GenerateRootCss()
    {
        var currentTheme = GetCurrentTheme();

        if (currentTheme?.BootstrapCssVariables == null)
            return null;

        return BootstrapCssGenerator.GenerateRootCss(currentTheme.BootstrapCssVariables);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Theme? GetCurrentTheme()
    {
        if (CurrentTheme.IsNullOrEmpty() || Themes == null)
            return null;

        return Themes.GetValueOrDefault(CurrentTheme);
    }
}
