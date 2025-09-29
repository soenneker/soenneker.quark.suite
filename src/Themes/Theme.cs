using Soenneker.Quark;

namespace Soenneker.Quark;

public sealed class Theme
{
    public AlertOptions? Alerts { get; set; }

    public AnchorOptions? Anchors { get; set; }

    public BadgeOptions? Badges { get; set; }

    public BarOptions? Bars { get; set; }

    public BreadcrumbOptions? Breadcrumbs { get; set; }

    public ButtonOptions? Buttons { get; set; }

    public CardOptions? Cards { get; set; }

    public CheckOptions? Checks { get; set; }

    public CodeOptions? Codes { get; set; }

    public ColumnOptions? Columns { get; set; }

    public ContainerOptions? Containers { get; set; }

    public DateEditOptions? DateEdits { get; set; }

    public DatepickerOptions? Datepickers { get; set; }

    public DivOptions? Divs { get; set; }

    public FieldOptions? Fields { get; set; }

    public HeadingOptions? Headings { get; set; }

    public IconOptions? Icons { get; set; }

    public ImageOptions? Images { get; set; }

    public LabelOptions? Labels { get; set; }

    public LayoutOptions? Layouts { get; set; }

    public ListGroupOptions? ListGroups { get; set; }

    public ListGroupItemOptions? ListGroupItems { get; set; }

    public MemoEditOptions? MemoEdits { get; set; }

    public ModalOptions? Modals { get; set; }

    public NavOptions? Navs { get; set; }

    public NumericEditOptions? NumericEdits { get; set; }

    public OffcanvasOptions? Offcanvases { get; set; }

    public OrderedListOptions? OrderedLists { get; set; }

    public OrderedListItemOptions? OrderedListItems { get; set; }

    public PaginationOptions? Paginations { get; set; }

    public ParagraphOptions? Paragraphs { get; set; }

    public ProgressOptions? Progresses { get; set; }

    public RadioOptions? Radios { get; set; }

    public RowOptions? Rows { get; set; }

    public SectionOptions? Sections { get; set; }

    public SliderOptions? Sliders { get; set; }

    public SnackbarThemeOptions? Snackbars { get; set; }

    public SpanOptions? Spans { get; set; }

    public StepOptions? Steps { get; set; }

    public StrongOptions? Strongs { get; set; }

    public SwitchOptions? Switches { get; set; }

    public TableOptions? Tables { get; set; }

    public TabOptions? Tabs { get; set; }

    public TextEditOptions? TextEdits { get; set; }

    public TextOptions? Texts { get; set; }

    public UnorderedListOptions? UnorderedLists { get; set; }

    public UnorderedListItemOptions? UnorderedListItems { get; set; }

    // Dropdown components
    public DropdownOptions? Dropdowns { get; set; }

    public DropdownToggleOptions? DropdownToggles { get; set; }

    public DropdownMenuOptions? DropdownMenus { get; set; }

    public DropdownItemOptions? DropdownItems { get; set; }

    public DropdownDividerOptions? DropdownDividers { get; set; }

    // Validation components
    public ValidationSuccessOptions? ValidationSuccess { get; set; }

    public ValidationsContainerOptions? ValidationsContainers { get; set; }

    public ValidationErrorsOptions? ValidationErrors { get; set; }

    public ValidationErrorOptions? ValidationError { get; set; }

    // Table components
    public QuarkTrOptions? QuarkTrs { get; set; }

    public QuarkTheadOptions? QuarkTheads { get; set; }

    public QuarkTdOptions? QuarkTds { get; set; }

    public QuarkTbodyOptions? QuarkTbodys { get; set; }

    public QuarkTableSearchOptions? QuarkTableSearches { get; set; }

    public QuarkTablePaginationOptions? QuarkTablePaginations { get; set; }

    public QuarkTablePageSizeSelectorOptions? QuarkTablePageSizeSelectors { get; set; }

    public QuarkTableNoDataOptions? QuarkTableNoDatas { get; set; }

    public QuarkTableLoaderOptions? QuarkTableLoaders { get; set; }

    public QuarkTableInfoOptions? QuarkTableInfos { get; set; }

    public QuarkTableElementOptions? QuarkTableElements { get; set; }

    // Snackbar components
    public SnackbarHeaderOptions? SnackbarHeaders { get; set; }

    public SnackbarFooterOptions? SnackbarFooters { get; set; }

    public SnackbarBodyOptions? SnackbarBodys { get; set; }

    public SnackbarActionOptions? SnackbarActions { get; set; }

    // Navigation components
    public TabsContainerOptions? TabsContainers { get; set; }

    public StepsContainerOptions? StepsContainers { get; set; }

    public StepPanelOptions? StepPanels { get; set; }

    public StepContentOptions? StepContents { get; set; }

    public PaginationLinkOptions? PaginationLinks { get; set; }

    public PaginationItemOptions? PaginationItems { get; set; }

    public BreadcrumbItemOptions? BreadcrumbItems { get; set; }

    // Modal components
    public OffcanvasHeaderOptions? OffcanvasHeaders { get; set; }

    public OffcanvasBodyOptions? OffcanvasBodys { get; set; }

    public ModalTitleOptions? ModalTitles { get; set; }

    public ModalHeaderOptions? ModalHeaders { get; set; }

    public ModalFooterOptions? ModalFooters { get; set; }

    public ModalContentOptions? ModalContents { get; set; }

    public ModalCloseButtonOptions? ModalCloseButtons { get; set; }

    public ModalBodyOptions? ModalBodys { get; set; }

    // Form components
    public FieldLabelOptions? FieldLabels { get; set; }

    public FieldHelpOptions? FieldHelps { get; set; }

    public FieldBodyOptions? FieldBodys { get; set; }

    // Content components
    public SmallOptions? Smalls { get; set; }

    public PreCodeOptions? PreCodes { get; set; }

    // Bar components
    public BarTogglerOptions? BarTogglers { get; set; }

    public BarStartOptions? BarStarts { get; set; }

    public BarMenuOptions? BarMenus { get; set; }

    public BarLinkOptions? BarLinks { get; set; }

    public BarLabelOptions? BarLabels { get; set; }

    public BarItemOptions? BarItems { get; set; }

    public BarIconOptions? BarIcons { get; set; }

    public BarEndOptions? BarEnds { get; set; }

    public BarDropdownToggleOptions? BarDropdownToggles { get; set; }

    public BarDropdownOptions? BarDropdowns { get; set; }

    public BarDropdownMenuOptions? BarDropdownMenus { get; set; }

    public BarDropdownItemOptions? BarDropdownItems { get; set; }

    public BarDropdownDividerOptions? BarDropdownDividers { get; set; }

    public BarBrandOptions? BarBrands { get; set; }

    // Bootstrap CSS Variables
    /// <summary>
    /// Bootstrap color CSS variables for this theme
    /// </summary>
    public BootstrapColorsCssVariables? BootstrapColors { get; set; }

    /// <summary>
    /// Bootstrap typography CSS variables for this theme
    /// </summary>
    public BootstrapTypographyCssVariables? BootstrapTypography { get; set; }

    /// <summary>
    /// Bootstrap spacing CSS variables for this theme
    /// </summary>
    public BootstrapSpacingCssVariables? BootstrapSpacing { get; set; }

    /// <summary>
    /// Bootstrap border CSS variables for this theme
    /// </summary>
    public BootstrapBordersCssVariables? BootstrapBorders { get; set; }

    /// <summary>
    /// Bootstrap shadow CSS variables for this theme
    /// </summary>
    public BootstrapShadowsCssVariables? BootstrapShadows { get; set; }

    /// <summary>
    /// Bootstrap button CSS variables for this theme
    /// </summary>
    public BootstrapButtonsCssVariables? BootstrapButtons { get; set; }

    /// <summary>
    /// Bootstrap card CSS variables for this theme
    /// </summary>
    public BootstrapCardsCssVariables? BootstrapCards { get; set; }

    /// <summary>
    /// Bootstrap alert CSS variables for this theme
    /// </summary>
    public BootstrapAlertsCssVariables? BootstrapAlerts { get; set; }

    /// <summary>
    /// Bootstrap badge CSS variables for this theme
    /// </summary>
    public BootstrapBadgesCssVariables? BootstrapBadges { get; set; }

    /// <summary>
    /// Bootstrap modal CSS variables for this theme
    /// </summary>
    public BootstrapModalsCssVariables? BootstrapModals { get; set; }

    /// <summary>
    /// Bootstrap navigation CSS variables for this theme
    /// </summary>
    public BootstrapNavigationCssVariables? BootstrapNavigation { get; set; }

    /// <summary>
    /// Bootstrap form CSS variables for this theme
    /// </summary>
    public BootstrapFormsCssVariables? BootstrapForms { get; set; }

    /// <summary>
    /// Bootstrap list group CSS variables for this theme
    /// </summary>
    public BootstrapListGroupsCssVariables? BootstrapListGroups { get; set; }

    /// <summary>
    /// Bootstrap progress CSS variables for this theme
    /// </summary>
    public BootstrapProgressCssVariables? BootstrapProgress { get; set; }

    /// <summary>
    /// Bootstrap close button CSS variables for this theme
    /// </summary>
    public BootstrapCloseButtonsCssVariables? BootstrapCloseButtons { get; set; }

    /// <summary>
    /// Bootstrap offcanvas CSS variables for this theme
    /// </summary>
    public BootstrapOffcanvasCssVariables? BootstrapOffcanvas { get; set; }

    /// <summary>
    /// Bootstrap carousel CSS variables for this theme
    /// </summary>
    public BootstrapCarouselCssVariables? BootstrapCarousel { get; set; }

    /// <summary>
    /// Bootstrap accordion CSS variables for this theme
    /// </summary>
    public BootstrapAccordionCssVariables? BootstrapAccordion { get; set; }

    /// <summary>
    /// Bootstrap general CSS variables for this theme
    /// </summary>
    public BootstrapGeneralCssVariables? BootstrapGeneral { get; set; }
}