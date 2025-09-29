using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Soenneker.Quark;

///<inheritdoc cref="IThemeProvider"/>
public sealed class ThemeProvider : IThemeProvider
{
    public string? CurrentTheme { get; set; }

    public Dictionary<string, Theme>? Themes { get; set; }

    public Dictionary<string, Func<Theme, ComponentOptions?>> ComponentOptions { get; set; } = new()
    {
        ["Alert"] = theme => theme.Alerts,
        ["Anchor"] = theme => theme.Anchors,
        ["Badge"] = theme => theme.Badges,
        ["Bar"] = theme => theme.Bars,
        ["Breadcrumb"] = theme => theme.Breadcrumbs,
        ["Button"] = theme => theme.Buttons,
        ["Card"] = theme => theme.Cards,
        ["Check"] = theme => theme.Checks,
        ["Code"] = theme => theme.Codes,
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
        ["ValidationsContainer"] = theme => theme.ValidationsContainers,
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
        
        // Navigation components
        ["TabsContainer"] = theme => theme.TabsContainers,
        ["StepsContainer"] = theme => theme.StepsContainers,
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GenerateRootCss()
    {
        var currentTheme = GetCurrentTheme();
        if (currentTheme == null)
            return string.Empty;

        var objects = new object?[]
        {
            // Core Bootstrap variables from current theme
            currentTheme.BootstrapColors,
            currentTheme.BootstrapTypography,
            currentTheme.BootstrapSpacing,
            currentTheme.BootstrapBorders,
            currentTheme.BootstrapShadows,
            currentTheme.BootstrapButtons,

            // Component Bootstrap variables from current theme
            currentTheme.BootstrapCards,
            currentTheme.BootstrapAlerts,
            currentTheme.BootstrapBadges,
            currentTheme.BootstrapModals,
            currentTheme.BootstrapNavigation,
            currentTheme.BootstrapForms,

            // Additional Bootstrap variables from current theme
            currentTheme.BootstrapListGroups,
            currentTheme.BootstrapProgress,
            currentTheme.BootstrapCloseButtons,
            currentTheme.BootstrapOffcanvas,
            currentTheme.BootstrapCarousel,
            currentTheme.BootstrapAccordion,
            currentTheme.BootstrapGeneral
        };

        return BootstrapCssGenerator.GenerateRootCss(objects);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Theme? GetCurrentTheme()
    {
        if (string.IsNullOrEmpty(CurrentTheme) || Themes == null)
            return null;

        return Themes.TryGetValue(CurrentTheme, out var theme) ? theme : null;
    }
}
