using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Represents a theme configuration containing styling options for all Quark components.
/// </summary>
public sealed class Theme
{
    /// <summary>
    /// Gets or sets the name of the theme.
    /// </summary>
    public string Name { get; set; } = "Default";

    /// <summary>
    /// Gets or sets the alert component styling options.
    /// </summary>
    public AlertOptions? Alerts { get; set; }

    /// <summary>
    /// Gets or sets the accordion component styling options.
    /// </summary>
    public AccordionOptions? Accordions { get; set; }

    /// <summary>
    /// Gets or sets the accordion item component styling options.
    /// </summary>
    public AccordionItemOptions? AccordionItems { get; set; }

    /// <summary>
    /// Gets or sets the accordion header component styling options.
    /// </summary>
    public AccordionHeaderOptions? AccordionHeaders { get; set; }

    /// <summary>
    /// Gets or sets the accordion body component styling options.
    /// </summary>
    public AccordionBodyOptions? AccordionBodys { get; set; }

    /// <summary>
    /// Gets or sets the anchor component styling options.
    /// </summary>
    public AnchorOptions? Anchors { get; set; }

    /// <summary>
    /// Gets or sets the badge component styling options.
    /// </summary>
    public BadgeOptions? Badges { get; set; }

    /// <summary>
    /// Gets or sets the bar component styling options.
    /// </summary>
    public BarOptions? Bars { get; set; }

    /// <summary>
    /// Gets or sets the breadcrumb component styling options.
    /// </summary>
    public BreadcrumbOptions? Breadcrumbs { get; set; }

    /// <summary>
    /// Gets or sets the button component styling options.
    /// </summary>
    public ButtonOptions? Buttons { get; set; }

    /// <summary>
    /// Gets or sets the button group component styling options.
    /// </summary>
    public ButtonGroupOptions? ButtonGroups { get; set; }

    /// <summary>
    /// Gets or sets the card component styling options.
    /// </summary>
    public CardOptions? Cards { get; set; }

    /// <summary>
    /// Gets or sets the check component styling options.
    /// </summary>
    public CheckOptions? Checks { get; set; }

    /// <summary>
    /// Gets or sets the code component styling options.
    /// </summary>
    public CodeOptions? Codes { get; set; }

    /// <summary>
    /// Gets or sets the collapse component styling options.
    /// </summary>
    public CollapseOptions? Collapses { get; set; }

    /// <summary>
    /// Gets or sets the column component styling options.
    /// </summary>
    public ColumnOptions? Columns { get; set; }

    /// <summary>
    /// Gets or sets the container component styling options.
    /// </summary>
    public ContainerOptions? Containers { get; set; }

    /// <summary>
    /// Gets or sets the date edit component styling options.
    /// </summary>
    public DateEditOptions? DateEdits { get; set; }

    /// <summary>
    /// Gets or sets the datepicker component styling options.
    /// </summary>
    public DatepickerOptions? Datepickers { get; set; }

    /// <summary>
    /// Gets or sets the div component styling options.
    /// </summary>
    public DivOptions? Divs { get; set; }

    /// <summary>
    /// Gets or sets the field component styling options.
    /// </summary>
    public FieldOptions? Fields { get; set; }

    /// <summary>
    /// Gets or sets the heading component styling options.
    /// </summary>
    public HeadingOptions? Headings { get; set; }

    /// <summary>
    /// Gets or sets the icon component styling options.
    /// </summary>
    public IconOptions? Icons { get; set; }

    /// <summary>
    /// Gets or sets the image component styling options.
    /// </summary>
    public ImageOptions? Images { get; set; }

    /// <summary>
    /// Gets or sets the label component styling options.
    /// </summary>
    public LabelOptions? Labels { get; set; }

    /// <summary>
    /// Gets or sets the layout component styling options.
    /// </summary>
    public LayoutOptions? Layouts { get; set; }

    /// <summary>
    /// Gets or sets the list group component styling options.
    /// </summary>
    public ListGroupOptions? ListGroups { get; set; }

    /// <summary>
    /// Gets or sets the list group item component styling options.
    /// </summary>
    public ListGroupItemOptions? ListGroupItems { get; set; }

    /// <summary>
    /// Gets or sets the memo edit component styling options.
    /// </summary>
    public MemoEditOptions? MemoEdits { get; set; }

    /// <summary>
    /// Gets or sets the modal component styling options.
    /// </summary>
    public ModalOptions? Modals { get; set; }

    /// <summary>
    /// Gets or sets the nav component styling options.
    /// </summary>
    public NavOptions? Navs { get; set; }

    /// <summary>
    /// Gets or sets the numeric edit component styling options.
    /// </summary>
    public NumericEditOptions? NumericEdits { get; set; }

    /// <summary>
    /// Gets or sets the offcanvas component styling options.
    /// </summary>
    public OffcanvasOptions? Offcanvases { get; set; }

    /// <summary>
    /// Gets or sets the ordered list component styling options.
    /// </summary>
    public OrderedListOptions? OrderedLists { get; set; }

    /// <summary>
    /// Gets or sets the ordered list item component styling options.
    /// </summary>
    public OrderedListItemOptions? OrderedListItems { get; set; }

    /// <summary>
    /// Gets or sets the pagination component styling options.
    /// </summary>
    public PaginationOptions? Paginations { get; set; }

    /// <summary>
    /// Gets or sets the paragraph component styling options.
    /// </summary>
    public ParagraphOptions? Paragraphs { get; set; }

    /// <summary>
    /// Gets or sets the progress component styling options.
    /// </summary>
    public ProgressOptions? Progresses { get; set; }

    /// <summary>
    /// Gets or sets the radio component styling options.
    /// </summary>
    public RadioOptions? Radios { get; set; }

    /// <summary>
    /// Gets or sets the row component styling options.
    /// </summary>
    public RowOptions? Rows { get; set; }

    /// <summary>
    /// Gets or sets the section component styling options.
    /// </summary>
    public SectionOptions? Sections { get; set; }

    /// <summary>
    /// Gets or sets the slider component styling options.
    /// </summary>
    public SliderOptions? Sliders { get; set; }

    /// <summary>
    /// Gets or sets the snackbar component styling options.
    /// </summary>
    public SnackbarThemeOptions? Snackbars { get; set; }

    /// <summary>
    /// Gets or sets the span component styling options.
    /// </summary>
    public SpanOptions? Spans { get; set; }

    /// <summary>
    /// Gets or sets the strong component styling options.
    /// </summary>
    public StrongOptions? Strongs { get; set; }

    /// <summary>
    /// Gets or sets the switch component styling options.
    /// </summary>
    public SwitchOptions? Switches { get; set; }

    /// <summary>
    /// Gets or sets the text edit component styling options.
    /// </summary>
    public TextEditOptions? TextEdits { get; set; }

    /// <summary>
    /// Gets or sets the text component styling options.
    /// </summary>
    public TextOptions? Texts { get; set; }

    /// <summary>
    /// Gets or sets the unordered list component styling options.
    /// </summary>
    public UnorderedListOptions? UnorderedLists { get; set; }

    /// <summary>
    /// Gets or sets the unordered list item component styling options.
    /// </summary>
    public UnorderedListItemOptions? UnorderedListItems { get; set; }

    // Dropdown components
    /// <summary>
    /// Gets or sets the dropdown component styling options.
    /// </summary>
    public DropdownOptions? Dropdowns { get; set; }

    /// <summary>
    /// Gets or sets the dropdown toggle component styling options.
    /// </summary>
    public DropdownToggleOptions? DropdownToggles { get; set; }

    /// <summary>
    /// Gets or sets the dropdown menu component styling options.
    /// </summary>
    public DropdownMenuOptions? DropdownMenus { get; set; }

    /// <summary>
    /// Gets or sets the dropdown item component styling options.
    /// </summary>
    public DropdownItemOptions? DropdownItems { get; set; }

    /// <summary>
    /// Gets or sets the dropdown divider component styling options.
    /// </summary>
    public DropdownDividerOptions? DropdownDividers { get; set; }

    // Validation components
    /// <summary>
    /// Gets or sets the validation success component styling options.
    /// </summary>
    public ValidationSuccessOptions? ValidationSuccess { get; set; }

    /// <summary>
    /// Gets or sets the validations component styling options.
    /// </summary>
    public ValidationsOptions? Validations { get; set; }

    /// <summary>
    /// Gets or sets the validation errors component styling options.
    /// </summary>
    public ValidationErrorsOptions? ValidationErrors { get; set; }

    /// <summary>
    /// Gets or sets the validation error component styling options.
    /// </summary>
    public ValidationErrorOptions? ValidationError { get; set; }

    // Table components
    /// <summary>
    /// Gets or sets the table row component styling options.
    /// </summary>
    public TrOptions? Trs { get; set; }

    /// <summary>
    /// Gets or sets the table head component styling options.
    /// </summary>
    public TheadOptions? Theads { get; set; }

    /// <summary>
    /// Gets or sets the table data cell component styling options.
    /// </summary>
    public TdOptions? Tds { get; set; }

    /// <summary>
    /// Gets or sets the table body component styling options.
    /// </summary>
    public TbodyOptions? Tbodys { get; set; }

    /// <summary>
    /// Gets or sets the table search component styling options.
    /// </summary>
    public TableSearchOptions? TableSearches { get; set; }

    /// <summary>
    /// Gets or sets the table pagination component styling options.
    /// </summary>
    public TablePaginationOptions? TablePaginations { get; set; }

    /// <summary>
    /// Gets or sets the table page size selector component styling options.
    /// </summary>
    public TablePageSizeSelectorOptions? TablePageSizeSelectors { get; set; }

    /// <summary>
    /// Gets or sets the table no data component styling options.
    /// </summary>
    public TableNoDataOptions? TableNoDatas { get; set; }

    /// <summary>
    /// Gets or sets the table loader component styling options.
    /// </summary>
    public TableLoaderOptions? TableLoaders { get; set; }

    /// <summary>
    /// Gets or sets the table info component styling options.
    /// </summary>
    public TableInfoOptions? TableInfos { get; set; }

    /// <summary>
    /// Gets or sets the table element component styling options.
    /// </summary>
    public TableElementOptions? TableElements { get; set; }

    // Snackbar components
    /// <summary>
    /// Gets or sets the snackbar header component styling options.
    /// </summary>
    public SnackbarHeaderOptions? SnackbarHeaders { get; set; }

    /// <summary>
    /// Gets or sets the snackbar footer component styling options.
    /// </summary>
    public SnackbarFooterOptions? SnackbarFooters { get; set; }

    /// <summary>
    /// Gets or sets the snackbar body component styling options.
    /// </summary>
    public SnackbarBodyOptions? SnackbarBodys { get; set; }

    /// <summary>
    /// Gets or sets the snackbar action component styling options.
    /// </summary>
    public SnackbarActionOptions? SnackbarActions { get; set; }

    /// <summary>
    /// Gets or sets the snackbar stack component styling options.
    /// </summary>
    public SnackbarStackOptions? SnackbarStacks { get; set; }

    // Navigation components
    /// <summary>
    /// Gets or sets the tabs component styling options.
    /// </summary>
    public TabsOptions? Tabs { get; set; }

    /// <summary>
    /// Gets or sets the steps component styling options.
    /// </summary>
    public StepsOptions? Steps { get; set; }

    /// <summary>
    /// Gets or sets the step panel component styling options.
    /// </summary>
    public StepPanelOptions? StepPanels { get; set; }

    /// <summary>
    /// Gets or sets the step content component styling options.
    /// </summary>
    public StepContentOptions? StepContents { get; set; }

    /// <summary>
    /// Gets or sets the pagination link component styling options.
    /// </summary>
    public PaginationLinkOptions? PaginationLinks { get; set; }

    /// <summary>
    /// Gets or sets the pagination item component styling options.
    /// </summary>
    public PaginationItemOptions? PaginationItems { get; set; }

    /// <summary>
    /// Gets or sets the breadcrumb item component styling options.
    /// </summary>
    public BreadcrumbItemOptions? BreadcrumbItems { get; set; }

    // Modal components
    /// <summary>
    /// Gets or sets the offcanvas header component styling options.
    /// </summary>
    public OffcanvasHeaderOptions? OffcanvasHeaders { get; set; }

    /// <summary>
    /// Gets or sets the offcanvas body component styling options.
    /// </summary>
    public OffcanvasBodyOptions? OffcanvasBodys { get; set; }

    /// <summary>
    /// Gets or sets the modal title component styling options.
    /// </summary>
    public ModalTitleOptions? ModalTitles { get; set; }

    /// <summary>
    /// Gets or sets the modal header component styling options.
    /// </summary>
    public ModalHeaderOptions? ModalHeaders { get; set; }

    /// <summary>
    /// Gets or sets the modal footer component styling options.
    /// </summary>
    public ModalFooterOptions? ModalFooters { get; set; }

    /// <summary>
    /// Gets or sets the modal content component styling options.
    /// </summary>
    public ModalContentOptions? ModalContents { get; set; }

    /// <summary>
    /// Gets or sets the modal close button component styling options.
    /// </summary>
    public ModalCloseButtonOptions? ModalCloseButtons { get; set; }

    /// <summary>
    /// Gets or sets the modal body component styling options.
    /// </summary>
    public ModalBodyOptions? ModalBodys { get; set; }

    // Form components
    /// <summary>
    /// Gets or sets the field label component styling options.
    /// </summary>
    public FieldLabelOptions? FieldLabels { get; set; }

    /// <summary>
    /// Gets or sets the field help component styling options.
    /// </summary>
    public FieldHelpOptions? FieldHelps { get; set; }

    /// <summary>
    /// Gets or sets the field body component styling options.
    /// </summary>
    public FieldBodyOptions? FieldBodys { get; set; }

    // Content components
    /// <summary>
    /// Gets or sets the small component styling options.
    /// </summary>
    public SmallOptions? Smalls { get; set; }

    /// <summary>
    /// Gets or sets the pre code component styling options.
    /// </summary>
    public PreCodeOptions? PreCodes { get; set; }

    // Bar components
    /// <summary>
    /// Gets or sets the bar toggler component styling options.
    /// </summary>
    public BarTogglerOptions? BarTogglers { get; set; }

    /// <summary>
    /// Gets or sets the bar start component styling options.
    /// </summary>
    public BarStartOptions? BarStarts { get; set; }

    /// <summary>
    /// Gets or sets the bar menu component styling options.
    /// </summary>
    public BarMenuOptions? BarMenus { get; set; }

    /// <summary>
    /// Gets or sets the bar link component styling options.
    /// </summary>
    public BarLinkOptions? BarLinks { get; set; }

    /// <summary>
    /// Gets or sets the bar label component styling options.
    /// </summary>
    public BarLabelOptions? BarLabels { get; set; }

    /// <summary>
    /// Gets or sets the bar item component styling options.
    /// </summary>
    public BarItemOptions? BarItems { get; set; }

    /// <summary>
    /// Gets or sets the bar icon component styling options.
    /// </summary>
    public BarIconOptions? BarIcons { get; set; }

    /// <summary>
    /// Gets or sets the bar end component styling options.
    /// </summary>
    public BarEndOptions? BarEnds { get; set; }

    /// <summary>
    /// Gets or sets the bar dropdown toggle component styling options.
    /// </summary>
    public BarDropdownToggleOptions? BarDropdownToggles { get; set; }

    /// <summary>
    /// Gets or sets the bar dropdown component styling options.
    /// </summary>
    public BarDropdownOptions? BarDropdowns { get; set; }

    /// <summary>
    /// Gets or sets the bar dropdown menu component styling options.
    /// </summary>
    public BarDropdownMenuOptions? BarDropdownMenus { get; set; }

    /// <summary>
    /// Gets or sets the bar dropdown item component styling options.
    /// </summary>
    public BarDropdownItemOptions? BarDropdownItems { get; set; }

    /// <summary>
    /// Gets or sets the bar dropdown divider component styling options.
    /// </summary>
    public BarDropdownDividerOptions? BarDropdownDividers { get; set; }

    /// <summary>
    /// Gets or sets the bar brand component styling options.
    /// </summary>
    public BarBrandOptions? BarBrands { get; set; }

    // Typographic components
    /// <summary>
    /// Gets or sets the H1 heading component styling options.
    /// </summary>
    public H1Options? H1S { get; set; }

    /// <summary>
    /// Gets or sets the H2 heading component styling options.
    /// </summary>
    public H2Options? H2S { get; set; }

    /// <summary>
    /// Gets or sets the H3 heading component styling options.
    /// </summary>
    public H3Options? H3S { get; set; }

    /// <summary>
    /// Gets or sets the H4 heading component styling options.
    /// </summary>
    public H4Options? H4S { get; set; }

    /// <summary>
    /// Gets or sets the H5 heading component styling options.
    /// </summary>
    public H5Options? H5S { get; set; }

    /// <summary>
    /// Gets or sets the H6 heading component styling options.
    /// </summary>
    public H6Options? H6S { get; set; }

    /// <summary>
    /// Gets or sets the blockquote component styling options.
    /// </summary>
    public BlockquoteOptions? Blockquotes { get; set; }

    /// <summary>
    /// Gets or sets the lead component styling options.
    /// </summary>
    public LeadOptions? Leads { get; set; }

    /// <summary>
    /// Gets or sets the muted component styling options.
    /// </summary>
    public MutedOptions? Muteds { get; set; }

    /// <summary>
    /// Gets or sets the keyboard key component styling options.
    /// </summary>
    public KbdOptions? Kbds { get; set; }

    /// <summary>
    /// Gets or sets the form text component styling options.
    /// </summary>
    public FormTextOptions? FormTexts { get; set; }

    /// <summary>
    /// Gets or sets the keyboard chip component styling options.
    /// </summary>
    public KbdChipOptions? KbdChips { get; set; }

    /// <summary>
    /// Gets or sets the pill component styling options.
    /// </summary>
    public PillOptions? Pills { get; set; }

    /// <summary>
    /// Gets or sets the code chip component styling options.
    /// </summary>
    public CodeChipOptions? CodeChips { get; set; }

    // Structure components
    /// <summary>
    /// Gets or sets the figure component styling options.
    /// </summary>
    public FigureOptions? Figures { get; set; }

    /// <summary>
    /// Gets or sets the figcaption component styling options.
    /// </summary>
    public FigcaptionOptions? Figcaptions { get; set; }

    /// <summary>
    /// Gets or sets the main component styling options.
    /// </summary>
    public MainOptions? Mains { get; set; }

    /// <summary>
    /// Gets or sets the legend component styling options.
    /// </summary>
    public LegendOptions? Legends { get; set; }

    /// <summary>
    /// Gets or sets the aside component styling options.
    /// </summary>
    public AsideOptions? Asides { get; set; }

    /// <summary>
    /// Gets or sets the article component styling options.
    /// </summary>
    public ArticleOptions? Articles { get; set; }

    /// <summary>
    /// Gets or sets the fieldset component styling options.
    /// </summary>
    public FieldsetOptions? Fieldsets { get; set; }

    // Media components
    /// <summary>
    /// Gets or sets the audio component styling options.
    /// </summary>
    public AudioOptions? Audios { get; set; }

    /// <summary>
    /// Gets or sets the video component styling options.
    /// </summary>
    public VideoOptions? Videos { get; set; }

    /// <summary>
    /// Gets or sets the iframe component styling options.
    /// </summary>
    public IFrameOptions? IFrames { get; set; }

    // Utilities
    /// <summary>
    /// Gets or sets the horizontal rule component styling options.
    /// </summary>
    public HrOptions? Hrs { get; set; }

    /// <summary>
    /// Gets or sets the line break component styling options.
    /// </summary>
    public BrOptions? Brs { get; set; }

    /// <summary>
    /// Gets or sets the details component styling options.
    /// </summary>
    public DetailsOptions? Details { get; set; }

    /// <summary>
    /// Gets or sets the summary component styling options.
    /// </summary>
    public SummaryOptions? Summaries { get; set; }

    // Card sub-components
    /// <summary>
    /// Gets or sets the card text component styling options.
    /// </summary>
    public CardTextOptions? CardTexts { get; set; }

    /// <summary>
    /// Gets or sets the card title component styling options.
    /// </summary>
    public CardTitleOptions? CardTitles { get; set; }

    /// <summary>
    /// Gets or sets the card image component styling options.
    /// </summary>
    public CardImgOptions? CardImgs { get; set; }

    /// <summary>
    /// Gets or sets the card subtitle component styling options.
    /// </summary>
    public CardSubtitleOptions? CardSubtitles { get; set; }

    /// <summary>
    /// Gets or sets the card body component styling options.
    /// </summary>
    public CardBodyOptions? CardBodys { get; set; }

    /// <summary>
    /// Gets or sets the card header component styling options.
    /// </summary>
    public CardHeaderOptions? CardHeaders { get; set; }

    /// <summary>
    /// Gets or sets the card footer component styling options.
    /// </summary>
    public CardFooterOptions? CardFooters { get; set; }

    // Alert sub-components
    /// <summary>
    /// Gets or sets the alert message component styling options.
    /// </summary>
    public AlertMessageOptions? AlertMessages { get; set; }

    /// <summary>
    /// Gets or sets the alert description component styling options.
    /// </summary>
    public AlertDescriptionOptions? AlertDescriptions { get; set; }

    // Surface components
    /// <summary>
    /// Gets or sets the progress bar component styling options.
    /// </summary>
    public ProgressBarOptions? ProgressBars { get; set; }

    /// <summary>
    /// Gets or sets the callout component styling options.
    /// </summary>
    public CalloutOptions? Callouts { get; set; }

    /// <summary>
    /// Gets or sets the panel component styling options.
    /// </summary>
    public PanelOptions? Panels { get; set; }

    // Input components
    /// <summary>
    /// Gets or sets the select item component styling options.
    /// </summary>
    public SelectItemOptions? SelectItems { get; set; }

    /// <summary>
    /// Gets or sets the select group component styling options.
    /// </summary>
    public SelectGroupOptions? SelectGroups { get; set; }

    /// <summary>
    /// Gets or sets the select component styling options.
    /// </summary>
    public SelectOptions? Selects { get; set; }

    /// <summary>
    /// Gets or sets the input component styling options.
    /// </summary>
    public InputOptions? Inputs { get; set; }

    /// <summary>
    /// Gets or sets the date time picker component styling options.
    /// </summary>
    public DateTimePickerOptions? DateTimePickers { get; set; }

    // Layout components
    /// <summary>
    /// Gets or sets the layout header component styling options.
    /// </summary>
    public LayoutHeaderOptions? LayoutHeaders { get; set; }

    /// <summary>
    /// Gets or sets the layout footer component styling options.
    /// </summary>
    public LayoutFooterOptions? LayoutFooters { get; set; }

    /// <summary>
    /// Gets or sets the layout content component styling options.
    /// </summary>
    public LayoutContentOptions? LayoutContents { get; set; }

    /// <summary>
    /// Gets or sets the layout container component styling options.
    /// </summary>
    public LayoutContainerOptions? LayoutContainers { get; set; }

    /// <summary>
    /// Gets or sets the layout sider component styling options.
    /// </summary>
    public LayoutSiderOptions? LayoutSiders { get; set; }

    /// <summary>
    /// Gets or sets the layout sider content component styling options.
    /// </summary>
    public LayoutSiderContentOptions? LayoutSiderContents { get; set; }

    /// <summary>
    /// Gets or sets the overlay container component styling options.
    /// </summary>
    public OverlayContainerOptions? OverlayContainers { get; set; }

    // Navigation sub-components
    /// <summary>
    /// Gets or sets the tab component styling options.
    /// </summary>
    public TabOptions? Tab { get; set; }

    /// <summary>
    /// Gets or sets the step component styling options.
    /// </summary>
    public StepOptions? Step { get; set; }

    /// <summary>
    /// Gets or sets the step marker component styling options.
    /// </summary>
    public StepMarkerOptions? StepMarkers { get; set; }

    /// <summary>
    /// Gets or sets the step caption component styling options.
    /// </summary>
    public StepCaptionOptions? StepCaptions { get; set; }

    /// <summary>
    /// Gets or sets the steps content component styling options.
    /// </summary>
    public StepsContentOptions? StepsContents { get; set; }

    // Table sub-components
    /// <summary>
    /// Gets or sets the table header cell component styling options.
    /// </summary>
    public ThOptions? Ths { get; set; }

    /// <summary>
    /// Gets or sets the table bottom bar component styling options.
    /// </summary>
    public TableBottomBarOptions? TableBottomBars { get; set; }

    /// <summary>
    /// Gets or sets the table top bar component styling options.
    /// </summary>
    public TableTopBarOptions? TableTopBars { get; set; }

    /// <summary>
    /// Gets or sets the table left component styling options.
    /// </summary>
    public TableLeftOptions? TableLefts { get; set; }

    /// <summary>
    /// Gets or sets the table right component styling options.
    /// </summary>
    public TableRightOptions? TableRights { get; set; }

    /// <summary>
    /// Gets or sets the table theme component styling options.
    /// </summary>
    public TableThemeOptions? Tables { get; set; }

    // Collection components
    /// <summary>
    /// Gets or sets the tree view component styling options.
    /// </summary>
    public TreeViewOptions? TreeViews { get; set; }

    /// <summary>
    /// Gets or sets the tree view node component styling options.
    /// </summary>
    public TreeViewNodeOptions? TreeViewNodes { get; set; }

    /// <summary>
    /// Gets or sets the tree view node content component styling options.
    /// </summary>
    public TreeViewNodeContentOptions? TreeViewNodeContents { get; set; }

    // Forms sub-components
    /// <summary>
    /// Gets or sets the validations container component styling options.
    /// </summary>
    public ValidationsContainerOptions? ValidationsContainers { get; set; }

    // Modal/Overlay main components
    /// <summary>
    /// Gets or sets the snackbar component styling options.
    /// </summary>
    public SnackbarOptions? Snackbar { get; set; }

    /// <summary>
    /// Gets all ComponentOptions instances in this theme without using reflection.
    /// </summary>
    /// <returns>An enumerable collection of all component options in this theme.</returns>
    internal IEnumerable<ComponentOptions> GetAllComponentOptions()
    {
        if (Alerts != null)
            yield return Alerts;
        if (Accordions != null)
            yield return Accordions;
        if (AccordionItems != null)
            yield return AccordionItems;
        if (AccordionHeaders != null)
            yield return AccordionHeaders;
        if (AccordionBodys != null)
            yield return AccordionBodys;
        if (Anchors != null)
            yield return Anchors;
        if (Badges != null)
            yield return Badges;
        if (Bars != null)
            yield return Bars;
        if (Breadcrumbs != null)
            yield return Breadcrumbs;
        if (Buttons != null)
            yield return Buttons;
        if (ButtonGroups != null)
            yield return ButtonGroups;
        if (Cards != null)
            yield return Cards;
        if (Checks != null)
            yield return Checks;
        if (Codes != null)
            yield return Codes;
        if (Collapses != null)
            yield return Collapses;
        if (Columns != null)
            yield return Columns;
        if (Containers != null)
            yield return Containers;
        if (DateEdits != null)
            yield return DateEdits;
        if (Datepickers != null)
            yield return Datepickers;
        if (Divs != null)
            yield return Divs;
        if (Fields != null)
            yield return Fields;
        if (Headings != null)
            yield return Headings;
        if (Icons != null)
            yield return Icons;
        if (Images != null)
            yield return Images;
        if (Labels != null)
            yield return Labels;
        if (Layouts != null)
            yield return Layouts;
        if (ListGroups != null)
            yield return ListGroups;
        if (ListGroupItems != null)
            yield return ListGroupItems;
        if (MemoEdits != null)
            yield return MemoEdits;
        if (Modals != null)
            yield return Modals;
        if (Navs != null)
            yield return Navs;
        if (NumericEdits != null)
            yield return NumericEdits;
        if (Offcanvases != null)
            yield return Offcanvases;
        if (OrderedLists != null)
            yield return OrderedLists;
        if (OrderedListItems != null)
            yield return OrderedListItems;
        if (Paginations != null)
            yield return Paginations;
        if (Paragraphs != null)
            yield return Paragraphs;
        if (Progresses != null)
            yield return Progresses;
        if (Radios != null)
            yield return Radios;
        if (Rows != null)
            yield return Rows;
        if (Sections != null)
            yield return Sections;
        if (Sliders != null)
            yield return Sliders;
        if (Snackbars != null)
            yield return Snackbars;
        if (Spans != null)
            yield return Spans;
        if (Strongs != null)
            yield return Strongs;
        if (Switches != null)
            yield return Switches;
        if (TextEdits != null)
            yield return TextEdits;
        if (Texts != null)
            yield return Texts;
        if (UnorderedLists != null)
            yield return UnorderedLists;
        if (UnorderedListItems != null)
            yield return UnorderedListItems;
        if (Dropdowns != null)
            yield return Dropdowns;
        if (DropdownToggles != null)
            yield return DropdownToggles;
        if (DropdownMenus != null)
            yield return DropdownMenus;
        if (DropdownItems != null)
            yield return DropdownItems;
        if (DropdownDividers != null)
            yield return DropdownDividers;
        if (ValidationSuccess != null)
            yield return ValidationSuccess;
        if (Validations != null)
            yield return Validations;
        if (ValidationErrors != null)
            yield return ValidationErrors;
        if (ValidationError != null)
            yield return ValidationError;
        if (Trs != null)
            yield return Trs;
        if (Theads != null)
            yield return Theads;
        if (Tds != null)
            yield return Tds;
        if (Tbodys != null)
            yield return Tbodys;
        if (TableSearches != null)
            yield return TableSearches;
        if (TablePaginations != null)
            yield return TablePaginations;
        if (TablePageSizeSelectors != null)
            yield return TablePageSizeSelectors;
        if (TableNoDatas != null)
            yield return TableNoDatas;
        if (TableLoaders != null)
            yield return TableLoaders;
        if (TableInfos != null)
            yield return TableInfos;
        if (TableElements != null)
            yield return TableElements;
        if (SnackbarHeaders != null)
            yield return SnackbarHeaders;
        if (SnackbarFooters != null)
            yield return SnackbarFooters;
        if (SnackbarBodys != null)
            yield return SnackbarBodys;
        if (SnackbarActions != null)
            yield return SnackbarActions;
        if (SnackbarStacks != null)
            yield return SnackbarStacks;
        if (Tabs != null)
            yield return Tabs;
        if (Steps != null)
            yield return Steps;
        if (StepPanels != null)
            yield return StepPanels;
        if (StepContents != null)
            yield return StepContents;
        if (PaginationLinks != null)
            yield return PaginationLinks;
        if (PaginationItems != null)
            yield return PaginationItems;
        if (BreadcrumbItems != null)
            yield return BreadcrumbItems;
        if (OffcanvasHeaders != null)
            yield return OffcanvasHeaders;
        if (OffcanvasBodys != null)
            yield return OffcanvasBodys;
        if (ModalTitles != null)
            yield return ModalTitles;
        if (ModalHeaders != null)
            yield return ModalHeaders;
        if (ModalFooters != null)
            yield return ModalFooters;
        if (ModalContents != null)
            yield return ModalContents;
        if (ModalCloseButtons != null)
            yield return ModalCloseButtons;
        if (ModalBodys != null)
            yield return ModalBodys;
        if (FieldLabels != null)
            yield return FieldLabels;
        if (FieldHelps != null)
            yield return FieldHelps;
        if (FieldBodys != null)
            yield return FieldBodys;
        if (Smalls != null)
            yield return Smalls;
        if (PreCodes != null)
            yield return PreCodes;
        if (BarTogglers != null)
            yield return BarTogglers;
        if (BarStarts != null)
            yield return BarStarts;
        if (BarMenus != null)
            yield return BarMenus;
        if (BarLinks != null)
            yield return BarLinks;
        if (BarLabels != null)
            yield return BarLabels;
        if (BarItems != null)
            yield return BarItems;
        if (BarIcons != null)
            yield return BarIcons;
        if (BarEnds != null)
            yield return BarEnds;
        if (BarDropdownToggles != null)
            yield return BarDropdownToggles;
        if (BarDropdowns != null)
            yield return BarDropdowns;
        if (BarDropdownMenus != null)
            yield return BarDropdownMenus;
        if (BarDropdownItems != null)
            yield return BarDropdownItems;
        if (BarDropdownDividers != null)
            yield return BarDropdownDividers;
        if (BarBrands != null)
            yield return BarBrands;
        if (H1S != null)
            yield return H1S;
        if (H2S != null)
            yield return H2S;
        if (H3S != null)
            yield return H3S;
        if (H4S != null)
            yield return H4S;
        if (H5S != null)
            yield return H5S;
        if (H6S != null)
            yield return H6S;
        if (Blockquotes != null)
            yield return Blockquotes;
        if (Leads != null)
            yield return Leads;
        if (Muteds != null)
            yield return Muteds;
        if (Kbds != null)
            yield return Kbds;
        if (FormTexts != null)
            yield return FormTexts;
        if (KbdChips != null)
            yield return KbdChips;
        if (Pills != null)
            yield return Pills;
        if (CodeChips != null)
            yield return CodeChips;
        if (Figures != null)
            yield return Figures;
        if (Figcaptions != null)
            yield return Figcaptions;
        if (Mains != null)
            yield return Mains;
        if (Legends != null)
            yield return Legends;
        if (Asides != null)
            yield return Asides;
        if (Articles != null)
            yield return Articles;
        if (Fieldsets != null)
            yield return Fieldsets;
        if (Audios != null)
            yield return Audios;
        if (Videos != null)
            yield return Videos;
        if (IFrames != null)
            yield return IFrames;
        if (Hrs != null)
            yield return Hrs;
        if (Brs != null)
            yield return Brs;
        if (Details != null)
            yield return Details;
        if (Summaries != null)
            yield return Summaries;
        if (CardTexts != null)
            yield return CardTexts;
        if (CardTitles != null)
            yield return CardTitles;
        if (CardImgs != null)
            yield return CardImgs;
        if (CardSubtitles != null)
            yield return CardSubtitles;
        if (CardBodys != null)
            yield return CardBodys;
        if (CardHeaders != null)
            yield return CardHeaders;
        if (CardFooters != null)
            yield return CardFooters;
        if (AlertMessages != null)
            yield return AlertMessages;
        if (AlertDescriptions != null)
            yield return AlertDescriptions;
        if (ProgressBars != null)
            yield return ProgressBars;
        if (Callouts != null)
            yield return Callouts;
        if (Panels != null)
            yield return Panels;
        if (SelectItems != null)
            yield return SelectItems;
        if (SelectGroups != null)
            yield return SelectGroups;
        if (Selects != null)
            yield return Selects;
        if (Inputs != null)
            yield return Inputs;
        if (DateTimePickers != null)
            yield return DateTimePickers;
        if (LayoutHeaders != null)
            yield return LayoutHeaders;
        if (LayoutFooters != null)
            yield return LayoutFooters;
        if (LayoutContents != null)
            yield return LayoutContents;
        if (LayoutContainers != null)
            yield return LayoutContainers;
        if (LayoutSiders != null)
            yield return LayoutSiders;
        if (LayoutSiderContents != null)
            yield return LayoutSiderContents;
        if (OverlayContainers != null)
            yield return OverlayContainers;
        if (Tab != null)
            yield return Tab;
        if (Step != null)
            yield return Step;
        if (StepMarkers != null)
            yield return StepMarkers;
        if (StepCaptions != null)
            yield return StepCaptions;
        if (StepsContents != null)
            yield return StepsContents;
        if (Ths != null)
            yield return Ths;
        if (TableBottomBars != null)
            yield return TableBottomBars;
        if (TableTopBars != null)
            yield return TableTopBars;
        if (TableLefts != null)
            yield return TableLefts;
        if (TableRights != null)
            yield return TableRights;
        if (Tables != null)
            yield return Tables;
        if (TreeViews != null)
            yield return TreeViews;
        if (TreeViewNodes != null)
            yield return TreeViewNodes;
        if (TreeViewNodeContents != null)
            yield return TreeViewNodeContents;
        if (ValidationsContainers != null)
            yield return ValidationsContainers;
        // Note: Snackbar is not a ComponentOptions, so it's excluded
    }
}