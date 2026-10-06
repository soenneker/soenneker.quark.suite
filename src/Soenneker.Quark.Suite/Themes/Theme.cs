using System;
using Soenneker.Utils.PooledStringBuilders;
using Soenneker.Quark.Tokens;

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
    /// Gets or sets the strongly typed shadcn and Tailwind theme tokens used to generate the shared theme fragment.
    /// </summary>
    public ThemeTokens Tokens { get; set; } = new();

    /// <summary>
    /// Gets or sets the alert component styling options.
    /// </summary>
    public AlertOptions? Alerts { get; set; }

    /// <summary>
    /// Gets or sets the anchor component styling options.
    /// </summary>
    public AnchorOptions? Anchors { get; set; }

    /// <summary>
    /// Gets or sets the badge component styling options.
    /// </summary>
    public BadgeOptions? Badges { get; set; }

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
    /// Gets or sets the date input component styling options.
    /// </summary>
    public DateInputOptions? DateInputs { get; set; }

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
    /// Gets or sets the memo input component styling options.
    /// </summary>
    public MemoInputOptions? MemoInputs { get; set; }

    /// <summary>
    /// Gets or sets the dialog component styling options.
    /// </summary>
    public DialogOptions? Dialogs { get; set; }

    /// <summary>
    /// Gets or sets the nav component styling options.
    /// </summary>
    public NavOptions? Navs { get; set; }

    /// <summary>
    /// Gets or sets the numeric input component styling options.
    /// </summary>
    public NumericInputOptions? NumericInputs { get; set; }

    /// <summary>
    /// Gets or sets the ordered list component styling options.
    /// </summary>
    public OrderedListOptions? OrderedLists { get; set; }

    /// <summary>
    /// Gets or sets the ordered list item component styling options.
    /// </summary>
    public OrderedListItemOptions? OrderedListItems { get; set; }

    /// <summary>
    /// Gets or sets the datatable-pagination component styling options.
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
    /// Gets or sets the section component styling options.
    /// </summary>
    public SectionOptions? Sections { get; set; }

    /// <summary>
    /// Gets or sets the slider component styling options.
    /// </summary>
    public SliderOptions? Sliders { get; set; }

    /// <summary>
    /// Gets or sets the Sonner toast styling options.
    /// </summary>
    public SonnerOptions? Sonners { get; set; }

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
    /// Gets or sets the text input component styling options.
    /// </summary>
    public TextInputOptions? TextInputs { get; set; }

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
    /// Gets or sets the data table search component styling options.
    /// </summary>
    public DataTableSearchOptions? DataTableSearches { get; set; }

    /// <summary>
    /// Gets or sets the data table pagination component styling options.
    /// </summary>
    public DataTablePaginationOptions? DataTablePaginations { get; set; }

    /// <summary>
    /// Gets or sets the data table page size selector component styling options.
    /// </summary>
    public DataTablePageSizeSelectorOptions? DataTablePageSizeSelectors { get; set; }

    /// <summary>
    /// Gets or sets the data table no data component styling options.
    /// </summary>
    public DataTableNoDataOptions? DataTableNoDatas { get; set; }

    /// <summary>
    /// Gets or sets the data table loader component styling options.
    /// </summary>
    public DataTableLoaderOptions? DataTableLoaders { get; set; }

    /// <summary>
    /// Gets or sets the data table info component styling options.
    /// </summary>
    public DataTableInfoOptions? DataTableInfos { get; set; }

    /// <summary>
    /// Gets or sets the table component styling options.
    /// </summary>
    public TableOptions? Tables { get; set; }

    // Sonner components
    /// <summary>
    /// Gets or sets the Sonner toaster styling options.
    /// </summary>
    public SonnerToasterOptions? SonnerToaster { get; set; }

    // Navigation components
    /// <summary>
    /// Gets or sets the tabs component styling options.
    /// </summary>
    public TabsOptions? Tabs { get; set; }

    /// <summary>
    /// Gets or sets the datatable-pagination link component styling options.
    /// </summary>
    public PaginationLinkOptions? PaginationLinks { get; set; }

    /// <summary>
    /// Gets or sets the datatable-pagination item component styling options.
    /// </summary>
    public PaginationItemOptions? PaginationItems { get; set; }

    /// <summary>
    /// Gets or sets the breadcrumb item component styling options.
    /// </summary>
    public BreadcrumbItemOptions? BreadcrumbItems { get; set; }

    // Dialog components
    /// <summary>
    /// Gets or sets the dialog title component styling options.
    /// </summary>
    public DialogTitleOptions? DialogTitles { get; set; }

    /// <summary>
    /// Gets or sets the dialog header component styling options.
    /// </summary>
    public DialogHeaderOptions? DialogHeaders { get; set; }

    /// <summary>
    /// Gets or sets the dialog footer component styling options.
    /// </summary>
    public DialogFooterOptions? DialogFooters { get; set; }

    /// <summary>
    /// Gets or sets the dialog content component styling options.
    /// </summary>
    public DialogContentOptions? DialogContents { get; set; }

    /// <summary>
    /// Gets or sets the dialog close button component styling options.
    /// </summary>
    public DialogCloseButtonOptions? DialogCloseButtons { get; set; }

    /// <summary>
    /// Gets or sets the dialog body component styling options.
    /// </summary>
    public DialogBodyOptions? DialogBodys { get; set; }

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
    /// Gets or sets the alert action component styling options.
    /// </summary>
    public AlertActionOptions? AlertActions { get; set; }

    /// <summary>
    /// Gets or sets the alert title component styling options.
    /// </summary>
    public AlertTitleOptions? AlertTitles { get; set; }

    /// <summary>
    /// Legacy alias for <see cref="AlertTitles"/>.
    /// </summary>
    [Obsolete("Use AlertTitles instead.")]
    public AlertMessageOptions? AlertMessages
    {
        get => AlertTitles as AlertMessageOptions;
        set => AlertTitles = value;
    }

    /// <summary>
    /// Gets or sets the alert description component styling options.
    /// </summary>
    public AlertDescriptionOptions? AlertDescriptions { get; set; }

    // Surface components
    /// <summary>
    /// Gets or sets the callout component styling options.
    /// </summary>
    public CalloutOptions? Callouts { get; set; }

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

    /// <summary>
    /// Gets or sets the overlay container component styling options.
    /// </summary>
    public OverlayContainerOptions? OverlayContainers { get; set; }

    // Navigation sub-components
    /// <summary>
    /// Gets or sets the tab component styling options.
    /// </summary>
    public TabOptions? Tab { get; set; }

    // Table sub-components
    /// <summary>
    /// Gets or sets the table header cell component styling options.
    /// </summary>
    public ThOptions? Ths { get; set; }

    /// <summary>
    /// Gets or sets the data table bottom bar component styling options.
    /// </summary>
    public DataTableBottomBarOptions? DataTableBottomBars { get; set; }

    /// <summary>
    /// Gets or sets the data table top bar component styling options.
    /// </summary>
    public DataTableTopBarOptions? DataTableTopBars { get; set; }

    /// <summary>
    /// Gets or sets the data table left component styling options.
    /// </summary>
    public DataTableLeftOptions? DataTableLefts { get; set; }

    /// <summary>
    /// Gets or sets the data table right component styling options.
    /// </summary>
    public DataTableRightOptions? DataTableRights { get; set; }

    /// <summary>
    /// Gets or sets the data table theme component styling options.
    /// </summary>
    public DataTableThemeOptions? DataTables { get; set; }

    // Collection components
    /// <summary>
    /// Gets or sets the tree component styling options.
    /// </summary>
    public TreeOptions? Trees { get; set; }

    /// <summary>
    /// Gets or sets the tree item component styling options.
    /// </summary>
    public TreeItemOptions? TreeItems { get; set; }

    /// <summary>
    /// Gets or sets the tree item label component styling options.
    /// </summary>
    public TreeItemLabelOptions? TreeItemLabels { get; set; }

    // Forms sub-components
    /// <summary>
    /// Gets or sets the validations container component styling options.
    /// </summary>
    public ValidationsContainerOptions? ValidationsContainers { get; set; }

    // Dialog/Overlay main components
    /// <summary>
    /// Gets or sets the Sonner toast styling options.
    /// </summary>
    public SonnerOptions? Sonner { get; set; }

    /// <summary>
    /// Gets all ComponentOptions instances in this theme without using reflection.
    /// </summary>
    /// <returns>An enumerable collection of all component options in this theme.</returns>
    internal void AppendComponentCss(ref PooledStringBuilder builder)
    {
        if (Alerts != null)
            ComponentCssGenerator.Append(ref builder, Alerts);
        if (Anchors != null)
            ComponentCssGenerator.Append(ref builder, Anchors);
        if (Badges != null)
            ComponentCssGenerator.Append(ref builder, Badges);
        if (Breadcrumbs != null)
            ComponentCssGenerator.Append(ref builder, Breadcrumbs);
        if (Buttons != null)
            ComponentCssGenerator.Append(ref builder, Buttons);
        if (ButtonGroups != null)
            ComponentCssGenerator.Append(ref builder, ButtonGroups);
        if (Cards != null)
            ComponentCssGenerator.Append(ref builder, Cards);
        if (Checks != null)
            ComponentCssGenerator.Append(ref builder, Checks);
        if (Codes != null)
            ComponentCssGenerator.Append(ref builder, Codes);
        if (Collapses != null)
            ComponentCssGenerator.Append(ref builder, Collapses);
        if (Columns != null)
            ComponentCssGenerator.Append(ref builder, Columns);
        if (Containers != null)
            ComponentCssGenerator.Append(ref builder, Containers);
        if (DateInputs != null)
            ComponentCssGenerator.Append(ref builder, DateInputs);
        if (Datepickers != null)
            ComponentCssGenerator.Append(ref builder, Datepickers);
        if (Divs != null)
            ComponentCssGenerator.Append(ref builder, Divs);
        if (Fields != null)
            ComponentCssGenerator.Append(ref builder, Fields);
        if (Headings != null)
            ComponentCssGenerator.Append(ref builder, Headings);
        if (Icons != null)
            ComponentCssGenerator.Append(ref builder, Icons);
        if (Images != null)
            ComponentCssGenerator.Append(ref builder, Images);
        if (Labels != null)
            ComponentCssGenerator.Append(ref builder, Labels);
        if (MemoInputs != null)
            ComponentCssGenerator.Append(ref builder, MemoInputs);
        if (Dialogs != null)
            ComponentCssGenerator.Append(ref builder, Dialogs);
        if (Navs != null)
            ComponentCssGenerator.Append(ref builder, Navs);
        if (NumericInputs != null)
            ComponentCssGenerator.Append(ref builder, NumericInputs);
        if (OrderedLists != null)
            ComponentCssGenerator.Append(ref builder, OrderedLists);
        if (OrderedListItems != null)
            ComponentCssGenerator.Append(ref builder, OrderedListItems);
        if (Paginations != null)
            ComponentCssGenerator.Append(ref builder, Paginations);
        if (Paragraphs != null)
            ComponentCssGenerator.Append(ref builder, Paragraphs);
        if (Progresses != null)
            ComponentCssGenerator.Append(ref builder, Progresses);
        if (Radios != null)
            ComponentCssGenerator.Append(ref builder, Radios);
        if (Sections != null)
            ComponentCssGenerator.Append(ref builder, Sections);
        if (Sliders != null)
            ComponentCssGenerator.Append(ref builder, Sliders);
        if (Sonners != null)
            ComponentCssGenerator.Append(ref builder, Sonners);
        if (Spans != null)
            ComponentCssGenerator.Append(ref builder, Spans);
        if (Strongs != null)
            ComponentCssGenerator.Append(ref builder, Strongs);
        if (Switches != null)
            ComponentCssGenerator.Append(ref builder, Switches);
        if (TextInputs != null)
            ComponentCssGenerator.Append(ref builder, TextInputs);
        if (Texts != null)
            ComponentCssGenerator.Append(ref builder, Texts);
        if (UnorderedLists != null)
            ComponentCssGenerator.Append(ref builder, UnorderedLists);
        if (UnorderedListItems != null)
            ComponentCssGenerator.Append(ref builder, UnorderedListItems);
        if (Dropdowns != null)
            ComponentCssGenerator.Append(ref builder, Dropdowns);
        if (DropdownToggles != null)
            ComponentCssGenerator.Append(ref builder, DropdownToggles);
        if (DropdownMenus != null)
            ComponentCssGenerator.Append(ref builder, DropdownMenus);
        if (DropdownItems != null)
            ComponentCssGenerator.Append(ref builder, DropdownItems);
        if (DropdownDividers != null)
            ComponentCssGenerator.Append(ref builder, DropdownDividers);
        if (ValidationSuccess != null)
            ComponentCssGenerator.Append(ref builder, ValidationSuccess);
        if (Validations != null)
            ComponentCssGenerator.Append(ref builder, Validations);
        if (ValidationErrors != null)
            ComponentCssGenerator.Append(ref builder, ValidationErrors);
        if (ValidationError != null)
            ComponentCssGenerator.Append(ref builder, ValidationError);
        if (Trs != null)
            ComponentCssGenerator.Append(ref builder, Trs);
        if (Theads != null)
            ComponentCssGenerator.Append(ref builder, Theads);
        if (Tds != null)
            ComponentCssGenerator.Append(ref builder, Tds);
        if (Tbodys != null)
            ComponentCssGenerator.Append(ref builder, Tbodys);
        if (DataTableSearches != null)
            ComponentCssGenerator.Append(ref builder, DataTableSearches);
        if (DataTablePaginations != null)
            ComponentCssGenerator.Append(ref builder, DataTablePaginations);
        if (DataTablePageSizeSelectors != null)
            ComponentCssGenerator.Append(ref builder, DataTablePageSizeSelectors);
        if (DataTableNoDatas != null)
            ComponentCssGenerator.Append(ref builder, DataTableNoDatas);
        if (DataTableLoaders != null)
            ComponentCssGenerator.Append(ref builder, DataTableLoaders);
        if (DataTableInfos != null)
            ComponentCssGenerator.Append(ref builder, DataTableInfos);
        if (Tables != null)
            ComponentCssGenerator.Append(ref builder, Tables);
        if (SonnerToaster != null)
            ComponentCssGenerator.Append(ref builder, SonnerToaster);
        if (Tabs != null)
            ComponentCssGenerator.Append(ref builder, Tabs);
        if (PaginationLinks != null)
            ComponentCssGenerator.Append(ref builder, PaginationLinks);
        if (PaginationItems != null)
            ComponentCssGenerator.Append(ref builder, PaginationItems);
        if (BreadcrumbItems != null)
            ComponentCssGenerator.Append(ref builder, BreadcrumbItems);
        if (DialogTitles != null)
            ComponentCssGenerator.Append(ref builder, DialogTitles);
        if (DialogHeaders != null)
            ComponentCssGenerator.Append(ref builder, DialogHeaders);
        if (DialogFooters != null)
            ComponentCssGenerator.Append(ref builder, DialogFooters);
        if (DialogContents != null)
            ComponentCssGenerator.Append(ref builder, DialogContents);
        if (DialogCloseButtons != null)
            ComponentCssGenerator.Append(ref builder, DialogCloseButtons);
        if (DialogBodys != null)
            ComponentCssGenerator.Append(ref builder, DialogBodys);
        if (FieldLabels != null)
            ComponentCssGenerator.Append(ref builder, FieldLabels);
        if (FieldHelps != null)
            ComponentCssGenerator.Append(ref builder, FieldHelps);
        if (FieldBodys != null)
            ComponentCssGenerator.Append(ref builder, FieldBodys);
        if (Smalls != null)
            ComponentCssGenerator.Append(ref builder, Smalls);
        if (PreCodes != null)
            ComponentCssGenerator.Append(ref builder, PreCodes);
        if (H1S != null)
            ComponentCssGenerator.Append(ref builder, H1S);
        if (H2S != null)
            ComponentCssGenerator.Append(ref builder, H2S);
        if (H3S != null)
            ComponentCssGenerator.Append(ref builder, H3S);
        if (H4S != null)
            ComponentCssGenerator.Append(ref builder, H4S);
        if (H5S != null)
            ComponentCssGenerator.Append(ref builder, H5S);
        if (H6S != null)
            ComponentCssGenerator.Append(ref builder, H6S);
        if (Blockquotes != null)
            ComponentCssGenerator.Append(ref builder, Blockquotes);
        if (Leads != null)
            ComponentCssGenerator.Append(ref builder, Leads);
        if (Muteds != null)
            ComponentCssGenerator.Append(ref builder, Muteds);
        if (Kbds != null)
            ComponentCssGenerator.Append(ref builder, Kbds);
        if (FormTexts != null)
            ComponentCssGenerator.Append(ref builder, FormTexts);
        if (KbdChips != null)
            ComponentCssGenerator.Append(ref builder, KbdChips);
        if (Pills != null)
            ComponentCssGenerator.Append(ref builder, Pills);
        if (CodeChips != null)
            ComponentCssGenerator.Append(ref builder, CodeChips);
        if (Figures != null)
            ComponentCssGenerator.Append(ref builder, Figures);
        if (Figcaptions != null)
            ComponentCssGenerator.Append(ref builder, Figcaptions);
        if (Mains != null)
            ComponentCssGenerator.Append(ref builder, Mains);
        if (Legends != null)
            ComponentCssGenerator.Append(ref builder, Legends);
        if (Asides != null)
            ComponentCssGenerator.Append(ref builder, Asides);
        if (Articles != null)
            ComponentCssGenerator.Append(ref builder, Articles);
        if (Fieldsets != null)
            ComponentCssGenerator.Append(ref builder, Fieldsets);
        if (Audios != null)
            ComponentCssGenerator.Append(ref builder, Audios);
        if (Videos != null)
            ComponentCssGenerator.Append(ref builder, Videos);
        if (IFrames != null)
            ComponentCssGenerator.Append(ref builder, IFrames);
        if (Hrs != null)
            ComponentCssGenerator.Append(ref builder, Hrs);
        if (Brs != null)
            ComponentCssGenerator.Append(ref builder, Brs);
        if (Details != null)
            ComponentCssGenerator.Append(ref builder, Details);
        if (Summaries != null)
            ComponentCssGenerator.Append(ref builder, Summaries);
        if (CardTexts != null)
            ComponentCssGenerator.Append(ref builder, CardTexts);
        if (CardTitles != null)
            ComponentCssGenerator.Append(ref builder, CardTitles);
        if (CardImgs != null)
            ComponentCssGenerator.Append(ref builder, CardImgs);
        if (CardSubtitles != null)
            ComponentCssGenerator.Append(ref builder, CardSubtitles);
        if (CardBodys != null)
            ComponentCssGenerator.Append(ref builder, CardBodys);
        if (CardHeaders != null)
            ComponentCssGenerator.Append(ref builder, CardHeaders);
        if (CardFooters != null)
            ComponentCssGenerator.Append(ref builder, CardFooters);
        if (AlertActions != null)
            ComponentCssGenerator.Append(ref builder, AlertActions);
        if (AlertTitles != null)
            ComponentCssGenerator.Append(ref builder, AlertTitles);
        if (AlertDescriptions != null)
            ComponentCssGenerator.Append(ref builder, AlertDescriptions);
        if (Callouts != null)
            ComponentCssGenerator.Append(ref builder, Callouts);
        if (SelectItems != null)
            ComponentCssGenerator.Append(ref builder, SelectItems);
        if (SelectGroups != null)
            ComponentCssGenerator.Append(ref builder, SelectGroups);
        if (Selects != null)
            ComponentCssGenerator.Append(ref builder, Selects);
        if (Inputs != null)
            ComponentCssGenerator.Append(ref builder, Inputs);
        if (DateTimePickers != null)
            ComponentCssGenerator.Append(ref builder, DateTimePickers);
        if (OverlayContainers != null)
            ComponentCssGenerator.Append(ref builder, OverlayContainers);
        if (Sonner != null)
            ComponentCssGenerator.Append(ref builder, Sonner);
        if (Tab != null)
            ComponentCssGenerator.Append(ref builder, Tab);
        if (Ths != null)
            ComponentCssGenerator.Append(ref builder, Ths);
        if (DataTableBottomBars != null)
            ComponentCssGenerator.Append(ref builder, DataTableBottomBars);
        if (DataTableTopBars != null)
            ComponentCssGenerator.Append(ref builder, DataTableTopBars);
        if (DataTableLefts != null)
            ComponentCssGenerator.Append(ref builder, DataTableLefts);
        if (DataTableRights != null)
            ComponentCssGenerator.Append(ref builder, DataTableRights);
        if (DataTables != null)
            ComponentCssGenerator.Append(ref builder, DataTables);
        if (Trees != null)
            ComponentCssGenerator.Append(ref builder, Trees);
        if (TreeItems != null)
            ComponentCssGenerator.Append(ref builder, TreeItems);
        if (TreeItemLabels != null)
            ComponentCssGenerator.Append(ref builder, TreeItemLabels);
        if (ValidationsContainers != null)
            ComponentCssGenerator.Append(ref builder, ValidationsContainers);
    }
}
