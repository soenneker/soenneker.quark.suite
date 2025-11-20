namespace Soenneker.Quark;

/// <summary>
/// Container class for Bootstrap CSS variables.
/// Includes colors for basic Bootstrap variable overrides and component-specific variables.
/// </summary>
public sealed class BootstrapCssVariables
{
    public BootstrapColorsCssVariables? Colors { get; set; }

    public BootstrapTypographyCssVariables? Typography { get; set; }

    public BootstrapSpacingCssVariables? Spacing { get; set; }

    public BootstrapBordersCssVariables? Borders { get; set; }

    public BootstrapShadowsCssVariables? Shadows { get; set; }

    public BootstrapGeneralCssVariables? General { get; set; }

    public BootstrapBreakpointsCssVariables? Breakpoints { get; set; }

    public BootstrapNavbarCssVariables? Navbar { get; set; }

    public BootstrapLinkCssVariables? Link { get; set; }

    public BootstrapButtonCssVariables? Button { get; set; }

    public BootstrapPrimaryButtonCssVariables? PrimaryButton { get; set; }
    public BootstrapSecondaryButtonCssVariables? SecondaryButton { get; set; }
    public BootstrapSuccessButtonCssVariables? SuccessButton { get; set; }
    public BootstrapDangerButtonCssVariables? DangerButton { get; set; }
    public BootstrapWarningButtonCssVariables? WarningButton { get; set; }
    public BootstrapInfoButtonCssVariables? InfoButton { get; set; }
    public BootstrapLightButtonCssVariables? LightButton { get; set; }
    public BootstrapDarkButtonCssVariables? DarkButton { get; set; }

    public BootstrapOutlinePrimaryButtonCssVariables? OutlinePrimaryButton { get; set; }
    public BootstrapOutlineSecondaryButtonCssVariables? OutlineSecondaryButton { get; set; }
    public BootstrapOutlineSuccessButtonCssVariables? OutlineSuccessButton { get; set; }
    public BootstrapOutlineDangerButtonCssVariables? OutlineDangerButton { get; set; }
    public BootstrapOutlineWarningButtonCssVariables? OutlineWarningButton { get; set; }
    public BootstrapOutlineInfoButtonCssVariables? OutlineInfoButton { get; set; }
    public BootstrapOutlineLightButtonCssVariables? OutlineLightButton { get; set; }
    public BootstrapOutlineDarkButtonCssVariables? OutlineDarkButton { get; set; }

    // Alerts
    public BootstrapAlertCssVariables? Alert { get; set; }
    public BootstrapAlertPrimaryCssVariables? AlertPrimary { get; set; }
    public BootstrapAlertSecondaryCssVariables? AlertSecondary { get; set; }
    public BootstrapAlertSuccessCssVariables? AlertSuccess { get; set; }
    public BootstrapAlertDangerCssVariables? AlertDanger { get; set; }
    public BootstrapAlertWarningCssVariables? AlertWarning { get; set; }
    public BootstrapAlertInfoCssVariables? AlertInfo { get; set; }
    public BootstrapAlertLightCssVariables? AlertLight { get; set; }
    public BootstrapAlertDarkCssVariables? AlertDark { get; set; }

    // Badges
    public BootstrapBadgeCssVariables? Badge { get; set; }
    public BootstrapBadgePrimaryCssVariables? BadgePrimary { get; set; }
    public BootstrapBadgeSecondaryCssVariables? BadgeSecondary { get; set; }
    public BootstrapBadgeSuccessCssVariables? BadgeSuccess { get; set; }
    public BootstrapBadgeDangerCssVariables? BadgeDanger { get; set; }
    public BootstrapBadgeWarningCssVariables? BadgeWarning { get; set; }
    public BootstrapBadgeInfoCssVariables? BadgeInfo { get; set; }
    public BootstrapBadgeLightCssVariables? BadgeLight { get; set; }
    public BootstrapBadgeDarkCssVariables? BadgeDark { get; set; }

    // Cards
    public BootstrapCardCssVariables? Card { get; set; }

    // Forms
    public BootstrapFormsCssVariables? Forms { get; set; }
    public BootstrapFormControlCssVariables? FormControl { get; set; }
    public BootstrapFormCheckInputCssVariables? FormCheckInput { get; set; }
    public BootstrapFormSwitchInputCssVariables? FormSwitchInput { get; set; }

    // List group
    public BootstrapListGroupCssVariables? ListGroup { get; set; }
    public BootstrapListGroupItemCssVariables? ListGroupItem { get; set; }

    // Navs & Tabs
    public BootstrapNavLinkCssVariables? NavLink { get; set; }
    public BootstrapNavTabsCssVariables? NavTabs { get; set; }

    // Pagination
    public BootstrapPaginationCssVariables? Pagination { get; set; }
    public BootstrapPageLinkCssVariables? PageLink { get; set; }

    // Progress
    public BootstrapProgressCssVariables? Progress { get; set; }
    public BootstrapProgressBarCssVariables? ProgressBar { get; set; }

    // Modals
    public BootstrapModalCssVariables? Modal { get; set; }
    public BootstrapModalContentCssVariables? ModalContent { get; set; }
    public BootstrapModalBackdropCssVariables? ModalBackdrop { get; set; }

    // Offcanvas
    public BootstrapOffcanvasCssVariables? Offcanvas { get; set; }
    public BootstrapOffcanvasBackdropCssVariables? OffcanvasBackdrop { get; set; }

    // Dropdowns
    public BootstrapDropdownMenuCssVariables? DropdownMenu { get; set; }
    public BootstrapDropdownItemCssVariables? DropdownItem { get; set; }
    public BootstrapDropdownToggleCssVariables? DropdownToggle { get; set; }

    // Breadcrumbs
    public BootstrapBreadcrumbCssVariables? Breadcrumb { get; set; }
    public BootstrapBreadcrumbItemCssVariables? BreadcrumbItem { get; set; }

    // Toasts
    public BootstrapToastCssVariables? Toast { get; set; }

    // Tooltip & Popover
    public BootstrapTooltipCssVariables? Tooltip { get; set; }
    public BootstrapPopoverCssVariables? Popover { get; set; }

    // Tables
    public BootstrapTableCssVariables? Table { get; set; }
    public BootstrapTableStripedCssVariables? TableStriped { get; set; }
    public BootstrapTableHoverCssVariables? TableHover { get; set; }

    // Accordion
    public BootstrapAccordionCssVariables? Accordion { get; set; }
    public BootstrapAccordionButtonCssVariables? AccordionButton { get; set; }

    // Carousel
    public BootstrapCarouselCssVariables? Carousel { get; set; }

    // Close Button
    public BootstrapCloseButtonCssVariables? CloseButton { get; set; }

    // Spinner
    public BootstrapSpinnerCssVariables? Spinner { get; set; }

    /// <summary>Gets all CSS variable group instances in this BootstrapCssVariables without using reflection.</summary>
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