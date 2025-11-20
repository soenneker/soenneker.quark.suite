namespace Soenneker.Quark;

/// <summary>
/// Container class for Bootstrap CSS variables.
/// Includes colors for basic Bootstrap variable overrides and component-specific variables.
/// </summary>
public sealed class BootstrapCssVariables
{
	/// <summary>
	/// Gets or sets the Bootstrap colors CSS variables.
	/// </summary>
    public BootstrapColorsCssVariables? Colors { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap typography CSS variables.
	/// </summary>
    public BootstrapTypographyCssVariables? Typography { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap spacing CSS variables.
	/// </summary>
    public BootstrapSpacingCssVariables? Spacing { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap borders CSS variables.
	/// </summary>
    public BootstrapBordersCssVariables? Borders { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap shadows CSS variables.
	/// </summary>
    public BootstrapShadowsCssVariables? Shadows { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap general CSS variables.
	/// </summary>
    public BootstrapGeneralCssVariables? General { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap breakpoints CSS variables.
	/// </summary>
    public BootstrapBreakpointsCssVariables? Breakpoints { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap navbar CSS variables.
	/// </summary>
    public BootstrapNavbarCssVariables? Navbar { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap link CSS variables.
	/// </summary>
    public BootstrapLinkCssVariables? Link { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap base button CSS variables.
	/// </summary>
    public BootstrapButtonCssVariables? Button { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap primary button CSS variables.
	/// </summary>
    public BootstrapPrimaryButtonCssVariables? PrimaryButton { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap secondary button CSS variables.
	/// </summary>
    public BootstrapSecondaryButtonCssVariables? SecondaryButton { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap success button CSS variables.
	/// </summary>
    public BootstrapSuccessButtonCssVariables? SuccessButton { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap danger button CSS variables.
	/// </summary>
    public BootstrapDangerButtonCssVariables? DangerButton { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap warning button CSS variables.
	/// </summary>
    public BootstrapWarningButtonCssVariables? WarningButton { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap info button CSS variables.
	/// </summary>
    public BootstrapInfoButtonCssVariables? InfoButton { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap light button CSS variables.
	/// </summary>
    public BootstrapLightButtonCssVariables? LightButton { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap dark button CSS variables.
	/// </summary>
    public BootstrapDarkButtonCssVariables? DarkButton { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap outline primary button CSS variables.
	/// </summary>
    public BootstrapOutlinePrimaryButtonCssVariables? OutlinePrimaryButton { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap outline secondary button CSS variables.
	/// </summary>
    public BootstrapOutlineSecondaryButtonCssVariables? OutlineSecondaryButton { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap outline success button CSS variables.
	/// </summary>
    public BootstrapOutlineSuccessButtonCssVariables? OutlineSuccessButton { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap outline danger button CSS variables.
	/// </summary>
    public BootstrapOutlineDangerButtonCssVariables? OutlineDangerButton { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap outline warning button CSS variables.
	/// </summary>
    public BootstrapOutlineWarningButtonCssVariables? OutlineWarningButton { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap outline info button CSS variables.
	/// </summary>
    public BootstrapOutlineInfoButtonCssVariables? OutlineInfoButton { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap outline light button CSS variables.
	/// </summary>
    public BootstrapOutlineLightButtonCssVariables? OutlineLightButton { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap outline dark button CSS variables.
	/// </summary>
    public BootstrapOutlineDarkButtonCssVariables? OutlineDarkButton { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap base alert CSS variables.
	/// </summary>
    public BootstrapAlertCssVariables? Alert { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap primary alert CSS variables.
	/// </summary>
    public BootstrapAlertPrimaryCssVariables? AlertPrimary { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap secondary alert CSS variables.
	/// </summary>
    public BootstrapAlertSecondaryCssVariables? AlertSecondary { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap success alert CSS variables.
	/// </summary>
    public BootstrapAlertSuccessCssVariables? AlertSuccess { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap danger alert CSS variables.
	/// </summary>
    public BootstrapAlertDangerCssVariables? AlertDanger { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap warning alert CSS variables.
	/// </summary>
    public BootstrapAlertWarningCssVariables? AlertWarning { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap info alert CSS variables.
	/// </summary>
    public BootstrapAlertInfoCssVariables? AlertInfo { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap light alert CSS variables.
	/// </summary>
    public BootstrapAlertLightCssVariables? AlertLight { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap dark alert CSS variables.
	/// </summary>
    public BootstrapAlertDarkCssVariables? AlertDark { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap base badge CSS variables.
	/// </summary>
    public BootstrapBadgeCssVariables? Badge { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap primary badge CSS variables.
	/// </summary>
    public BootstrapBadgePrimaryCssVariables? BadgePrimary { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap secondary badge CSS variables.
	/// </summary>
    public BootstrapBadgeSecondaryCssVariables? BadgeSecondary { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap success badge CSS variables.
	/// </summary>
    public BootstrapBadgeSuccessCssVariables? BadgeSuccess { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap danger badge CSS variables.
	/// </summary>
    public BootstrapBadgeDangerCssVariables? BadgeDanger { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap warning badge CSS variables.
	/// </summary>
    public BootstrapBadgeWarningCssVariables? BadgeWarning { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap info badge CSS variables.
	/// </summary>
    public BootstrapBadgeInfoCssVariables? BadgeInfo { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap light badge CSS variables.
	/// </summary>
    public BootstrapBadgeLightCssVariables? BadgeLight { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap dark badge CSS variables.
	/// </summary>
    public BootstrapBadgeDarkCssVariables? BadgeDark { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap card CSS variables.
	/// </summary>
    public BootstrapCardCssVariables? Card { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap forms CSS variables.
	/// </summary>
    public BootstrapFormsCssVariables? Forms { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap form control CSS variables.
	/// </summary>
    public BootstrapFormControlCssVariables? FormControl { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap form check input CSS variables.
	/// </summary>
    public BootstrapFormCheckInputCssVariables? FormCheckInput { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap form switch input CSS variables.
	/// </summary>
    public BootstrapFormSwitchInputCssVariables? FormSwitchInput { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap list group CSS variables.
	/// </summary>
    public BootstrapListGroupCssVariables? ListGroup { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap list group item CSS variables.
	/// </summary>
    public BootstrapListGroupItemCssVariables? ListGroupItem { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap nav link CSS variables.
	/// </summary>
    public BootstrapNavLinkCssVariables? NavLink { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap nav tabs CSS variables.
	/// </summary>
    public BootstrapNavTabsCssVariables? NavTabs { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap pagination CSS variables.
	/// </summary>
    public BootstrapPaginationCssVariables? Pagination { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap page link CSS variables.
	/// </summary>
    public BootstrapPageLinkCssVariables? PageLink { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap progress CSS variables.
	/// </summary>
    public BootstrapProgressCssVariables? Progress { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap progress bar CSS variables.
	/// </summary>
    public BootstrapProgressBarCssVariables? ProgressBar { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap modal CSS variables.
	/// </summary>
    public BootstrapModalCssVariables? Modal { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap modal content CSS variables.
	/// </summary>
    public BootstrapModalContentCssVariables? ModalContent { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap modal backdrop CSS variables.
	/// </summary>
    public BootstrapModalBackdropCssVariables? ModalBackdrop { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap offcanvas CSS variables.
	/// </summary>
    public BootstrapOffcanvasCssVariables? Offcanvas { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap offcanvas backdrop CSS variables.
	/// </summary>
    public BootstrapOffcanvasBackdropCssVariables? OffcanvasBackdrop { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap dropdown menu CSS variables.
	/// </summary>
    public BootstrapDropdownMenuCssVariables? DropdownMenu { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap dropdown item CSS variables.
	/// </summary>
    public BootstrapDropdownItemCssVariables? DropdownItem { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap dropdown toggle CSS variables.
	/// </summary>
    public BootstrapDropdownToggleCssVariables? DropdownToggle { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap breadcrumb CSS variables.
	/// </summary>
    public BootstrapBreadcrumbCssVariables? Breadcrumb { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap breadcrumb item CSS variables.
	/// </summary>
    public BootstrapBreadcrumbItemCssVariables? BreadcrumbItem { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap toast CSS variables.
	/// </summary>
    public BootstrapToastCssVariables? Toast { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap tooltip CSS variables.
	/// </summary>
    public BootstrapTooltipCssVariables? Tooltip { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap popover CSS variables.
	/// </summary>
    public BootstrapPopoverCssVariables? Popover { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap table CSS variables.
	/// </summary>
    public BootstrapTableCssVariables? Table { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap table striped CSS variables.
	/// </summary>
    public BootstrapTableStripedCssVariables? TableStriped { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap table hover CSS variables.
	/// </summary>
    public BootstrapTableHoverCssVariables? TableHover { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap accordion CSS variables.
	/// </summary>
    public BootstrapAccordionCssVariables? Accordion { get; set; }
	/// <summary>
	/// Gets or sets the Bootstrap accordion button CSS variables.
	/// </summary>
    public BootstrapAccordionButtonCssVariables? AccordionButton { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap carousel CSS variables.
	/// </summary>
    public BootstrapCarouselCssVariables? Carousel { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap close button CSS variables.
	/// </summary>
    public BootstrapCloseButtonCssVariables? CloseButton { get; set; }

	/// <summary>
	/// Gets or sets the Bootstrap spinner CSS variables.
	/// </summary>
    public BootstrapSpinnerCssVariables? Spinner { get; set; }

	/// <summary>
	/// Gets all CSS variable group instances in this BootstrapCssVariables without using reflection.
	/// </summary>
	/// <returns>An enumerable collection of CSS variable group instances.</returns>
    internal System.Collections.Generic.IEnumerable<IBootstrapCssVariableGroup> GetAllCssVariableGroups()
    {
        if (Colors != null)
            yield return Colors;
        if (Typography != null)
            yield return Typography;
        if (Spacing != null)
            yield return Spacing;
        if (Borders != null)
            yield return Borders;
        if (Shadows != null)
            yield return Shadows;
        if (General != null)
            yield return General;
        if (Breakpoints != null)
            yield return Breakpoints;
        if (Navbar != null)
            yield return Navbar;
        if (Link != null)
            yield return Link;
        if (Button != null)
            yield return Button;
        if (PrimaryButton != null)
            yield return PrimaryButton;
        if (SecondaryButton != null)
            yield return SecondaryButton;
        if (SuccessButton != null)
            yield return SuccessButton;
        if (DangerButton != null)
            yield return DangerButton;
        if (WarningButton != null)
            yield return WarningButton;
        if (InfoButton != null)
            yield return InfoButton;
        if (LightButton != null)
            yield return LightButton;
        if (DarkButton != null)
            yield return DarkButton;
        if (OutlinePrimaryButton != null)
            yield return OutlinePrimaryButton;
        if (OutlineSecondaryButton != null)
            yield return OutlineSecondaryButton;
        if (OutlineSuccessButton != null)
            yield return OutlineSuccessButton;
        if (OutlineDangerButton != null)
            yield return OutlineDangerButton;
        if (OutlineWarningButton != null)
            yield return OutlineWarningButton;
        if (OutlineInfoButton != null)
            yield return OutlineInfoButton;
        if (OutlineLightButton != null)
            yield return OutlineLightButton;
        if (OutlineDarkButton != null)
            yield return OutlineDarkButton;
        if (Alert != null)
            yield return Alert;
        if (AlertPrimary != null)
            yield return AlertPrimary;
        if (AlertSecondary != null)
            yield return AlertSecondary;
        if (AlertSuccess != null)
            yield return AlertSuccess;
        if (AlertDanger != null)
            yield return AlertDanger;
        if (AlertWarning != null)
            yield return AlertWarning;
        if (AlertInfo != null)
            yield return AlertInfo;
        if (AlertLight != null)
            yield return AlertLight;
        if (AlertDark != null)
            yield return AlertDark;
        if (Badge != null)
            yield return Badge;
        if (BadgePrimary != null)
            yield return BadgePrimary;
        if (BadgeSecondary != null)
            yield return BadgeSecondary;
        if (BadgeSuccess != null)
            yield return BadgeSuccess;
        if (BadgeDanger != null)
            yield return BadgeDanger;
        if (BadgeWarning != null)
            yield return BadgeWarning;
        if (BadgeInfo != null)
            yield return BadgeInfo;
        if (BadgeLight != null)
            yield return BadgeLight;
        if (BadgeDark != null)
            yield return BadgeDark;
        if (Card != null)
            yield return Card;
        if (Forms != null)
            yield return Forms;
        if (FormControl != null)
            yield return FormControl;
        if (FormCheckInput != null)
            yield return FormCheckInput;
        if (FormSwitchInput != null)
            yield return FormSwitchInput;
        if (ListGroup != null)
            yield return ListGroup;
        if (ListGroupItem != null)
            yield return ListGroupItem;
        if (NavLink != null)
            yield return NavLink;
        if (NavTabs != null)
            yield return NavTabs;
        if (Pagination != null)
            yield return Pagination;
        if (PageLink != null)
            yield return PageLink;
        if (Progress != null)
            yield return Progress;
        if (ProgressBar != null)
            yield return ProgressBar;
        if (Modal != null)
            yield return Modal;
        if (ModalContent != null)
            yield return ModalContent;
        if (ModalBackdrop != null)
            yield return ModalBackdrop;
        if (Offcanvas != null)
            yield return Offcanvas;
        if (OffcanvasBackdrop != null)
            yield return OffcanvasBackdrop;
        if (DropdownMenu != null)
            yield return DropdownMenu;
        if (DropdownItem != null)
            yield return DropdownItem;
        if (DropdownToggle != null)
            yield return DropdownToggle;
        if (Breadcrumb != null)
            yield return Breadcrumb;
        if (BreadcrumbItem != null)
            yield return BreadcrumbItem;
        if (Toast != null)
            yield return Toast;
        if (Tooltip != null)
            yield return Tooltip;
        if (Popover != null)
            yield return Popover;
        if (Table != null)
            yield return Table;
        if (TableStriped != null)
            yield return TableStriped;
        if (TableHover != null)
            yield return TableHover;
        if (Accordion != null)
            yield return Accordion;
        if (AccordionButton != null)
            yield return AccordionButton;
        if (Carousel != null)
            yield return Carousel;
        if (CloseButton != null)
            yield return CloseButton;
        if (Spinner != null)
            yield return Spinner;
    }
}