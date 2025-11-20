using System.Collections.Generic;

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

    /// <summary>Gets all ComponentOptions instances in this theme without using reflection.</summary>
    internal IEnumerable<ComponentOptions> GetAllComponentOptions()
    {
        if (Alerts != null) yield return Alerts;
        if (Accordions != null) yield return Accordions;
        if (AccordionItems != null) yield return AccordionItems;
        if (AccordionHeaders != null) yield return AccordionHeaders;
        if (AccordionBodys != null) yield return AccordionBodys;
        if (Anchors != null) yield return Anchors;
        if (Badges != null) yield return Badges;
        if (Bars != null) yield return Bars;
        if (Breadcrumbs != null) yield return Breadcrumbs;
        if (Buttons != null) yield return Buttons;
        if (ButtonGroups != null) yield return ButtonGroups;
        if (Cards != null) yield return Cards;
        if (Checks != null) yield return Checks;
        if (Codes != null) yield return Codes;
        if (Collapses != null) yield return Collapses;
        if (Columns != null) yield return Columns;
        if (Containers != null) yield return Containers;
        if (DateEdits != null) yield return DateEdits;
        if (Datepickers != null) yield return Datepickers;
        if (Divs != null) yield return Divs;
        if (Fields != null) yield return Fields;
        if (Headings != null) yield return Headings;
        if (Icons != null) yield return Icons;
        if (Images != null) yield return Images;
        if (Labels != null) yield return Labels;
        if (Layouts != null) yield return Layouts;
        if (ListGroups != null) yield return ListGroups;
        if (ListGroupItems != null) yield return ListGroupItems;
        if (MemoEdits != null) yield return MemoEdits;
        if (Modals != null) yield return Modals;
        if (Navs != null) yield return Navs;
        if (NumericEdits != null) yield return NumericEdits;
        if (Offcanvases != null) yield return Offcanvases;
        if (OrderedLists != null) yield return OrderedLists;
        if (OrderedListItems != null) yield return OrderedListItems;
        if (Paginations != null) yield return Paginations;
        if (Paragraphs != null) yield return Paragraphs;
        if (Progresses != null) yield return Progresses;
        if (Radios != null) yield return Radios;
        if (Rows != null) yield return Rows;
        if (Sections != null) yield return Sections;
        if (Sliders != null) yield return Sliders;
        if (Snackbars != null) yield return Snackbars;
        if (Spans != null) yield return Spans;
        if (Strongs != null) yield return Strongs;
        if (Switches != null) yield return Switches;
        if (TextEdits != null) yield return TextEdits;
        if (Texts != null) yield return Texts;
        if (UnorderedLists != null) yield return UnorderedLists;
        if (UnorderedListItems != null) yield return UnorderedListItems;
        if (Dropdowns != null) yield return Dropdowns;
        if (DropdownToggles != null) yield return DropdownToggles;
        if (DropdownMenus != null) yield return DropdownMenus;
        if (DropdownItems != null) yield return DropdownItems;
        if (DropdownDividers != null) yield return DropdownDividers;
        if (ValidationSuccess != null) yield return ValidationSuccess;
        if (Validations != null) yield return Validations;
        if (ValidationErrors != null) yield return ValidationErrors;
        if (ValidationError != null) yield return ValidationError;
        if (Trs != null) yield return Trs;
        if (Theads != null) yield return Theads;
        if (Tds != null) yield return Tds;
        if (Tbodys != null) yield return Tbodys;
        if (TableSearches != null) yield return TableSearches;
        if (TablePaginations != null) yield return TablePaginations;
        if (TablePageSizeSelectors != null) yield return TablePageSizeSelectors;
        if (TableNoDatas != null) yield return TableNoDatas;
        if (TableLoaders != null) yield return TableLoaders;
        if (TableInfos != null) yield return TableInfos;
        if (TableElements != null) yield return TableElements;
        if (SnackbarHeaders != null) yield return SnackbarHeaders;
        if (SnackbarFooters != null) yield return SnackbarFooters;
        if (SnackbarBodys != null) yield return SnackbarBodys;
        if (SnackbarActions != null) yield return SnackbarActions;
        if (SnackbarStacks != null) yield return SnackbarStacks;
        if (Tabs != null) yield return Tabs;
        if (Steps != null) yield return Steps;
        if (StepPanels != null) yield return StepPanels;
        if (StepContents != null) yield return StepContents;
        if (PaginationLinks != null) yield return PaginationLinks;
        if (PaginationItems != null) yield return PaginationItems;
        if (BreadcrumbItems != null) yield return BreadcrumbItems;
        if (OffcanvasHeaders != null) yield return OffcanvasHeaders;
        if (OffcanvasBodys != null) yield return OffcanvasBodys;
        if (ModalTitles != null) yield return ModalTitles;
        if (ModalHeaders != null) yield return ModalHeaders;
        if (ModalFooters != null) yield return ModalFooters;
        if (ModalContents != null) yield return ModalContents;
        if (ModalCloseButtons != null) yield return ModalCloseButtons;
        if (ModalBodys != null) yield return ModalBodys;
        if (FieldLabels != null) yield return FieldLabels;
        if (FieldHelps != null) yield return FieldHelps;
        if (FieldBodys != null) yield return FieldBodys;
        if (Smalls != null) yield return Smalls;
        if (PreCodes != null) yield return PreCodes;
        if (BarTogglers != null) yield return BarTogglers;
        if (BarStarts != null) yield return BarStarts;
        if (BarMenus != null) yield return BarMenus;
        if (BarLinks != null) yield return BarLinks;
        if (BarLabels != null) yield return BarLabels;
        if (BarItems != null) yield return BarItems;
        if (BarIcons != null) yield return BarIcons;
        if (BarEnds != null) yield return BarEnds;
        if (BarDropdownToggles != null) yield return BarDropdownToggles;
        if (BarDropdowns != null) yield return BarDropdowns;
        if (BarDropdownMenus != null) yield return BarDropdownMenus;
        if (BarDropdownItems != null) yield return BarDropdownItems;
        if (BarDropdownDividers != null) yield return BarDropdownDividers;
        if (BarBrands != null) yield return BarBrands;
        if (H1s != null) yield return H1s;
        if (H2s != null) yield return H2s;
        if (H3s != null) yield return H3s;
        if (H4s != null) yield return H4s;
        if (H5s != null) yield return H5s;
        if (H6s != null) yield return H6s;
        if (Blockquotes != null) yield return Blockquotes;
        if (Leads != null) yield return Leads;
        if (Muteds != null) yield return Muteds;
        if (Kbds != null) yield return Kbds;
        if (FormTexts != null) yield return FormTexts;
        if (KbdChips != null) yield return KbdChips;
        if (Pills != null) yield return Pills;
        if (CodeChips != null) yield return CodeChips;
        if (Figures != null) yield return Figures;
        if (Figcaptions != null) yield return Figcaptions;
        if (Mains != null) yield return Mains;
        if (Legends != null) yield return Legends;
        if (Asides != null) yield return Asides;
        if (Articles != null) yield return Articles;
        if (Fieldsets != null) yield return Fieldsets;
        if (Audios != null) yield return Audios;
        if (Videos != null) yield return Videos;
        if (IFrames != null) yield return IFrames;
        if (Hrs != null) yield return Hrs;
        if (Brs != null) yield return Brs;
        if (Details != null) yield return Details;
        if (Summaries != null) yield return Summaries;
        if (CardTexts != null) yield return CardTexts;
        if (CardTitles != null) yield return CardTitles;
        if (CardImgs != null) yield return CardImgs;
        if (CardSubtitles != null) yield return CardSubtitles;
        if (CardBodys != null) yield return CardBodys;
        if (CardHeaders != null) yield return CardHeaders;
        if (CardFooters != null) yield return CardFooters;
        if (AlertMessages != null) yield return AlertMessages;
        if (AlertDescriptions != null) yield return AlertDescriptions;
        if (ProgressBars != null) yield return ProgressBars;
        if (Callouts != null) yield return Callouts;
        if (Panels != null) yield return Panels;
        if (SelectItems != null) yield return SelectItems;
        if (SelectGroups != null) yield return SelectGroups;
        if (Selects != null) yield return Selects;
        if (Inputs != null) yield return Inputs;
        if (DateTimePickers != null) yield return DateTimePickers;
        if (LayoutHeaders != null) yield return LayoutHeaders;
        if (LayoutFooters != null) yield return LayoutFooters;
        if (LayoutContents != null) yield return LayoutContents;
        if (LayoutContainers != null) yield return LayoutContainers;
        if (LayoutSiders != null) yield return LayoutSiders;
        if (LayoutSiderContents != null) yield return LayoutSiderContents;
        if (OverlayContainers != null) yield return OverlayContainers;
        if (Tab != null) yield return Tab;
        if (Step != null) yield return Step;
        if (StepMarkers != null) yield return StepMarkers;
        if (StepCaptions != null) yield return StepCaptions;
        if (StepsContents != null) yield return StepsContents;
        if (Ths != null) yield return Ths;
        if (TableBottomBars != null) yield return TableBottomBars;
        if (TableTopBars != null) yield return TableTopBars;
        if (TableLefts != null) yield return TableLefts;
        if (TableRights != null) yield return TableRights;
        if (Tables != null) yield return Tables;
        if (TreeViews != null) yield return TreeViews;
        if (TreeViewNodes != null) yield return TreeViewNodes;
        if (TreeViewNodeContents != null) yield return TreeViewNodeContents;
        if (ValidationsContainers != null) yield return ValidationsContainers;
        // Note: Snackbar is not a ComponentOptions, so it's excluded
    }
}
