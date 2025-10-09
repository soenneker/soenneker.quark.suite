
namespace Soenneker.Quark;

public sealed class StepsOptions : ComponentOptions
{
    public StepsOptions()
    {
        ThemeKey = "Steps";
    }
    
    // Steps-specific CSS variables (theme should set these)
    public string? ConnectorColor { get; set; } // --steps-connector-color
    public string? MarkerActiveBg { get; set; } // --steps-marker-active-bg
    public string? MarkerActiveBorder { get; set; } // --steps-marker-active-border
    public string? MarkerActiveColor { get; set; } // --steps-marker-active-color
    public string? TextActive { get; set; } // --steps-text-active
    public string? Success { get; set; } // --steps-success
    public string? DisabledBg { get; set; } // --steps-disabled-bg
    public string? DisabledColor { get; set; } // --steps-disabled-color
    public string? ContentBg { get; set; } // --steps-content-bg
    public string? ContentBorder { get; set; } // --steps-content-border
    public string? ContentRadius { get; set; } // --steps-content-radius
    public string? ContentShadow { get; set; } // --steps-content-shadow
    public string? FocusOutline { get; set; } // --steps-focus-outline
    public string? MarkerSuccessColor { get; set; } // --steps-marker-success-color
}
