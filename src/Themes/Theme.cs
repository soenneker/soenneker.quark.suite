namespace Soenneker.Quark;

public sealed class Theme
{
    public string Name { get; set; } = "Default";

    public AlertOptions? Alerts { get; set; }

    public AccordionOptions? Accordions { get; set; }

    public AccordionItemOptions? AccordionItems { get; set; }

    public AccordionHeaderOptions? AccordionHeaders { get; set; }

    public AccordionBodyOptions? AccordionBodys { get; set; }

    public AnchorOptions? Anchors { get; set; }

    public BadgeOptions? Badges { get; set; }

    public BarOptions? Bars { get; set; }

    public BreadcrumbOptions? Breadcrumbs { get; set; }

    public ButtonOptions? Buttons { get; set; }

    public ButtonGroupOptions? ButtonGroups { get; set; }

    public CardOptions? Cards { get; set; }

    public CheckOptions? Checks { get; set; }

    public CodeOptions? Codes { get; set; }

    public CollapseOptions? Collapses { get; set; }

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

    public StrongOptions? Strongs { get; set; }

    public SwitchOptions? Switches { get; set; }

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

    public ValidationsOptions? Validations { get; set; }

    public ValidationErrorsOptions? ValidationErrors { get; set; }

    public ValidationErrorOptions? ValidationError { get; set; }

    // Table components
    public TrOptions? Trs { get; set; }

    public TheadOptions? Theads { get; set; }

    public TdOptions? Tds { get; set; }

    public TbodyOptions? Tbodys { get; set; }

    public TableSearchOptions? TableSearches { get; set; }

    public TablePaginationOptions? TablePaginations { get; set; }

    public TablePageSizeSelectorOptions? TablePageSizeSelectors { get; set; }

    public TableNoDataOptions? TableNoDatas { get; set; }

    public TableLoaderOptions? TableLoaders { get; set; }

    public TableInfoOptions? TableInfos { get; set; }

    public TableElementOptions? TableElements { get; set; }

    // Snackbar components
    public SnackbarHeaderOptions? SnackbarHeaders { get; set; }

    public SnackbarFooterOptions? SnackbarFooters { get; set; }

    public SnackbarBodyOptions? SnackbarBodys { get; set; }

    public SnackbarActionOptions? SnackbarActions { get; set; }

    public SnackbarStackOptions? SnackbarStacks { get; set; }

    // Navigation components
    public TabsOptions? Tabs { get; set; }

    public StepsOptions? Steps { get; set; }

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

    // Typographic components
    public H1Options? H1s { get; set; }

    public H2Options? H2s { get; set; }

    public H3Options? H3s { get; set; }

    public H4Options? H4s { get; set; }

    public H5Options? H5s { get; set; }

    public H6Options? H6s { get; set; }

    public BlockquoteOptions? Blockquotes { get; set; }

    public LeadOptions? Leads { get; set; }

    public MutedOptions? Muteds { get; set; }

    public KbdOptions? Kbds { get; set; }

    public FormTextOptions? FormTexts { get; set; }

    public KbdChipOptions? KbdChips { get; set; }

    public PillOptions? Pills { get; set; }

    public CodeChipOptions? CodeChips { get; set; }

    // Structure components
    public FigureOptions? Figures { get; set; }

    public FigcaptionOptions? Figcaptions { get; set; }

    public MainOptions? Mains { get; set; }

    public LegendOptions? Legends { get; set; }

    public AsideOptions? Asides { get; set; }

    public ArticleOptions? Articles { get; set; }

    public FieldsetOptions? Fieldsets { get; set; }

    // Media components
    public AudioOptions? Audios { get; set; }

    public VideoOptions? Videos { get; set; }

    public IFrameOptions? IFrames { get; set; }

    // Utilities
    public HrOptions? Hrs { get; set; }

    public BrOptions? Brs { get; set; }

    public DetailsOptions? Details { get; set; }

    public SummaryOptions? Summaries { get; set; }

    // Card sub-components
    public CardTextOptions? CardTexts { get; set; }

    public CardTitleOptions? CardTitles { get; set; }

    public CardImgOptions? CardImgs { get; set; }

    public CardSubtitleOptions? CardSubtitles { get; set; }

    public CardBodyOptions? CardBodys { get; set; }

    public CardHeaderOptions? CardHeaders { get; set; }

    public CardFooterOptions? CardFooters { get; set; }

    // Alert sub-components
    public AlertMessageOptions? AlertMessages { get; set; }

    public AlertDescriptionOptions? AlertDescriptions { get; set; }

    // Surface components
    public ProgressBarOptions? ProgressBars { get; set; }

    public CalloutOptions? Callouts { get; set; }

    public PanelOptions? Panels { get; set; }

    // Input components
    public SelectItemOptions? SelectItems { get; set; }

    public SelectGroupOptions? SelectGroups { get; set; }

    public SelectOptions? Selects { get; set; }

    public InputOptions? Inputs { get; set; }

    public DateTimePickerOptions? DateTimePickers { get; set; }

    // Layout components
    public LayoutHeaderOptions? LayoutHeaders { get; set; }

    public LayoutFooterOptions? LayoutFooters { get; set; }

    public LayoutContentOptions? LayoutContents { get; set; }

    public LayoutContainerOptions? LayoutContainers { get; set; }

    public LayoutSiderOptions? LayoutSiders { get; set; }

    public LayoutSiderContentOptions? LayoutSiderContents { get; set; }

    public OverlayContainerOptions? OverlayContainers { get; set; }

    // Navigation sub-components
    public TabOptions? Tab { get; set; }

    public StepOptions? Step { get; set; }

    public StepMarkerOptions? StepMarkers { get; set; }

    public StepCaptionOptions? StepCaptions { get; set; }

    public StepsContentOptions? StepsContents { get; set; }

    // Table sub-components
    public ThOptions? Ths { get; set; }

    public TableBottomBarOptions? TableBottomBars { get; set; }

    public TableTopBarOptions? TableTopBars { get; set; }

    public TableLeftOptions? TableLefts { get; set; }

    public TableRightOptions? TableRights { get; set; }

    public TableThemeOptions? Tables { get; set; }

    // Collection components
    public TreeViewOptions? TreeViews { get; set; }

    public TreeViewNodeOptions? TreeViewNodes { get; set; }

    public TreeViewNodeContentOptions? TreeViewNodeContents { get; set; }

    // Forms sub-components
    public ValidationsContainerOptions? ValidationsContainers { get; set; }

    // Modal/Overlay main components
    public SnackbarOptions? Snackbar { get; set; }

    public BootstrapCssVariables? BootstrapCssVariables { get; set; }
}
