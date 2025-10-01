
namespace Soenneker.Quark;

/// <summary>
/// Container class for Bootstrap CSS variables.
/// Includes colors for basic Bootstrap variable overrides and component-specific variables.
/// </summary>
public class BootstrapCssVariables
{
    public BootstrapColorsCssVariables Colors { get; set; } = new();

    public BootstrapButtonCssVariables Button { get; set; } = new();

    public BootstrapPrimaryButtonCssVariables PrimaryButton { get; set; } = new();
    public BootstrapSecondaryButtonCssVariables SecondaryButton { get; set; } = new();
    public BootstrapSuccessButtonCssVariables SuccessButton { get; set; } = new();
    public BootstrapDangerButtonCssVariables DangerButton { get; set; } = new();
    public BootstrapWarningButtonCssVariables WarningButton { get; set; } = new();
    public BootstrapInfoButtonCssVariables InfoButton { get; set; } = new();
    public BootstrapLightButtonCssVariables LightButton { get; set; } = new();
    public BootstrapDarkButtonCssVariables DarkButton { get; set; } = new();

    public BootstrapOutlinePrimaryButtonCssVariables OutlinePrimaryButton { get; set; } = new();
    public BootstrapOutlineSecondaryButtonCssVariables OutlineSecondaryButton { get; set; } = new();
    public BootstrapOutlineSuccessButtonCssVariables OutlineSuccessButton { get; set; } = new();
    public BootstrapOutlineDangerButtonCssVariables OutlineDangerButton { get; set; } = new();
    public BootstrapOutlineWarningButtonCssVariables OutlineWarningButton { get; set; } = new();
    public BootstrapOutlineInfoButtonCssVariables OutlineInfoButton { get; set; } = new();
    public BootstrapOutlineLightButtonCssVariables OutlineLightButton { get; set; } = new();
    public BootstrapOutlineDarkButtonCssVariables OutlineDarkButton { get; set; } = new();

    // Alerts
    public BootstrapAlertCssVariables Alert { get; set; } = new();
    public BootstrapAlertPrimaryCssVariables AlertPrimary { get; set; } = new();
    public BootstrapAlertSecondaryCssVariables AlertSecondary { get; set; } = new();
    public BootstrapAlertSuccessCssVariables AlertSuccess { get; set; } = new();
    public BootstrapAlertDangerCssVariables AlertDanger { get; set; } = new();
    public BootstrapAlertWarningCssVariables AlertWarning { get; set; } = new();
    public BootstrapAlertInfoCssVariables AlertInfo { get; set; } = new();
    public BootstrapAlertLightCssVariables AlertLight { get; set; } = new();
    public BootstrapAlertDarkCssVariables AlertDark { get; set; } = new();

    // Badges
    public BootstrapBadgeCssVariables Badge { get; set; } = new();
    public BootstrapBadgePrimaryCssVariables BadgePrimary { get; set; } = new();
    public BootstrapBadgeSecondaryCssVariables BadgeSecondary { get; set; } = new();
    public BootstrapBadgeSuccessCssVariables BadgeSuccess { get; set; } = new();
    public BootstrapBadgeDangerCssVariables BadgeDanger { get; set; } = new();
    public BootstrapBadgeWarningCssVariables BadgeWarning { get; set; } = new();
    public BootstrapBadgeInfoCssVariables BadgeInfo { get; set; } = new();
    public BootstrapBadgeLightCssVariables BadgeLight { get; set; } = new();
    public BootstrapBadgeDarkCssVariables BadgeDark { get; set; } = new();

    // Cards
    public BootstrapCardCssVariables Card { get; set; } = new();

    // Forms
    public BootstrapFormControlCssVariables FormControl { get; set; } = new();
    public BootstrapFormCheckInputCssVariables FormCheckInput { get; set; } = new();
    public BootstrapFormSwitchInputCssVariables FormSwitchInput { get; set; } = new();

    // List group
    public BootstrapListGroupCssVariables ListGroup { get; set; } = new();
    public BootstrapListGroupItemCssVariables ListGroupItem { get; set; } = new();

    // Navs & Tabs
    public BootstrapNavLinkCssVariables NavLink { get; set; } = new();
    public BootstrapNavTabsCssVariables NavTabs { get; set; } = new();

    // Pagination
    public BootstrapPaginationCssVariables Pagination { get; set; } = new();
    public BootstrapPageLinkCssVariables PageLink { get; set; } = new();

    // Progress
    public BootstrapProgressCssVariables Progress { get; set; } = new();
    public BootstrapProgressBarCssVariables ProgressBar { get; set; } = new();

    // Modals
    public BootstrapModalCssVariables Modal { get; set; } = new();
    public BootstrapModalContentCssVariables ModalContent { get; set; } = new();

    // Offcanvas
    public BootstrapOffcanvasCssVariables Offcanvas { get; set; } = new();

    // Dropdowns
    public BootstrapDropdownMenuCssVariables DropdownMenu { get; set; } = new();
    public BootstrapDropdownItemCssVariables DropdownItem { get; set; } = new();
    public BootstrapDropdownToggleCssVariables DropdownToggle { get; set; } = new();

    // Breadcrumbs
    public BootstrapBreadcrumbItemCssVariables BreadcrumbItem { get; set; } = new();

    // Toasts
    public BootstrapToastCssVariables Toast { get; set; } = new();

    // Tooltip & Popover
    public BootstrapTooltipCssVariables Tooltip { get; set; } = new();
    public BootstrapPopoverCssVariables Popover { get; set; } = new();

    // Tables
    public BootstrapTableCssVariables Table { get; set; } = new();
    public BootstrapTableStripedCssVariables TableStriped { get; set; } = new();
    public BootstrapTableHoverCssVariables TableHover { get; set; } = new();

    // Accordion
    public BootstrapAccordionCssVariables Accordion { get; set; } = new();
    public BootstrapAccordionButtonCssVariables AccordionButton { get; set; } = new();

    // Carousel
    public BootstrapCarouselCssVariables Carousel { get; set; } = new();
}
