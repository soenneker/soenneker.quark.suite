
namespace Soenneker.Quark;

/// <summary>
/// Container class for Bootstrap CSS variables.
/// Includes colors for basic Bootstrap variable overrides and component-specific variables.
/// </summary>
public sealed class BootstrapCssVariables
{
    public BootstrapColorsCssVariables? Colors { get; set; }

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

    // Offcanvas
    public BootstrapOffcanvasCssVariables? Offcanvas { get; set; }

    // Dropdowns
    public BootstrapDropdownMenuCssVariables? DropdownMenu { get; set; }
    public BootstrapDropdownItemCssVariables? DropdownItem { get; set; }
    public BootstrapDropdownToggleCssVariables? DropdownToggle { get; set; }

    // Breadcrumbs
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
}
